using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Work
{
    public class WorkUI : MonoBehaviour
    {
        [SerializeField] private Transform mailUI, printerUI;

        [SerializeField] private MailManager mm;

        [SerializeField] private TamponManager tm;
        [SerializeField] private Text mailGoal, tamponGoal;
        private void Start()
        {
            RefreshGoal();
        }

        public void ShowWork(float work)
        {
            switch ((int) work)
            {
                case (int) PlayerControls.PlayerZone.Mail:
                    if (mailUI.transform.position.x > Screen.width)
                    {
                        mailUI.DOLocalMove(Vector3.zero, .2f).SetEase(Ease.InQuint);
                    }

                    break;
                case (int) PlayerControls.PlayerZone.Printer:
                    if (printerUI.transform.position.y > Screen.height)
                    {
                        printerUI.DOLocalMove(new Vector3(0f, -100f), .2f).SetEase(Ease.InQuint);
                    }

                    break;
            }
        }

        public void ShowUI(bool isShowing)
        {
            if (!isShowing)
            {
                if (mailUI.transform.position.x < Screen.width)
                {
                    Transform transform1;
                    mailUI.DOLocalMove(
                        new Vector3((transform1 = mailUI.transform).position.x + Screen.width, transform1.localPosition.y,
                            transform1.position.z), .2f).SetEase(Ease.InQuint);
                }

                if (printerUI.position.y < Screen.height)
                {
                    var position = printerUI.transform.position;
                    printerUI.DOLocalMove(
                            new Vector3(printerUI.localPosition.x, position.y + Screen.height, position.z), .2f)
                        .SetEase(Ease.InQuint);
                }
            }
        }

        public void RefreshGoal()
        {
            mailGoal.text = mm.MailGoalCounter + "/" + mm.MailGoal;
            tamponGoal.text = tm.TamponGoalCounter + "/" + tm.TamponGoal;
        }
    }
}