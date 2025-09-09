using System;
using UserService;
using EXPService;

public class StatusPresenter : IDisposable
{
    private readonly StatusModel m_model;
    private readonly IStatusView m_view;
    private readonly IUserService m_userService;
    private readonly IEXPService m_expService;

    public StatusPresenter(StatusModel model, IStatusView view, IUserService user_service, IEXPService exp_service)
    {
        m_model = model;
        m_view = view;
        m_userService = user_service;
        m_expService = exp_service;

        // View�� Presenter ����
        m_view.Inject(this);

        // Model �̺�Ʈ ����
        m_model.OnUpdatedHP = OnUpdatedHP;
        m_model.OnUpdatedThirst = OnUpdatedThirst;
        m_model.OnUpdatedHunger = OnUpdatedHunger;

        // UserService �̺�Ʈ ����
        m_userService.OnUpdatedLevel += OnUpdatedLevel;

        // �ʱ� ���� �ݿ�
        m_model.Initialize();
        m_userService.InitLevel();
    }

    private void OnUpdatedHP(float current, float max)
    {
        m_view.UpdateHP(current / max);
    }

    private void OnUpdatedThirst(float current, float max)
    {
        m_view.UpdateThirst(current / max);
    }

    private void OnUpdatedHunger(float current, float max)
    {
        m_view.UpdateHunger(current / max);
    }

    private void OnUpdatedLevel(int level, int exp)
    {
        int maxExp = m_expService.GetEXP(level);
        float expRate = maxExp > 0 ? (float)exp / maxExp : 0f;
        m_view.UpdateLV(level, expRate);
    }

    public void Dispose()
    {
        m_model.OnUpdatedHP = null;
        m_model.OnUpdatedThirst = null;
        m_model.OnUpdatedHunger = null;
        m_userService.OnUpdatedLevel -= OnUpdatedLevel;
    }
}
