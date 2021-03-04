using System.Collections.Generic;
using UnityEngine;

namespace Events.NoType
{
    [CreateAssetMenu(fileName = "Event", menuName = "Events/Event", order = 1)]
    public class EventNoType : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();
        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; --i)
            {
                listeners[i].RaiseEvent();
            }
        }
    }
}
