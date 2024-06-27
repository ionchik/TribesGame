using GamePhase.Professions;
using UnityEngine;
using UnityEngine.UI;

public class ProfessionChooser : MonoBehaviour
{
    [SerializeField] private GameObject _professionScreen;
    [SerializeField] private Image _cardImage;
    
    public ProfessionType Profession;
    
    private bool _isChosen;
    private Card _card;

    public bool IsChosen() => _isChosen;

    public void SetProfession(int professionType)
    {
        Profession = (ProfessionType) professionType;
	_professionScreen.SetActive(false);
	_isChosen = true;
    }

    public void StartChoosing(Card card)
    {
        _isChosen = false;
        _card = card;
        _cardImage.sprite = _card.GetComponent<Image>().sprite;
        _professionScreen.SetActive(true);
    }
}
