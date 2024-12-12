using UnityEngine;

public class QUIT : MonoBehaviour
{
    public void exitGame(){
        PlayerPrefs.Save();
        Application.Quit();
    }
}
