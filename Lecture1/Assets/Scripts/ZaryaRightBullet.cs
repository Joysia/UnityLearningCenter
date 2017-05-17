using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZaryaRightBullet : MonoBehaviour {
    
    public Vector3 firePos;
    public int damage = 20;
    public float upForce = 100.0f;
    public float forwardForce = 800.0f;
    public Rigidbody rBody;
        
    public PlayerController pc;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * forwardForce + transform.up * upForce);          // 로컬 좌표  
        //GetComponent<Rigidbody>().AddForce(Vector3.forward * speed);            // 월드 좌표 기준(동서남북)
        //GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);        

        //firePos = transform.position;
    }
    
    private void OnCollisionEnter(Collision coll)
    {
        
    }




    /* RightKey
    private void OnCollisionEnter(Collision coll)
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 10.0f);

        for (int i = 0; i < colls.Length; i++)
        {
            rBody = colls[i].GetComponent<Rigidbody>();

            if (rBody != null)
            {
                rBody.mass = 5;
                rBody.AddExplosionForce(-1000.0f, transform.position, 10.0f);
            }
        }
      
        Destroy(gameObject);
        //Debug.DrawRay();
    }
    */


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        pc.PlayerHp -= damage;
    //    }
    //    Destroy(gameObject);
    //}

    // Use this for initialization
  


    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward, Space.Self);
    }
}
