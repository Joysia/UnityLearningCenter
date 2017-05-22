using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    //public PlayerController PC;

    // Use this for initialization
    private void Start()
    {
        //Debug.Log("Shield On");
        //Destroy(gameObject, 2.0f);
        //PC =
        //   InvokeRepeating("ShieldRotates", 0f, 0.1f);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("trigger");
    //    Destroy(other.gameObject);
    //}

    //private void OnCollisionEnter(Collision coll)
    //{
    //    GameObject Player = GameObject.FindGameObjectWithTag("Player");
    //    PC = Player.GetComponent<PlayerController>();
    //    Debug.Log(11);

    //    if (coll.gameObject.name == "ZaryaRightBullet(Clone)")
    //    {
    //        Debug.Log(2);
    //        PC.gauge += coll.gameObject.GetComponent<ZaryaRightBullet>().damage / 2;
    //    }
    //    Destroy(coll.gameObject);
    //}

    // Update is called once per frame
    private void Update()
    {
    }

    //private Transform ro;

    //float i = 1;
    //float j = 0;

    //void ShieldRotates()
    //{
    //    //i += 0.1f;
    //    transform.Rotate(UnityEngine.Random.Range(1f,2f), UnityEngine.Random.Range(1f, 2f), UnityEngine.Random.Range(1f, 2f));
    //}
}