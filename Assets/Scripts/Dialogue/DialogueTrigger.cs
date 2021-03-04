using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour {
        public List<Dialogue> dialogue;
        private int _indexDialogue;

        public int IndexDialogue => _indexDialogue;

        public List<Dialogue> Dialogue => dialogue;
        public void TriggerDialogue() {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue[_indexDialogue]);
            _indexDialogue++;
        }
    }
}
