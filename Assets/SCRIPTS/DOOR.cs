using UnityEngine;

public class DOOR : MonoBehaviour
{
    //put this in invisible object that is placed as a child of a door (good grammar)
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject doorCollision;
    [SerializeField] private Animator anim;
    private bool isDoorOpen;
    void start()
    {
        //primitive as fuck but we ball
        isDoorOpen = false;

        //why move it normally when there's an in-engine animation function
        anim = GetComponent<Animator>();
    }
    public void DoorMovement()
    {
        //how door opens/closes (might need tweaking later but fuck it)
        if (isDoorOpen == false)
        {
            doorCollision.gameObject.GetComponent<Collider>().enabled = false;
            anim.Play("Open");
            isDoorOpen = true;
        }
        else if (isDoorOpen == true)
        {
            anim.Play("Close");
            doorCollision.gameObject.GetComponent<Collider>().enabled = true;
            isDoorOpen = false;
        }
    }
}
