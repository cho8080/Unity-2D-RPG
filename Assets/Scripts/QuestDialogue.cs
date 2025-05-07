using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class QuestDialogue : MonoBehaviour
{
    string[] sentences;
    public Text dialogueText;
    public bool isTalking;
    bool isTyping;
    int currentLineIndex;
    private float delay =0.125f;
    // Update is called once per frame
    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.G))
        {
          if(!isTyping)
            {
                DisplayNextLine();
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(this.gameObject);
        }
    }
    public void StartDialogue()
    {
        isTalking = true;

        sentences = new string[]
        {
            " 안녕하세요. 모험가님.",
            "두번째 미니 게임을 진행해주세요.\n미니 게임은 북쪽 입구로 가시면 진행하실 수 있답니다."
        };

        //dialogueText.text = sentences[currentLineIndex]; // 첫 문장 출력
        StartCoroutine(TextPrint(delay, sentences[currentLineIndex]));
        currentLineIndex++;
    }
    void DisplayNextLine()
    {
        Debug.Log("다음문장");
        if (currentLineIndex < sentences.Length)
        {
            // dialogueText.text = sentences[currentLineIndex];
            StartCoroutine(TextPrint(delay, sentences[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            isTalking = false;
        }
    }
    IEnumerator TextPrint(float delay, string sentence)
    {
        isTyping = true;
        dialogueText.text = null;
        for (int i = 0;i< sentence.Length; i++)
        {
             dialogueText.text += sentence[i];
            yield return new WaitForSeconds(delay);
        }
        isTyping = false;
    }
 }
