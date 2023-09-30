using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Juice Bools")]
    [SerializeField] private bool showExplosions = false;

    [Header("Object Pools")]
    [SerializeField] private ObjectPoolScript _projectilePool;

    [Header("Components")]
    [SerializeField] GameObject _explosionEffect;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private PoolAfterTime _poolAfter;
    [SerializeField] private Vector2 _direction;
    private readonly float gravity = 0.0f;   

    private Rigidbody2D _rigidbody;

    public float Damage { get => _damage; set => _damage = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public Vector2 Direction { get => _direction; set => _direction = value; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();        
    }

    private void OnEnable()
    {
        Launch();
    }

    void Launch()
    {
        Vector2 launchDirection = transform.TransformDirection(Vector2.up);
        Vector2 initialVelocity = _moveSpeed * launchDirection;
        _rigidbody.velocity = initialVelocity;
        _rigidbody.gravityScale = gravity;

        StartCoroutine(ObjectPoolScript.DelayedReturnInstance(gameObject, 5.0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool collided = collision.gameObject.layer == LayerMask.NameToLayer("BlackHole") || collision.gameObject.layer == LayerMask.NameToLayer("Rune");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) return;
        if (collided)
        {
            if(showExplosions) Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            ObjectPoolScript.ReturnInstance(gameObject);
            return;
        }
    }
}
