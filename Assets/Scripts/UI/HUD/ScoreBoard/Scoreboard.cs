using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace UI.HUD.ScoreBoard
{
    public class Scoreboard : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Transform content;
        [SerializeField] private ScoreboardItem scoreBoardItemPrefab;
        [SerializeField] private Canvas _canvas;
        
        private Dictionary<Photon.Realtime.Player, ScoreboardItem> _scoreboardItems =
            new Dictionary<Photon.Realtime.Player, ScoreboardItem>();

        private void Start()
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                AddScoreboardItem(player);
            }
        }

        private void Update()
        {
            _canvas.enabled = Input.GetKey(KeyCode.Tab);
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            AddScoreboardItem(newPlayer);
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            RemoveScoreboardItem(otherPlayer);
        }

        private void AddScoreboardItem(Photon.Realtime.Player player)
        {
            ScoreboardItem item = Instantiate(scoreBoardItemPrefab, content);
            item.Initialize(player);
            _scoreboardItems[player] = item;
        }

        private void RemoveScoreboardItem(Photon.Realtime.Player player)
        {
            Destroy(_scoreboardItems[player].gameObject);
            _scoreboardItems.Remove(player);
        }
    }
}