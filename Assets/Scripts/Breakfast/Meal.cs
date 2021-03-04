using System.Collections.Generic;
using UnityEngine;

namespace Breakfast
{
    [CreateAssetMenu(fileName = "Meal", menuName = "Meal/Meal")]
    public class Meal : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private List<Ingredient> ingredients;
        [SerializeField] private float nourishing;
        public Sprite Icon => icon;

        public List<Ingredient> Ingredients => ingredients;

        public float Nourishing => nourishing;
    }
}
