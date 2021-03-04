using System.Collections.Generic;
using UnityEngine;

namespace Events.Float
{
    [CreateAssetMenu(fileName = "Event Float", menuName = "Events/Event Float", order = 1)]
    public class EventFloat : ScriptableObject
    {
        private List<GameEventFloatListener> listeners = new List<GameEventFloatListener>();
        public void RegisterListener(GameEventFloatListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventFloatListener listener)
        {
            listeners.Remove(listener);
        }

        public void Raise(float responseType)
        {
            for (int i = listeners.Count - 1; i >= 0; --i)
            {
                listeners[i].RaiseEvent(responseType);
            }
        }
    }
}
