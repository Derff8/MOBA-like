using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitSpawner : MonoBehaviour
{
    [SerializeField] private float _timeToSpawn;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private MedKitExample _medKitPrefab;

    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        StartSapwning();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_spawnCoroutine != null)
                StopSpawning();
            else
                StartSapwning();
        }
    }

    public void StartSapwning()
    {
        if (_spawnCoroutine == null)
        {
            _spawnCoroutine = StartCoroutine(SpawnMedKit());
        }
    }

    public void StopSpawning()
    {
        if ( _spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnMedKit()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToSpawn);
            Vector3 randomPointForSpawn = GetRandomPointAroundCharacter();
            Instantiate(_medKitPrefab, randomPointForSpawn, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPointAroundCharacter()
    {
        Vector2 randomPoint2D = Random.insideUnitCircle * _spawnRadius;
        Vector3 randomPoint3D = new Vector3(randomPoint2D.x, 1.5f, randomPoint2D.y);
        return transform.position + randomPoint3D;
    }
}
