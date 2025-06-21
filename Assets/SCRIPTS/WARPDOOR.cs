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

    public void InSceneTP()
    {
        //teleports in-scene
        if (isSceneTransition == false)
        {
            player.transform.position = destination.transform.position;
        }
    }
    public void LoadSceneTP()
    {
        //teleports between scenes
        if (isSceneTransition == true)
        {
            SceneManager.LoadScene(sceneID);
        }
    }
}
