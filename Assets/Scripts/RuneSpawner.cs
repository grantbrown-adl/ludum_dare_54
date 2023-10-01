using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSpawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] RuneScript _rune;

    [Header("Settings")]
    [SerializeField] private float _spawnSpeed = 3.0f;
    [SerializeField] private int _spawnAmount = 1;
    [SerializeField] private float _spawnDistance = 12.0f;
    [SerializeField] private float _trajectoryVariance = 15.0f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnRune), _spawnSpeed, _spawnSpeed);
    }

    private void SpawnRune()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            Vector3 randomDirection = Random.insideUnitCircle.normalized * _spawnDistance;
            Vector3 spawnLocation = transform.position + randomDirection;

            float randomAngle = Random.Range(-_trajectoryVariance, _trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(randomAngle, Vector3.forward);

            RuneScript rune = Instantiate(_rune, spawnLocation, rotation);

            rune.Scale = Random.Range(rune.MinScale, rune.MaxScale);
            rune.SetTrajectory(rotation * -spawnLocation);
        }
    }

}
