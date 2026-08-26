using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsController : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Image _buttonIcon;

    private string _audioMixerKey = "MasterVolume";

    private float _soundsOnValue = 0f;
    private float _soundsOffValue = -80f;

    private float _volume;

    private bool _isMuted;

    private void Awake()
    {
        _isMuted = false;
    }

    public void SetOnAudio()
    {
        if (!_isMuted)
            return;

        _volume = _soundsOnValue;
           
        _audioMixer.SetFloat(_audioMixerKey, _volume);

        _isMuted = false;
    }

    public void SetOffAudio()
    {
        if (_isMuted) 
            return;

        _volume = _soundsOffValue;

        _audioMixer.SetFloat(_audioMixerKey, _volume);

        _isMuted = true;
    }
}
