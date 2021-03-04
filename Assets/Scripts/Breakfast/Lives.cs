using System.Collections.Generic;
using DG.Tweening;
using Events.Bool;
using Events.Float;
using UnityEngine;
using UnityEngine.UI;

namespace Breakfast
{
    public class Lives : MonoBehaviour
    {
        [SerializeField] private int lives;
        private int _currentLive;
        [SerializeField] private GameObject heart;

        private List<GameObject> _instHearts = new List<GameObject>();

        [SerializeField] private EventFloat modifyGaugeEvent;
        [SerializeField] private EventBool loseEvent;

        public int CurrentLive => _currentLive;
        // Start is called before the first frame update
        void Start()
        {
            InitHearts();
        }

        public void FadeHeart(bool fadeIn)
        {
            if (fadeIn)
            {
                foreach (var h in _instHearts)
                {
                    if (h.TryGetComponent(out Image im))
                    {
                        im.DOFade(1f, .3f);
                    }
                }
            }
            else
            {
                foreach (var h in _instHearts)
                {
                    if (h.TryGetComponent(out Image im))
                    {
                        im.DOFade(0f, .3f);
                    }
                }
            }
        }

        public void LostLife(float val)
        {
            _currentLive -= (int)val;
            for (int i = 0;i<val;i++)
            {
                Destroy(_instHearts[i]);
                _instHearts.RemoveAt(i);
            }

            if (_currentLive <= 0)
            {
                modifyGaugeEvent.Raise(-2f);
                loseEvent.Raise(true);
            }
        }

        public void InitHearts()
        {
            _currentLive = lives;
            if(_instHearts.Count > 0) ClearHearts(_instHearts);
            _instHearts = new List<GameObject>();
            for (int i = 0; i < lives; i++)
            {
                var inst = Instantiate(heart, transform, false);
                if (inst.TryGetComponent(out Image im))
                {
                    im.DOFade(0f, 0f);
                    _instHearts.Add(inst);
                }
            }
        }

        private void ClearHearts(List<GameObject> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                Destroy(l[i].gameObject);
            }
        }
    }
}
