using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class PlayerCustomizer : MonoBehaviour
{
    [SerializeField] private PlayersChooser _playersChooser;
	[SerializeField] private PlayerInfoTransfer _playerInfoTransfer;
    [SerializeField] private Button _nextButton;
	[SerializeField] private InputField _nameInput;
	[SerializeField] private TribeToggle[] _tribeToggles;

    public event Action<Tribe> PlayerCustomized;

	private void OnEnable()
	{
        _nextButton.onClick.AddListener(Customize);
	}

	private void OnDisable()
	{
		_nextButton.onClick.RemoveAllListeners();
	}

	private Dictionary<Tribe, string> _players = new Dictionary<Tribe, string>();

    public Dictionary<Tribe, string> GetPlayersInfo() => _players;

    private void Customize()
    {
		TribeToggle toggle = _tribeToggles.First(tt => tt.LinkedToggle.isOn);
        if (toggle == null) return;

		Tribe chosenTribe = toggle.TribeVariant;
        _players.Add(chosenTribe, _nameInput.text);
        PlayerCustomized?.Invoke(chosenTribe);

        foreach (TribeToggle tt in _tribeToggles)
        { 
			tt.LinkedToggle.isOn = false; 
		}

		if (_players.Keys.Count == _playersChooser.PlayersNumber) 
		{
			_playerInfoTransfer.PlayersInfo = _players;
			SceneManager.LoadScene("GameScene"); 
		}
	}
}
