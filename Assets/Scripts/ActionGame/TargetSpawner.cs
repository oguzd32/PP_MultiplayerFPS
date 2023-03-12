using System.Collections.Generic;
using System.IO;
using Core.Utilities;
using Data.Item;
using Photon.Pun;
using UnityEngine;

namespace ActionGame
{
    public class TargetSpawner : NetworkSingleton<TargetSpawner>
    {
        public List<Target> targets;
        public int initialTargetCount = 30;

        public BulletList bulletList;

        private void Start()
        {
            for (int i = 0; i < initialTargetCount; i++)
            {
                Target target = PhotonNetwork
                    .Instantiate(Path.Combine("Photon Prefabs", "Target"), GetRandomPoint(), GetRandomQuaternion())
                    .GetComponent<Target>();
                
                target.Initialize(GetRandomBulletItem());
            
                targets.Add(target);
            
                target.OnDie += TargetOnDie;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (Target target in targets)
            {
                target.OnDie -= TargetOnDie;
            }
        }

        private void TargetOnDie()
        {
            
        }

        private Vector3 GetRandomPoint()
        {
            float x = Random.Range(-40f, 40f);
            float y = 6f;
            float z = Random.Range(-40f, 40f);

            Vector3 randomPoint = new Vector3(x, y, z);

            return randomPoint;
        }

        private Quaternion GetRandomQuaternion()
        {
            Quaternion randomQuaternion;

            float y = Random.Range(0f, 360f);
            
            randomQuaternion = Quaternion.Euler(Vector3.up * y);

            return randomQuaternion;
        }

        private BulletItem GetRandomBulletItem()
        {
            BulletItem bulletItem;

            int randomIndex = Random.Range(0, bulletList.configuration.Count);
            bulletItem = bulletList.configuration[randomIndex];
            return bulletItem;
        }
    }
}
