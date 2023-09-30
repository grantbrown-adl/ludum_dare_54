using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RuneScript : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D _rigidbody2D;
    [SerializeField] SpriteRenderer _spriteRenderer;

    [Header("Settings")]
    [SerializeField] private bool _showRunes;
    [SerializeField] private Sprite[] _hiddenRunes;
    [SerializeField] private Sprite[] _runes;
    [SerializeField] private float _scale = 1.0f;
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    [SerializeField] private float _runeSpeed;
    [SerializeField] private float _lifeTime;

    public float Scale { get => _scale; set => _scale = value; }
    public float MinScale { get => _minScale; set => _minScale = value; }
    public float MaxScale { get => _maxScale; set => _maxScale = value; }

    private void Awake()
    {
        if(MinScale <= 0.0f) MinScale = 0.5f;
        if (MaxScale <= 1.5f) MaxScale = 1.5f;
        if (_runeSpeed <= 0.0f) _runeSpeed = 10.0f;
        if (_lifeTime <= 0.0f) _lifeTime = 30.0f;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = _showRunes ? _runes[Random.Range(0, _runes.Length)] : _hiddenRunes[Random.Range(0, _hiddenRunes.Length)];

        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        transform.localScale = new Vector3();

        transform.localScale = Vector3.one * Scale;

        _rigidbody2D.mass = Scale * 2;
    }


    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * _runeSpeed);

        Destroy(gameObject, _lifeTime);
    }

    private void SplitRune()
    {
        if ((Scale * 0.5f) <= MinScale) return;

        int pieces = (int)(Scale / MinScale);

        for (int i = 0; i < pieces; i++)
        {
            Vector2 currentPosition = transform.position;
            currentPosition += Random.insideUnitCircle * 0.5f;

            RuneScript split = Instantiate(this, currentPosition, transform.rotation);
            split.Scale = Scale * MinScale;
            split.SetTrajectory(Random.insideUnitCircle.normalized * _runeSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool collided = collision.gameObject.layer == LayerMask.NameToLayer("Projectile") || collision.gameObject.layer == LayerMask.NameToLayer("Player");
        if (collided)
        {
            //if (showExplosions) Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            //ObjectPoolScript.ReturnInstance(gameObject);
            SplitRune();
            Destroy(gameObject);
            return;
        }
    }


}
