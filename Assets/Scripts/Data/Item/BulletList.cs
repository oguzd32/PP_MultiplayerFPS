using System.Collections.Generic;
using UnityEngine;

namespace Data.Item
{
    [CreateAssetMenu(menuName = "FPS/BulletList")]
    public class BulletList : ScriptableObject
    {
        public List<BulletItem> configuration;
    }
}