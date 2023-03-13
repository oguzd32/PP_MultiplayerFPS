using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform camHolder;
        [SerializeField] private Vector2 clampCam;

        public float JumpForce { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 CameraRotation { get; set; }
        
        // cached components
        private Rigidbody m_Rigidbody;

        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        // Run every physics iteration
        private void FixedUpdate()
        {
            if (GameUI.instance.isPaused)
            {
                JumpForce = 0;
                Velocity = Rotation = CameraRotation = Vector3.zero;
                return;
            }
            
            PerformMovement();
            PerformRotation();
            PerformJump();
        }
        
        // Perform movement based on velocity variable
        private void PerformMovement()
        {
            if(Velocity == Vector3.zero) return;
            
            m_Rigidbody.MovePosition(m_Rigidbody.position + Velocity * Time.fixedDeltaTime);
        }

        private void PerformJump()
        {
            if(JumpForce == 0) return;
            
            m_Rigidbody.AddForce(m_Rigidbody.transform.up * JumpForce);
            JumpForce = 0;
        }

        // Perform rotation
        private void PerformRotation()
        {
            // rotate Y axis
            m_Rigidbody.MoveRotation(m_Rigidbody.rotation * Quaternion.Euler(Rotation));

            // rotate X axis
            if (camHolder != null)
            {
                camHolder.transform.Rotate(CameraRotation);
                Vector3 angle = camHolder.transform.localEulerAngles;
                angle.x = ClampAngle(angle.x, clampCam.x, clampCam.y);
                camHolder.transform.localEulerAngles = angle;
            }
        }
        
        private float ClampAngle(float angle, float min, float max)
        {
            angle = Mathf.Repeat(angle, 360);
            min = Mathf.Repeat(min, 360);
            max = Mathf.Repeat(max, 360);
            bool inverse = false;
            var tmin = min;
            var tangle = angle;
            if(min > 180)
            {
                inverse = !inverse;
                tmin -= 180;
            }
            if(angle > 180)
            {
                inverse = !inverse;
                tangle -= 180;
            }
            var result = !inverse ? tangle > tmin : tangle < tmin;
            if(!result)
                angle = min;
         
            inverse = false;
            tangle = angle;
            var tmax = max;
            if(angle > 180)
            {
                inverse = !inverse;
                tangle -= 180;
            }
            if(max > 180)
            {
                inverse = !inverse;
                tmax -= 180;
            }
         
            result = !inverse ? tangle < tmax : tangle > tmax;
            if(!result)
                angle = max;
            return angle;
        }
    }
}