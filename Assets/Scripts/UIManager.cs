using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject scoreWindowPrefab;
    GameObject scoreWindow;
    public TMP_Text _score;
    public Slider hpBar;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void ChangeHp()
    {
        hpBar.value = (float)playerController.currentHealth / playerController.maxHealth;
    }
    // Update is called once per frame
    public void SeeMyScore()
    {
       scoreWindow = Instantiate(scoreWindowPrefab);
       scoreWindow.transform.SetParent(canvas.transform);
       scoreWindow.transform.localPosition = Vector2.zero;
    }
    public void ChangeScore()
    {
        _score.text = GameManager.Instance.score.ToString();
        DBManager.Instance.AddScore(1);
    }
}
