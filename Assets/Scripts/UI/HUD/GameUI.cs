using Core.Utilities;
using Data.Item;
using Game;
using Photon.Pun;
using TMPro;
using UI.HUD;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI>
{
    [SerializeField] private OptionsMenu optionsMenu;
    [SerializeField] private Image bulletDisplayImage;
    [SerializeField] private TextMeshProUGUI bulletDisplayText;
    [SerializeField] private TextMeshProUGUI nextBulletDisplayText;
    [SerializeField] private Image nextBulletImage;
    [SerializeField] private TextMeshProUGUI timerText;

    public PhotonView photonView;

    public BulletSideBar bulletSideBar;
    
    public bool isPaused 
    {
        get
        {
            return pause;
        }
        set
        {
            if (value)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            pause = value;
        }
    }

    private bool pause = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            ShowOrHideOptions();
        }
        
    }

    private void ShowOrHideOptions()
    {
        if (optionsMenu.canvas != null)
        {
            if (optionsMenu.canvas.enabled)
            {
                isPaused = false;
                optionsMenu.Hide();
            }
            else
            {
                isPaused = true;
                optionsMenu.Show();
            }
        }
        else
        {
            if (optionsMenu.gameObject.activeInHierarchy)
            {
                isPaused = false;
                optionsMenu.Hide();
            }
            else
            {
                isPaused = true;
                optionsMenu.Show();
            }
        }
    }

    public void UpdateMyBulletDisplay(BulletItem bulletItem)
    {
        bulletDisplayText.text = bulletItem.size.ToString();
        bulletDisplayImage.color = bulletItem.color;
    }

    public void UpdateNextBulletDisplay(BulletItem bulletItem)
    {
        nextBulletDisplayText.text = $"{bulletItem.colorType.ToString()} - {bulletItem.size.ToString()} size.";
        nextBulletImage.color = bulletItem.color;
    }
}
