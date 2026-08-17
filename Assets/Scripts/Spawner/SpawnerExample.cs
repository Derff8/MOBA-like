using UnityEngine;

public abstract class SpawnerExample : MonoBehaviour
{
    [SerializeField] protected GameObject _spawnebleObjectPrefab;

    protected void Spawn()
    {
        GameObject spawnObject = Instantiate(_spawnebleObjectPrefab, transform.position, Quaternion.identity);

        InitializeSpawnedObject(spawnObject);
    }

    protected abstract void InitializeSpawnedObject(GameObject spawnObject);
}
