using DG.Tweening;
using Events.Float;
using UnityEngine;

namespace Work
{
    public class PlayerControls : MonoBehaviour
    {
        [SerializeField] private Transform phone, printer, mail;
        private bool _isMoving;
        public enum PlayerZone
        {
            Phone = 0,
            Printer = 1,
            Mail = 2
        }

        private bool _doingActivity;
        private Animator _anim;
        [SerializeField] private EventFloat startWorkingEvent;
        private PlayerZone _playerState;
        private Vector3 _originalScale;

        public PlayerZone PlayerState => _playerState;
        // Start is called before the first frame update
        void Start()
        {
            _playerState = PlayerZone.Phone;
            _anim = GetComponent<Animator>();
            _originalScale = transform.localScale;
        }

        public void InitPos()
        {
            transform.position = phone.position;
            var localEulerAngles = transform.localEulerAngles;
            localEulerAngles = new Vector3(localEulerAngles.x, -180f,
                localEulerAngles.z);
            transform.localEulerAngles = localEulerAngles;
            _anim.SetTrigger("Phone");
        }

        // Update is called once per frame
        void Update()
        {
            if (!_doingActivity)
            {
                if (Input.GetKeyDown(KeyCode.A) && _playerState != PlayerZone.Phone && !_isMoving)
                {
                    ChangeScale(_originalScale, .2f);
                    transform.DOMove(phone.position, .2f).OnStart(() =>
                    {
                        CheckAnimState(PlayerZone.Phone);
                        var localEulerAngles = transform.localEulerAngles;
                        localEulerAngles = new Vector3(localEulerAngles.x, -180f,
                            localEulerAngles.z);
                        transform.localEulerAngles = localEulerAngles;
                        _isMoving = true;
                        _anim.SetBool("isWalking", _isMoving);
                    }).OnComplete(() =>
                    {
                        _isMoving = false;
                        _anim.SetBool("isWalking", _isMoving);
                        _anim.SetTrigger("Phone");
                        _playerState = PlayerZone.Phone;
                        startWorkingEvent.Raise((int)_playerState);
                    });
                } else if (Input.GetKeyDown(KeyCode.W) && _playerState != PlayerZone.Printer && !_isMoving)
                {
                    var newScale = new Vector2(2.7f, 2.7f);
                    ChangeScale(newScale, .2f);
                    transform.DOMove(printer.position, .2f).OnStart(() =>
                    {
                        CheckAnimState(PlayerZone.Printer);
                        var localEulerAngles = transform.localEulerAngles;
                        localEulerAngles = new Vector3(localEulerAngles.x, 0f,
                            localEulerAngles.z);
                        transform.localEulerAngles = localEulerAngles;
                        _isMoving = true;
                        _anim.SetBool("isWalking", _isMoving);
                    }).OnComplete(() =>
                    {
                        _isMoving = false;
                        _anim.SetBool("isWalking", _isMoving);
                        _anim.SetTrigger("Printer");
                        _playerState = PlayerZone.Printer;
                        _doingActivity = true;
                        startWorkingEvent.Raise((int)_playerState);
                    });;
                } else if (Input.GetKeyDown(KeyCode.D) && _playerState != PlayerZone.Mail && !_isMoving)
                {
                    var newScale = new Vector2(3.8f, 3.8f);
                    ChangeScale(newScale, .2f);
                    transform.DOMove(mail.position, .2f).OnStart(() =>
                    {
                        CheckAnimState(PlayerZone.Mail);
                        var localEulerAngles = transform.localEulerAngles;
                        localEulerAngles = new Vector3(localEulerAngles.x, 0f,
                            localEulerAngles.z);
                        transform.localEulerAngles = localEulerAngles;
                        _isMoving = true;
                        _anim.SetBool("isWalking", _isMoving);
                    }).OnComplete(() =>
                    {
                        _isMoving = false;
                        _anim.SetBool("isWalking", _isMoving);
                        _anim.SetTrigger("Mail");
                        _playerState = PlayerZone.Mail;
                        _doingActivity = true;
                        startWorkingEvent.Raise((int)_playerState);
                    });;
                }       
            }
        }

        private void CheckAnimState(PlayerZone state)
        {
            if (_playerState != state)
            {
                if (_playerState == PlayerZone.Mail)
                {
                    _anim.SetTrigger("Mail");
                } else if(_playerState == PlayerZone.Printer)
                {
                    _anim.SetTrigger("Printer");
                }
            }
            
        }

        public void ChangeActivityState(bool isDoing)
        {
            _doingActivity = isDoing;
        }

        private void ChangeScale(Vector2 scale, float duration)
        {
            transform.DOScale(scale, duration);
        }
    }
}
