using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Anim
{
    public AnimationClip idle;
    public AnimationClip runFoward;
    public AnimationClip runBackWard;
    public AnimationClip runRight;
    public AnimationClip runLeft;
}

public class PlayerController : MonoBehaviour
{
    private float h = 0.0f;      // x축
    private float v = 0.0f;      // z축
    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;
    private int initHp;
    public Image imgHpBar;

    // private GameMgr _gameMgr;
    // 마이 코드
    public GameUI _gameUI;

    public int PlayerHp = 500;

    public int bulletEnergy = 1000;
    public int gauge = 0;
    public float gaugeHoldTime = 1f;
    private bool gaugeStat = false;
    //

    private Transform tr;

    public Anim anim;
    public Animation _animation;

    public Transform ShotPoint;

    // 델리게이트 !!!!!!
    public delegate void PlayerDieHandler();

    public static event PlayerDieHandler DeleOnPlayerDie;

    // Use this for initialization
    private void Start()
    {
        tr = GetComponent<Transform>();
        initHp = PlayerHp;
        // _gameMgr = GameObject.Find("GameManager").GetComponent<GameMgr>();
        _animation = GetComponentInChildren<Animation>();
        _animation.clip = anim.idle;
        _animation.Play();
    }

    public void gaugeUp(int value)
    {
        if (IsInvoking("gaugeDown"))
        {
            CancelInvoke("gaugeDown");
            Debug.Log("Invoke Cancel, gauge Up");
        }

        gauge += value;
        InvokeRepeating("gaugeDown", 1.0f, 0.5f);           // 실드 게이지 감소 1초 지연
    }

    // 0.5초마다 1씩 줄어듬.
    private void gaugeDown()
    {
        if (gauge > 0)
        {
            gauge -= 1;
            _gameUI.gagueText.text = gauge.ToString();
        }
    }

    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        if (v >= 0.1f) { _animation.CrossFade(anim.runFoward.name, 0.3f); }
        else if (v < 0f) { _animation.CrossFade(anim.runBackWard.name, 0.3f); }
        else if (h >= 0.1f) { _animation.CrossFade(anim.runRight.name, 0.3f); }
        else if (h < 0f) { _animation.CrossFade(anim.runLeft.name, 0.3f); }
        else { _animation.CrossFade(anim.idle.name, 0.3f); }

        //_animation.Play();
        //transform.Translate(new Vector3(h, 0f, v) * moveSpeed * Time.deltaTime);
        //transform.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime, Space.Self);
        //transform.Translate(Vector3.right * moveSpeed * h * Time.deltaTime, Space.Self);
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);       // 대각선방향으로 이동시 속도가 빨라지는 부분을 normalize를 통해 해결.

        //float vect = Vector3.Magnitude(moveDir);
        //Debug.Log(vect);
        //Debug.Log(moveDir.magnitude);
        //Debug.Log(moveDir.normalized.magnitude);              // Vector3 방향값을 magnitude로 확인할 수 있다.
        //transform.Rotate(Vector3.up * rotSpeed * Input.GetAxis("Mouse X") * Time.deltaTime);

        //if (HpPercent >= 0.8f)
        //    LerpColor = Color.green;
        //else if (HpPercent >= 0.4f && HpPercent < 0.8f)
        //{
        //    LerpColor = Color.Lerp(Color.green, Color.yellow, Mathf.PingPong(Time.time, 1));
        //    imgHpBar.color = LerpColor;
        //}

        //else if (HpPercent < 0.4f)
        //{
        //    LerpColor = Color.Lerp(Color.yellow, Color.red, 0.3f);
        //    imgHpBar.color = LerpColor;
        //}

        //imgHpBar.color = LerpColor;
    }

    private Color LerpColor;
    private float HpPercent;

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "MonsterHands")
        {
            PlayerHp -= 5;

            float hpBar = (float)PlayerHp / (float)initHp;
            Color hpBarColor = Color.black;

            if (hpBar <= 0.5f)
            {
                hpBarColor.r = 1.0f;
                hpBarColor.g = 1.0f * hpBar * 2.0f;
            }
            else
            {
                hpBarColor.g = 1.0f;
                hpBarColor.r = (1.0f - (1.0f * hpBar)) * 2.0f;
            }

            imgHpBar.fillAmount = HpPercent;
            imgHpBar.color = hpBarColor;

            //HpPercent = (float)PlayerHp / (float)initHp;

            //if (HpPercent >= 0.8f )
            //    LerpColor = Color.green;
            //else if (HpPercent >= 0.4f && HpPercent < 0.8f)
            //{
            //    LerpColor = Color.Lerp(Color.green, Color.yellow, Mathf.PingPong(Time.time, 1));
            //    //imgHpBar.color = LerpColor;
            //}

            //else if (HpPercent < 0.4f)
            //{
            //    LerpColor = Color.Lerp(Color.yellow, Color.red, 0.3f);
            //    //imgHpBar.color = LerpColor;
            //}

            //Debug.Log("Player Hp = " + PlayerHp.ToString());      ********************    <<<    Player hp 감소   >>>
            if (PlayerHp <= 0)
            {
                PlayerDie();
            }
        }
    }

    private void PlayerDie()
    {
        DeleOnPlayerDie();          // Delegate 모아둔것 실행.
        //_gameMgr.isGameOver = true;
        GameMgr.instance.isGameOver = true;         // static 으로 선언되어 있음.
        //Debug.Log("Player Die!!!");
        //GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        //foreach(GameObject monster in monsters)
        //{
        //    monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        //}
    }
}