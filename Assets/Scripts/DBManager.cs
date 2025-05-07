using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
[System.Serializable]
public class PlayerScore
{
    public string timestamp;
    public int score;

    public PlayerScore(int score)
    {
        this.timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        this.score = score;     
    }
}

public class DBManager : MonoBehaviour
{
    public static DBManager Instance { get; private set; }
    public List<PlayerScore> playerScores = new List<PlayerScore>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        Load();
    }

    [System.Serializable]
    public class PlayerScoresWrapper
    {
        public List<PlayerScore> playerScores;  // �������� ��� ����Ʈ
    }
    // Update is called once per frame
    public void AddScore(int score)
    {
        PlayerScore newScore = new PlayerScore(score);
        playerScores.Add(newScore);

        // ���� �������� ����
        playerScores.Sort((a, b) => b.score.CompareTo(a.score));

        // �ߺ� ����
        playerScores = playerScores
        .GroupBy(p => p.score)
        .Select(g => g.First())
        .ToList();

        // ���� 5���� ����
        if (playerScores.Count > 5)
        {
            playerScores.RemoveRange(5, playerScores.Count - 5);  // 5��° ������ ������ ����
        }
        // �ְ� ���� ����
        BestScore();
        Save();
   
    }
    public void Save()
    {       
        // PlayerScore ����Ʈ�� JSON���� ��ȯ
        string json = JsonUtility.ToJson(new PlayerScoresWrapper { playerScores = playerScores }, true);

        // JSON�� PlayerPrefs�� ���� (Ȥ�� ���� �ý��ۿ� ���� ����)
        PlayerPrefs.SetString("Leaderboard", json);
        PlayerPrefs.Save();

    }
    // ���� �ε�
    public void Load()
    {
        string json = PlayerPrefs.GetString("Leaderboard", "");  // ����� JSON ���ڿ��� �ҷ���
        if (!string.IsNullOrEmpty(json))
        {
            // JSON�� ����Ʈ�� ��ȯ
            PlayerScoresWrapper loadedData = JsonUtility.FromJson<PlayerScoresWrapper>(json);
            playerScores = loadedData.playerScores;
        }
    }

    // ����Ʈ ���ھ� ã��
    public void BestScore()
    {
        int bestScore = 0;
        foreach (var _score in playerScores)
        {
            if(_score.score > bestScore)
            {
                bestScore = _score.score;
            }
        }
        PlayerPrefs.SetInt("bestScore", bestScore);
    }
}
