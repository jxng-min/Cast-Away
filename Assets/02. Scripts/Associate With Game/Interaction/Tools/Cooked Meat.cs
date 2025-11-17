using UnityEngine;

public class CookedMeat : Hand
{
    protected override void OnRightUse()
    {
        PlaySFX("Drink");
        m_player_ctrl.State.ChangeHunger(20);
        m_inventory_service.UseItem(Index);
    }

    private void PlaySFX(string sfx_name)
    {
        SoundManager.Instance.PlaySFX("Drink", true, transform.position);
    }
}