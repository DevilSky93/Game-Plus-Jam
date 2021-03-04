using System;
using System.Collections;
using System.Collections.Generic;
using Events.NoType;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;
        public Text nameText;
        public Text dialogueText;
        private Queue<string> _sentences;
        private bool _isTyping;
        public bool isOver;
        private bool _hasStarted;
        [SerializeField] private EventNoType resumeDialogueEvent;

        public bool HasStarted => _hasStarted;
        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start() {
            _sentences = new Queue<string>();
            _isTyping = false;
            isOver = false;
        }

        public void StartDialogue(Dialogue dialogue) {
            nameText.text = dialogue.name;
            _hasStarted = true;
            _sentences.Clear();
            isOver = false;
            foreach (string sentence in dialogue.sentences) {
                _sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }

        public void ContinueOrDisplay() {
            if (_isTyping) {
                DisplaySequence();
                _sentences.Dequeue();
            } else {
                DisplayNextSentence();
            }
        }

        public void DisplaySequence() {
            _isTyping = false;
            StopAllCoroutines();
            if (_sentences.Count == 0) {
                EndDialogue();
                return;
            }
            dialogueText.text = "";
            dialogueText.text = _sentences.Peek();
        }
        public void DisplayNextSentence() {
            if (_sentences.Count == 0) {
                EndDialogue();
                return;
            }

            string sentence = _sentences.Peek();

            StartCoroutine(TypeSentence(sentence));
        }

        IEnumerator TypeSentence(string sentence) {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray()) {
                dialogueText.text += letter;
                _isTyping = true;

                yield return new WaitForSeconds(.05f);
            }
            _sentences.Dequeue();
            _isTyping = false;
        }

        private void EndDialogue() {
            isOver = true;
            _hasStarted = false;
            resumeDialogueEvent.Raise();
        }

        public void ActivateDeactivateText(bool activate)
        {
            nameText.gameObject.SetActive(activate);
            dialogueText.gameObject.SetActive(activate);
        }
    }
}
