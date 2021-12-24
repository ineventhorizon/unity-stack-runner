using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera playerCam;
    [SerializeField] public CinemachineVirtualCamera finalCam;
   
  
    private void OnEnable()
    {
        CameraSwitcher.AddCamera(playerCam);
        CameraSwitcher.AddCamera(finalCam);
        Observer.switchCam += SwitchCam;
    }
    private void OnDisable()
    {
        Observer.switchCam -= SwitchCam;
    }

    private void SwitchCam(string cameraName)
    {
        if(cameraName.Equals("PlayerCam") && !CameraSwitcher.IsActiveCamera(playerCam))
        {
            CameraSwitcher.SwitchCamera(playerCam);
        }
        else if(cameraName.Equals("FinalCam") && !CameraSwitcher.IsActiveCamera(finalCam))
        {
            CameraSwitcher.SwitchCamera(finalCam);
        }
    }
}
