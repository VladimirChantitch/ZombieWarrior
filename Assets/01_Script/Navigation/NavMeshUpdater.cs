using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus;
using NavMeshPlus.Components;

public class NavMeshUpdater : MonoBehaviour
{
    public NavMeshSurface surface2D;

    private void OnEnable()
    {
        if (surface2D == null)
        {
            surface2D = FindObjectOfType<NavMeshSurface>();
        }
    }

    public void UpdateNavMesh()
    {

        surface2D.UpdateNavMesh(surface2D.navMeshData);
        surface2D.BuildNavMeshAsync();
    }
}
