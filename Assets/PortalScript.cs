using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PortalScript : MonoBehaviour 
{
    private static int portalIdRegistry = 1;
    private int portalId;
    public GameObject PortalMesh;
    public GameObject ExampleObject;
    public GameObject BackgroundBox;

    private Material[] _targetMaterials;

    private void Start()
    {
        portalId = portalIdRegistry;
        portalIdRegistry++;
        _targetMaterials = GetMaterials();
        PortalMesh.transform.SetParent(null, true);
        ExampleObject.transform.SetParent(null, true);
        BackgroundBox.transform.SetParent(PortalMesh.transform, true);
    }

    private Material[] GetMaterials()
    {
        Material[] ret = GetComponentsInChildren<MeshRenderer>()
            .SelectMany(item => item.materials).ToArray();
        ret = ret.Concat(GetComponentsInChildren<SkinnedMeshRenderer>()
            .SelectMany(item => item.materials)).ToArray();
        return ret.ToArray();
    }

    void Update () 
    {
        foreach (Material mat in _targetMaterials)
        {
            mat.SetFloat("_COLORMASK", portalId);
            mat.SetVector("_PortalNormal", PortalMesh.transform.up);
            mat.SetVector("_PortalPoint", PortalMesh.transform.position);
        }
	}
}
