using System;
using System.Collections.Generic;
using GamePhase.Professions;
using UnityEngine;
using UnityEngine.Events;

namespace GamePhase
{
    public class TabsHandler: MonoBehaviour
    {
        [SerializeField] private Game _game;

        public UnityEvent<Dictionary<ProfessionType,  Tuple<int, int>>> Refreshing;

        private void Awake()
        {
            Refreshing = new UnityEvent<Dictionary<ProfessionType,  Tuple<int, int>>>();
        }

        private void Start()
        {
            _game.TurnChanged += Refresh;
        }

        private void OnDestroy()
        {
            _game.TurnChanged -= Refresh;
        }

        private void Refresh(Player player)
        {
            Refreshing?.Invoke(player.GetCounters());
        }
    }
}