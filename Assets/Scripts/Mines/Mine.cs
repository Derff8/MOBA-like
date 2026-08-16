using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private readonly int _ExplosionTriggerKey = Animator.StringToHash("Explosion");

    [SerializeField] private ParticleSystem _explosionEffectPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;

    [SerializeField] private float _timeToExplode = 2;
    private float _timer;

    private SphereCollider _collider;

    private bool _isActivated = false;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
        _collider.radius = _radius;

        _timer = _timeToExplode;
    }

    private void Update()
    {
        if (_isActivated)
        {
            _animator.SetTrigger(_ExplosionTriggerKey);
            _timer -= Time.deltaTime;

            if(_timer <= 0)
                Explosion();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated) return;

        IDamageble damageble = other.GetComponent<IDamageble>();

        if (damageble != null)
        {
            _isActivated = true;
        }
    }

    private void Explosion()
    {
        Instantiate(_explosionEffectPrefab, transform.position, Quaternion.identity);

        Collider[] collidersInRadius = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider hitCollider in collidersInRadius)
        {
            IDamageble damageble = hitCollider.GetComponent<IDamageble>();
            if (damageble != null)
            {
                damageble.TakeDamage(_damage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
