using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PortalScript : MonoBehaviour 
{
    public int PortalId;
    public GameObject Portal;
    public GameObject PortaledContent;

    private Material[] _targetMaterials;

    private void Start()
    {
        Material[] mats = Portal.GetComponentsInChildren<MeshRenderer>()
            .SelectMany(item => item.materials).ToArray();
        mats = mats.Concat(Portal.GetComponentsInChildren<SkinnedMeshRenderer>()
            .SelectMany(item => item.materials)).ToArray();
        mats = mats.Concat(PortaledContent.GetComponentsInChildren<MeshRenderer>()
            .SelectMany(item => item.materials)).ToArray();
        mats = mats.Concat(PortaledContent.GetComponentsInChildren<SkinnedMeshRenderer>()
            .SelectMany(item => item.materials)).ToArray();
        _targetMaterials = mats.ToArray();
    }

    void Update () 
    {
        foreach (Material mat in _targetMaterials)
        {
            mat.SetFloat("_COLORMASK", PortalId);
            mat.SetVector("_PortalNormal", Portal.transform.up);
            mat.SetVector("_PortalPoint", Portal.transform.position);
        }
	}
}
