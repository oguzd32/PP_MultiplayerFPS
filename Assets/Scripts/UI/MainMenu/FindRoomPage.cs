using System.Collections.Generic;
using Core.UI;
using Photon.Realtime;
using UnityEngine;

namespace UI.MainMenu
{
    public class FindRoomPage : SimpleMainMenuPage
    {
        [SerializeField] private RoomListItem _roomListItemPrefab;
        [SerializeField] private Transform content;
        
        public void GetRoomList(List<RoomInfo> roomList)
        {
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
            
            for (int i = 0; i < roomList.Count; i++)
            {
                if(roomList[i].RemovedFromList)
                    continue;
                RoomListItem item = Instantiate(_roomListItemPrefab, content);
                item.Initialize(roomList[i]);
            }
        }
    }
}