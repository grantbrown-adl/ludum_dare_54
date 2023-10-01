using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthVisual : MonoBehaviour
{
    [SerializeField] private Sprite[] _healthSprites;
    [SerializeField] private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    private void Update()
    {
        if(!GameHandler.Instance.ShowHealth) _image.enabled = false;
        else _image.enabled = true;

        int index = PlayerManager.Instance.PlayerHealth;
        if (index > _healthSprites.Length - 1) index = _healthSprites.Length - 1;
        else if (index < 0) index = 0;

        _image.sprite = _healthSprites[index];
    }
}
