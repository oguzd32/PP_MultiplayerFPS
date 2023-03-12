using System;
using System.Collections.Generic;
using Data.Item;
using UnityEngine;

namespace UI.HUD
{
    public class BulletSideBar : MonoBehaviour
    {
        [SerializeField] private BulletList _bulletList;
        [SerializeField] private BulletSelectionUI _bulletSelectionUIPrefab;

        private List<BulletSelectionUI> bulletButtons;

        public event Action<int> OnBulletChanged;
        
        private void Start()
        {
            bulletButtons = new List<BulletSelectionUI>();
            
            for (int i = 0; i < _bulletList.configuration.Count; i++)
            {
                BulletSelectionUI button = Instantiate(_bulletSelectionUIPrefab, transform);
                button.Initialize(_bulletList.configuration[i]);
                button.OnButtonTapped += OnButtonTapped;
                
                bulletButtons.Add(button);
            }
        }

        private void OnDestroy()
        {
            foreach (BulletSelectionUI button in bulletButtons)
            {
                button.OnButtonTapped -= OnButtonTapped;
            }
        }

        private void OnButtonTapped(BulletItem item)
        {
            int index = _bulletList.configuration.IndexOf(item);
            OnBulletChanged?.Invoke(index);
        }
    }
}