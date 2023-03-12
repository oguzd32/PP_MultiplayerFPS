using Core.UI;
using Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class RoomMenuPage : SimpleMainMenuPage
    {
        public TextMeshProUGUI roomNameText;
        [SerializeField] private PlayerListItem _playerListItemPrefab;
        [SerializeField] private Transform content;
        [SerializeField] private Button startButton;

        public void GetPlayerList(Photon.Realtime.Player newPlayer)
        {
            PlayerListItem item = Instantiate(_playerListItemPrefab, content);
            item.Initialize(newPlayer);
        }

        public void OnClickStart()
        {
            NetworkManager.instance.StartGame();
        }

        public void ClearList()
        {
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
        }

        public void EnableStartButton(bool value) => startButton.gameObject.SetActive(value);
    }
}