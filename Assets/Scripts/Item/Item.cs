using Data.Item;
using UnityEngine;

namespace Item
{
    public abstract class Item : MonoBehaviour
    {
        public ItemInfo itemInfo;
        public GameObject itemGameObject;

        public abstract void Use(ItemInfo itemInfo);
    }
}
