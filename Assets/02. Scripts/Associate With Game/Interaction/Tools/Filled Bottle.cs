using UnityEngine;

public class FilledBottle : Hand
{
    protected override void OnRightUse()
    {
        PlaySFX("Drink");
        m_player_ctrl.State.ChangeThirst(20);
        m_inventory_service.UseItem(Index);
    }

    private void PlaySFX(string sfx_name)
    {
        SoundManager.Instance.PlaySFX("Drink", true, transform.position);
    }
}