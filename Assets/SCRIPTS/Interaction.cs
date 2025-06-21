using UnityEngine;
public class Interaction : MonoBehaviour
{
    private bool isDoorTouched;
    private GameObject Door;
    private DOOR doorscript;
    void start()
    {
        isDoorTouched = false;
    }
    //remember to swap into switch later
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            isDoorTouched = true;
            Door = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door")
        {
            isDoorTouched = false;
        }
    }
    void Update()
    {
        if (isDoorTouched == true && Input.GetButtonDown("Submit"))
        {
            doorscript = Door.GetComponent<DOOR>();
            doorscript.DoorMovement();
        }
    }
}
