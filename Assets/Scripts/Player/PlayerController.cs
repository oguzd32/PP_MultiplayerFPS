using System;
using Data.Item;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        [Header("Stats")]
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float lookSensitivity = 3f;
        [SerializeField] private float jumpForce;
        
        [Space] 
        [SerializeField] private Item.Item gun;

        [SerializeField] private Item.Item bomb;

        public BulletList BulletList;
        public BulletItem currentBulletItem;
        
        // cached components
        private PlayerMovement _movement;
        private PhotonView _photonView;
        private GameUI _gameUI;

        // private variables
        private bool isGrounded;

        private int bulletTypeIndex;
        private int previousBulletTypeIndex = -1;

        private PlayerManager _playerManager;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _photonView = GetComponent<PhotonView>();

            _playerManager = PhotonView.Find((int)_photonView.InstantiationData[0]).GetComponent<PlayerManager>();
        }

        private void Start()
        {
            isGrounded = true;
            Cursor.lockState = CursorLockMode.Locked;

            if (photonView.IsMine)
            {
                ChangeBullet(0);

                _gameUI = GameUI.instance;
                _gameUI.bulletSideBar.OnBulletChanged += ChangeBullet;
            }
            else
            {
                Destroy(GetComponentInChildren<Camera>().gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_photonView.IsMine)
            {
                _gameUI.bulletSideBar.OnBulletChanged -= ChangeBullet;
            }
        }

        private void Update()
        {
            if(!_photonView.IsMine) return;
            if(GameUI.instance.isPaused) return;

            MoveCharacter();
            JumpCharacter();
            RotateCharacter();
            RotateCamera();
            MouseScroll();
            HotKey();

            if (Input.GetMouseButtonDown(0))
            {
                gun.Use(currentBulletItem);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                //bomb.Use();
            }
        }
        private void HotKey()
        {
            for (int i = 0; i < BulletList.configuration.Count; i++)
            {
                if (Input.GetKeyDown(BulletList.configuration[i].hotKey))
                {
                    ChangeBullet(i);
                }
            }
        }

        private void MouseScroll()
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                if (bulletTypeIndex >= BulletList.configuration.Count - 1)
                {
                    ChangeBullet(0);
                }
                else
                {
                    ChangeBullet(bulletTypeIndex + 1);
                }
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                if (bulletTypeIndex <= 0)
                {
                    ChangeBullet(BulletList.configuration.Count - 1);
                }
                else
                {
                    ChangeBullet(bulletTypeIndex - 1);
                }
            }
        }

        #region Movement

        /// <summary>
        /// Calculate camera rotation as a 3D vector (turning around Y axis)
        /// </summary>
        private void RotateCamera()
        {
            float _xRot = Input.GetAxisRaw("Mouse Y");

            Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

            // Apply rotation
            _movement.CameraRotation = _cameraRotation;
        }

        /// <summary>
        /// Calculate rotation as a 3D vector (turning around X axis)
        /// </summary>
        private void RotateCharacter()
        {
            float _yRot = Input.GetAxisRaw("Mouse X");

            Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

            // Apply rotation
            _movement.Rotation = _rotation;
        }

        /// <summary>
        /// Calculate movement velocity as a 3D vector
        /// </summary>
        private void MoveCharacter()
        {
            // inputs
            float _xMov = Input.GetAxisRaw("Horizontal");
            float _zMov = Input.GetAxisRaw("Vertical");
            bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
            
            // switch speed
            float speed = shiftPressed && isGrounded ? sprintSpeed : walkSpeed;

            Vector3 _MovHorizontal = transform.right * _xMov;
            Vector3 _MovVertical = transform.forward * _zMov;

            // Final movement vector
            Vector3 _velocity = (_MovHorizontal + _MovVertical).normalized * speed;

            // Apply movement
            _movement.Velocity = _velocity;
        }

        private void JumpCharacter()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                _movement.JumpForce = jumpForce;
            }
        }

        #endregion

        public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps)
        {
            if (_photonView == null)
            {
                _photonView = GetComponent<PhotonView>();
            }
            
            if (!_photonView.IsMine && targetPlayer == _photonView.Owner)
            {
                ChangeBullet((int)changedProps["BulletIndex"]);
            }
        }

        private void ChangeBullet(int index)
        {
            if(index == previousBulletTypeIndex) return;

            bulletTypeIndex = index;

            currentBulletItem = BulletList.configuration[bulletTypeIndex];

            previousBulletTypeIndex = bulletTypeIndex;
            
            if (_photonView.IsMine)
            {
                Hashtable hash = new Hashtable();
                hash.Add("BulletIndex", bulletTypeIndex);
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
                GameUI.instance.UpdateMyBulletDisplay(currentBulletItem);
            }
        }

        public void SetGroundedState(bool grounded)
        {
            isGrounded = grounded;
        }
    }
}