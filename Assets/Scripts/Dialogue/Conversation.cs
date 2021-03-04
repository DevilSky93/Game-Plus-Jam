using System;
using UnityEngine;

namespace Dialogue
{
    [Serializable]
    public class Conversation
    {
        public string name;

        [TextArea(3, 10)] //Permet d'avoir des boîtes de textes dans l'Inspector
        public string sentences;
    }
}
