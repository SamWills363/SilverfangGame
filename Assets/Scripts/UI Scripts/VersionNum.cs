using UnityEngine;
using TMPro; // Add this line to use TextMeshPro

public class VersionDisplay : MonoBehaviour
{
    public TextMeshProUGUI versionText; // Change the type to TextMeshProUGUI

    void Start()
    {
        versionText.text = "Game Version: " + Application.version;
    }
}