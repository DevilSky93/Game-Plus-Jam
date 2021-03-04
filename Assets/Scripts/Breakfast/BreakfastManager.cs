using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Events.Bool;
using Events.Float;
using Events.NoType;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Breakfast
{
    public class BreakfastManager : MonoBehaviour
    {
        public static BreakfastManager instance;
        [SerializeField] private Transform mealStep1, mealStep2;
        [SerializeField] private float timeBetweenStep;
        [SerializeField] private int numberOfTimeStep;
        [Header("Step 1")] [SerializeField] private Image imgMeal1;
        [SerializeField] private Image imgMeal2;

        [SerializeField] private Transform[] rows;
        [SerializeField] private Text firstStep;
        [SerializeField] private GameObject iconMeal;

        private int _currentNumberOfTimeStep;
        private bool _isOver;
        private List<GameObject> _iconInst = new List<GameObject>();
        private Meal[] _currentMeal;
        private List<KeyValuePair<int, int>> _mealToFill;
        private List<Ingredient> _listIng = new List<Ingredient>();
        private int _firstMealSize, _secondMealSize;
        [Header("Step 2")] [SerializeField] private Text secondStep;
        [SerializeField] private Transform firstMeal, secondMeal;
        [SerializeField] private GameObject emptyTransform;
        [SerializeField] private Lives lives;

        [SerializeField] private GameObject correctMeal;
        [SerializeField] private Text winText, loseText;
        [Header("Events")]
        [SerializeField] private EventFloat modifyGaugeEvent;
        [SerializeField] private EventBool startTimerEvent, loseEvent;
        [SerializeField] private EventNoType resetTimerEvent;
        [Space(5)] [SerializeField] private List<Meal> meals;
        [SerializeField] private List<Ingredient> ingredients;

        [SerializeField] private List<Transform> slots;
        [SerializeField] private GameObject iconIngredient;

        private List<bool> _slotsOccupied;

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void InitBreakfast()
        {
            mealStep1.gameObject.SetActive(true);
            firstStep.gameObject.SetActive(true);

            winText.transform.localScale = Vector3.zero;
            loseText.transform.localScale = Vector3.zero;
            _mealToFill = new List<KeyValuePair<int, int>>();
            _currentMeal = new Meal[2];
            _slotsOccupied = Enumerable.Repeat(false, slots.Count).ToList();
            InitStep1();            
        }

        private void Reset()
        {
            _iconInst.Clear();
            Array.Clear(_currentMeal, 0, _currentMeal.Length);
            _mealToFill.Clear();
            _listIng.Clear();
            _slotsOccupied = Enumerable.Repeat(false, slots.Count).ToList();
        }

        private Meal CreateMealToShow()
        {
            var choose = Random.Range(0, meals.Count);
            var chosenMeal = meals[choose];
            var ico = Instantiate(iconMeal, imgMeal1.transform, false);
            if (ico.TryGetComponent(out Breakfast b))
            {
                b.Meal = chosenMeal;
                b.Ingredients = chosenMeal.Ingredients;
            }
            // ico.GetComponent<Breakfast>().Meal = chosenMeal;
            // ico.GetComponent<Breakfast>().Ingredients = chosenMeal.Ingredients;
            if (ico.TryGetComponent(out Image i))
            {
                i.sprite = chosenMeal.Icon;
            }
            // ico.GetComponent<Image>().sprite = chosenMeal.Icon;
            _iconInst.Add(ico);
            return chosenMeal;
        }

        private void Step2ToStep1()
        {
            if (winText.gameObject.activeInHierarchy)
            {
                winText.transform.DOScale(Vector3.zero, .5f).OnComplete(() =>
                {
                    winText.gameObject.SetActive(false);
                });
            }
            else
            {
                loseText.transform.DOScale(Vector3.zero, .5f).OnComplete(() =>
                {
                    loseText.gameObject.SetActive(false);
                });
            }

            for (int i = 0;i<slots.Count;i++)
            {
                ClearChildTransform(slots[i]);
            }
            secondStep.DOFade(0f, .2f).OnComplete(() => { secondStep.gameObject.SetActive(false); });

            imgMeal2.DOFade(1f, .3f);
            imgMeal1.DOFade(1f, .3f).OnComplete(() =>
            {
                mealStep2.gameObject.SetActive(false);
                mealStep1.gameObject.SetActive(true);

                firstStep.gameObject.SetActive(true);
                firstStep.DOFade(1f, .2f);
            });
            startTimerEvent.Raise(false);
            lives.FadeHeart(false);
            ClearChildTransform(firstMeal);
            ClearChildTransform(secondMeal);
            Reset();
            resetTimerEvent.Raise();
            InitStep1();

        }

        private void InitStep1()
        {
            _currentMeal[0] = CreateMealToShow();
            _currentMeal[1] = CreateMealToShow();
            _firstMealSize = _currentMeal[0].Ingredients.Count;
            _secondMealSize = _currentMeal[1].Ingredients.Count;
            for (int h = 0; h < _currentMeal.Length; h++)
            {
                for (int j = h; j < rows.Length; j++)
                {
                    if (rows[j].childCount <= 0)
                    {
                        for (int i = 0; i < _currentMeal[h].Ingredients.Count; i++)
                        {
                            _listIng.Add(_currentMeal[h].Ingredients[i]);
                            if (i < 4)
                            {
                                var ico1 = Instantiate(iconMeal, rows[j].transform, false);
                                if (ico1.TryGetComponent(out Breakfast b))
                                {
                                    b.Meal = _currentMeal[h];
                                    b.Ingredients = _currentMeal[h].Ingredients;
                                }
                                if (ico1.TryGetComponent(out Image im))
                                {
                                    im.sprite = _currentMeal[h].Ingredients[i].Icon;
                                }
                                _iconInst.Add(ico1);
                            }
                            else
                            {
                                var ico1 = Instantiate(iconMeal, rows[j+1].transform, false);
                                if (ico1.TryGetComponent(out Breakfast b))
                                {
                                    b.Meal = _currentMeal[h];
                                    b.Ingredients = _currentMeal[h].Ingredients;
                                }
                                if (ico1.TryGetComponent(out Image im))
                                {
                                    im.sprite = _currentMeal[h].Ingredients[i].Icon;
                                }
                                _iconInst.Add(ico1);
                            }
                        }

                        break;
                    }
                }


            }

            firstStep.DOFade(1f, .2f);
            imgMeal1.DOFade(1f, .3f);
            imgMeal2.DOFade(1f, .3f);

            NextStep();
        }

        private void InitStep2()
        {
            List<int> slotsNumber = new List<int>();
            for (int i = 0; i < slots.Count; i++)
            {
                slotsNumber.Add(i);
            }

            for (int h = 0;h<_currentMeal.Length;h++)
            {
                for (int i = 0; i < _currentMeal[h].Ingredients.Count; i++)
                {
                    var rnd = slotsNumber[Random.Range(0, slotsNumber.Count)];
                    var instIng = Instantiate(iconIngredient, slots[rnd].transform, false);
                    if (instIng.TryGetComponent(out IngredientPref ip))
                    {
                        ip.Ingredient = _currentMeal[h].Ingredients[i];
                        ip.ID = _currentMeal[h].Ingredients[i].ID;
                    }
                    if (instIng.TryGetComponent(out Image im))
                    {
                        im.sprite = _currentMeal[h].Ingredients[i].Icon;
                    }
                    _slotsOccupied[rnd] = true;
                    slotsNumber.Remove(rnd);
                }            
            }


            for (int i = 0; i < slots.Count; i++)
            {
                if (!_slotsOccupied[i])
                {
                    var rndIng = Random.Range(0, ingredients.Count);
                    var instIng = Instantiate(iconIngredient, slots[i].transform, false);
                    if (instIng.TryGetComponent(out IngredientPref ip))
                    {
                        ip.Ingredient = ingredients[rndIng];
                        ip.ID = ingredients[rndIng].ID;
                    }
                    if (instIng.TryGetComponent(out Image im))
                    {
                        im.sprite = ingredients[rndIng].Icon;
                    }
                    _slotsOccupied[i] = true;
                    slotsNumber.Remove(i);
                }
            }
        }

        private void NextStep()
        {
            StartCoroutine(NextStepCor());
        }

        private IEnumerator NextStepCor()
        {
            yield return new WaitForSeconds(timeBetweenStep);
            firstStep.DOFade(0f, .2f).OnComplete(() => { firstStep.gameObject.SetActive(false); });

            imgMeal1.DOFade(0f, .3f);
            imgMeal2.DOFade(0f, .3f).OnComplete(() =>
            {
                mealStep1.gameObject.SetActive(false);
                mealStep2.gameObject.SetActive(true);

                secondStep.gameObject.SetActive(true);
                secondStep.DOFade(1f, .2f);
            });
            foreach (var ico in _iconInst)
            {
                if (ico.TryGetComponent(out Image i))
                {
                    i.DOFade(0f, .3f);
                }
                // ico.GetComponent<Image>().DOFade(0f, .3f);
            }
            lives.FadeHeart(true);
            startTimerEvent.Raise(true);
            ClearChildTransform(imgMeal1.transform);
            for (int i = 0;i<rows.Length;i++)
            {
                ClearChildTransform(rows[i]);
            }
            InitStep2();
        }

        public bool ChoseMeal(Transform ing, Ingredient i)
        {
            for (int  j = 0; j < _currentMeal.Length ;j++)
            {
                foreach (var ig in _currentMeal[j].Ingredients)
                {
                    if (ig.ID == i.ID && _listIng.Contains(ig))
                    {
                        GameObject inst = null;
                        switch (j)
                        {
                            case 0:
                                if (_firstMealSize <= 0) goto case 1;
                                if (!_mealToFill.Contains(new KeyValuePair<int, int>(j, ig.ID)))
                                {
                                    _mealToFill.Add(new KeyValuePair<int, int>(j, ig.ID));
                                    inst = Instantiate(emptyTransform, firstMeal.transform);
                                    _firstMealSize--;

                                    goto AddSuccess;
                                }
                                break;
                            case 1:
                                if (_secondMealSize <= 0) goto case 0;
                                if (!_mealToFill.Contains(new KeyValuePair<int, int>(j, ig.ID)))
                                {
                                    _mealToFill.Add(new KeyValuePair<int, int>(j, ig.ID));
                                    inst = Instantiate(emptyTransform, secondMeal.transform);
                                    _secondMealSize--;

                                    goto AddSuccess;
                                }
                                break;  
                            default:
                                inst = Instantiate(emptyTransform, firstMeal.transform);
                                break;
                        }
                        AddSuccess :
                        {
                            if (inst)
                            {
                                ing.DOMove(inst.transform.position, .5f).OnComplete(() =>
                                {
                                    ing.transform.SetParent(inst.transform);
                                    ing.DOLocalMove(Vector3.zero, .2f).OnComplete(() =>
                                    {
                                        if (_firstMealSize == 0)
                                        { 
                                            var inst2 = Instantiate(emptyTransform, firstMeal.transform,false);
                                            inst2.AddComponent<VerticalLayoutGroup>();
                                            var ico = Instantiate(iconMeal, firstMeal.transform);
                                            ico.GetComponent<Breakfast>().Meal = _currentMeal[j];
                                            ico.GetComponent<Breakfast>().Ingredients = _currentMeal[j].Ingredients;
                                            ico.GetComponent<Image>().sprite = _currentMeal[j].Icon;
                                            ico.transform.SetParent(inst2.transform);
                                            var correct = Instantiate(correctMeal, ico.transform, false);
                                            correct.transform.SetParent(ico.transform);
                                            ico.transform.localScale = Vector3.zero;

                                            ico.transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutSine);
                                            _firstMealSize--;
                                        } else if (_secondMealSize == 0)
                                        { 
                                            var inst2 = Instantiate(emptyTransform, secondMeal.transform, false);
                                            inst2.AddComponent<VerticalLayoutGroup>();
                                            var ico = Instantiate(iconMeal, secondMeal.transform);
                                            ico.GetComponent<Breakfast>().Meal = _currentMeal[j];
                                            ico.GetComponent<Breakfast>().Ingredients = _currentMeal[j].Ingredients;
                                            ico.GetComponent<Image>().sprite = _currentMeal[j].Icon;
                                            ico.transform.SetParent(inst2.transform);
                                            var correct = Instantiate(correctMeal, ico.transform, false);
                                            correct.transform.SetParent(ico.transform);
                                            ico.transform.localScale = Vector3.zero;

                                            ico.transform.DOScale(Vector3.one, .1f).SetEase(Ease.OutSine);
                                            _secondMealSize--;
                                        }     
                                    });
                                });
                                _listIng.Remove(ig);
                                goto LoopEnd;   
                            }
                        }
                    }
                }
            }

            return false;
            LoopEnd:
            {
                if (_firstMealSize <= 0 && _secondMealSize <= 0)
                {
                    Debug.Log("over");
                    _currentNumberOfTimeStep++;
                    if (_currentNumberOfTimeStep >= numberOfTimeStep)
                    {
                        _isOver = true;
                        winText.gameObject.SetActive(true);
                        winText.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutSine);
                        EndBreakfast();
                    }
                    else
                    {
                        EndOfStep2();
                    }
                }   
                return true;
            }
        }

        private void EndOfStep2()
        {
            StartCoroutine(EndOfStep2Cor());
        }

        private IEnumerator EndOfStep2Cor()
        {
            yield return new WaitForSeconds(1f);
            float val = 0;
            foreach (var m in _currentMeal)
            {
                val += m.Nourishing;
            }
            modifyGaugeEvent.Raise(val);
            startTimerEvent.Raise(false);
            ChangeStep(false);
        }

        public void ChangeStep(bool isLose)
        {
            if (!_isOver)
            {
                if (!isLose)
                {
                    winText.gameObject.SetActive(true);
                    winText.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutSine);
                }
                else
                {
                    modifyGaugeEvent.Raise(-2f);
                    loseText.gameObject.SetActive(true);
                    loseText.transform.DOScale(Vector3.one, .5f).SetEase(Ease.OutSine);
                }
                StartCoroutine(ChangeStepCor());   
            }
        }

        private IEnumerator ChangeStepCor()
        {
            yield return new WaitForSeconds(2f);

            Step2ToStep1();
        }

        private void ClearChildTransform(Transform t)
        {
            for (int i = 0;i<t.childCount;i++)
            {
                Destroy(t.GetChild(i).gameObject);
            }
        }

        private void EndBreakfast()
        {
            StartCoroutine(EndBreakfastCor());
        }

        private IEnumerator EndBreakfastCor()
        {
            yield return new WaitForSeconds(2f);
            loseEvent.Raise(false);
            startTimerEvent.Raise(false);
            lives.FadeHeart(false);
            ClearChildTransform(firstMeal);
            ClearChildTransform(secondMeal);
            Reset();
            mealStep1.gameObject.SetActive(false);
            mealStep2.gameObject.SetActive(false);
            firstStep.gameObject.SetActive(false);
            secondStep.gameObject.SetActive(false);
            winText.gameObject.SetActive(false);
            loseText.gameObject.SetActive(false);
            resetTimerEvent.Raise();
        }
    }
}