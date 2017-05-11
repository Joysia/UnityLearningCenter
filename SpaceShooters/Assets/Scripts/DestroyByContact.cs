using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    
    // effect
    public GameObject explosion;
    public GameObject playerExplosion;

    // score
    public int ScoreValue;
    public GameController gameController;

    private void Start()
    {
        GameObject gameObjectController = GameObject.FindWithTag("GameController");
        if(gameObjectController != null)
        {
            gameController = gameObjectController.GetComponent<GameController>();
        }
        if (gameController == null)
            Debug.Log("빈 게임 컨트롤러");        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);               //  Debug문

        if (other.tag == "Boundary" || other.CompareTag("Enemy") || other.tag == "EnemyBolt")         //  충돌체 중 Boundary, 적끼리 충돌 무시
        {
            return;
        }
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        // Player 충돌경우 처리
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.PlayerStats = Stats.Die;
        }

        gameController.AddScore(ScoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
