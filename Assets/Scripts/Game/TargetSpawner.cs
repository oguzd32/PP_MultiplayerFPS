using System;
using System.Collections.Generic;
using System.IO;
using Core.Utilities;
using Data.Item;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class TargetSpawner : Singleton<TargetSpawner>
    {
        public float gameModeTime = 30f;
        public BulletList bulletList;
        public Target targetPrefab;
        public int initialTargetCount = 30;
        public PhotonView _PhotonView;
        public BulletItem currentBullet;

        private TargetList _targetList;

        private List<Target> _targets;
        private List<Target> syncTargets;

        public float timer { get; private set; }
        private bool isStarted = false;

        private void Start()
        {
            _targets = new List<Target>();
            syncTargets = new List<Target>();
            
            if (PhotonNetwork.IsMasterClient)
            {
                isStarted = true;
                StartOrResetMode();
            }
        }

        private void Update()
        {
            if(!isStarted) return;

            timer -= Time.deltaTime;

            if (PhotonNetwork.IsMasterClient)
            {
                if (Input.GetKeyDown(KeyCode.H))
                {
                    StartOrResetMode();
                }
            }

            if(timer > 0) return;

            if (PhotonNetwork.IsMasterClient)
            {
                StartOrResetMode();
            }
        }

        private void StartOrResetMode()
        {
            timer = gameModeTime;
            _targetList = new TargetList();
            _targetList.positions = new List<Vector3>();
            _targetList.rotations = new List<Quaternion>();
            _targetList.bulletIndex = GetRandomBulletItem();
            currentBullet = bulletList.configuration[_targetList.bulletIndex];

            foreach (Target target in _targets)
            {
                if (target != null)
                {
                    Destroy(target.gameObject);
                }
            }    
            
            _targets.Clear();
            
            for (int i = 0; i < initialTargetCount; i++)
            {
                Target target = Instantiate(targetPrefab, GetRandomPoint(), GetRandomQuaternion());
                target.Initialize(currentBullet);
                
                _targets.Add(target);
                    
                _targetList.positions.Add(target.transform.position);
                _targetList.rotations.Add(target.transform.rotation);
            }
                
            GameUI.instance.UpdateNextBulletDisplay(currentBullet);
            string targetString = JsonUtility.ToJson(_targetList);
            _PhotonView.RPC(nameof(RPC_SyncTargets), RpcTarget.OthersBuffered, targetString);
        }

        [PunRPC]
        private void RPC_SyncTargets(string targets)
        {
            _targetList = JsonUtility.FromJson<TargetList>(targets);

            foreach (Target target in syncTargets)
            {
                if (target != null)
                {
                    PhotonNetwork.Destroy(target.gameObject);
                }
            }    
            
            syncTargets.Clear();
            
            for (int i = 0; i < _targetList.positions.Count; i++)
            {
                //Target target = Instantiate(targetPrefab);
                Target target = PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Target"),
                    _targetList.positions[i],
                    _targetList.rotations[i]).GetComponent<Target>();
                target._bulletItem = bulletList.configuration[_targetList.bulletIndex];
                currentBullet = bulletList.configuration[_targetList.bulletIndex];
                
                syncTargets.Add(target);
            }
            
            GameUI.instance.UpdateNextBulletDisplay(currentBullet);
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

        [PunRPC]
        private int GetRandomBulletItem()
        {
            int randomIndex = Random.Range(0, bulletList.configuration.Count);

            return randomIndex;
        }

        [PunRPC]
        public string TimeLeft()
        {
            return timer.ToString();
        }
    }

    public class TargetList
    {
        public int bulletIndex;
        public List<Vector3> positions;
        public List<Quaternion> rotations;
    }
}
