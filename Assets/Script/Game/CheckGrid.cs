using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrid : MonoBehaviour
{
    [Header("Layer Detect")]
    [SerializeField] private LayerMask layerCheck;
    private Collider[] hitColliders;

    public bool playerOnRange { get; private set; }

    public void GetNeighbors()
    {
        hitColliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity, layerCheck);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].GetComponent<Grid>().PossibleMoveGrid();

            if (hitColliders[i].CompareTag("PlayerPeace"))
                playerOnRange = true;
        }
    }

    public void ClearNeigbors()
    {
        if (hitColliders == null)
            return;

        for (int i = 0; i < hitColliders.Length; i++)
        {
            hitColliders[i].GetComponent<Grid>().Clear();
        }

        hitColliders = new Collider[0];
        playerOnRange = false;
    }
}
