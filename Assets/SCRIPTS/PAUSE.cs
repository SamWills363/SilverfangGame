using UnityEngine;

public class PAUSE : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inventoryMenu;
    public GameObject optionsMenu;
    private bool pausestate = false;

    void Start()
    {

    }


    void Update()
    {

        if (Input.GetButtonDown("Cancel")){
            pausestate = !pausestate;
        }

        if (pausestate == true){
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        if (pausestate == false){
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            inventoryMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }
    }
}