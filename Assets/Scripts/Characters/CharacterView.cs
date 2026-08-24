using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int IsTakeDamageKey = Animator.StringToHash("IsTakeDamage");
    private readonly int IsDieKey = Animator.StringToHash("IsDie");
    private readonly int IsAttackKey = Animator.StringToHash("IsAttack");

    private string _layerName = "InjuredLayer";

    [SerializeField] private MovementIndicatorExample _indicator;
    [SerializeField] private bool _showIndicator;

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude > 0.05f)
            StartRunning();
        else
            StopRunning();
    }

    public void TakeDamage()
    {
        _animator.SetTrigger(IsTakeDamageKey);
    }

    public void PlayDieAnimation()
    {
        _animator.SetBool(IsDieKey, true);
    }

    public void SetInjuredLayerWeight(float weight)
    {
        int layerIndex = _animator.GetLayerIndex(_layerName);

        if (layerIndex != -1)
        {
            _animator.SetLayerWeight(layerIndex, weight);
        }
    }

    public void DoAttackAnimation()
    {
        _animator.SetTrigger(IsAttackKey);
    }

    public void PlaceIndicator(Vector3 destination)
    {
        if (_showIndicator && _indicator != null)
        {
            _indicator.SetIndicatorTo(destination);
        }
    }

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }
}
