using System.Collections;
using DG.Tweening;
using Events.Bool;
using UnityEngine;
using UnityEngine.UI;

namespace Work
{
    public class WorkUIManager : MonoBehaviour
    {
        [SerializeField] private GaugeController gc;

        [SerializeField] private MailManager mm;

        [SerializeField] private TamponManager tm;

        [SerializeField] private EventBool loseEvent, startTimerEvent;

        private bool _isGameOver;

        [SerializeField] private EventBool updateBoolEvent;

        [SerializeField] private Text winText, loseText;
        private bool _hasStarted;

        public void InitWork()
        {
            startTimerEvent.Raise(true);
            _hasStarted = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isGameOver && _hasStarted)
            {
                updateBoolEvent.Raise(true);
                if (gc.CurrentValue >= gc.MAXValue)
                {
                    if (tm.TamponGoalCounter >= tm.TamponGoal && mm.MailGoalCounter >= mm.MailGoal)
                    {
                        loseEvent.Raise(false);
                        _isGameOver = true;
                        gc.DecreaseWithTime = false;
                    }
                }   
            }
            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.N))
            {
                loseEvent.Raise(false);
                _isGameOver = true;
                gc.DecreaseWithTime = false;
            }
            #endif
        }

        public void WinLose(bool isLose)
        {
            if (isLose)
            {
                loseText.gameObject.SetActive(true);
                loseText.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    StartCoroutine(HideWinLose(true));
                });
            }
            else
            {
                winText.gameObject.SetActive(true);
                winText.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    StartCoroutine(HideWinLose(false));
                });;
            }
        }

        private IEnumerator HideWinLose(bool isLose)
        {
            yield return new WaitForSeconds(1f);
            if (isLose)
            {
                loseText.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutSine);
            }
            else
            {
                winText.transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutSine);
            }
        }
    }
}
