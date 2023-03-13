using Data.Item;
using UnityEngine;

namespace Game
{
    public class StunBomb : Item.Item
    {
        public override void Use(ItemInfo itemInfo)
        {
            Debug.Log("Using " + itemInfo.itemName);
        }
    }
}
