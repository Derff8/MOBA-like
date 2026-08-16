using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int IsTakeDamageKey = Animator.StringToHash("IsTakeDamage");
    private readonly int IsDieKey = Animator.StringToHash("IsDie");

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

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }
}
