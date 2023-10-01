using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoFireToggle : MonoBehaviour
{
    [SerializeField] private Toggle _autoFireToggle;

    private void Awake()
    {
        _autoFireToggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        // Set the toggle's value based on your GameHandler logic
        _autoFireToggle.isOn = GameHandler.Instance.AutoFire;

        // Attach a listener to the onValueChanged event to update the GameHandler.Instance.AutoFire value
        _autoFireToggle.onValueChanged.AddListener(UpdateAutoFireValue);
    }



    public void ToggleAutoFire()
    {
        GameHandler.Instance.AutoFire = !GameHandler.Instance.AutoFire;
        _autoFireToggle.isOn = GameHandler.Instance.AutoFire;
    }

    private void UpdateAutoFireValue(bool isOn)
    {
        // Update the GameHandler.Instance.AutoFire value based on the toggle's value
        GameHandler.Instance.AutoFire = isOn;
    }
}
