using UnityEngine;

namespace Scripts.SceneObjects
{
    public class FinishLine: MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}