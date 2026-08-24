using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private readonly int _ExplosionTriggerKey = Animator.StringToHash("Explosion");

    private ParticleSystem _explosionEffectPrefab;
    private Animator _animator;
    private float _damage;
    private float _radius;

    private float _timeToExplode;

    private float _timer;

    private SphereCollider _collider;

    public bool IsActivated { get; private set; }

    private void Update()
    {
        if (IsActivated)
        {
            _timer -= Time.deltaTime;

            if(_timer <= 0)
                Explosion();
        }
    }

    public void Init(ParticleSystem explosionEffectPrefab, float damage, float radius, float timeToExplode)
    {
        _explosionEffectPrefab = explosionEffectPrefab;
        _damage = damage;
        _radius = radius;
        _timeToExplode = timeToExplode;

        _collider = GetComponent<SphereCollider>();
        _animator = GetComponent<Animator>();

        _collider.isTrigger = true;
        _collider.radius = _radius;

        IsActivated = false;

        _timer = _timeToExplode;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsActivated) return;

        IDamageble damageble = other.GetComponent<IDamageble>();

        if (damageble != null && damageble.IsDead == false)
        {
            IsActivated = true;
            _animator.SetTrigger(_ExplosionTriggerKey);
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
