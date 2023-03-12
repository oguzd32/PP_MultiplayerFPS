using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerGroundCheck : MonoBehaviour
    {
        public PlayerController playerController;

        private void Update()
        {
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            
            RaycastHit hit;

            if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask)) return;
            
            float distance = transform.position.y - hit.transform.position.y;

            if (distance < 0.5f)
            {
                playerController.SetGroundedState(true);
            }
            else
            {
                playerController.SetGroundedState(false);
            }
        }
    }
}