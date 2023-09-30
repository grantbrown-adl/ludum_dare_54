using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAttractor : MonoBehaviour
{
    [SerializeField] private float _gravitationalForce = 10f;
    [SerializeField] private float _radius = 5.0f;
    //[SerializeField] private List<Rigidbody2D> _rigidbodies = new();    
    [SerializeField] private HashSet<Rigidbody2D> _rigidbodies = new();
    [SerializeField] private int _numRigidbodies;


    void FixedUpdate()
    {
        _numRigidbodies = _rigidbodies.Count;
        Debug.Log("Fixed Update");
        foreach (Collider2D objectCollision in Physics2D.OverlapCircleAll(transform.position, _radius))
        {
            Debug.Log("Overlap Circle");
            Rigidbody2D colliderRigidbody = objectCollision.GetComponent<Rigidbody2D>();
            Debug.Log($"Collider RB: {colliderRigidbody}");
            if (colliderRigidbody != null && !_rigidbodies.Contains(colliderRigidbody)) _rigidbodies.Add(objectCollision.GetComponent<Rigidbody2D>());
            if (_rigidbodies == null) return;
        }

        foreach (Rigidbody2D rb2d in _rigidbodies)
        {
            Debug.Log($"Applying gravitational force");
            ApplyGravitationalForce(rb2d);
        }
    }

    private void ApplyGravitationalForce(Rigidbody2D rb2d)
    {
        Vector2 direction = transform.position  - rb2d.transform.position;
        float distance = direction.magnitude;
        float forceMagnitude = _gravitationalForce / (distance * distance);

        Vector2 force = direction.normalized * forceMagnitude;

        rb2d.AddForce(force);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision entered");
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Add the Rigidbody2D component to the HashSet.
            _rigidbodies.Add(rb);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("collision exited");
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Remove the Rigidbody2D component from the HashSet.
            _rigidbodies.Remove(rb);
        }
    }
}
