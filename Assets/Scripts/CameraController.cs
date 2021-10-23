using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private void Update()
    {
        if(target != null)
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
        }
    }
}
