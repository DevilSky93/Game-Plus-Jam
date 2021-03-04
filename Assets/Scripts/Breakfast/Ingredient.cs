using UnityEngine;

namespace Breakfast
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "Meal/Ingredient")]
    public class Ingredient : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private int id;

        public Sprite Icon => icon;

        public int ID => id;
    }
}
