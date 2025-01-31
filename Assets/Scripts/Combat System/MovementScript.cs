using UnityEngine;
using UnityEngine.UI;

public class MoveObject : MonoBehaviour
{
    public Button enableMovementButton;
    private bool canMove = false;
    public float moveSpeed = 5f;

    void Start()
    {
        // Ensure the Button is set up
        if (enableMovementButton != null)
        {
            enableMovementButton.onClick.AddListener(EnableMovement);
        }
    }

    void Update()
    {
        if (canMove && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                StartCoroutine(MoveToPosition(hit.point));
                canMove = false; // Disable movement until the button is pressed again
            }
        }
    }

    private void EnableMovement()
    {
        canMove = true;
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}