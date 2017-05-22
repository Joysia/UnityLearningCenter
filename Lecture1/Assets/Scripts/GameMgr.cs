using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public Transform[] points;
    public GameObject monsterPrefab;
    public List<GameObject> monsterPool = new List<GameObject>();
    public float createTime = 2.0f;
    public int maxMonster = 10;
    public bool isGameOver = false;
    public static GameMgr instance = null;

    //

    public NetworkView NTv;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    private void Start()
    {
        for (int i = 0; i < maxMonster; i++)
        {
            GameObject monster = (GameObject)Instantiate(monsterPrefab);
            monster.name = "Monster_" + i.ToString();
            monster.SetActive(false);
            monsterPool.Add(monster);
        }

        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();        // Spawn 자리 검색해옴.

        if (points.Length > 0)
        {
            StartCoroutine(this.CreateMonster());
        }
    }

    private IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(createTime);
            if (isGameOver) yield break;
            foreach (GameObject monster in monsterPool)
            {
                if (!monster.activeSelf)
                {
                    int idx = Random.Range(1, points.Length);
                    monster.transform.position = points[idx].position;
                    monster.SetActive(true);
                    break;
                }
            }
        }
        //while (!isGameOver)
        //{
        //    int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;        // 몬스터가 몇개인지 check

        //    if(monsterCount < maxMonster)
        //    {
        //        yield return new WaitForSeconds(createTime);

        //        int idx = Random.Range(1, points.Length);

        //        Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
        //    }
        //    else
        //        yield return null;
        //}
    }

    private bool isSfxMute = true;
    private float sfxVolume = 1;

    public void PlaySfx(Vector3 pos, AudioClip sfx)
    {
        if (isSfxMute) return;
        GameObject soundObj = new GameObject("Sfx");
        soundObj.transform.position = pos;
        AudioSource _audioSource = soundObj.AddComponent<AudioSource>();
        _audioSource.clip = sfx;
        _audioSource.minDistance = 10.0f;
        _audioSource.maxDistance = 30.0f;
        _audioSource.volume = sfxVolume;
        _audioSource.Play();
        Destroy(soundObj, sfx.length);       //  ??
    }
}