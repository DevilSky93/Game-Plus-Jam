using DG.Tweening;
using Events.Bool;
using Events.Float;
using Events.NoType;
using UnityEngine;

namespace Work
{
    public class TamponManager : MonoBehaviour
    {
        [SerializeField] private int tamponGoal;
        private int _tamponGoalCounter;
        [SerializeField] private EventNoType refreshGoalEvent;
        [SerializeField] private int tamponPerSeries;
        private int _tamponPerSeriesCounter;
        private int _counterStackPrint;

        [SerializeField] private float timeBetweenTampon;
        [SerializeField] private TextMesh counter;
        private int _counterSize;
        private float _timeBetweenTamponCounter;
        private float _currentTimeBetweenTampon;
        
        [SerializeField] private EventBool doingActivityEvent;
        [SerializeField] private EventFloat showUIEvent;
        private TamponUI _tUi;

        [SerializeField] private PlayerControls player;
        [SerializeField] private float affectGaugeValue;
        [SerializeField] private EventFloat affectGaugeEvent;

        [SerializeField] private EventFloat startAnimEvent;
        public int TamponGoal => tamponGoal;

        public int TamponGoalCounter => _tamponGoalCounter;
        public int TamponPerSeriesCounter
        {
            get => _tamponPerSeriesCounter;
            set => _tamponPerSeriesCounter = value;
        }
        // Start is called before the first frame update
        void Start()
        {
            counter.text = _counterStackPrint.ToString();
            _currentTimeBetweenTampon = Random.Range(timeBetweenTampon - (timeBetweenTampon / 3),
                timeBetweenTampon);
            _timeBetweenTamponCounter = timeBetweenTampon / 3;
            _tUi = GetComponent<TamponUI>();
            _counterSize = counter.fontSize;
        }

        // Update is called once per frame
        public void UpdateTm(bool isStarted)
        {
            if (isStarted)
            {
                if (player.PlayerState == PlayerControls.PlayerZone.Printer)
                {
                    if (Input.GetKeyDown(KeyCode.Backspace))
                    {
                        doingActivityEvent.Raise(false);
                        showUIEvent.Raise((int)PlayerControls.PlayerZone.Printer);
                        _tUi.ResetPos();
                    }
                }
                _timeBetweenTamponCounter += Time.deltaTime;
                if (_timeBetweenTamponCounter >= _currentTimeBetweenTampon)
                {
                    affectGaugeEvent.Raise(affectGaugeValue);
                    _counterStackPrint++;
                    _tamponPerSeriesCounter += tamponPerSeries;
                    _timeBetweenTamponCounter = 0;
                    _currentTimeBetweenTampon = Random.Range(timeBetweenTampon - (timeBetweenTampon / 2),
                        timeBetweenTampon);
                    counter.text = _counterStackPrint.ToString();
                    counter.fontSize = 0;
                    DOTween.To(() => counter.fontSize, x => counter.fontSize = x, _counterSize, .2f);
                    _tUi.InitUI((int)PlayerControls.PlayerZone.Printer);
                    startAnimEvent.Raise((float)PlayerControls.PlayerZone.Printer);
                }     
            }
        }

        public void DecreaseStackPrint()
        {
            if (_tamponPerSeriesCounter%tamponPerSeries == 0)
            {
                _counterStackPrint--;
                if (_tamponGoalCounter < tamponGoal)
                {
                    _tamponGoalCounter++;
                    refreshGoalEvent.Raise();
                }
                counter.text = _counterStackPrint.ToString();
                affectGaugeEvent.Raise(-affectGaugeValue);
            }
        }
    }
}
