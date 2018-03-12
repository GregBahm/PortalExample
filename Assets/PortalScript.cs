using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour 
{
	void Update () 
    {
        Shader.SetGlobalVector("_PortalNormal", transform.up);
        Shader.SetGlobalVector("_PortalPoint", transform.position);
	}
}
