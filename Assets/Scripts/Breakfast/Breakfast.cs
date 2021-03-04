using System.Collections.Generic;
using UnityEngine;

namespace Breakfast
{
    public class Breakfast : MonoBehaviour
    {
        [SerializeField] private Meal meal;

        private Sprite _icon;

        private List<Ingredient> _ingredients;
        private float _nourishing;
        public Meal Meal
        {
            get => meal;
            set => meal = value;
        }
        public Sprite Icon => _icon;

        public List<Ingredient> Ingredients
        {
            get => _ingredients;
            set => _ingredients = value;
        }

        public float Nourishing
        {
            get => _nourishing;
            set => _nourishing = value;
        }
        // Start is called before the first frame update
        void Start()
        {
            _icon = meal.Icon;
            _ingredients = meal.Ingredients;
            _nourishing = meal.Nourishing;
        }
    }
}
