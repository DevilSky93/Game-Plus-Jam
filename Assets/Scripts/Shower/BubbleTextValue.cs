using DG.Tweening;
using UnityEngine;

namespace Shower
{
    public class BubbleTextValue : MonoBehaviour
    {
        [SerializeField] private Vector3 offsetX,offsetY;
        // Start is called before the first frame update
        void Start()
        {
            var rndX = Random.Range(-offsetX.x, offsetX.x);
            // var rndY = Random.Range(-offset.y, offset.y);
            var currentOffsetX = new Vector3(rndX, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition += currentOffsetX;
            var rndOffset = new Vector2(transform.localPosition.x, offsetY.y);
            transform.DOLocalMove(rndOffset, .3f).SetEase(Ease.InSine).OnComplete(() =>
            {
                
                Destroy(gameObject);
            });
        }
    }
}
