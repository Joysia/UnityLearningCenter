using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour {

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
    


    // Shield Test
    public GameObject Shield;
    public bool hasShield = false;

    public float monsterHp = 500.0f;



    private void OnEnable()
    {
        PlayerController.DeleOnPlayerDie += this.OnPlayerDie;       // 메소드를 한꺼번에 모은다 !!!!        
    }

    private void OnDisable()
    {
        PlayerController.DeleOnPlayerDie -= this.OnPlayerDie;
    }


    void Start() {
        // 캐시 처리
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = GetComponent<NavMeshAgent>();
        monsterAnim = GetComponent<Animator>();
        // 캐시 처리 끝             
        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());      
    }

    IEnumerator MonsterAction()
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
        if(coll.collider.tag == "Bullet")
        {
            CreateBloodEffect(coll.transform.position);
            Destroy(coll.gameObject);
            monsterAnim.SetTrigger("IsHit");
            monsterHp -= 50f;

            if (monsterHp < 0)
                MonsterDie();
            //monsterHp -= coll.gameObject.GetComponent<BulletController>().damage;
        }
    }

    private void MonsterDie()
    {
        StopAllCoroutines();
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
    }
    
    void CreateBloodEffect(Vector3 pos)
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

            if (dist <= attackDist)    monState = MonsterStats.ATTACK;      //nvAgent.destination = playerTr.position;
            else if (dist < traceDist) monState = MonsterStats.TRACE;
            else                       monState = MonsterStats.IDLE;
        }     
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();
        nvAgent.Stop();
        CancelInvoke();
        monsterAnim.SetTrigger("IsPlayerDie");
    }
}
