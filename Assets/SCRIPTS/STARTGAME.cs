using UnityEngine;

public class STARTGAME : MonoBehaviour
{
    public GameObject startMenu;
    public void StartGame()
    {
        Time.timeScale = 1f;
        startMenu.SetActive(false);
    }
}
