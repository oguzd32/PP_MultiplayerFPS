using System;
using Data.Item;
using Photon.Pun;
using Player;
using UnityEngine;

namespace Game
{
    public class Target : MonoBehaviour, IDamageable
    {
        public Renderer m_Renderer;

        public event Action OnSpawn;
        public event Action OnDie;

        public PhotonView _photonView;


        public BulletItem _bulletItem;

        public void Initialize(BulletItem bulletItem)
        {
            _bulletItem = bulletItem;
        }

        public void TakeDamage(BulletItem bullet)
        {
            if (bullet.colorType == _bulletItem.colorType
                && bullet.size == _bulletItem.size)
            {
                _photonView.RPC(nameof(RPC_AddPoint), _photonView.Owner);
            }
            else
            {
                _photonView.RPC(nameof(RPC_RemovePoint), _photonView.Owner);
            }
        }

        [PunRPC]
        void RPC_AddPoint(PhotonMessageInfo info)
        {
            PlayerManager.Find(info.Sender).IncreasePoint();
            Die();
        }

        [PunRPC]
        void RPC_RemovePoint(PhotonMessageInfo info)
        {
            PlayerManager.Find(info.Sender).DecreasePoint();
            Die();
        }

        private void Die()
        {
            OnDie?.Invoke();
            if (_photonView.IsMine)
            {
                Destroy(gameObject);
            }
        }
    }
}
