using DG.Tweening;
using Events.Float;
using UnityEngine;

namespace Shower
{
    public class BubbleClick : MonoBehaviour
    {
        public enum ValueClick
        {
            Excellent = 0,
            Good = 1,
            Bad = 2
        }

        [SerializeField] private GameObject excellentText, goodText, badText;
        [SerializeField] private Transform bubbleGfx;
        [SerializeField] private Transform outlineGfx;
        [SerializeField] private float excellentValue, goodValue, badValue;

        [SerializeField] private EventFloat addGaugeUIEvent;

        private Animator _anim;
        private bool _hasPop;

        // Start is called before the first frame update
        void Start()
        {
            _anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnMouseDown()
        {
            _anim.SetTrigger("Pop");
            outlineGfx.GetComponent<SpriteRenderer>().DOFade(0f, .5f).SetEase(Ease.OutSine);
            if (Mathf.Abs(Vector3.Distance(bubbleGfx.transform.localScale, outlineGfx.transform.localScale)) < .3f)
            {
                if (excellentText)
                {
                    ShowValueText((int) ValueClick.Excellent);
                }

                addGaugeUIEvent.Raise(excellentValue);
            }
            else if (Mathf.Abs(Vector3.Distance(bubbleGfx.transform.localScale, outlineGfx.transform.localScale)) < .6f)
            {
                if (goodText)
                {
                    ShowValueText((int) ValueClick.Good);
                }

                addGaugeUIEvent.Raise(goodValue);
            }
            else
            {
                if (badText)
                {
                    ShowValueText((int) ValueClick.Bad);
                }

                addGaugeUIEvent.Raise(badValue);
            }

            _hasPop = true;
            outlineGfx.DOKill();
        }

        private void ShowValueText(int val)
        {
            switch (val)
            {
                case (int) ValueClick.Excellent:
                    Instantiate(excellentText, transform.position, Quaternion.identity, transform);
                    break;
                case (int) ValueClick.Good:
                    Instantiate(goodText, transform.position, Quaternion.identity, transform);
                    break;
                case (int) ValueClick.Bad:
                    Instantiate(badText, transform.position, Quaternion.identity, transform);
                    break;
            }
        }

        public void Pop()
        {
            if (!_hasPop)
            {
                addGaugeUIEvent.Raise(badValue);
            }
            Destroy(transform.parent.gameObject);
        }
    }
}