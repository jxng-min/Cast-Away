using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "Timeline DataBase", menuName = "SO/DB/Create Timeline DataBase")]
public class TimelineDataBase : ScriptableObject, ITimelineDataBase
{
    [Header("타임라인 데이터 목록")]
    [SerializeField] private TimelineData[] m_data_list;
    private Dictionary<TimelineCode, TimelineAsset> m_data_dict;

#if UNITY_EDITOR
    private void OnEnable()
    {
        Initialize();
    }
#endif

    private void Initialize()
    {
        m_data_dict = new();

        foreach(var data in m_data_list)
        {
            m_data_dict.Add(data.Code, data.Asset);
        }
    }

    public TimelineAsset GetTimeline(TimelineCode code)
    {
        if(m_data_dict == null)
        {
            Initialize();
        }
        
        return m_data_dict.TryGetValue(code, out var timeline) ? timeline : null;
    }
}
