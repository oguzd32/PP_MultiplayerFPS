using System;
using Data.Item;
using Photon.Pun;
using UnityEngine;

namespace ActionGame
{
    public class Target : MonoBehaviourPunCallbacks, IDamageable
    {
        public Renderer m_Renderer;

        public event Action OnSpawn;
        public event Action OnDie;

        private PhotonView _photonView;

        private BulletItem _bulletItem;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public void Initialize(BulletItem bulletItem)
        {
            _bulletItem = bulletItem;
            m_Renderer.material.color = bulletItem.color;
        }

        public void TakeDamage(BulletItem bullet)
        {
            if (bullet.colorType == _bulletItem.colorType)
            {
                _photonView.RPC("RPC_TakeDamage", RpcTarget.All);
            }
            else
            {
                
            }
        }

        [PunRPC]
        void RPC_TakeDamage()
        {
            if(!_photonView.IsMine) return;

            Debug.Log("took damage");
            Die();
        }

        private void Die()
        {
            OnDie?.Invoke();
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
