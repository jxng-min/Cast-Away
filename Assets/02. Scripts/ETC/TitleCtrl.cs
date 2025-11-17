using UnityEngine;

public class TitleCtrl : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
        else if(Input.anyKeyDown)
        {
            LoadScene("Select");
        }
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void LoadScene(string scene_name)
    {
        LoadingManager.Instance.LoadScene(scene_name);
    }
}
