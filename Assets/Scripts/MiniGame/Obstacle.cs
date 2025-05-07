using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public GameObject top;
    public GameObject bottom;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameManager.Instance.score++;

            UIManager uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
            uiManager.ChangeScore();

            //nvoke("RequestReposition", 3f);       
        }
        else if(col.gameObject.name == "PlayerBack")
        {
            RequestReposition();
        }
    }
    // ���� ��ġ�� ��ֹ� ��ġ
    public void SetRandomPlace(Vector2 previousPos)
    {
        float randomX = Random.Range(5, 10);
        float posX = previousPos.x + randomX;
        transform.position = new Vector2(posX,transform.position.y);

        // ���� ��ֹ� ��ġ ����
        float topY = Random.Range(1.2f, 2.5f);
        top.transform.localPosition = new Vector2(0, topY);

        // �Ʒ��� ��ֹ� ��ġ ����
        float bottomY = -Random.Range(1.2f, 2.5f);
        bottom.transform.localPosition = new Vector2(0, bottomY);
 
    }
    // ��ġ ���ġ 
    void RequestReposition()
    {
       InfiniteMap obstaclePool = GameObject.Find("MiniGame01(Clone)").GetComponent<InfiniteMap>();        
       SetRandomPlace(obstaclePool.LastObject(obstaclePool.obstacles));
    }
}
