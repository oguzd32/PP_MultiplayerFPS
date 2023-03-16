using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("userName"))
        {
            usernameInputText.text = PlayerPrefs.GetString("userName");
            PhotonNetwork.NickName = PlayerPrefs.GetString("userName");
        }
        else
        {
            usernameInputText.text = "Player " + Random.Range(0, 10000).ToString("0000");
            OnInputFieldValueChanged();
        }
    }

    public void OnInputFieldValueChanged()
    {
        PhotonNetwork.NickName = usernameInputText.text;
        PlayerPrefs.SetString("userName", usernameInputText.text);
    }
}
