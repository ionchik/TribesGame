using UnityEngine;
using UnityEngine.Events;

public class AudioSettingsSaver : MonoBehaviour
{
    [SerializeField] private AudioSettings _settings;

    public UnityEvent<float, bool, bool> Started;

    private void Awake()
    {
        Started = new UnityEvent<float, bool, bool>();
    }

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        bool music = PlayerPrefs.GetInt("EnableMusic") == 1;
        bool effects = PlayerPrefs.GetInt("EnableEffects") == 1;
        Started?.Invoke(volume, music, effects);
    }

    private void OnEnable()
    {
        _settings.VolumeChanged.AddListener(SaveVolume);
        _settings.EffectsChanged.AddListener(SaveEffects);
        _settings.MusicChanged.AddListener(SaveMusic);
    }

    private void OnDisable()
    {
        _settings.VolumeChanged.RemoveListener(SaveVolume);
        _settings.EffectsChanged.RemoveListener(SaveEffects);
        _settings.MusicChanged.RemoveListener(SaveMusic);
    }

    private void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
    }

    private void SaveMusic(bool enabled)
    {
        PlayerPrefs.SetInt("EnableMusic", enabled ? 1 : 0);
    }

    private void SaveEffects(bool enabled)
    {
        PlayerPrefs.SetInt("EnableEffects", enabled ? 1 : 0);
    }
}
