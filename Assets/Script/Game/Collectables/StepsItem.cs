using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsItem : Collectable
{
    [SerializeField] private int amoutSteps;

    public override void Collect(PlayerPeace player)
    {
        player.GetExtraMove(amoutSteps);

        base.Collect(player);
    }

    public override void PlayEffect()
    {
        base.PlayEffect();
        ObjectPoolerSystem.SpawFromPool("GlowGreen", transform.position);
    }
}
