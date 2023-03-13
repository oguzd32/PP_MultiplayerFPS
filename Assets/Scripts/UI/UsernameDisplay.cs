using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UsernameDisplay : MonoBehaviour
    {
        [SerializeField] private PhotonView playerPhotonView;
        [SerializeField] private TextMeshProUGUI userNameText;

        private void Start()
        {
            /*
            if (playerPhotonView.IsMine)
            {
                gameObject.SetActive(false);
            }
            userNameText.text = PhotonNetwork.NickName;
            */

            if (playerPhotonView.IsMine)
            {
                userNameText.text = PhotonNetwork.NickName;
            }
            else
            {
                userNameText.text = playerPhotonView.Owner.NickName;
            }
        }
    }
}
