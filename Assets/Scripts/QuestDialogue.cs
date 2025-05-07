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
            " �ȳ��ϼ���. ���谡��.",
            "�ι�° �̴� ������ �������ּ���.\n�̴� ������ ���� �Ա��� ���ø� �����Ͻ� �� �ִ�ϴ�."
        };

        //dialogueText.text = sentences[currentLineIndex]; // ù ���� ���
        StartCoroutine(TextPrint(delay, sentences[currentLineIndex]));
        currentLineIndex++;
    }
    void DisplayNextLine()
    {
        Debug.Log("��������");
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
