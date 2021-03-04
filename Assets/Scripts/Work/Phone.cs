using Events.Float;
using UnityEngine;

namespace Work
{
    public class Phone : MonoBehaviour
    {
        [SerializeField] private float valueGauge;

        private float _currentValueGauge;
        private PlayerControls _player;

        [SerializeField] private EventFloat modifyGaugeEvent;
        // Start is called before the first frame update
        void Start()
        {
            _currentValueGauge = valueGauge;
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
            if (_player.PlayerState == PlayerControls.PlayerZone.Phone)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    modifyGaugeEvent.Raise(_currentValueGauge);
                }   
            }
        }
    }
}
