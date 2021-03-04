using Events.Float;
using UnityEngine;

namespace Work
{
    public class Printer : MonoBehaviour
    {
        [SerializeField] private float tamponSpeed;

        private PlayerControls _player;
        private bool _isShowing;

        [SerializeField] private EventFloat updateTamponEvent;
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
            if (_player.PlayerState == PlayerControls.PlayerZone.Printer)
            {
                updateTamponEvent.Raise(tamponSpeed);
            }
        }
        
        public void PrinterUIShowing(float activity)
        {
            if ((int)activity == (int)PlayerControls.PlayerZone.Printer)
            {
                _isShowing = !_isShowing;
            }
        } 
    }
}
