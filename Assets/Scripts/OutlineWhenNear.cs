using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OutlineWhenNear : MonoBehaviour
{
    [SerializeField] MeshRenderer mr;
    [SerializeField] Material outlineMaterial;
    [SerializeField] Material originalMaterial;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mr.material = outlineMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            mr.material = originalMaterial;
        }
    }
}
