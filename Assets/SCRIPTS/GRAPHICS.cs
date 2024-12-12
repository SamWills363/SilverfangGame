using UnityEngine;
using TMPro;

public class GRAPHICS : MonoBehaviour
{
    public TMP_Dropdown FullscreenDropdown;
    public TMP_Dropdown ResolutionDropdown;
    


    public void resolutionSet(){
        if (ResolutionDropdown.value == 0 & FullscreenDropdown.value == 0){
             Screen.SetResolution(854, 480, FullScreenMode.FullScreenWindow);
        }
        if (ResolutionDropdown.value == 0 & FullscreenDropdown.value == 1){
             Screen.SetResolution(854, 480, FullScreenMode.ExclusiveFullScreen);
        }
        if (ResolutionDropdown.value == 0 & FullscreenDropdown.value == 2){
             Screen.SetResolution(854, 480, FullScreenMode.Windowed);
        }
        
        if (ResolutionDropdown.value == 1 & FullscreenDropdown.value == 0){
             Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow);
        }
        if (ResolutionDropdown.value == 1 & FullscreenDropdown.value == 1){
             Screen.SetResolution(1280, 720, FullScreenMode.ExclusiveFullScreen);
        }
        if (ResolutionDropdown.value == 1 & FullscreenDropdown.value == 2){
             Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        }
        
        if (ResolutionDropdown.value == 2 & FullscreenDropdown.value == 0){
             Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
        }
        if (ResolutionDropdown.value == 2 & FullscreenDropdown.value == 1){
             Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
        }
        if (ResolutionDropdown.value == 2 & FullscreenDropdown.value == 2){
             Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
        }
    }
}
