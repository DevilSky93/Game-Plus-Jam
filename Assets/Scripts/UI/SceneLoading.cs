using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SceneLoading : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;

        public void LoadScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
