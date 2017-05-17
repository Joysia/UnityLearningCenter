using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Anim
{
    public AnimationClip idle;
    public AnimationClip runFoward;
    public AnimationClip runBackWard;
    public AnimationClip runRight;
    public AnimationClip runLeft;
}

public class PlayerController : MonoBehaviour {

    private float h = 0.0f;      // x축
    private float v = 0.0f;      // z축
    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;

    // 마이 코드
    public int PlayerHp = 500;
    public int bulletEnergy = 1000;
    public int gauge = 0;
    //

    private Transform tr;

    public Anim anim;
    public Animation _animation;

    public Transform ShotPoint;


    // 델리게이트 !!!!!!
    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler DeleOnPlayerDie;


    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();
        _animation = GetComponentInChildren<Animation>();
        _animation.clip = anim.idle;
        _animation.Play();
    }

    void Update () {
        
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        
        if (v >= 0.1f)      { _animation.CrossFade(anim.runFoward.name , 0.3f);  }
        else if (v < 0f)    { _animation.CrossFade(anim.runBackWard.name, 0.3f); }
        else if (h >= 0.1f) { _animation.CrossFade(anim.runRight.name, 0.3f); }
        else if (h < 0f)    { _animation.CrossFade(anim.runLeft.name, 0.3f); }
        else                { _animation.CrossFade(anim.idle.name, 0.3f); }

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
        
	}

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "MonsterHands")
        {
            PlayerHp -= 50;
            Debug.Log("Player Hp = " + PlayerHp.ToString());
            if(PlayerHp <= 0)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        DeleOnPlayerDie();          // Delegate 모아둔것 실행.
        //Debug.Log("Player Die!!!");
        //GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        //foreach(GameObject monster in monsters)
        //{
        //    monster.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        //}
    }
}
