using UnityEngine;

public class DOOR : MonoBehaviour
{
    //put this in invisible object that is placed as a child of a door (good grammar)
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject doorCollision;
    [SerializeField] private Animator anim;
    private bool isDoorOpen;
    private bool isPlayerinRange;
    void start()
    {
        //primitive as fuck but we ball
        isDoorOpen = false;

        //why move it normally when there's an in-engine animation function
        anim = GetComponent<Animator>();

        
    }
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
    public void openDoor()
    {
        //how door opens (might need tweaking later but fuck it)
        if (isDoorOpen == false && isPlayerinRange == true && Input.GetButtonDown("Interact"))
        {
            doorCollision.gameObject.GetComponent<Collider>().enabled = false;
            anim.Play("Open");
        }
    }
    public void closeDoor()
    {
        //how door closes (might need tweaking later)
        if (isDoorOpen == true && isPlayerinRange == false)
        {
            doorCollision.gameObject.GetComponent<Collider>().enabled = true;
            anim.Play("Close");
        }
    }
}
