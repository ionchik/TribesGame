using System;
using System.Collections.Generic;
using GamePhase;
using GamePhase.Professions;
using UnityEngine;
using UnityEngine.UI;

public class ProfessionTab : MonoBehaviour
{
    [SerializeField] private ProfessionType _professionType;
    [SerializeField] private TabsHandler _tabsHandler;
    [SerializeField] private Text _workersNumber;
    [SerializeField] private Text _resourceNumber;

    private void Start()
    {
        _tabsHandler.Refreshing.AddListener(Refresh);
    }

    private void OnDestroy()
    {
        _tabsHandler.Refreshing.RemoveListener(Refresh);
    }

    private void Refresh(Dictionary<ProfessionType,  Tuple<int, int>> professions)
    {
        Tuple<int, int> counters = professions[_professionType];
        _workersNumber.text = counters.Item1.ToString();
        _resourceNumber.text = counters.Item2.ToString();
    }
}

