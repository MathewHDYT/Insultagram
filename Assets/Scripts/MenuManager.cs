using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text notifications;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
            return;

        notifications.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void Instagram()
    {
        PlaySelectSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void CloseInTrashCan()
    {
        PlaySelectSound();
        SceneManager.LoadScene("Trashcan");
    }

    public void TxtFile()
    {
        PlaySelectSound();
        SceneManager.LoadScene("Help");
    }

    public void TxtFileCheat()
    {
        PlaySelectSound();
        SceneManager.LoadScene("Cheats");
    }

    public void ImageinTrashCan()
    {
        PlaySelectSound();
    }

    public void Menu()
    {
        PlaySelectSound();
        SceneManager.LoadScene("Menu");
    }

    public void RecycleBin()
    {
        PlaySelectSound();
        SceneManager.LoadScene("Trashcan");
    }

    public void Quit()
    {
        PlaySelectSound();
        Application.Quit();
    }

    private void PlaySelectSound()
    {
        FindObjectOfType<AudioManager>().Play("Select");
    }
}
