using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotation : MonoBehaviour
{
    public float rotationSpeed;

    private float targetRotation;

    private void Awake()
    {
        rotationSpeed = 360.0f;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
        
}
