using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    private bool isMobileRes = false;

    void Update()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        if (screenWidth < screenHeight)
        {
            if (!isMobileRes) 
            {
                Camera.main.fieldOfView = 120;
                isMobileRes = !isMobileRes;
            }
        }
        if (screenWidth > screenHeight)
        {
            if (isMobileRes)
            {
                Camera.main.fieldOfView = 60;
                isMobileRes = !isMobileRes;
            }
        }
    }
}
