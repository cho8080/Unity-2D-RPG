using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public Canvas canvas;
    public GameObject questWindow;
    GameObject _questWindow;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Player")
        {
            _questWindow = Instantiate(questWindow);
            _questWindow.transform.SetParent(canvas.transform);
            _questWindow.GetComponent<QuestDialogue>().StartDialogue();

            RectTransform rect = _questWindow.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2 (0, 73.7f);

            rect.anchorMin = new Vector2(0, rect.anchorMin.y);
            rect.anchorMax = new Vector2(1, rect.anchorMax.y);
            rect.offsetMin = new Vector2(0, rect.offsetMin.y);    // ¿ÞÂÊ
            rect.offsetMax = new Vector2(0, rect.offsetMax.y);  // ¿À¸¥ÂÊ
        }
    }
}
