using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnController : MonoBehaviour
{
    [SerializeField]float turnSpeed;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(Vector3.up * -Input.GetAxis("Mouse X")* turnSpeed*Time.deltaTime); 
        } 
    }
}
