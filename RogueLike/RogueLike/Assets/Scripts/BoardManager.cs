using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
// 유니티와 C#에 Random 클래스가 둘다 존재할때 unity것을 쓰기 위해 선언.

public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);       // 몇개씩 생성할 것인지 Count를 정해둠.
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();      

    void InitializeList()           // Food, Enemy, 배치에 유효한 공간만을 따로 초기화 작업.
    {
        gridPositions.Clear();      // 초기화

        for (int x = 1; x < columns - 1; x++)           // 1 ~ 6 
        {
            for (int y = 1; y < rows - 1; y++)          // 1 ~ 6
            {
                gridPositions.Add(new Vector3(x, y, 0f));           // 좌표 List에 순차적으로 Add
            }
        }
    }

    private void BoardSetup()                           // Layer의 맨 뒤쪽 floor 배치
    {
        boardHolder = new GameObject("Board").transform;

        for(int x = -1; x < columns + 1; x ++)          // -1 ~ 9
        {
            for(int y = -1; y < rows +1; y++)           
            {
                GameObject toinstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if(x == -1 || y == -1 || x == columns || y == rows)          // 경계 outerWall
                {
                    toinstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }
                GameObject instance = Instantiate(toinstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);                   // 생성한 후 자식으로 넣어서 정리.
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);     // 좌표들중 랜덤값 뽑고
        Vector3 randomPosition = gridPositions[randomIndex];        // 리턴할 값 저장하고
        gridPositions.RemoveAt(randomIndex);                        // 중첩된 값이 뽑히지 않도록 뽑힌값은 리스트에서 제거.
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum , int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        for(int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();                                   // 중첩을 걸러내 새 좌표를 받은 이후
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];        // 타일 종류 랜덤으로 선택.
            Instantiate(tileChoice, randomPosition, Quaternion.identity);                // 그리고 생성.
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();       // 장판 다 깔고~
        InitializeList();   // item들 깔곳 정리하고~ 생성하고~
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);      // Count class ,  갯수만큼 생성배치
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);      // Count class ,  갯수만큼 생성 배치
        int enemyCount = (int)Mathf.Log(level,2f);                                  // 레벨별로 많아지게 끔 Mathf.Log 함수를 이용함.
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);     // 출구 배치.
    }    
}
