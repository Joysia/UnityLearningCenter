using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireCtrl : MonoBehaviour {

    public GameObject bullet;
    public Transform firePos;
    public float nextFire = 0.1f;
    private float fireTime = 0.0f;

    private float AimRadius = 1f;
    
    public GameObject DropGun;
    bool flag = true;

	void Start () {
		
	}
	
	void Update () {
       
        if (Input.GetMouseButton(0) && DropGun.active)
        {
            fireTime += Time.deltaTime;
            if(fireTime >= nextFire)
            {
                fireTime = 0.0f;
                Fire();
            }
        }           

        if(Input.GetMouseButtonDown(1))
        {
            DropGun.SetActive(flag);
            flag = !flag;
        }
	}

    private void Fire()
    {
        CreateBullet();
    }

    private void CreateBullet()
    {       
        Quaternion aimRotation = Quaternion.identity;

        float randx = Random.Range(firePos.rotation.eulerAngles.x - AimRadius, firePos.rotation.eulerAngles.x + AimRadius);
        float randy = Random.Range(firePos.rotation.eulerAngles.y - AimRadius, firePos.rotation.eulerAngles.y + AimRadius);
        
        Vector3 aimVec = new Vector3(randx, randy);
        aimRotation.eulerAngles = aimVec;
        Instantiate(bullet, firePos.position, aimRotation);
        //Debug.Log(randx);
        //Debug.Log(randy);
    }
}
