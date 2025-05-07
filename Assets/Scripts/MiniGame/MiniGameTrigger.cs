using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniGameTrigger : MonoBehaviour
{
    public GameObject[] allObjects = new GameObject[3];

    public GameObject descriptionWindow;
    public string miniGameName;
    public GameObject miniGamePrefab;
   [HideInInspector] public GameObject miniGame;
    public GameObject scoreButton;
    Camera _camera;
    public Slider hpBar;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Player")
        {
            StartMiniGame();
        }
    }
    // Update is called once per frame
    void StartMiniGame()
    {
        foreach (GameObject obj in allObjects)
        {
            obj.SetActive(false);
        }
        scoreButton.SetActive(false);
        hpBar.gameObject.SetActive(false);
        if(GameManager.Instance.enemy != null)
        {
            GameManager.Instance.enemy.SetActive(false);
        }
        

        miniGame = Instantiate(miniGamePrefab);
        miniGame.transform.position = new Vector2(0, 0);
        GameManager.Instance._score.gameObject.SetActive(true);
        descriptionWindow.SetActive(true);
        _camera.GetComponent<CameraController>().target = GameObject.Find("Airplane").transform;
    }
}
