using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [Header("타임라인 데이터베이스")]
    [SerializeField] private TimelineDataBase m_timeline_db;

    [Header("디렉터 관련 컴포넌트")]
    [Header("플레이어블 디렉터")]
    [SerializeField] private PlayableDirector m_director;

    [Space(30f)]
    [Header("카메라 관련 컴포넌트")]
    [Header("인플레이 카메라")]
    [SerializeField] private CinemachineCamera m_inplay_camera;

    [Header("컷씬 카메라 그룹")]
    [SerializeField] private GameObject m_cut_scene_cam_group;

    [Space(30f)]
    [Header("UI 제어 관련 컴포넌트")]
    [Header("총괄 캔버스 그룹")]
    [SerializeField] private CanvasGroup m_canvas_group; 

    private void Awake()
    {
        m_director.played += TimelineBeginHandler;
        m_director.stopped += TimelineEndHandler;
    }

    public void StartTrailer(TimelineCode code)
    {
        m_director.playableAsset = m_timeline_db.GetTimeline(code);
        m_director.Play();
    }

    private void TimelineBeginHandler(PlayableDirector director)
    {
        GameEventBus.Publish(GameEventType.CUTSCENE);
        
        m_cut_scene_cam_group.SetActive(true);
        
        ToggleUI(false);
    }

    private void TimelineEndHandler(PlayableDirector director)
    {
        GameEventBus.Dequeue();
        GameEventBus.PriorityPublish();

        m_cut_scene_cam_group.SetActive(false);
        m_inplay_camera.Priority = 10;

        ToggleUI(true);
    }

    private void ToggleUI(bool active)
    {
        if(active)
        {
            m_canvas_group.alpha = 1f;
        }
        else
        {
            m_canvas_group.alpha = 0f;
        }
    }
}
