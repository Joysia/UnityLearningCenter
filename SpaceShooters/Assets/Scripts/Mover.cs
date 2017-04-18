using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float Speed;
	// Use this for initialization
	void Start () {
		Rigidbody rd = GetComponent<Rigidbody>();
        rd.velocity = Speed * transform.forward;            // Z축 +1 를 transform.forward로 표기가능
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
