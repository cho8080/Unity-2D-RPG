using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    Transform cam; // ī�޶� Ʈ������

    public GameObject[] obstacles = new GameObject[8];
    public Vector2[] obstaclesStartPos = new Vector2[8];

    public GameObject[] backgrounds = new GameObject[5]; // ��� 5��
    public Vector2[] backgroundsStartPos = new Vector2[5]; // ��� 5���� ������ġ
    public float backgroundWidth = 7.9f; // �� ����� ��
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
        // ��� ���� ����
        if (!isResetting)
            InfiniteBackGround();
    }
    // ���� ��ġ ����
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

    // ��ֹ� ��ġ ����
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
    // �� �ʱ�ȭ
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
        yield return null; // �� ������ ��ٸ�

        isResetting = false;
    }
    // ��� ���� ����
    void InfiniteBackGround()
    {
        foreach (GameObject bg in backgrounds)
        {
            // ����� ī�޶� ���ʿ��� ����� ����� ���
            if (bg.transform.position.x + backgroundWidth + interval < cam.position.x)
            {
                // ���� ������ ����� ��ġ ã��
                float rightmostX = LastObject(backgrounds).x;

                // �ش� ����� �� ���������� �̵�
                bg.transform.position = new Vector2(rightmostX + backgroundWidth, bg.transform.position.y);
            }
        }
    }
    // �迭���� ���� ������ ������Ʈ�� ��ġ ã��
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
