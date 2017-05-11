using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;


public enum Stats{
    Play,
    Die
};

public class GameController : MonoBehaviour
{
    public GameObject[] hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    
    // UI Score
    private int Score;
    public GameObject scoreObject;
    public GameObject ResetObject;
    public GameObject GameoverObject;
    
    private TextMesh scoreText;
    public StringBuilder sb;
    public Stats PlayerStats = Stats.Play;

    void Start()
    {
        StartCoroutine("SpawnWaves");
        sb = new StringBuilder("");
        scoreText = scoreObject.GetComponent<TextMesh>();
        ResetObject.GetComponent<TextMesh>().text = "Press 'R'key To Reset";
        GameoverObject.GetComponent<TextMesh>().text = "GAME OVER";
    }

    public void AddScore(int DestroyPoint)
    {
        Score += DestroyPoint;
        sb.AppendFormat("Score : {0}점", Score.ToString());        
        scoreText.text = sb.ToString();
        sb.Remove(0, sb.Length);            // 문자열 비움
    }

    private void Update()
    {
        if (PlayerStats == Stats.Die)
        {
            GameoverObject.SetActive(true);
            ResetObject.SetActive(true);
        }

        if (PlayerStats == Stats.Die && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
            //   "_scene/main");
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds (startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                int hazardIndex = Random.Range(0, hazard.Length);
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard[hazardIndex], spawnPosition, spawnRotation);             // 이상없음.
               
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }        
    }      
}