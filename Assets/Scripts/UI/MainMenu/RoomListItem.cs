using Network;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace UI.MainMenu
{
    public class RoomListItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public RoomInfo info;
        
        public void Initialize(RoomInfo info)
        {
            this.info = info;
            text.text = info.Name;
        }

        public void OnClick()
        {
            NetworkManager.instance.JoinRoom(info);
        }
    }
}