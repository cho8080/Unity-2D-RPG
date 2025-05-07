using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExitWindow : MonoBehaviour
{
    Image panel;
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        OpenPanel();
    }
    // Update is called once per frame
    public void Exit()
    {
        Time.timeScale = 1.0f;
        ClosePanel();
        this.gameObject.SetActive(false);
    }
    void OpenPanel()
    {
        if (this.gameObject.name != "ScoreWindow(Clone)")
        {
            panel = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<Image>();
            panel.gameObject.SetActive(true);
        }
    }
    void ClosePanel()
    {
        if (this.gameObject.name != "ScoreWindow(Clone)") { panel.gameObject.SetActive(false); }
    }
}
