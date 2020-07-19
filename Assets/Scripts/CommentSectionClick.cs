using UnityEngine;
using UnityEngine.UI;

public class CommentSectionClick : MonoBehaviour
{
    public int maxwordcount = 240;
    public Text wordcount;
    public Text commentbox;

    private int currentwordcount;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.instance;
        currentwordcount = maxwordcount;
        SetCurrentWordCount();
    }

    private void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (commentbox.text.Length != 0)
                {
                    commentbox.text = commentbox.text.Substring(0, commentbox.text.Length - 1);
                    currentwordcount += 1;
                }
            }
            else if ((c == '\n') || (c == '\r')) // enter/return
            {
                gm.CalculateScore(commentbox.text);
                gameObject.SetActive(false);
                commentbox.text = "";
                currentwordcount = maxwordcount;
            }
            else
            {
                if (currentwordcount <= 0)
                    return;

                commentbox.text += c;
                FindObjectOfType<AudioManager>().Play("KeyBoardHit");
                currentwordcount -= 1;
            }
        }

        SetCurrentWordCount();
    }

    private void SetCurrentWordCount()
    {
        wordcount.text = "Words: " + currentwordcount.ToString();
    }
}
