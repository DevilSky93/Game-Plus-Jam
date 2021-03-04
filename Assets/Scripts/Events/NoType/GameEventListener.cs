using UnityEngine;

namespace Events.NoType
{
    public class GameEventListener : MonoBehaviour
    {
        // The game event instance to register to.
        public EventNoType GameEvent;
        // The unity event responce created for the event.
        public CustomUnityEvent Response;

        private void OnEnable()
        {
            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEvent.UnregisterListener(this);
        }

        public void RaiseEvent()
        {
            Response.Invoke();
        }
    }
}
