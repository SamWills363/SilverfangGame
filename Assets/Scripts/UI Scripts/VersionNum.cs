using UnityEngine;
using TMPro;

public class VersionDisplay : MonoBehaviour
{
    public TextMeshProUGUI versionText; // Change the type to TextMeshProUGUI

    void Start()
    {
        versionText.text = "Game Version: " + Application.version;
    }
}