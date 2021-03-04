using DG.Tweening;
using UnityEngine;

namespace Shower
{
    public class Bubbles : MonoBehaviour
    {
        [SerializeField] private float waveLength;

        [SerializeField] private Transform outline;

        [SerializeField] private BubbleClick bbClick;
        // Start is called before the first frame update
        void Start()
        {
            var rndWaveLength = Random.Range(waveLength - (waveLength / 3), waveLength);
            outline.transform.DOScale(Vector3.zero, rndWaveLength).SetEase(Ease.OutSine).OnComplete(() =>
            {
                bbClick.Pop();
                // Destroy(gameObject);
                // bubble.gameObject.SetActive(false);
                // outline.gameObject.SetActive(false);
            });
        }
    }
}
