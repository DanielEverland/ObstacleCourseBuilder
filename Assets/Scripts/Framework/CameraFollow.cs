using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject target;

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private void Update()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.z = transform.position.z;

        transform.position = targetPosition;
    }
}