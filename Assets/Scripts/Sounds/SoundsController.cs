using UnityEngine;
using UnityEngine.Audio;

public class SoundsController
{
    private AudioMixer _audioMixer;

    private const string MusicKey = "MusicVolume";
    private const string ExplosionSoundsKey = "ExplosionVolume";

    private float _soundsOnValue = 0f;
    private float _soundsOffValue = -80f;

    public SoundsController(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
    }

    public bool IsMusicOn() => IsVolumeOn(MusicKey);

    public bool IsExplosionOn() => IsVolumeOn(ExplosionSoundsKey);

    public void OffMusic() => OffVolume(MusicKey);
    public void OnMusic() => OnVolume(MusicKey);

    public void OffExplosionSounds() => OffVolume(ExplosionSoundsKey);
    public void OnExplosionSounds() => OnVolume(ExplosionSoundsKey);

    private bool IsVolumeOn(string key) => _audioMixer.GetFloat(key, out float volume) && Mathf.Abs(volume - _soundsOnValue) <= 0.01f;

    private void OnVolume(string key) => _audioMixer.SetFloat(key, _soundsOnValue);

    private void OffVolume(string key) => _audioMixer.SetFloat(key, _soundsOffValue);
}
