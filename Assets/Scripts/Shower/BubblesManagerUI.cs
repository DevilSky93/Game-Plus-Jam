using System;
using DG.Tweening;
using Events.Bool;
using UnityEngine;
using UnityEngine.UI;

namespace Shower
{
    public class BubblesManagerUI : MonoBehaviour
    {
        [SerializeField] private Slider bubblesSlide;
        [SerializeField] private SpawnBubblesRandom bubbleSpawn;

        private bool _isOver;

        [SerializeField] private EventBool loseEvent;
        // Start is called before the first frame update
        void Start()
        {
            bubblesSlide.value = 0;
            bubblesSlide.maxValue = bubbleSpawn.NumberOfSpawn;
        }

        private void Update()
        {
            if (bubblesSlide.value >= bubblesSlide.maxValue && !_isOver)
            {
                loseEvent.Raise(false);
                _isOver = true;
            }
        }


        public void AddGauge(float val)
        {
            DOTween.To(() => bubblesSlide.value, x => bubblesSlide.value = x, bubblesSlide.value + val, .2f).SetEase(Ease.OutSine);
        }
    }
}
