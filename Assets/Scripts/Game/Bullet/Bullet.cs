using BulletEnums;
using Photon.Pun;
using UnityEngine;

namespace Bullet
{
    public class Bullet : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Renderer _bulletRenderer;
        [SerializeField] private float[] _bulletSizes;
        
        public BulletEnums.BulletSize currentBulletSize = BulletSize.Standard;
        public BulletEnums.BulletColor currentBulletColor = BulletColor.Red;

        public void ChangeBulletSize(BulletSize newBulletSize)
        {
            if (currentBulletSize == newBulletSize) return;

            currentBulletSize = newBulletSize;
            ChangeBulletSize();
        }

        public void ChangeBulletColor(BulletColor newBulletColor)
        {
            if(currentBulletColor == newBulletColor) return;
            ChangeBulletColor();
        }

        private void ChangeBulletSize()
        {
            if(_bulletRenderer == null) return;
            
            switch (currentBulletSize)
            {
                case BulletSize.Small:
                    _bulletRenderer.transform.localScale = Vector3.one * _bulletSizes[0];
                    break;
                
                case BulletSize.Standard:
                    _bulletRenderer.transform.localScale = Vector3.one * _bulletSizes[0];
                    break;
                
                case BulletSize.Large:
                    _bulletRenderer.transform.localScale = Vector3.one * _bulletSizes[0];
                    break;
            }
        }

        private void ChangeBulletColor()
        {
            if(_bulletRenderer == null) return;
            
            switch (currentBulletColor)
            {
                case BulletColor.Blue:

                    _bulletRenderer.material.color = Color.blue;
                    break;
                    
                case BulletColor.Green:
                    
                    _bulletRenderer.material.color = Color.green;
                    break;
                
                case BulletColor.Red:
                    
                    _bulletRenderer.material.color = Color.red;
                    break;
            }
        }
    }
}