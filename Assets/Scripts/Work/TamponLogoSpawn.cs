using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Work
{
    public class TamponLogoSpawn : MonoBehaviour
    {
        [SerializeField] private RectTransform paper;
        private RectTransform _tamponRect;

        private void Start()
        {
            _tamponRect = GetComponent<RectTransform>();
        }

        public void RandomSpawn()
        {
            var rect = paper.rect;
            var newPosX = Random.Range(-(rect.width / 3), (rect.width / 3));
            var newPosY = Random.Range(-(rect.height / 3), (rect.height / 3));
            transform.localPosition = new Vector2(newPosX, newPosY);
            if (TryGetComponent(out Image im))
            {
                im.color = new Color(im.color.r, im.color.g, im.color.b, .3f);
            }
        }
    }
}
