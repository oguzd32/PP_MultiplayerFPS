using Core.UI;
using Photon.Pun;
using UnityEngine;

namespace UI.HUD
{
    public class OptionsMenu : SimpleMainMenuPage
    {
        public void OnClickLeaveRoom()
        {
            PhotonNetwork.Disconnect();
        }

        public override void Hide()
        {
            base.Hide();

            GameUI.instance.isPaused = false;
        }

        public void OnClickQuitGame()
        {
            Application.Quit();
        }
    }
}