using UnityEngine;
using System;

public class AsteroidOrbit : MonoBehaviour
{
    [SerializeField]
    private Vector3 _point;

    void Update()
    {
        transform.RotateAround(_point, Vector3.up, 
               Mathf.SmoothStep(0,90,Time.deltaTime));
    }
}
