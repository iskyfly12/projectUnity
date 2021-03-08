using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItem : Collectable
{
    [SerializeField] private int amoutLife;

    public override void Collect(PlayerPeace player)
    {
        player.GetExtraLife(amoutLife);

        base.Collect(player);
    }

    public override void PlayEffect()
    {
        base.PlayEffect();
        ObjectPoolerSystem.SpawFromPool("GlowRed", transform.position);
    }
}
