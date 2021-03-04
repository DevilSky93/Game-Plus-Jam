using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Work
{
    public class RandomizeMail : MonoBehaviour
    {
        public enum TypeMail
        {
            Transfer,
            Delete,
            Send,
            Read
        }

        private TypeMail _typeMail;
        private Image _mailGfx;
        [SerializeField] private Sprite[] mailType;

        public TypeMail TypeMail1 => _typeMail;
        
        // Start is called before the first frame update
        private void Awake()
        {
            _mailGfx = GetComponent<Image>();
        }

        public void Init()
        {
            var mailT = Random.Range(0, mailType.Length);
            _typeMail = (TypeMail)mailT;
            _mailGfx.sprite = mailType[mailT];
        }
    }
}
