using UnityEngine.Timeline;

public interface ITimelineDataBase
{
    TimelineAsset GetTimeline(TimelineCode code);
}