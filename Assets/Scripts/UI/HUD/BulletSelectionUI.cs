using System;
using Data.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class BulletSelectionUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI shortCutText;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI sizeDisplayText;

        public event Action<BulletItem> OnButtonTapped; 

        private BulletItem _bulletItem;

        public void Initialize(BulletItem item)
        {
            shortCutText.text = item.displayHotKey;
            sizeDisplayText.text = item.size.ToString();
            _image.color = item.color;
            _bulletItem = item;
        }

        public void OnClickButton()
        {
            OnButtonTapped?.Invoke(_bulletItem);
        }
    }
}