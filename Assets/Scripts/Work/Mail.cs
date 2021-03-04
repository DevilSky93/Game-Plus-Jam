using Events.Float;
using UnityEngine;

namespace Work
{
    public class Mail : MonoBehaviour
    {
        private PlayerControls _player;
        [SerializeField] private EventFloat directionEvent;
        private bool _isShowing;
        // Start is called before the first frame update
        void Start()
        {
            _player = GetComponent<PlayerControls>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!GameManager.instance)
            {
                return;
            }

            if (GameManager.instance.gameState == GameManager.GameState.Cutscene)
            {
                return;
            }
            if (_player.PlayerState == PlayerControls.PlayerZone.Mail && _isShowing)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    directionEvent.Raise((float)RandomizeMail.TypeMail.Delete);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    directionEvent.Raise((float)RandomizeMail.TypeMail.Transfer);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    directionEvent.Raise((float)RandomizeMail.TypeMail.Send);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    directionEvent.Raise((float)RandomizeMail.TypeMail.Read);
                }       
            }
        }

        public void MailUIShowing(float activity)
        {
            if ((int)activity == (int)PlayerControls.PlayerZone.Mail)
            {
                _isShowing = !_isShowing;
            }
        } 
    }
}
