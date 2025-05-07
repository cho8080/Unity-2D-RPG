using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    Transform cam; // 카메라 트랜스폼

    public GameObject[] obstacles = new GameObject[8];
    public Vector2[] obstaclesStartPos = new Vector2[8];

    public GameObject[] backgrounds = new GameObject[5]; // 배경 5개
    public Vector2[] backgroundsStartPos = new Vector2[5]; // 배경 5개의 시작위치
    public float backgroundWidth = 7.9f; // 각 배경의 폭
    public float interval = 10f;

    private bool isResetting = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        cam = Camera.main.transform;

        SettingObstaclePos();
        SaveStartPos();
    }
    private void Update()
    {
        // 배경 무한 생성
        if (!isResetting)
            InfiniteBackGround();
    }
    // 시작 위치 저장
    void SaveStartPos()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstaclesStartPos[i] = obstacles[i].transform.position;
        }
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgroundsStartPos[i] = backgrounds[i].transform.position;
        }
    }

    // 장애물 위치 수정
    void SettingObstaclePos()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            Obstacle _obstacle = obstacles[i].GetComponent<Obstacle>();

            if (i == 0)
            {                                        
                _obstacle.SetRandomPlace(transform.position);
            }
            else
            {
                _obstacle.SetRandomPlace(obstacles[i - 1].transform.position);
            }
        }
    }
    // 맵 초기화
    public IEnumerator InitMap()
    {
        isResetting = true;
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].transform.position = obstaclesStartPos[i];
        }
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position = backgroundsStartPos[i];
        }
        yield return null; // 한 프레임 기다림

        isResetting = false;
    }
    // 배경 무한 생성
    void InfiniteBackGround()
    {
        foreach (GameObject bg in backgrounds)
        {
            // 배경이 카메라 왼쪽에서 충분히 벗어났을 경우
            if (bg.transform.position.x + backgroundWidth + interval < cam.position.x)
            {
                // 가장 오른쪽 배경의 위치 찾기
                float rightmostX = LastObject(backgrounds).x;

                // 해당 배경을 맨 오른쪽으로 이동
                bg.transform.position = new Vector2(rightmostX + backgroundWidth, bg.transform.position.y);
            }
        }
    }
    // 배열에서 가장 마지막 오브젝트의 위치 찾기
    public Vector2 LastObject(GameObject[] objects)
    {
        Vector2 lastObstaclePos = objects[0].transform.position;
        for (int i = 0; i < objects.Length; i++)
        {
            if(objects[i].transform.position.x> lastObstaclePos.x)
            {
                lastObstaclePos = objects[i].transform.position;
            }
        }
        return lastObstaclePos;
    }
   
}
