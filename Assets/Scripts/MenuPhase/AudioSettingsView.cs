using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsView : MonoBehaviour
{
    [SerializeField] private AudioSettings _settings;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Toggle _effectToggle;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Text _volumeView;
    [SerializeField] private int _maxVolume;

    private void Start()
    {
        _settings.VolumeChanged.AddListener(RefreshVolume);
        _settings.VolumeChanged.AddListener(RefreshVolumeSlider);
        _settings.EffectsChanged.AddListener(RefreshEffects);
        _settings.MusicChanged.AddListener(RefreshMusic);
    }

    private void OnDestroy()
    {
        _settings.VolumeChanged.RemoveListener(RefreshVolume);
        _settings.VolumeChanged.RemoveListener(RefreshVolumeSlider);
        _settings.EffectsChanged.RemoveListener(RefreshEffects);
        _settings.MusicChanged.RemoveListener(RefreshMusic);
    }

    private void RefreshVolume(float value)
    {
        _volumeView.text = ((int)(value * _maxVolume)).ToString();
    }

    private void RefreshVolumeSlider(float value) 
    {
        _volumeSlider.value = value;
    }

    private void RefreshEffects(bool enabled)
    {
        _effectToggle.isOn = enabled;
    }

    private void RefreshMusic(bool enabled)
    {
        _musicToggle.isOn = enabled;
    }
}
