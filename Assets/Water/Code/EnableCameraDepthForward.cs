//
// Attach this script to your camera in order to use depth nodes in forward rendering
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EnableCameraDepthForward : MonoBehaviour
{

#if UNITY_EDITOR
	void OnDrawGizmos()
    {

        CSet();
	}
#endif

	void Start()
    {
        CSet();
	}

	void CSet()
    {
		if(GetComponent<Camera>().depthTextureMode == DepthTextureMode.None)
			GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
	}
}
