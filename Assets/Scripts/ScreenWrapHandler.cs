using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScreenWrapHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 screenHorizontal = new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x);
        Vector2 screenVertical = new Vector2(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y);

        if (screenPosition.x <= 0 && _rigidbody2D.velocity.x < 0) transform.position = new Vector2(screenHorizontal.y, transform.position.y);
        else if (screenPosition.x >= Screen.width && _rigidbody2D.velocity.x > 0) transform.position = new Vector2(screenHorizontal.x, transform.position.y);
        else if (screenPosition.y <= 0 && _rigidbody2D.velocity.y < 0) transform.position = new Vector2(transform.position.x, screenVertical.y);
        else if (screenPosition.y >= Screen.height && _rigidbody2D.velocity.y > 0) transform.position = new Vector2(transform.position.x, screenVertical.x);
    }
}
