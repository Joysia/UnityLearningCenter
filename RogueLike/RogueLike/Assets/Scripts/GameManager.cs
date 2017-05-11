using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public static GameManager instance = null;              // 어디에서든 접근할수 있도록 static 선언, class에 종속적이지 않게 됨.

    public BoardManager boardScript;

    public  int level = 3;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);                      // 씬이 넘어갈때 파괴되지 않도록
        boardScript = GetComponent<BoardManager>(); 
        InitGame();

	}

    void InitGame()
    {
        boardScript.SetupScene(level);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
