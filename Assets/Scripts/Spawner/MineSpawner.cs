using UnityEngine;

public class MineSpawner : SpawnerExample
{
    [SerializeField] private ParticleSystem _explosionEffectPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _mineDamage;
    [SerializeField] private float _radius;

    [SerializeField] private float _timeToExplode;

    [SerializeField] private float _timeToRespawn;

    private float _timer;

    private Mine _currentMine;

    protected bool _isOccupied { get { return _currentMine != null; } }


    private void Awake()
    {
        Spawn();
    }

    private void Update()
    {
        if (_isOccupied)
        {
            _timer = _timeToRespawn;
            return;
        }      

        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            Spawn();
        }
    }

    protected override void InitializeSpawnedObject(GameObject spawnObject)
    {
        _currentMine = spawnObject.GetComponent<Mine>();

        if (_currentMine != null)
        {
            _currentMine.Init(_explosionEffectPrefab, _mineDamage, _radius, _timeToExplode);
        }
    }
}
