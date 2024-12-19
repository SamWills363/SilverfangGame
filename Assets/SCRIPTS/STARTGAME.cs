using UnityEngine;

public class STARTGAME : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject menuCamera;
    public GameObject menuBackGround;
    public void StartGame()
    {
        Time.timeScale = 1f;
        startMenu.SetActive(false);
        menuCamera.SetActive(false);
        menuBackGround.SetActive(false);
    }
}
