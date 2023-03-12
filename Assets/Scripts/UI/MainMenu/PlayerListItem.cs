using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI.MainMenu
{
    public class PlayerListItem : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TextMeshProUGUI text;

        private Photon.Realtime.Player player;
        
        public void Initialize(Photon.Realtime.Player player)
        {
            this.player = player;
            text.text = this.player.NickName;
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            if (player == otherPlayer)
            {
                Destroy(gameObject);
            }
        }

        public override void OnLeftRoom()
        {
            Destroy(gameObject);
        }
    }
}