using System.IO;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private PhotonView _photonView;
        private GameObject _controller;
        public int points;
        
        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            if (_photonView.IsMine)
            {
                CreateController();
            }
        }

        private void CreateController()
        {
            _controller = PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "PlayerController"),
                                                                    GetRandomPoint(),
                                                                    Quaternion.identity,
                                                                    0,
                                                                    new object[]{_photonView.ViewID});
            
        }
        
        private Vector3 GetRandomPoint()
        {
            float x = Random.Range(-45f, 45f);
            float y = 5f;
            float z = Random.Range(-45f, 45f);

            Vector3 point = new Vector3(x, y, z);
            
            return point;
        } 

        public static PlayerManager Find(Photon.Realtime.Player player)
        {
            return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x._photonView.Owner == player);
        }

        public void IncreasePoint()
        {
            _photonView.RPC(nameof(RPC_IncreasePoint), _photonView.Owner);
        }

        public void DecreasePoint()
        {
            _photonView.RPC(nameof(RPC_DecreasePoint), _photonView.Owner);
        }

        [PunRPC]
        private void RPC_IncreasePoint()
        {
            points++;

            Hashtable hash = new Hashtable();
            hash.Add("points", points);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }

        [PunRPC]
        private void RPC_DecreasePoint()
        {
            points--;
            
            Hashtable hash = new Hashtable();
            hash.Add("points", points);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
}
