using System.Collections;
using System.Collections.Generic;
using Core.UI;
using Core.Utilities;
using Photon.Pun;
using Photon.Realtime;
using UI;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Network
{
    public class NetworkManager : NetworkSingleton<NetworkManager>
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
            FPSMainMenu.instance.ShowLoadingPage();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            PhotonNetwork.JoinLobby();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            
            FPSMainMenu.instance.ShowButtonsPage();
            //PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            
            FPSMainMenu.instance.ShowRoomPage();
            FPSMainMenu.instance.roomMenuPage.roomNameText.text = PhotonNetwork.CurrentRoom.Name;

            Photon.Realtime.Player[] players = PhotonNetwork.PlayerList; 
            
            FPSMainMenu.instance.roomMenuPage.ClearList();
            
            for (int i = 0; i < players.Length; i++)
            {
                FPSMainMenu.instance.roomMenuPage.GetPlayerList(players[i]);
            }
            
            FPSMainMenu.instance.roomMenuPage.EnableStartButton(PhotonNetwork.IsMasterClient);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            FPSMainMenu.instance.errorMenuPage.errorText.text = "Room Creation Failed: " + message;
            FPSMainMenu.instance.ShowErrorPage();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            FPSMainMenu.instance.ShowButtonsPage();
        }

        public void CreateRoom(string roomName)
        {
            PhotonNetwork.CreateRoom(roomName);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            FPSMainMenu.instance.ShowLoadingPage();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            FPSMainMenu.instance.findRoomPage.GetRoomList(roomList);
        }

        public void JoinRoom(RoomInfo info)
        {
            PhotonNetwork.JoinRoom(info.Name);
            FPSMainMenu.instance.ShowLoadingPage();
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            FPSMainMenu.instance.roomMenuPage.GetPlayerList(newPlayer);
        }

        public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
        {
            FPSMainMenu.instance.roomMenuPage.EnableStartButton(PhotonNetwork.IsMasterClient);
        }

        public void StartGame()
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
