using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    public float minRotationSpeed = 90f;
    public float maxRotationSpeed = 360f;
    public float rotationChangeInterval = 30f; // Interval in seconds to change the random rotation.

    private Vector3 rotationAxis;
    private float rotationSpeed;

    private void Start()
    {
        // Set an initial random rotation axis and speed.
        GenerateRandomRotation();

        // Start the coroutine to change rotation periodically.
        StartCoroutine(ChangeRotationPeriodically());
    }

    private void Update()
    {
        // Rotate the cube around the selected axis at the chosen speed.
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    private void GenerateRandomRotation()
    {
        // Generate a random rotation axis (in this example, only X, Y, or Z).
        rotationAxis = Random.onUnitSphere;

        // Generate a random rotation speed between min and max values.
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }

    private IEnumerator ChangeRotationPeriodically()
    {
        while (true)
        {
            // Wait for the specified interval.
            yield return new WaitForSeconds(rotationChangeInterval);

            // Change the random rotation.
            GenerateRandomRotation();
        }
    }
}
