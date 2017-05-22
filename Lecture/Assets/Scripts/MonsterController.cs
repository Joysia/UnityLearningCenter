using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour
{
    public enum MonsterStats
    {
        IDLE,
        TRACE,
        WALK,
        ATTACK,
        FALL,
        GOTHIT,
        DIE
    };

    private GameObject Players;
    private PlayerController PC;

    public MonsterStats monState = MonsterStats.IDLE;
    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator monsterAnim;
    public float traceDist = 10.0f;
    public float attackDist = 2.0f;
    private bool isDie = false;
    public GameObject bloodEffect;
    public GameObject bloodDecal;
    public GameObject LeftArm;
    public GameObject RightArm;
    private GameUI _gameUI;

    // Shield Test
    // public GameObject Shield; 필요없는듯
    public bool hasShield = false;

    public float monsterHp = 100.0f;

    private void Awake()
    {
        // 캐시 처리
        monsterTr = GetComponent<Transform>();
        Players = GameObject.FindWithTag("Player");
        playerTr = Players.GetComponent<Transform>();
        PC = Players.GetComponent<PlayerController>();
        nvAgent = GetComponent<NavMeshAgent>();
        monsterAnim = GetComponent<Animator>();
        _gameUI = GameObject.FindGameObjectWithTag("GameUI").GetComponent<GameUI>();
        // 캐시 처리 끝
        //StartCoroutine(CheckMonsterState());   // Start에서 Awake로 변경하면서 사용할 수 없게 된다
        //StartCoroutine(MonsterAction());
    }

    private void OnEnable()
    {
        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
        PlayerController.DeleOnPlayerDie += this.OnPlayerDie;       // 메소드를 한꺼번에 모은다 !!!!
    }

    private void OnDisable()
    {
        PlayerController.DeleOnPlayerDie -= this.OnPlayerDie;
    }

    private IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch (monState)
            {
                case MonsterStats.IDLE:
                    nvAgent.Stop();
                    monsterAnim.SetBool("IsTrace", false);
                    break;

                case MonsterStats.TRACE:
                    nvAgent.destination = playerTr.position;
                    nvAgent.Resume();
                    monsterAnim.SetBool("IsTrace", true);
                    monsterAnim.SetBool("IsAttack", false);
                    LeftArm.GetComponent<TrailRenderer>().enabled = false;
                    RightArm.GetComponent<TrailRenderer>().enabled = false;
                    break;

                case MonsterStats.WALK:
                    break;

                case MonsterStats.ATTACK:
                    monsterAnim.SetBool("IsAttack", true);
                    LeftArm.GetComponent<TrailRenderer>().enabled = true;
                    RightArm.GetComponent<TrailRenderer>().enabled = true;
                    break;

                case MonsterStats.FALL:
                    break;

                case MonsterStats.GOTHIT:
                    break;

                case MonsterStats.DIE:
                    break;

                default:
                    break;
            }
            yield return null;          //  한 프레임 쉬고~
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Bullet")
        {
            CreateBloodEffect(coll.transform.position);
            Destroy(coll.gameObject);
            monsterAnim.SetTrigger("IsHit");
            //monsterHp -= 50f;

            if (monsterHp < 0)
                MonsterDie();

            if (hasShield)
            {
                if (coll.gameObject.name == "ZaryaRightBullet(Clone)")
                {
                    // Debug.Log(22);
                    // 플레이어에게 gauge값을 넘기자. 현재 실드 쓰고 충돌하는건... 실드...
                    if (PC.gauge < 100)
                    {
                        PC.gaugeUp(coll.gameObject.GetComponent<ZaryaRightBullet>().damage / 2);
                        _gameUI.gagueText.text = PC.gauge.ToString();
                    }
                }
            }
            else
            {
                if (coll.gameObject.name == "ZaryaRightBullet(Clone)")
                {
                    monsterHp -= coll.gameObject.GetComponent<ZaryaRightBullet>().damage;
                }
            }
            //monsterHp -= coll.gameObject.GetComponent<BulletController>().damage;
        }
    }

    private IEnumerator PushObjectPool()                // 다시 사용할 수 있도록 Pool 채우기
    {
        yield return new WaitForSeconds(3.0f);
        isDie = false;
        monsterHp = 100;
        gameObject.tag = "Monster";
        monState = MonsterStats.IDLE;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
        foreach (var coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = true;
        }

        gameObject.SetActive(false);
    }

    private void MonsterDie()
    {
        gameObject.tag = "Untagged";

        //StopAllCoroutines();
        StopCoroutine(CheckMonsterState());
        StopCoroutine(MonsterAction());
        isDie = true;
        monState = MonsterStats.DIE;
        nvAgent.Stop();
        monsterAnim.SetTrigger("IsDie");
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
        foreach (var coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }
        _gameUI.DispScore(50);
        StartCoroutine("PushObjectPool");
    }

    private void CreateBloodEffect(Vector3 pos)
    {
        GameObject blood1 = (GameObject)Instantiate(bloodEffect, pos, Quaternion.identity);
        Destroy(blood1, 1.0f);
        Vector3 decalPos = monsterTr.position + Vector3.up * 0.01f;
        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360));
        GameObject bloodDecal1 = (GameObject)Instantiate(bloodDecal, decalPos, decalRot);
        float scale = Random.Range(1.5f, 3.5f);
        bloodDecal1.transform.localScale = Vector3.one * scale;
        Destroy(bloodDecal1, 5.0f);
        //GameObject blood2 = (GameObject)Instantiate(bloodDecal, pos, Quaternion.identity);
    }

    private IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (dist <= attackDist) monState = MonsterStats.ATTACK;      //nvAgent.destination = playerTr.position;
            else if (dist < traceDist) monState = MonsterStats.TRACE;
            else monState = MonsterStats.IDLE;
        }
    }

    private void OnPlayerDie()
    {
        StopAllCoroutines();
        nvAgent.Stop();
        CancelInvoke();
        monsterAnim.SetTrigger("IsPlayerDie");
    }

    private void OnDamage(object[] _params)
    {
        //Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));
        CreateBloodEffect((Vector3)_params[0]);
        monsterHp -= (int)_params[1];
        if (monsterHp <= 0)
        {
            MonsterDie();
        }
        monsterAnim.SetTrigger("IsHit");
    }
}