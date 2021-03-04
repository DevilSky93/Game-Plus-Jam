using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Work
{
    public class TamponUI : MonoBehaviour
    {
        [SerializeField] private GameObject tampon;
        private Vector3 _originalPos;
        [SerializeField] private GameObject tamponLogo;
        private RectTransform _paper;
        private GameObject _statePrintOver;
        [SerializeField] private float distanceTampon;
        [SerializeField] private GameObject correct;
        private bool _stamping;

        [SerializeField] private Vector2 tamponOffset;

        private Vector2 _realPositionTampon;
        private TamponManager _tm;

        private bool _hasClicked;

        // Start is called before the first frame update
        void Start()
        {
            _paper = GetComponent<RectTransform>();
            _tm = GetComponent<TamponManager>();
            _statePrintOver = Instantiate(correct, tampon.transform, false);
            tamponLogo.SetActive(false);
            _originalPos = tampon.transform.localPosition;
        }

        // Update is called once per frame
        public void UpdateTampon(float tamponSpeed)
        {
            if (!_stamping)
            {
                var localPosition = tampon.transform.localPosition;
                _realPositionTampon = new Vector2(localPosition.x - tamponOffset.x, localPosition.y - tamponOffset.y);
                if (tampon.transform.localPosition.x >= -(_paper.rect.width / 2) &&
                    tampon.transform.localPosition.x <= (_paper.rect.width / 2) &&
                    tampon.transform.localPosition.y >= -(_paper.rect.height / 2) &&
                    tampon.transform.localPosition.y <= (_paper.rect.height / 2))
                {
                    tampon.transform.localPosition += new Vector3(tamponSpeed * Input.GetAxisRaw("Horizontal"),
                        tamponSpeed * Input.GetAxisRaw("Vertical"));
                }

                if (tampon.transform.localPosition.x < -(_paper.rect.width / 2))
                {
                    tampon.transform.localPosition =
                        new Vector2(-(_paper.rect.width / 2), tampon.transform.localPosition.y);
                }

                if (tampon.transform.localPosition.x > (_paper.rect.width / 2))
                {
                    tampon.transform.localPosition =
                        new Vector2((_paper.rect.width / 2), tampon.transform.localPosition.y);
                }

                if (tampon.transform.localPosition.y < -(_paper.rect.height / 2))
                {
                    tampon.transform.localPosition =
                        new Vector2(tampon.transform.localPosition.x, -(_paper.rect.height / 2));
                }

                if (tampon.transform.localPosition.y > (_paper.rect.height / 2))
                {
                    tampon.transform.localPosition =
                        new Vector2(tampon.transform.localPosition.x, (_paper.rect.height / 2));
                }

                if (Input.GetKeyDown(KeyCode.Space) && !_hasClicked)
                {
                    _hasClicked = true;
                    if (Vector2.Distance(tamponLogo.transform.localPosition, _realPositionTampon) < distanceTampon)
                    {
                        _tm.TamponPerSeriesCounter--;
                        var position = tampon.transform.localPosition;
                        tampon.transform.DOLocalMove(new Vector2(position.x - 80f, position.y - 20f), .2f)
                            .SetLoops(2, LoopType.Yoyo).OnStart(() => { _stamping = true; })
                            .OnComplete(() =>
                            {
                                _stamping = false;
                                if (tamponLogo.TryGetComponent(out Image im))
                                {
                                    im.DOFade(1f, .2f).OnComplete(() =>
                                    {
                                        if (_tm.TamponPerSeriesCounter > 0)
                                        {
                                            if (tamponLogo.TryGetComponent(out TamponLogoSpawn tsp))
                                            {
                                                StartCoroutine(ResetTamponLogo(tsp));
                                            }
                                        }
                                        else
                                        {
                                            tamponLogo.SetActive(false);
                                            _statePrintOver.SetActive(true);
                                            _hasClicked = false;
                                        }

                                        _tm.DecreaseStackPrint();
                                    });
                                }
                                else
                                {
                                    _hasClicked = false;
                                }
                            });
                    }
                    else
                    {
                        _hasClicked = false;
                    }
                }
            }
        }

        private IEnumerator ResetTamponLogo(TamponLogoSpawn tsp)
        {
            yield return new WaitForSeconds(.5f);
            tsp.RandomSpawn();
            _hasClicked = false;
        }

        public void InitUI(float playerState)
        {
            if ((int) playerState == (int) PlayerControls.PlayerZone.Printer && !tamponLogo.gameObject.activeSelf)
            {
                if (_statePrintOver && _tm.TamponPerSeriesCounter > 0)
                {
                    _statePrintOver.SetActive(false);
                    tamponLogo.SetActive(true);
                    if (tamponLogo.TryGetComponent(out TamponLogoSpawn tsp))
                    {
                        tsp.RandomSpawn();
                    }
                }
            }
        }

        public void ResetPos()
        {
            tampon.transform.localPosition = _originalPos;
        }
    }
}