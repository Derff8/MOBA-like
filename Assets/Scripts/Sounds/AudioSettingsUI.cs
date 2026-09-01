using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private SoundsController _controller;

    private void Awake()
    {
        _controller = new SoundsController(_audioMixer);
    }

    public void OnMusic()
    {
        _controller.OnMusic();
    }

    public void OffMusic()
    {
        _controller.OffMusic();
    }

    public void OnExplosionSounds()
    {
        _controller.OnExplosionSounds();
    }

    public void OffExplosionSounds()
    {
        _controller.OffExplosionSounds();
    }


}
