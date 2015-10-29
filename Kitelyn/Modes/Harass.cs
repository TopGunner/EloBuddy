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

using Settings = Kitelyn.Config.Modes.Harass;

namespace Kitelyn.Modes
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
                return;

            if (Settings.UseQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                if (target != null && Q.GetPrediction(target).HitChance >= HitChance.High && DamageLibrary.GetSpellDamage(Player.Instance, target, SpellSlot.Q) > Player.Instance.GetAutoAttackDamage(target))
                {
                    Q.Cast(target);
                }
            }
            if (Settings.UseR && R.IsReady())
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Physical);
                if (target != null && target.Distance(Player.Instance) > 700 && Player.Instance.CountEnemiesInRange(650) == 0)
                {
                    R.Cast(target);
                }
            }
        }
    }
}
