using System;
using UnityEngine;

public class Health
{
    private CharacterView _characterView;
    private AgentCharacter _agentCharacter;

    private float _maxHealth;

    private float _currentHealth ;

    public Health(CharacterView characterView, float maxHealth)
    {
        _characterView = characterView;
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public float CurrentHealth => _currentHealth;

    public bool IsDead { get; private set; }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp( _currentHealth, 0, _maxHealth );

        if (_currentHealth <= _maxHealth * 0.3f)
        {
            _characterView.SetInjuredLayerWeight(1f);
        }

        if (_characterView != null)
        {
            _characterView.TakeDamage();
        }       
    }

    public void IncreaseHealth(float heal)
    {
        if (IsDead) return;

        _currentHealth += heal;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_currentHealth > _maxHealth * 0.3f)
        {
            _characterView.SetBaseLayerWeight(1f);
        }
    }

    public void SetDeadStatus() => IsDead = true; 
}
