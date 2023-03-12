using Core.UI;
using Network;
using TMPro;
using UnityEngine;

namespace UI.MainMenu
{
    public class CreateRoomMenuPage : SimpleMainMenuPage
    {
        [SerializeField] private TMP_InputField roomNameInputField;

        public void CreateRoom()
        {
            if (string.IsNullOrEmpty(roomNameInputField.text)) return;

            FPSMainMenu.instance.ShowLoadingPage();
            NetworkManager.instance.CreateRoom(roomNameInputField.text);
        }
    }
}