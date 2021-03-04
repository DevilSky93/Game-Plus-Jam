using Events.Bool;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float timeLength;

        private float _currentTime;
        private bool _isStarted;
        [SerializeField] private Text timeText;

        [SerializeField] private EventBool loseEvent;
        // Start is called before the first frame update
        void Start()
        {
            _currentTime = timeLength;
            timeText.text = (int)_currentTime + " s";
        }

        // Update is called once per frame
        void Update()
        {
            if (_isStarted)
            {
                if (_currentTime > 0)
                {
                    _currentTime -= Time.deltaTime;
                    timeText.text = (int)_currentTime+ " s";
                }
                else
                {
                    loseEvent.Raise(true);
                    _isStarted = false;
                }
            }
        }

        public void StartTimer(bool isStarted)
        {
            _isStarted = isStarted;
            if (!isStarted)
            {
                _currentTime = timeLength;
            }
        }

        public void ResetTimer()
        {
            timeText.text = (int)_currentTime+ " s";
        }

        public void StopTimer()
        {
            _isStarted = false;
        }
    }
}
