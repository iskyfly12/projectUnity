using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public void MoveToTarget(Transform target, float t)
    {
        transform.DOMoveX(target.position.x, t);
        transform.DOMoveZ(target.position.z, t);
    }

    public void MoveToPosition(Vector3 position, float t)
    {
        transform.DOMove(position, t);
    }
}
