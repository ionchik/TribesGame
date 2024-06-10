using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseBehaviour : MonoBehaviour
{
    [SerializeField] private PauseButton _pauseButton;
    [SerializeField] private Image _pauseScreen;

    public UnityEvent<GameState> StateChanged;

    private GameState _pauseState;
    private GameState _playState;

    private void Awake()
    {
        StateChanged = new UnityEvent<GameState>();
        _pauseState = GameState.Pause;
        _playState = GameState.Play;
    }

    private void Start()
    {
        _pauseButton.PauseClicked.AddListener(Pause);
        _pauseButton.CancelClicked.AddListener(Cancel);
    }

    private void OnDestroy()
    {
        _pauseButton.PauseClicked.RemoveListener(Pause);
        _pauseButton.CancelClicked.RemoveListener(Cancel);
    }

    private void Pause()
    {
        StateChanged?.Invoke(_pauseState);
        _pauseScreen.gameObject.SetActive(true);
    }

    private void Cancel()
    {
        StateChanged?.Invoke(_playState);
        _pauseScreen.gameObject.SetActive(false);
    }
}
