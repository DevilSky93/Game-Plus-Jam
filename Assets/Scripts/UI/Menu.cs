using Events.NoType;
using UnityEngine;

namespace UI
{
    public class Menu : MonoBehaviour
    {
        private bool _isPause;
        [SerializeField] private EventNoType pauseEvent;

        public void PauseUnpause()
        {
            _isPause = !_isPause;
            if (_isPause)
            {
                pauseEvent.Raise();
                Time.timeScale = 0f;
            }
            else
            {
                if (!GameManager.instance) return;
                pauseEvent.Raise();
                Time.timeScale = 1f;
                GameManager.instance.menu.gameObject.SetActive(false);
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
