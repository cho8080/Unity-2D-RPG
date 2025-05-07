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
    public int bestScore; // �ְ� ����
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
        // ���� ����
        string score = GameObject.Find("ScoreText (TMP)").GetComponent<TMP_Text>().text;

        dBManager.AddScore(int.Parse(score));
        dBManager.Save();

        // ���� ǥ��
        bestScore = PlayerPrefs.GetInt("bestScore");
        Text scoreText = endGameWindow.transform.GetChild(0).GetComponent<Text>();
        scoreText.text = ($"[ ���� ���� ] \r\n\r\n�����Ͽ����ϴ�\r\n\r\n�ְ� ���� : {bestScore}  / �� ���ھ� : " + score.ToString());
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

                // ��ġ ���� Ǯ��
                Rigidbody2D _rigidbody = obj.GetComponent<Rigidbody2D>();
                _rigidbody.constraints = RigidbodyConstraints2D.None;
     

                // ��ġ �缳��
                obj.transform.position = airplane.startPos;

                // �� �ʱ�ȭ
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
        // ���󺹱�
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
        
        // ���� ����
        score = 0;      
        uIManager.ChangeScore();
        _score.gameObject.SetActive(false);

        _camera.GetComponent<CameraController>().target = player.transform;

        // â �ݱ�
        ExitWindow exitWindow = endGameWindow.GetComponent<ExitWindow>();
        exitWindow.Exit();

    }
}
