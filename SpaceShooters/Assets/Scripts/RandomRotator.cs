using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float tumble;
    Rigidbody rd;
	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody>();
        rd.angularVelocity = Random.insideUnitSphere * tumble;        
    }
	
}
