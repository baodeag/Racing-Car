using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target; 

    public float smoothValue; 

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target != null && transform.position.y >= 0)
        {
            Follow();
        }
    }

    void Follow()
    {
        Vector3 currentPos = transform.position;

        if (target != null)
        {
            Vector3 targetPos = target.position;
            transform.position = Vector3.Lerp(currentPos, targetPos, smoothValue * Time.deltaTime);
        }
    }
}
