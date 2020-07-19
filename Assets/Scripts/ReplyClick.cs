using UnityEngine;

public class ReplyClick : MonoBehaviour
{
    public GameObject commentsection;

    public void OnButtonClick()
    {
        gameObject.SetActive(false);
        commentsection.SetActive(true);
    }
}
