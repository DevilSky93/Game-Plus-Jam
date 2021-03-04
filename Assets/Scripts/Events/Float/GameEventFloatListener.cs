using UnityEngine;

namespace Events.Float
{
    public class GameEventFloatListener : MonoBehaviour
    {
        // The game event instance to register to.
        public EventFloat GameEvent;
        // The unity event responce created for the event.
        public CustomUnityEventFloat Response;

        private void OnEnable()
        {
            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEvent.UnregisterListener(this);
        }

        public void RaiseEvent(float responseType)
        {
            Response.Invoke(responseType);
        }
    }
}
