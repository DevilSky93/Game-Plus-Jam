using UnityEngine;

namespace Dialogue
{
    [System.Serializable]
    public class Dialogue {
        public string name;

        [TextArea(3, 10)] //Permet d'avoir des boîtes de textes dans l'Inspector
        public string[] sentences;
    }
}
