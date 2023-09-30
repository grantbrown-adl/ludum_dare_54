using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailHandler : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] TrailRenderer _trailRenderer;
    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _trailRenderer = GetComponent<TrailRenderer>();

        _trailRenderer.emitting = false;
    }

    private void Update()
    {
        if (_playerController.RenderTrail(out float rightVelocity, out bool braking))
            _trailRenderer.emitting = true;
        else _trailRenderer.emitting = false;
    }
}
