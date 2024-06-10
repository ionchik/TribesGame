using System;
using System.Collections;
using System.Collections.Generic;
using GamePhase.Professions;
using UnityEngine;

public class HumanCardsBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _choosingScreen;
    [SerializeField] private ProfessionChooser _professionChooser;

    private bool _isChosen;
    private bool _isKilling;
    private List<Card> _cards;
    private List<Tuple<ProfessionType, Human>> _workers;
    
    public bool IsChosen() => _isChosen;
    public bool IsKilling() => _isKilling;
    public List<Tuple<ProfessionType, Human>> GetWorkers() => _workers;
    
    public void StartChoosing(List<Card> cards)
    {
        _isChosen = false;
        _isKilling = false;
        _cards = cards;
        _choosingScreen.SetActive(true);
    }

    public void Kill()
    {
        _isKilling = true;
        _isChosen = true;
        _choosingScreen.SetActive(false);
    }

    public void Save()
    {
		_choosingScreen.SetActive(false);
		StartCoroutine(ChooseProfessions());
    }

    public IEnumerator ChooseProfessions()
    {
        _workers = new List<Tuple<ProfessionType, Human>>();
        foreach (Card card in _cards)
        {
            _professionChooser.StartChoosing(card);
            yield return new WaitUntil(_professionChooser.IsChosen);
            _workers.Add(new Tuple<ProfessionType, Human>(_professionChooser.Profession, (Human)card));
        }
        _isChosen = true;
    }
}
