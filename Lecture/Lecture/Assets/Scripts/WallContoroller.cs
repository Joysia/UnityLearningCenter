using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallContoroller : MonoBehaviour {

    public GameObject sparkEffect;
    //public GameObject firePos;
    Vector3 firePos;
    

    private void OnCollisionEnter(Collision coll)       // 충돌체크
    {
        // Vector3 aaddd = transform.position - coll.transform.position;
        firePos = coll.gameObject.GetComponent<BulletController>().firePos;
        Vector3 relativePos = transform.position - firePos;

        if(coll.collider.tag == "Bullet")
        {
            //GameObject spark = Instantiate(sparkEffect, coll.transform.position, Quaternion.identity) as GameObject;
            GameObject spark = Instantiate(sparkEffect, coll.transform.position, Quaternion.LookRotation(relativePos)) as GameObject;
            Destroy(coll.gameObject);
            Destroy(spark, 0.2f);
        }
    }
   
}
