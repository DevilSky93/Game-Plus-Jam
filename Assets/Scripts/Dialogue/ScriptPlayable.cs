using UnityEngine;
using UnityEngine.Playables;

namespace Dialogue
{
    public class ScriptPlayable : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        private RuntimeAnimatorController _playerAnim;
        [SerializeField] private PlayableDirector director;
        [SerializeField] private StartPlayerAnim playerA;
        private Vector3 _newPos;
        public Animator PlayerAnimator
        {
            get => playerAnimator;
            set => playerAnimator = value;
        }
        // Start is called before the first frame update
        void Start()
        {
            _playerAnim = playerAnimator.runtimeAnimatorController;
            playerAnimator.runtimeAnimatorController = null;
            director.stopped += OnPlayableDirectorStopped;
        }

        // Update is called once per frame
        void Update()
        {
            if (director.state == PlayState.Playing) {
                GameManager.instance.gameState = GameManager.GameState.Cutscene;
                playerAnimator.runtimeAnimatorController = _playerAnim;
            } 
            else if(director.state == PlayState.Paused)
            {
                // GameManager.instance.gameState = GameManager.GameState.InGame;
                playerAnimator.runtimeAnimatorController = _playerAnim;
                Debug.Log("PAUSE");
            }
        }

        public void PauseCutscene()
        {
            // director.Pause();
            director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        }

        private void OnPlayableDirectorStopped(PlayableDirector aDirector)
        {
            if (director == aDirector)
            {
                Debug.Log(aDirector.name + " STOP");
                // playerAnimator.runtimeAnimatorController = playerAnim;
            }
        }

        public void ResumeCutscene()
        {
            // director.Resume();
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }

        public void StopCutscene()
        {
            // director.Stop();
            Debug.Log("STOP");
            GameManager.instance.gameState = GameManager.GameState.InGame;
        }
        
        public void EndAnim()
        {
            if (playerA != null)
            {
                _newPos = playerA.transform.position;
            }
        }
    }
}
