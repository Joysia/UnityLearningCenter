using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public int damage = 20;
    public float speed = 1000.0f;
    
    // 마이코드
    public PlayerController pc;

    public Vector3 firePos;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pc.PlayerHp -= damage;
        }
        Destroy(gameObject);
    }
    

    // Use this for initialization
    void Start () {
       
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);          // 로컬 좌표  
        //GetComponent<Rigidbody>().AddForce(Vector3.forward * speed);            // 월드 좌표 기준(동서남북)
        //GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);        

        firePos = transform.position;
    }


    // Update is called once per frame
    void Update () {
        //transform.Translate(Vector3.forward, Space.Self);
	}
}
