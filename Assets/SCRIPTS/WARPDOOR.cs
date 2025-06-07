using UnityEngine;
using UnityEngine.SceneManagement;

public class WARPDOOR : MonoBehaviour
{
    //put this in invisible object that is placed as a child of a door (good grammar)
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject destination;
    [SerializeField] private bool isSceneTransition;
    [SerializeField] private int sceneID;
    private bool isPlayerinRange;
    //detects collision with player (this is why this script is put in an invisible object)
    //primitive gaming
    void OnTriggerEnter(Collider player)
    {
        isPlayerinRange = true;
    }
    void OnTriggerExit(Collider player)
    {
        isPlayerinRange = false;
    }
    public void inSceneTP()
    {
        //teleports in-scene
        if (isSceneTransition == false && isPlayerinRange == true && Input.GetButtonDown("Interact"))
        {
            player.transform.position = destination.transform.position;
        }
    }
    public void loadSceneTP()
    {
        //teleports between scenes
        if (isSceneTransition == true && isPlayerinRange == true && Input.GetButtonDown("Interact"))
        {
            SceneManager.LoadScene(sceneID);
        }
    }
}
