using UnityEngine;

namespace Work
{
    public class FurnitureNotif : MonoBehaviour
    {
        [SerializeField] private string animToPlay;

        private Animator _anim;

        [SerializeField] private PlayerControls.PlayerZone currentZone;
        // Start is called before the first frame update
        void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public void StartAnim(float state)
        {
            if ((PlayerControls.PlayerZone)state == currentZone)
            {
                _anim.SetTrigger(animToPlay);
            } 
        }
    }
}
