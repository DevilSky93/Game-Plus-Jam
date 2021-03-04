using UnityEngine;

namespace Events.Bool
{
    public class GameEventBoolListener : MonoBehaviour
    {
        // The game event instance to register to.
        public EventBool GameEvent;
        // The unity event responce created for the event.
        public CustomUnityEventBool Response;

        private void OnEnable()
        {
            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEvent.UnregisterListener(this);
        }

        public void RaiseEvent(bool responseType)
        {
            Response.Invoke(responseType);
        }
    }
}
