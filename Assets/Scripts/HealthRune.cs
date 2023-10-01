using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRune : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] SpriteRenderer _spriteRenderer;

    [Header("Settings")]
    [SerializeField] private float _runeSpeed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _health;
    [SerializeField] private bool _showExplosions;
    [SerializeField] private GameObject _explosionEffect;

    public int Health { get => _health; set => _health = value; }
    public bool ShowExplosions { get => _showExplosions; set => _showExplosions = value; }

    private void Awake()
    {
        if (Health <= 0) Health = 1;
        if (_runeSpeed <= 0.0f) _runeSpeed = 10.0f;
        if (_lifeTime <= 0.0f) _lifeTime = 30.0f;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
    }


    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * _runeSpeed);

        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool collided = collision.gameObject.layer == LayerMask.NameToLayer("Player");
        if (collided)
        {
            if (GameHandler.Instance.ShowHealth) PlayerManager.Instance.PlayerHealth++;
            if (ShowExplosions && _explosionEffect != null) Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

