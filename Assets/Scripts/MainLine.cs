using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLine : MonoBehaviour
{
    LineRenderer line;

    public Transform positionA;
    public Transform positionB;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();

        line.positionCount = 2;
        line.startWidth = 1f;
        line.endWidth = 1f;
    }


    private void Update()
    {
        Vector3[] positions = new Vector3[2];

        positions[0] = new Vector2(positionA.position.x, positionA.position.y);
        positions[1] = positionA.TransformPoint(positionB.position);

        line.SetPosition(0, positions[0]);
        line.SetPosition(1, positions[1]);
    }
}
