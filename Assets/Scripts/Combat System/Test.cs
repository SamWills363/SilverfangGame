using UnityEngine;
using System.Collections;

// Make sure this object always has a CharacterCore!!!
[RequireComponent(typeof(CharacterCore))]
public class Test : MonoBehaviour
{
    private CharacterCore characterCore;
    /*
    public Vector3 target = new Vector3(0, 0, 0);
    public float approachSpeed = 5;
    public float stopRadius = 0;
    */

    private void Awake()
    {
        // Grab the CharacterCore on this GameObject!!!
        characterCore = GetComponent<CharacterCore>();
    }

    private void Start()
    {
        /*
        if (characterCore != null)
        {
            // Start the approach coroutine!!!
            StartCoroutine(characterCore.approach(approachSpeed, target, stopRadius));
        }
        else
        {
            Debug.LogError("‼️ Missing CharacterCore on this GameObject!!!");
        }
        */
    }
}
