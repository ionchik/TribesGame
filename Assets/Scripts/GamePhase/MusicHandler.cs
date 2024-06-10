using System.Collections;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private Game _game;
	[SerializeField] private AudioSource _mainMusic;
	[SerializeField] private AudioSource _turnChangedAudio;

	private void OnEnable()
	{
		_game.TurnChanged += OnTurnChanged;
	}

	private void OnDisable()
	{
		_game.TurnChanged -= OnTurnChanged;
	}

	private void OnTurnChanged(Player player)
	{
		StartCoroutine(PlayTurnChangingSound());
	}

	private IEnumerator PlayTurnChangingSound()
	{
		_mainMusic.volume = 0.3f;
		_turnChangedAudio.Play();
		yield return new WaitForSecondsRealtime(1);
		_mainMusic.volume = 1;
	}
}
