using System;
using System.IO;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private PhotonView _photonView;

        private GameObject _controller;
        
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
    }
}
