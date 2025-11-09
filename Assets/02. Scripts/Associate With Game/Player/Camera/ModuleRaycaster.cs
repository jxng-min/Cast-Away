using KeyService;
using UnityEngine;

public class ModuleRaycaster : MonoBehaviour
{
    [Header("레이의 길이")]
    [SerializeField] private float m_ray_length;

    [Header("레이가 감지할 레이어")]
    [SerializeField] private LayerMask m_layer_mask;

    private IKeyService m_key_service;

    private TimeSettings m_time_settings;
    private TimeManager m_time_manager;
    private FadePresenter m_fade_presenter;
    private TimelineManager m_timeline_manager;

    private NoticePresenter m_notice_presenter;

    public void Inject(IKeyService key_service,
                       NoticePresenter notice_presenter,
                       TimeSettings time_settings,
                       TimeManager time_manager,
                       FadePresenter fade_presenter,
                       TimelineManager timeline_manager)
    {
        m_key_service = key_service;
        m_notice_presenter = notice_presenter;
        m_time_settings = time_settings;
        m_time_manager = time_manager;
        m_fade_presenter = fade_presenter;
        m_timeline_manager = timeline_manager;
    }

    private void Update()
    {
        if(GameManager.Instance.GameType != GameEventType.INPLAY)
        {
            return;
        }

        var center = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        var ray = Camera.main.ScreenPointToRay(center);
        Debug.DrawRay(ray.origin, ray.direction * m_ray_length, Color.red);

        if(Physics.Raycast(ray, out var hit, m_ray_length, m_layer_mask))
        {
            var realview_obj = hit.transform.GetComponentInChildren<RealviewObject>();

            Debug.Log(hit.transform.name);

            if(realview_obj is Bed)
            {
                var current_time = m_time_manager.CurrentTime;
                var hour = current_time.Hour;
                if(hour < 18)
                {
                    return;
                }

                m_notice_presenter.OpenUI($"취침을 하려면 [{m_key_service.GetKeyCode("PickUp").ToString().ToUpper()}]를 누르세요.");

                if(Input.GetKeyDown(m_key_service.GetKeyCode("PickUp")))
                {
                    (realview_obj as Bed).Interaction(m_time_settings, m_time_manager, m_fade_presenter);
                    m_notice_presenter.CloseUI();
                }
            }
            else if(realview_obj is Boat)
            {
                Debug.Log("들어옴");
                m_notice_presenter.OpenUI($"보트를 타려면 [{m_key_service.GetKeyCode("PickUp").ToString().ToUpper()}]를 누르세요.");

                if(Input.GetKeyDown(m_key_service.GetKeyCode("PickUp")))
                {
                    (realview_obj as Boat).Interaction(m_timeline_manager);
                    m_notice_presenter.CloseUI();
                }
            }
            else
            {
                m_notice_presenter.CloseUI();
            }
        }
        else
        {
            m_notice_presenter.CloseUI();
        }
    }
}
