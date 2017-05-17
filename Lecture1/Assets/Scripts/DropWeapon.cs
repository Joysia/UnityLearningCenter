using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapon : MonoBehaviour {

    public GameObject ShotGun;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("총 픽업");

        if (other.gameObject.tag == "Player")
            ShotGun.SetActive(true);
    }
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
