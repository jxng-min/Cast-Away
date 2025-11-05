using InventoryService;
using UnityEngine;

public class ToolInstaller : MonoBehaviour, IInstaller
{
    [Header("도구 핸들러")]
    [SerializeField] private Transform m_handler;

    public void Install()
    {
        InstallTool();
    }

    private void InstallTool()
    {
        var inventory_service = ServiceLocator.Get<IInventoryService>();

        var tools = m_handler.GetComponentsInChildren<BaseTool>(true);
        foreach(var tool in tools)
        {
            tool.Inject(inventory_service);
        }
    }
}
