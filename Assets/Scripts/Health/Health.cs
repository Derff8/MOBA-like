using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageble
{
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private AgentCharacter _agentCharacter;

    [SerializeField] private bool _destroyAfterDeath;

    [SerializeField] private float _maxHealth;

    private float _currentHealth ;

    public float CurrentHealt => _currentHealth;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp( _currentHealth, 0, _maxHealth );

        if (_currentHealth == 0)
        {
            DoDieBehaviour();                       
        }

        if (_characterView != null)
        {
            _characterView.TakeDamage();
        }
        
        Debug.Log(_currentHealth);
    }

    private void DoDieBehaviour()
    {
        IsDead = true;
        
        if (_characterView != null)
        {
            _characterView.PlayDieAnimation();
        }
        
        if (_agentCharacter != null)
        {
            _agentCharacter.StopMove();
        }

        if (_destroyAfterDeath)
        {
            Destroy(gameObject, 3f);
        }
    }
}
