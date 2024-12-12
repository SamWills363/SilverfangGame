using UnityEngine;
using TMPro;

public class GRAPHICS : MonoBehaviour
{
    public TMP_Dropdown FullscreenDropdown;
    public TMP_Dropdown ResolutionDropdown;
    
     public void Start(){
        if(PlayerPrefs.HasKey("resolutionIndex") && PlayerPrefs.HasKey("fullscreenIndex")){
            loadRes();
        }
    }



    public void resolutionSet(){
          int resindex = ResolutionDropdown.value;
          int fscreenindex= FullscreenDropdown.value;

        if (resindex == 0 & fscreenindex == 0){
             Screen.SetResolution(854, 480, FullScreenMode.FullScreenWindow);
             PlayerPrefs.SetInt("resolutionIndex", 0);
             PlayerPrefs.SetInt("fullscreenIndex", 0);
        }
        if (resindex == 0 & fscreenindex == 1){
             Screen.SetResolution(854, 480, FullScreenMode.ExclusiveFullScreen);
             PlayerPrefs.SetInt("resolutionIndex", 0);
             PlayerPrefs.SetInt("fullscreenIndex", 1);
        }
        if (resindex == 0 & fscreenindex == 2){
             Screen.SetResolution(854, 480, FullScreenMode.Windowed);
             PlayerPrefs.SetInt("resolutionIndex", 0);
             PlayerPrefs.SetInt("fullscreenIndex", 2);
        }
        
        if (resindex == 1 & fscreenindex == 0){
             Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow);
             PlayerPrefs.SetInt("resolutionIndex", 1);
             PlayerPrefs.SetInt("fullscreenIndex", 0);
             PlayerPrefs.Save();
        }
        if (resindex == 1 & fscreenindex == 1){
             Screen.SetResolution(1280, 720, FullScreenMode.ExclusiveFullScreen);
             PlayerPrefs.SetInt("resolutionIndex", 1);
             PlayerPrefs.SetInt("fullscreenIndex", 1);
             PlayerPrefs.Save();
        }
        if (resindex == 1 & fscreenindex == 2){
             Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
             PlayerPrefs.SetInt("resolutionIndex", 1);
             PlayerPrefs.SetInt("fullscreenIndex", 2);
             PlayerPrefs.Save();
        }
        
        if (resindex == 2 & fscreenindex == 0){
             Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
             PlayerPrefs.SetInt("resolutionIndex", 2);
             PlayerPrefs.SetInt("fullscreenIndex", 0);
             PlayerPrefs.Save();
        }
        if (resindex == 2 & fscreenindex == 1){
             Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
             PlayerPrefs.SetInt("resolutionIndex", 2);
             PlayerPrefs.SetInt("fullscreenIndex", 1);
             PlayerPrefs.Save();
        }
        if (resindex == 2 & fscreenindex == 2){
             Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
             PlayerPrefs.SetInt("resolutionIndex", 2);
             PlayerPrefs.SetInt("fullscreenIndex", 2);
             PlayerPrefs.Save();
        }
    }

     private void loadRes(){

          int resindex = PlayerPrefs.GetInt("resolutionIndex");
          int fscreenindex= PlayerPrefs.GetInt("fullscreenIndex");

          ResolutionDropdown.value = resindex;
          FullscreenDropdown.value = fscreenindex;

        if (resindex == 0 & fscreenindex == 0){
             Screen.SetResolution(854, 480, FullScreenMode.FullScreenWindow);
        }
        if (resindex == 0 & fscreenindex == 1){
             Screen.SetResolution(854, 480, FullScreenMode.ExclusiveFullScreen);
        }
        if (resindex == 0 & fscreenindex == 2){
             Screen.SetResolution(854, 480, FullScreenMode.Windowed);
        }
        
        if (resindex == 1 & fscreenindex == 0){
             Screen.SetResolution(1280, 720, FullScreenMode.FullScreenWindow);
             PlayerPrefs.Save();
        }
        if (resindex == 1 & fscreenindex == 1){
             Screen.SetResolution(1280, 720, FullScreenMode.ExclusiveFullScreen);
             PlayerPrefs.Save();
        }
        if (resindex == 1 & fscreenindex == 2){
             Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
             PlayerPrefs.Save();
        }
        
        if (resindex == 2 & fscreenindex == 0){
             Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);
             PlayerPrefs.Save();
        }
        if (resindex == 2 & fscreenindex == 1){
             Screen.SetResolution(1920, 1080, FullScreenMode.ExclusiveFullScreen);
             PlayerPrefs.Save();
        }
        if (resindex == 2 & fscreenindex == 2){
             Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
        }
     }

}
