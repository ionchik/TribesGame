using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AttributesView : MonoBehaviour
{
    [Serializable]
    public class PlayerAttribute
    {
        public Tribe PlayerTribe;
        public Sprite DeckIcon;
        public Sprite UpIcon;
    }

    [SerializeField] private Game _game;
    [SerializeField] private Text _name;
    [SerializeField] private Image _upImage;
    [SerializeField] private Image _deckImage;
    [SerializeField] private Canvas _turnChangedCanvas;
    [SerializeField] private Image _turnChangedImage;
	[SerializeField] private PlayerAttribute[] _playerAttributes;

	private void OnEnable()
	{
        _game.TurnChanged += Refresh;
	}

	private void OnDisable()
	{
		_game.TurnChanged -= Refresh;
	}

    private void Refresh(Player player)
    {
        PlayerAttribute attribute = _playerAttributes.First(attribute => attribute.PlayerTribe == player.GetTribe());
        _turnChangedImage.sprite = attribute.UpIcon;
        _turnChangedCanvas.gameObject.SetActive(true);
		_name.text = player.GetName();
        _upImage.sprite = attribute.UpIcon;
        _deckImage.sprite = attribute.DeckIcon;
    }
}
