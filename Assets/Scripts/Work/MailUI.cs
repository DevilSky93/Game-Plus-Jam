using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Work
{
    public class MailUI : MonoBehaviour
    {
        [SerializeField] private RandomizeMail mail;
        [SerializeField] private Transform send, transfer, delete, read;
        [SerializeField] private GameObject correct, fail;
        private MailManager _mm;
        private GameObject _stateMailOver; //Lorsque tous les mails sont finis, afficher correct au milieu
        private bool _isMoving;

        // Start is called before the first frame update
        void Start()
        {
            mail.Init();
            _mm = GetComponent<MailManager>();
            mail.gameObject.SetActive(false);
            _stateMailOver = Instantiate(correct, transform, false);
        }

        public void GetDirection(float dir)
        {
            if (!_isMoving && _mm.CounterStackMails > 0)
            {
                switch ((int)dir)
                {
                    case (int)RandomizeMail.TypeMail.Read:
                        MoveMail(mail.transform, read);
                        break;
                    case (int)RandomizeMail.TypeMail.Send:
                        MoveMail(mail.transform, send);
                        break;
                    case (int)RandomizeMail.TypeMail.Transfer:
                        MoveMail(mail.transform, transfer);
                        break;
                    case (int)RandomizeMail.TypeMail.Delete:
                        MoveMail(mail.transform, delete);
                        break;
                }
                var goodMail = _mm.CheckMail(mail, (RandomizeMail.TypeMail) dir);
                StartCoroutine(RightMailOrNot(goodMail, mail.transform));       
            }
        }

        private void MoveMail(Transform m, Transform goal)
        {
            m.transform.DOMove(goal.position, .2f).SetEase(Ease.OutSine).OnStart(() =>
            {
                _isMoving = true;

            });
        }

        private IEnumerator RightMailOrNot(bool isRight, Transform m)
        {
            GameObject inst;
            if (isRight)
            {
                inst = Instantiate(correct, m, false);
                _mm.MailPerSeriesCounter--;
            }
            else
            {
                inst = Instantiate(fail, m, false);
            }
            yield return new WaitForSeconds(.5f);
            Destroy(inst);
            if (_mm.MailPerSeriesCounter > 0)
            {
                // _mm.MailPerSeriesCounter--;
                ResetMailPos(mail); 
            }
            else
            {
                // _stateMailOver = Instantiate(correct, transform, false);
                _stateMailOver.SetActive(true);
                ResetMailPos(mail);
                mail.gameObject.SetActive(false);
            }
        }
        private void ResetMailPos(RandomizeMail m)
        {
            m.transform.localPosition = Vector3.zero;
            _isMoving = false;
            m.Init();
        }

        public void InitUI(float playerState)
        {
            if ((int)playerState == (int)PlayerControls.PlayerZone.Mail &&  !mail.gameObject.activeSelf)
            {
                if (_stateMailOver && _mm.CounterStackMails > 0)
                {
                    // Destroy(_stateMailOver);
                    _stateMailOver.SetActive(false);
                    mail.gameObject.SetActive(true);
                }
            }
        }
    }
}
