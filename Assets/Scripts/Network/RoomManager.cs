using System.IO;
using Core.Utilities;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network
{
    public class RoomManager : NetworkPersistantSingleton<RoomManager>
    {
        public override void OnEnable()
        {
            base.OnEnable();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Game") // We are in the game scene
            {
                GameObject newPlayer = PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Player"), GetRandomPoint(), Quaternion.identity);
                PhotonView playerNetwork = newPlayer.GetComponent<PhotonView>();
                playerNetwork.Owner.NickName = "Player" + playerNetwork.GetInstanceID();
            }
            else if (scene.name == "MainMenu") // we are in the main menu
            {
                
            }
        }
        
        private Vector3 GetRandomPoint()
        {
            float x = Random.Range(-45f, 45f);
            float y = 5f;
            float z = Random.Range(-45f, 45f);

            Vector3 point = new Vector3(x, y, z);
            
            return point;
        }
    }
}