using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private bool _canFire;
    [SerializeField] private float _fireRate;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _canFire = true;
        if (_fireRate <= 0) _fireRate = 0.25f;
    }

    void Update()
    {
        ReceiveInput();
    }

    void ReceiveInput()
    {
        if (!PlayerManager.Instance.InputAllowed) return;
        if(_playerController.IsDead || TimeManager.Instance.IsPaused) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 moveInput = new Vector2(x, y);

        bool fire = _canFire && (Input.GetButton("Fire1") || GameHandler.Instance.AutoFire);
        
        _playerController.HandleInput(moveInput, fire);

        if(fire)
        {
            _canFire = false;
            StartCoroutine(FireCooldown());
        }
    }


    private IEnumerator FireCooldown()
    {
        yield return new WaitForSeconds(_fireRate);
        _canFire = true;
    }
}
