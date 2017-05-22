using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ngui : MonoBehaviour {

    public void OnClickStartBtn()
    {
        Debug.Log("스타트 버튼...");
    }

    public void OnClickOptionBtn(string msg)
    {
        Debug.Log("옵션 버튼..." + msg);
    }

    public void OnClickCreditBtn(GameObject gobj)
    {
        Debug.Log("크레딧 버튼...");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
