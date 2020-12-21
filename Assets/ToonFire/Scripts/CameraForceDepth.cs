using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForceDepth : MonoBehaviour {

    [ContextMenu("Toggle Depth Rendering")]
    void DepthToggle()
    {
        Camera thisCamera = GetComponent<Camera>();
        if (thisCamera == null)
        {
            Debug.Log("Only works on a camera");
            return;
        }
        if (thisCamera.depthTextureMode == DepthTextureMode.Depth)
        {
            thisCamera.depthTextureMode = DepthTextureMode.None;
            Debug.Log("Camera Depth OFF");
        }
        else
        {
            thisCamera.depthTextureMode = DepthTextureMode.Depth;
            Debug.Log("Camera Depth ON");
        }
    }
}
