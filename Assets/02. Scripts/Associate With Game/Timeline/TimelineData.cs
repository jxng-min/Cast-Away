using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New Timeline Data", menuName = "SO/Create New Timeline Data")]
public class TimelineData: ScriptableObject
{
    [Header("타임라인 코드")]
    [SerializeField] private TimelineCode m_code;
    public TimelineCode Code => m_code;

    [Header("타임라인 에셋")]
    [SerializeField] private TimelineAsset m_asset;
    public TimelineAsset Asset => m_asset; 
}