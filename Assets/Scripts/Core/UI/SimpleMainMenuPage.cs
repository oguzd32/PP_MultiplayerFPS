using UnityEngine;

namespace Core.UI
{
    public class SimpleMainMenuPage : MonoBehaviour, IMainMenuPage
    {
        /// <summary>
        /// Canvas to disable. If this object is set, then the canvas is disabled instead of the game object
        /// </summary>
        public Canvas canvas;
        
        public virtual void Hide()
        {
            if (canvas != null)
            {
                canvas.enabled = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public virtual void Show()
        {
            if (canvas != null)
            {
                canvas.enabled = true;
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
