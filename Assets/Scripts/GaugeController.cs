using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    [SerializeField] private float maxValue;
    [SerializeField] private Slider slide;

    private float _currentValue;

    [SerializeField] private bool decreaseWithTime;

    [SerializeField] private float valueDecrease;

    private float _affectGaugeValue;

    public float MAXValue => maxValue;

    public float CurrentValue => _currentValue;

    public bool DecreaseWithTime
    {
        get => decreaseWithTime;
        set => decreaseWithTime = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        slide.maxValue = maxValue;
        slide.value = _currentValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (decreaseWithTime)
        {
            _currentValue -= valueDecrease * Time.deltaTime;
            if (_currentValue <= 0)
            {
                _currentValue = 0;
            }
            slide.value = _currentValue;            
        }
    }

    public void ModifyGauge(float val)
    {
        _currentValue += val-(val*(_affectGaugeValue/100));
        if (_currentValue >= maxValue)
        {
            _currentValue = maxValue+1;
        }

        DOTween.To(() => slide.value, x => slide.value = x, _currentValue, .2f);
        if (_currentValue < 0)
        {
            _currentValue = 0;
        }
    }

    public void AffectGauge(float val)
    {
        _affectGaugeValue += val;
    }
    
}
