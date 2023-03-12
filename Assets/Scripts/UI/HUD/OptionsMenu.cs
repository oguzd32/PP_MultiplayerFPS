using Core.UI;
using Network;
using UnityEngine;

namespace UI.HUD
{
    public class OptionsMenu : SimpleMainMenuPage
    {
        public void OnClickLeaveRoom()
        {
            
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