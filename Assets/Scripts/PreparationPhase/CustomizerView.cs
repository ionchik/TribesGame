using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomizerView : MonoBehaviour
{
    [SerializeField] private PlayerCustomizer _customizer;
	[SerializeField] private Text _playersText;
	[SerializeField] private TribeToggle[] _tribeToggles;

	private int currentPlayerIndex = 1;

	private void OnEnable()
	{
		_customizer.PlayerCustomized += OnPlayerCustomized;
	}

	private void OnDisable()
	{
		_customizer.PlayerCustomized -= OnPlayerCustomized;
	}

	private void OnPlayerCustomized(Tribe tribe)
	{
		_tribeToggles.First(tt => tt.TribeVariant == tribe).LinkedToggle.gameObject.SetActive(false);
		currentPlayerIndex++;
		_playersText.text = "customize player [" + currentPlayerIndex + "]";
	}
}
