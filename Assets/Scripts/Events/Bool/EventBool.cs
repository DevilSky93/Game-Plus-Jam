using System.Collections.Generic;
using UnityEngine;

namespace Events.Bool
{
    [CreateAssetMenu(fileName = "Event Bool", menuName = "Events/Event Bool", order = 1)]
    public class EventBool : ScriptableObject
    {
        private List<GameEventBoolListener> listeners = new List<GameEventBoolListener>();
        public void RegisterListener(GameEventBoolListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventBoolListener listener)
        {
            listeners.Remove(listener);
        }

        public void Raise(bool responseType)
        {
            for (int i = listeners.Count - 1; i >= 0; --i)
            {
                listeners[i].RaiseEvent(responseType);
            }
        }
    }
}
