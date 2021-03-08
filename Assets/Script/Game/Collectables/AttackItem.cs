using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItem : Collectable
{
    [SerializeField] private int amoutAttack;

    public override void Collect(PlayerPeace player)
    {
        player.GetExtraAttack(amoutAttack);

        base.Collect(player);
    }

    public override void PlayEffect()
    {
        base.PlayEffect();
        ObjectPoolerSystem.SpawFromPool("GlowBlue", transform.position);
    }
}
