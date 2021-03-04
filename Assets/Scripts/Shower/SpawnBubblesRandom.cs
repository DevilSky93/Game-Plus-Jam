using System.Collections;
using UnityEngine;

namespace Shower
{
    public class SpawnBubblesRandom : MonoBehaviour
    {
        [SerializeField] private GameObject bubblePrefab;

        [SerializeField] private int numberOfSpawn;
        [SerializeField] private float timeBetweenSpawn, minScaleBubble, maxScaleBubble;
        private IEnumerator _coroutineSpawn;
        public int NumberOfSpawn => numberOfSpawn;
        private Camera _mainCamera;
        // Start is called before the first frame update
        void Start()
        {
            _mainCamera = Camera.main; 
        }

        public void StartSpawn()
        {
            _coroutineSpawn = SpawnRandom();
            StartCoroutine(_coroutineSpawn);
        }

    
        private IEnumerator SpawnRandom()
        {
            for (int i = 0; i < numberOfSpawn; i++)
            {
                float spawnY = Random.Range
                    (_mainCamera.ScreenToWorldPoint(new Vector3(0, 0+(Screen.height/3),_mainCamera.nearClipPlane)).y, _mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height-(Screen.height/3),_mainCamera.nearClipPlane)).y);
                float spawnX = Random.Range
                    (_mainCamera.ScreenToWorldPoint(new Vector3(0+(Screen.width/4), 0, _mainCamera.nearClipPlane)).x, _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width - (Screen.width/4), 0, _mainCamera.nearClipPlane)).x);
                var randomScale = Random.Range(minScaleBubble, maxScaleBubble);
                Vector2 spawnPosition = new Vector2(spawnX, spawnY);

                var inst = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
                inst.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                var rndTime = Random.Range(timeBetweenSpawn - (timeBetweenSpawn / 2),
                    timeBetweenSpawn);
                yield return new WaitForSeconds(rndTime);
            }
        }

        public void StopSpawn(bool isLose)
        {
            StopCoroutine(_coroutineSpawn);
        }
    }
}
