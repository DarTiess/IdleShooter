using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class WindowBase: MonoBehaviour
    {
        [SerializeField] private Button _button;
        public event Action ClickedPanel;

        private void Start()
        {
            _button.onClick.AddListener(OnClickedPanel);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private  void OnClickedPanel()
        {
            ClickedPanel?.Invoke();
        }
    }
}