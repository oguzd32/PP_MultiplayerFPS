using System;
using Data.Item;
using Photon.Pun;
using UnityEngine;

namespace Game
{
    public class Target : MonoBehaviourPunCallbacks, IDamageable
    {
        public Renderer m_Renderer;

        public event Action OnSpawn;
        public event Action OnDie;

        private PhotonView _photonView;

        public BulletItem _bulletItem;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public void Initialize(BulletItem bulletItem)
        {
            _bulletItem = bulletItem;
            //_photonView.RPC("RPC_Initialize", RpcTarget.All);
            //m_Renderer.material.color = bulletItem.color;
        }

        [PunRPC]
        private void RPC_Initialize()
        {
            
        }

        public void TakeDamage(BulletItem bullet)
        {
            if (bullet.colorType == _bulletItem.colorType
                && bullet.size == _bulletItem.size)
            {
                _photonView.RPC("RPC_AddPoint", RpcTarget.All);
            }
            else
            {
                _photonView.RPC("RPC_RemovePoint", RpcTarget.All);
            }
        }

        [PunRPC]
        void RPC_AddPoint()
        {
            Debug.Log("Add point");
            Die();
        }

        [PunRPC]
        void RPC_RemovePoint()
        {
            Debug.Log("Remove point");
        }

        private void Die()
        {
            OnDie?.Invoke();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
