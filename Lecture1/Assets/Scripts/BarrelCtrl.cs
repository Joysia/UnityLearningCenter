using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour {

    public GameObject expEffect;
    public Texture[] textures;
    private Transform tr;
    private int hitCount = 0;

    public AudioClip BarrelBoomSound;
    AudioSource BarrelAS;

	// Use this for initialization
	void Start () {
        tr = GetComponent<Transform>();
        int idx = Random.Range(0, textures.Length);
        GetComponentInChildren<MeshRenderer>().material.mainTexture = textures[idx];
        BarrelAS = GetComponent<AudioSource>();
	}

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == "Bullet")
        {
            Destroy(coll.gameObject);

            //if(++hitCount >= 3)
            //{
            ExpBarrel();
            BarrelAS.clip = BarrelBoomSound;
            BarrelAS.Play();
            //}
        }
    }

    Collider[] colls;                       // 충돌체값을 공용화
    List<Rigidbody> LRigidbody;                // 충돌체중
    List<Transform> LTransform;
    ArrayList ACollider;
    //Rigidbody JariyaRbody;
    public float JariyaPower = 100.0f;
    public float StartTime = 0.0f;
    private int i= 0;

    Rigidbody rbody;

    //public void Jariya_Sub()
    //{
    //    Debug.Log("invoke");

    //    //JariyaRbody.transform.LookAt(tr.position);
    //    //JariyaRbody.AddForce(JariyaRbody.transform.forward * JariyaPower);
    //}

    void ExpBarrel()
    {
        //GetComponent<Rigidbody>().AddExplosionForce(1000.0f, transform.position, 10.0f, 300.0f);
        //GameObject  expEffects = Instantiate(expEffect, tr.position, Quaternion.identity) as GameObject;
        colls = Physics.OverlapSphere(tr.position, 10.0f);       // 특정 좌표를 기준으로 반경(설정한 거리) 내에 있는 충돌체들을 배열로 받아온다.

        foreach (Collider coll in colls)
        {
            Rigidbody rbody = coll.GetComponent<Rigidbody>();

            if (rbody != null)
            {                
                rbody.AddExplosionForce(-1000.0f, transform.position, 10.0f);
                //LRigidbody.Add(rbody);                      // null 이 있는놈만 담아서
            }
        }
        //InvokeRepeating("Repeating", 0f, 0.2f);             // 반복을 돌리자.
    }

    //void Repeating()
    //{
    //    foreach (var item in LRigidbody)
    //    {
    //        item.transform.LookAt(transform.position);
    //        item.AddForce(LRigidbody[0].transform.forward * JariyaPower);
    //    }        
    //}

    //private void Update()
    //{
    //    AddExplosionForce
    //}





    //JariyaRbody = coll.GetComponent<Rigidbody>();
    //collsRbodys.Add(coll.GetComponent<Rigidbody>());
    //JariyaRbody.mass = 1;

    /*
     * 
     * StartTime = Time.time;
    while (StartTime > Time.time + 10f)         
    {            
        Debug.Log(StartTime);
        StartTime += (Time.time - StartTime);

        rbody = colls[i].GetComponent<Rigidbody>();

        if (rbody != null)
        {
            colls[i].GetComponent<Transform>().LookAt(transform.position);
            rbody.AddForce(rbody.transform.forward * JariyaPower);
        }           
        i++;

        if (colls.Length <= i)
            i = 0;        
    }
    */




    //foreach (var item in colls)
    //{
    //    Transform rtr = item.GetComponent<Transform>();
    //    Rigidbody rbody = item.GetComponent<Rigidbody>();
    //    rbody.mass = 1;

    //    if (rbody != null)                      // null 이 아닌것들만 따로 리스트로 뺌.
    //        collsRbodys.Add(rbody);

    //    while ( 3f < (StartTime += Time.deltaTime))
    //    {
    //        //StartTime += Time.deltaTime;
    //        foreach (var rs in collsRbodys)
    //        {
    //            rtr.LookAt(transform.position);
    //            rbody.AddForce(rbody.transform.forward * JariyaPower);
    //        }

    //        if (rbody != null)
    //        {
    //            //Debug.Log(transform.position);
    //            rtr.LookAt(transform.position);
    //            rbody.AddForce(rbody.transform.forward * JariyaPower);
    //            //rbody.transform.forward);
    //        }
    //    }





    //


}
    //JariyaRbody.transform.LookAt(tr.position);
    //JariyaRbody.AddForce(Vector3.forward);
    //StartCoroutine("Jariya_Abillity");

    //if (rbody != null)               // 반응할 필요 없는 녀석들은 패스
    //{
    //    rbody.mass = 1;
    //    //rbody.AddExplosionForce(1000.1f, tr.position, 10.0f, 300.0f);       // 원본               
    //    StartCoroutine("Jariya_Abillity");
    //    //}
    //}
    //Destroy(expEffects, 3.0f);
    // Destroy(gameObject, 5.0f);        
    //}

    //public IEnumerator Jariya_Abillity()
    //{        
    //    InvokeRepeating("Jariya_Sub", 0f, 0.5f);
    //    Debug.Log("아돈빠가돈");
    //    yield return new WaitForSeconds(3.0f);
    //    CancelInvoke();
    //    Debug.Log("엔드");
    //}



    //   // Update is called once per frame
    //   void Update () {

    //}
