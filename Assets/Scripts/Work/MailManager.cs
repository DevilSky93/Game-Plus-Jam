using System;
using DG.Tweening;
using Events.Bool;
using Events.Float;
using Events.NoType;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Work
{
    public class MailManager : MonoBehaviour
    {
        [SerializeField] private int mailGoal;
        [SerializeField] private EventNoType refreshGoalEvent;
        [SerializeField] private int mailPerSeries;
        [SerializeField] private float timeBetweenMails;
        [SerializeField] private EventBool doingActivityEvent;
        [SerializeField] private EventFloat showUIEvent;
        [SerializeField] private TextMesh counter;
        [SerializeField] private PlayerControls player;
        [SerializeField] private float affectGaugeValue;
        [SerializeField] private EventFloat affectGaugeEvent, startAnimEvent;

        private int _mailPerSeriesCounter;
        private float _timeBetweenMailCounter;
        private int _counterStackMails;
        private int _counterSize;
        private float _currentTimeBetweenMail;
        private int _mailGoalCounter;
        private MailUI _mUi;
        public int MailGoal => mailGoal;

        public int MailGoalCounter => _mailGoalCounter;
        public int MailPerSeriesCounter
        {
            get => _mailPerSeriesCounter;
            set => _mailPerSeriesCounter = value;
        }

        public int CounterStackMails => _counterStackMails;

        private void Start()
        {
            counter.text = _counterStackMails.ToString();
            _currentTimeBetweenMail = Random.Range(timeBetweenMails - (timeBetweenMails / 3),
                timeBetweenMails + (timeBetweenMails / 3));
            _timeBetweenMailCounter = timeBetweenMails / 3;
            _mUi = GetComponent<MailUI>();
            _counterSize = counter.fontSize;
        }

        public void UpdateMm(bool isStarted)
        {
            if (isStarted)
            {
                if (player.PlayerState == PlayerControls.PlayerZone.Mail)
                {
                    if (Input.GetKeyDown(KeyCode.Backspace))
                    {
                        doingActivityEvent.Raise(false);
                        // startWorkingEvent.Raise((int)PlayerControls.PlayerZone.Mail);
                        showUIEvent.Raise((int)PlayerControls.PlayerZone.Mail);
                    }
                }
                _timeBetweenMailCounter += Time.deltaTime;
                if (_timeBetweenMailCounter >= _currentTimeBetweenMail)
                {
                    affectGaugeEvent.Raise(affectGaugeValue);
                    _counterStackMails++;
                    _mailPerSeriesCounter += mailPerSeries;
                    _timeBetweenMailCounter = 0;
                    _currentTimeBetweenMail = Random.Range(timeBetweenMails - (timeBetweenMails / 2),
                        timeBetweenMails + (timeBetweenMails / 2));
                    counter.text = _counterStackMails.ToString();
                    counter.fontSize = 0;
                    DOTween.To(() => counter.fontSize, x => counter.fontSize = x, _counterSize, .2f);
                    _mUi.InitUI((int)PlayerControls.PlayerZone.Mail);
                    startAnimEvent.Raise((float)PlayerControls.PlayerZone.Mail);
                } 
            }
        }

        public bool CheckMail(RandomizeMail mail, RandomizeMail.TypeMail dir)
        {
            if ((int)mail.TypeMail1 == (int)dir)
            {
                if ((_mailPerSeriesCounter-1)%mailPerSeries == 0)
                {
                    _counterStackMails--;
                    if (_mailGoalCounter < mailGoal)
                    {
                        _mailGoalCounter++;
                        refreshGoalEvent.Raise();
                    }
                }
                counter.text = _counterStackMails.ToString();
                affectGaugeEvent.Raise(-affectGaugeValue);
                return true;
            }
            return false;
        }
        
    }
}
