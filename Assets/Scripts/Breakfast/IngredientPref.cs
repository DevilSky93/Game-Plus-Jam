using Events.Float;
using UnityEngine;
using UnityEngine.UI;

namespace Breakfast
{
    public class IngredientPref : MonoBehaviour
    {
        [SerializeField] private Ingredient ingredient;
        private Sprite _icon;
        private int _id;
        [SerializeField] private GameObject fail;
        [SerializeField] private Button clickIng;
        [SerializeField] private EventFloat lostLifeEvent;
        public Ingredient Ingredient
        {
            get => ingredient;
            set => ingredient = value;
        }
        public Sprite Icon
        {
            get => _icon;
            set => _icon = value;
        }

        public int ID
        {
            get => _id;
            set => _id = value;
        }
    
        private void Start()
        {
            clickIng = GetComponent<Button>();
            _icon = ingredient.Icon;
            _id = ingredient.ID;
        }

        public void ClickIngredient()
        {
            var didFind = BreakfastManager.instance.ChoseMeal(transform, ingredient);
            if (!didFind)
            {
                Instantiate(fail, transform, false);
                if (clickIng)
                {
                    clickIng.interactable = false;
                }
                lostLifeEvent.Raise(1);
            }
        }
    }
}
