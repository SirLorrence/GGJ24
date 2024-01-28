using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OutlineWhenNear : MonoBehaviour
{
    [SerializeField] MeshRenderer mr;
    [SerializeField] SkinnedMeshRenderer smr;
    [SerializeField] Material outlineMaterial;
    [SerializeField] Material originalMaterial;

    private void Start()
    {
        if (!TryGetComponent<MeshRenderer>(out mr))
            smr = GetComponent<SkinnedMeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (mr != null)
                mr.material = outlineMaterial;
            if (smr != null)
                smr.material = outlineMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (mr != null)
                mr.material = outlineMaterial;
            if (smr != null)
                smr.material = outlineMaterial;
        }
    }
}
