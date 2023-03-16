using Hashtable = ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI.HUD.ScoreBoard
{
    public class ScoreboardItem : MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI userNameText;
        public TextMeshProUGUI scoreText;

        private Photon.Realtime.Player player;
        
        public void Initialize(Photon.Realtime.Player player)
        {
            userNameText.text = player.NickName;
            this.player = player;
            UpdateScoreDisplay();
        }

        private void UpdateScoreDisplay()
        {
            if (player.CustomProperties.TryGetValue("points", out object points))
            {
                scoreText.text = points.ToString();
            }
        }
        
        public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable.Hashtable changedProps)
        {
            if (targetPlayer == player)
            {
                if (changedProps.ContainsKey("points"))
                {
                    UpdateScoreDisplay();
                }
            }
        }
    }
}