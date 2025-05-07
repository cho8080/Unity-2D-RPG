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
        public List<PlayerScore> playerScores;  // 점수들을 담는 리스트
    }
    // Update is called once per frame
    public void AddScore(int score)
    {
        PlayerScore newScore = new PlayerScore(score);
        playerScores.Add(newScore);

        // 점수 내림차순 정렬
        playerScores.Sort((a, b) => b.score.CompareTo(a.score));

        // 중복 제거
        playerScores = playerScores
        .GroupBy(p => p.score)
        .Select(g => g.First())
        .ToList();

        // 상위 5개만 유지
        if (playerScores.Count > 5)
        {
            playerScores.RemoveRange(5, playerScores.Count - 5);  // 5번째 이후의 점수를 삭제
        }
        // 최고 점수 갱신
        BestScore();
        Save();
   
    }
    public void Save()
    {       
        // PlayerScore 리스트를 JSON으로 변환
        string json = JsonUtility.ToJson(new PlayerScoresWrapper { playerScores = playerScores }, true);

        // JSON을 PlayerPrefs에 저장 (혹은 파일 시스템에 저장 가능)
        PlayerPrefs.SetString("Leaderboard", json);
        PlayerPrefs.Save();

    }
    // 점수 로드
    public void Load()
    {
        string json = PlayerPrefs.GetString("Leaderboard", "");  // 저장된 JSON 문자열을 불러옴
        if (!string.IsNullOrEmpty(json))
        {
            // JSON을 리스트로 변환
            PlayerScoresWrapper loadedData = JsonUtility.FromJson<PlayerScoresWrapper>(json);
            playerScores = loadedData.playerScores;
        }
    }

    // 베스트 스코어 찾기
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
