using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameManager;
    	
	void Awake () {
        if (GameManager.instance == null)       //      인스턴스가 비어있다면 생성하자.
            Instantiate(gameManager);
	}
}
