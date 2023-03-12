using BulletEnums;
using UnityEngine;

namespace Data.Item
{
    [CreateAssetMenu(menuName = "FPS/New Bullet")]
    public class BulletItem : ItemInfo
    {
        public string displayHotKey;
        public KeyCode hotKey;
        public Color color = Color.blue;
        public BulletEnums.BulletColor colorType = BulletColor.Blue;
        public BulletEnums.BulletSize size = BulletSize.Small;
    }
}