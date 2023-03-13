using Core.Utilities;
using Data.Item;
using Photon.Pun;
using UnityEngine;

namespace Game
{
    public class LevelManager : Singleton<LevelManager>
    {
        public float gameModeTime = 30f;
        public TargetSpawner targetSpawner;
        public BulletList bulletList;
        
        public BulletItem currentBullet { get; private set; }
        
        private float timer = 0f;
        private bool isStarted = false;

        private void Start()
        {
            isStarted = true;
            currentBullet = targetSpawner.Spawn();
            GameUI.instance.UpdateNextBulletDisplay(currentBullet);
        }
        
        private void ChangeBullet()
        {
            currentBullet = targetSpawner.Spawn();
            GameUI.instance.UpdateNextBulletDisplay(currentBullet);
        }

        public void StartorResetTimer()
        {
            timer = gameModeTime;
            ChangeBullet();
        }

        private void Update()
        {
            if(!isStarted) return;
            
            timer -= Time.deltaTime;
            
            if(timer > 0) return;
            StartorResetTimer();
        }
    }
}
