using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndView : MonoBehaviour
{
	[Serializable]
	public class TribeAttribute
	{
		public Tribe PlayerTribe;
		public Sprite Icon;
	}

	[SerializeField] private Game _game;
    [SerializeField] private Canvas _winterCanvas;
    [SerializeField] private Canvas _winCanvas;
	[SerializeField] private Image _tribeIcon;
	[SerializeField] private Text _pointsView;
	[SerializeField] private Button _nextButton;
	[SerializeField] private TribeAttribute[] _tribeAttributes;

	private List<Tuple<Tribe, int>> _playerPoints;
	private int _playerIndex = 0;

	private void OnEnable()
	{
		_game.WinterStarted += OnWinterCame;
		_game.GameEnded += OnGameEnd;
		_nextButton.onClick.AddListener(ShowNextPoints);
	}

	private void OnDisable()
	{
		_game.WinterStarted -= OnWinterCame;
		_game.GameEnded -= OnGameEnd;
		_nextButton.onClick.RemoveListener(ShowNextPoints);
	}

	private void OnWinterCame()
	{
		_winterCanvas.gameObject.SetActive(true);
	}

	private void OnGameEnd(List<Tuple<Tribe, int>> playerPoints)
	{
		_playerPoints = playerPoints;
		_winCanvas.gameObject.SetActive(true);
		ShowNextPoints();
	}

	private void ShowNextPoints()
	{
		if(_playerIndex == _playerPoints.Count) SceneManager.LoadScene("MenuScene");
		var (tribe, points) = _playerPoints[_playerIndex];
		_tribeIcon.sprite = _tribeAttributes.Where(ta => ta.PlayerTribe == tribe).First().Icon;
		_pointsView.text = points.ToString();
		_playerIndex++;
	}
}
