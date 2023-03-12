using Core.UI;
using Core.Utilities;
using UnityEngine;

namespace UI.MainMenu
{
    public class FPSMainMenu : Singleton<FPSMainMenu>
    {
        public LoadingPage loadingPage;
        public ButtonsMenu buttonsMenu;
        public FindRoomPage findRoomPage;
        public CreateRoomMenuPage createRoomMenuPage;
        public RoomMenuPage roomMenuPage;
        public ErrorMenuPage errorMenuPage;
        
        private SimpleMainMenuPage currentPage;

        public void ShowLoadingPage()
        {
            if (currentPage != null)
            {
                currentPage.Hide();
            }

            currentPage = loadingPage;
            loadingPage.Show();  
        } 

        public void HideLoadingPage() => loadingPage.Hide();

        public void ShowButtonsPage()
        {
            if (currentPage != null)
            {
                currentPage.Hide();
            }

            currentPage = buttonsMenu;
            buttonsMenu.Show();  
        } 

        public void HideButtonsPage() => buttonsMenu.Hide();
        
        public void ShowCreateRoomPage()
        {
            if (currentPage != null)
            {
                currentPage.Hide();
            }

            currentPage = createRoomMenuPage;
            createRoomMenuPage.Show();  
        }

        public void HideCreateRoomPage() => createRoomMenuPage.Hide();

        public void ShowRoomPage()
        {
            if (currentPage != null)
            {
                currentPage.Hide();
            }

            currentPage = roomMenuPage;
            roomMenuPage.Show(); 
        }

        public void HideRoomPage() => roomMenuPage.Hide();
        
        public void ShowErrorPage()
        {
            if (currentPage != null)
            {
                currentPage.Hide();
            }

            currentPage = errorMenuPage;
            errorMenuPage.Show(); 
        }

        public void HideErrorPage() => errorMenuPage.Hide();
        
        public void ShowFindRoomPage()
        {
            if (currentPage != null)
            {
                currentPage.Hide();
            }

            currentPage = findRoomPage;
            findRoomPage.Show(); 
        }

        public void HideFindRoomPage() => findRoomPage.Hide();
        
        #region Button Events

        public void OnClickQuit()
        {
            Application.Quit();
        }
        
        #endregion
    }
}
