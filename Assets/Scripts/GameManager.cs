using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    UIManager uIManager;
    DBManager dBManager;

    public GameObject player;
    public GameObject map;
    public GameObject endGameWindow;
    public GameObject windGameWindow;
    public GameObject loseGameWindow;
    Camera _camera;

    public int score;
    public int bestScore; // 최고 점수
    public TMP_Text _score;
    public GameObject scoreButton;
    public Slider hpBar;
    public GameObject enemy;
    public GameObject npc;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _camera = Camera.main;
        uIManager = GetComponent<UIManager>();
        dBManager = GetComponent<DBManager>();
    }
    public void WinGame()
    {
        windGameWindow.SetActive(true);
    }
    public void LoseGame()
    {
        loseGameWindow.SetActive(true);
    }
    public void EndGame()
    {
        endGameWindow.SetActive(true);
        scoreButton.SetActive(true);
        // 점수 저장
        string score = GameObject.Find("ScoreText (TMP)").GetComponent<TMP_Text>().text;

        dBManager.AddScore(int.Parse(score));
        dBManager.Save();

        // 점수 표시
        bestScore = PlayerPrefs.GetInt("bestScore");
        Text scoreText = endGameWindow.transform.GetChild(0).GetComponent<Text>();
        scoreText.text = ($"[ 게임 종료 ] \r\n\r\n실패하였습니다\r\n\r\n최고 점수 : {bestScore}  / 내 스코어 : " + score.ToString());
    }
    public void RestartGame()
    {
        scoreButton.SetActive(false);
        GameObject obj = GameObject.FindWithTag("Player");
        score = 0;

        uIManager.ChangeScore();

        switch (obj.name)
        {
            case "Airplane":
                Airplane airplane = obj.GetComponent<Airplane>();
                airplane.isDead = false;
                airplane.animator.SetInteger("IsDie", 0);

                // 위치 고정 풀기
                Rigidbody2D _rigidbody = obj.GetComponent<Rigidbody2D>();
                _rigidbody.constraints = RigidbodyConstraints2D.None;
     

                // 위치 재설정
                obj.transform.position = airplane.startPos;

                // 맵 초기화
                InfiniteMap infiniteMap = GameObject.Find("MiniGame01(Clone)").GetComponent<InfiniteMap>();
                StartCoroutine( infiniteMap.InitMap());
                ExitWindow exitWindow = endGameWindow.GetComponent<ExitWindow>();
                exitWindow.Exit();
                break;
            default:
                break;
        }
    }

    public void ExitGame()
    {
        // 원상복귀
        scoreButton.SetActive(true);
        player.SetActive(true);
        map.SetActive(true);
        hpBar.gameObject.SetActive(true);
        npc.gameObject.SetActive(true );
        if (GameManager.Instance.enemy != null)
        {
            if (GameManager.Instance.enemy.GetComponent<EnemyController>().currentHealth != 0 )
            {
                GameManager.Instance.enemy.SetActive(true);
            }
        }
      

        player.transform.position = Vector2.zero;

        PlayerController playerControlloer = player.GetComponent<PlayerController>();
        playerControlloer.input = Vector2.zero;

        MiniGameTrigger miniGameTrigger = GameObject.Find("MiniGameTrigger").GetComponent<MiniGameTrigger>();
        Destroy(miniGameTrigger.miniGame);
        
        // 점수 설정
        score = 0;      
        uIManager.ChangeScore();
        _score.gameObject.SetActive(false);

        _camera.GetComponent<CameraController>().target = player.transform;

        // 창 닫기
        ExitWindow exitWindow = endGameWindow.GetComponent<ExitWindow>();
        exitWindow.Exit();

    }
}
