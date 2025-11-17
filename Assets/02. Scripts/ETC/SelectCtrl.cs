using Unity.Cinemachine; 
using UnityEngine; 

public class SelectCtrl : MonoBehaviour 
{ 
    [Header("시네머신 관련 컴포넌트")] 
    [Header("시퀀서 카메라")] 
    [SerializeField] private CinemachineSequencerCamera m_sequener; 
    
    [Header("블렌딩 시간")] 
    [SerializeField] private float m_blend_interval = 1f; 
    
    private CinemachineVirtualCameraBase[] m_virtual_cameras; 
    
    private void Awake() 
    { 
        m_virtual_cameras = m_sequener.ChildCameras.ToArray(); 
    } 
    
    private void Start() 
    { 
        SetActiveCamera(0); 
    } 
    
    private void Update() 
    { 
        if(Input.GetKeyDown(KeyCode.RightArrow)) 
        { 
            SetActiveCamera(0); 
        } 
        else if(Input.GetKeyDown(KeyCode.LeftArrow)) 
        { 
            SetActiveCamera(1); 
        } 
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            // TODO: 현재 인덱스에 따른 로드 또는 초기화 필요요
            LoadScene("Game");
        }
    } 
    
    private void SetActiveCamera(int index) 
    { 
        var instruction = new CinemachineSequencerCamera.Instruction()
        {
            Camera = m_virtual_cameras[index],
            Blend =
            {
                Time = m_blend_interval,
                Style = CinemachineBlendDefinition.Styles.EaseInOut
            } 
        };

        m_sequener.Instructions.Clear(); 
        m_sequener.Instructions.Add(instruction); 
    } 

    private void LoadScene(string scene_name)
    {
        LoadingManager.Instance.LoadScene(scene_name);
    }
}