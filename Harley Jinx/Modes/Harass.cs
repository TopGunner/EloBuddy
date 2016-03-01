using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

using Settings = HarleyJinx.Config.Modes.Harass;

namespace HarleyJinx.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            if (Settings.Mana >= Player.Instance.ManaPercent)
            {
                SpellManager.toPowpow();
                return;
            }
            if (Orbwalker.IsAutoAttacking)
                return;

            if (Settings.UseQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(SpellManager.FishbonesRange, DamageType.Physical);
                if (target == null)
                    return;

                if (Orbwalker.LastHitMinionsList.Where(t => t.IsInRange(Player.Instance, SpellManager.PowpowRange)).Count() > 0)
                {
                    //Minion to lasthit with powpow
                    SpellManager.toPowpow();
                }
                else if (Orbwalker.LastHitMinionsList.Where(t => t.IsInRange(Player.Instance, SpellManager.FishbonesRange)).Count() > 0)
                {
                    //minion to lasthit with fishbones
                    SpellManager.toFishBones();
                }
                else if (Player.Instance.CountEnemiesInRange(SpellManager.PowpowRange) > 0)
                {
                    //no minions to lasthit AND champion in powpowrange
                    SpellManager.toPowpow();
                }
                else if (Player.Instance.CountEnemiesInRange(SpellManager.FishbonesRange) > 0)
                {
                    //no minions to lasthit and enemy in fishbones range
                    SpellManager.toFishBones();
                }
            }
            if (Settings.UseW && W.IsReady())
            {
                var target = TargetSelector.GetTarget(SpellManager.W.Range, DamageType.Physical);
                var pred = W.GetPrediction(target);
                if (target != null && pred.HitChance >= HitChance.High && pred.GetCollisionObjects<Obj_AI_Base>()[0].NetworkId == target.NetworkId)
                {
                    W.Cast(pred.CastPosition);
                }
            }
        }
    }
}
