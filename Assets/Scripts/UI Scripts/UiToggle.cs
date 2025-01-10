using UnityEngine;

public class UiToggle : MonoBehaviour
{
    public GameObject UI;
    [Header("If there are ClosableUIs in the list, if the UI is toggled, the closables will automatically toggled to close.")]
    public GameObject[] ClosableUIList;

    public void UIToggle() {
        if (UI == null) {
            return;
        }

        if (ClosableUIList != null) {
            foreach (var Closable in ClosableUIList)
            {
                Closable.SetActive(false);
            }
        }

        if (UI.activeSelf == true) {
            UI.gameObject.SetActive(false);
        }
        else {
            UI.gameObject.SetActive(true);
        }
    }
}
