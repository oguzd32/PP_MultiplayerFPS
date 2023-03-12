using Data.Item;
using UnityEngine;
using UnityEngine.UIElements;

namespace ActionGame.Bullet
{
    public class Gun : Item.Item
    {
        [SerializeField] private Camera cam;
        
        public override void Use(ItemInfo itemInfo)
        {
            Debug.Log("Using " + itemInfo.itemName);
            Shoot(((BulletItem)itemInfo));
        }
        
        void Shoot(BulletItem bullet)
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f));
            ray.origin = cam.transform.position;
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("We hit " + hit.collider.gameObject.name);
                if (hit.transform.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(bullet);
                }
            }
        }
    }
}
