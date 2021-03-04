using Events.Bool;
using UnityEngine;

namespace Final_act
{
    public class PlayerControlAlternate : MonoBehaviour
    {
        [SerializeField] private KeyCode firstClick, secondClick;
        [SerializeField] private int numberOfClick;
        [SerializeField] private EventBool loseEvent;
        [SerializeField] private float distanceGain;
        [SerializeField] private Transform startPoint;
        [SerializeField] private GameObject balloon;
        [SerializeField] private Transform childHand;
        private bool _isOver;
        private KeyCode _currentClick;

        private int _currentVal;
        
        // Start is called before the first frame update
        void Start()
        {
            _currentClick = firstClick;
        }

        public void InitPos()
        {
            transform.position = startPoint.position;
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

            if (_isOver)
            {
                return;
            }
            if (Input.GetKeyDown(firstClick))
            {
                if (_currentClick == firstClick)
                {
                    _currentClick = secondClick;
                    _currentVal++;
                    transform.position += new Vector3(0f, distanceGain);
                    if (WinLose(_currentVal, numberOfClick))
                    {
                        balloon.transform.SetParent(transform);
                        balloon.transform.position = Vector3.zero;
                        loseEvent.Raise(false);
                        _isOver = true;
                        GetComponent<Animator>().enabled = true;
                    }
                }
            }
            if (Input.GetKeyDown(secondClick))
            {
                if (_currentClick == secondClick)
                {
                    _currentClick = firstClick;
                    _currentVal++;
                    transform.position += new Vector3(0f, distanceGain);

                    if (WinLose(_currentVal, numberOfClick))
                    {
                        balloon.transform.SetParent(transform);
                        balloon.transform.position = Vector3.zero;
                        loseEvent.Raise(false);
                        _isOver = true;
                        GetComponent<Animator>().enabled = true;
                    }
                }
            }
        }

        public void GiveBalloon()
        {
            balloon.transform.SetParent(childHand);
            balloon.transform.localPosition = new Vector3(0f, 0.27f, 0f);
        }

        private bool WinLose(int cur, int max)
        {
            if (cur >= max)
            {
                return true;
            }

            return false;
        }
    }
}
