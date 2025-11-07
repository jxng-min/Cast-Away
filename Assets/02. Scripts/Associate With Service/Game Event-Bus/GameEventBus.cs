using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Events;

public class GameEventBus
{
    private static readonly IDictionary<GameEventType, UnityEvent> m_events
        = new Dictionary<GameEventType, UnityEvent>();

    public static LinkedList<GameEventType> m_event_list = new();

    public static void Subscribe(GameEventType event_type, UnityAction listener)
    {
        if(m_events.TryGetValue(event_type, out var this_event))
        {
            this_event.AddListener(listener);
        }
        else
        {
            this_event = new UnityEvent();
            this_event.AddListener(listener);

            m_events.Add(event_type, this_event);
        }
    }

    public static void Unsubscribe(GameEventType event_type, UnityAction listener)
    {
        if(m_events.TryGetValue(event_type, out var this_event))
        {
            this_event.RemoveListener(listener);
        }
    }

    public static void Publish(GameEventType event_type)
    {    
        if(m_events.TryGetValue(event_type, out var this_event))
        {
            if(event_type != GameEventType.PAUSE)
            {
                m_event_list.AddLast(event_type);
            }

#if UNITY_EDITOR
            UnityEngine.Debug.Log($"정상적으로 {event_type}이 실행되었습니다.");

            foreach(var value in m_event_list)
            {
                UnityEngine.Debug.Log(value);
            }
#endif

            this_event.Invoke();
        }
    }

    public static void PriorityPublish()
    {
        if(m_events.TryGetValue(m_event_list.Last.Value, out var this_event))
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Log($"정상적으로 {m_event_list.Last.Value}이 실행되었습니다.");

            foreach(var value in m_event_list)
            {
                UnityEngine.Debug.Log(value);
            }
#endif
            this_event.Invoke();
        }
    }

    public static void Dequeue()
    {
        m_event_list.RemoveLast();
    }
}