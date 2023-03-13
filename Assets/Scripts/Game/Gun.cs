using System;
using Data.Item;
using Photon.Pun;
using UnityEngine;

namespace Game
{
    public class Gun : Item.Item
    {
        [SerializeField] private Camera cam;

        public GameObject bulletImpactPrefab;

        private PhotonView _photonView;

        private void Awake()
        {
            _photonView = GetComponent<PhotonView>();
        }

        public override void Use(ItemInfo itemInfo)
        {
            Shoot(((BulletItem)itemInfo));
        }
        
        void Shoot(BulletItem bullet)
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f));
            ray.origin = cam.transform.position;
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _photonView.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
                
                if (hit.transform.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(bullet);
                }
            }
        }

        [PunRPC]
        private void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
        {
            Collider[] colliders = Physics.OverlapSphere(hitPosition, .3f);
            if (colliders.Length != 0)
            {
                GameObject bulletInstance =Instantiate(bulletImpactPrefab,
                                                hitPosition + hitNormal * 0.001f,
                                                Quaternion.LookRotation(hitPosition, Vector3.up) * bulletImpactPrefab.transform.rotation);
                
                bulletInstance.transform.SetParent(colliders[0].transform);
            }
            
        }
    }
}
