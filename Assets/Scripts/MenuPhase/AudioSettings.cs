using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _masterGroup;
    [SerializeField] private AudioSettingsSaver _saver;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Toggle _effectToggle;
    [SerializeField] private Toggle _musicToggle;

    public UnityEvent<float> VolumeChanged;
    public UnityEvent<bool> EffectsChanged;
    public UnityEvent<bool> MusicChanged;

    private void Awake()
    {
        VolumeChanged = new UnityEvent<float>();
        EffectsChanged = new UnityEvent<bool>();
        MusicChanged = new UnityEvent<bool>();
    }

    private void Start()
    {
        _saver.Started.AddListener(OnLoad);
    }

    private void OnEnable()
    {
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
        _effectToggle.onValueChanged.AddListener(TurnEffects);
        _musicToggle.onValueChanged.AddListener(TurnMusic);
    }

    private void OnDisable()
    {
        _volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
        _effectToggle.onValueChanged.RemoveListener(TurnEffects);
        _musicToggle.onValueChanged.RemoveListener(TurnMusic);
    }

    private void OnDestroy()
    {
        _saver.Started.RemoveListener(OnLoad);
    }

    private void OnLoad(float volume, bool music, bool effects)
    {
        ChangeVolume(volume);
        TurnEffects(effects);
        TurnMusic(music);
    }

    private void ChangeVolume(float volume)
    {
        _masterGroup.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
        VolumeChanged?.Invoke(volume);
    }

    private void TurnMusic(bool enabled)
    {
        _masterGroup.audioMixer.SetFloat("MusicVolume", enabled ? 0 : -80);
        MusicChanged?.Invoke(enabled);
    }

    private void TurnEffects(bool enabled)
    {
        _masterGroup.audioMixer.SetFloat("EffectsVolume", enabled ? 0 : -80);
        EffectsChanged?.Invoke(enabled);
    }
}
