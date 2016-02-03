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

using Settings = Anivia.Config.Modes.Harass;

namespace Anivia.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            deactivateUlt();
            if (Settings.Mana >= Player.Instance.ManaPercent)
                return;

            if (Settings.UseQ && Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 1 && PermaActive.missile == null)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target != null && Q.GetPrediction(target).HitChance >= HitChance.High)
                {
                    PermaActive.castedForChampion = true;
                    PermaActive.castedForMinions = false;
                    PermaActive.castedOn = target;
                    Q.Cast(Q.GetPrediction(target).CastPosition);
                }
            }
            if (Settings.UseR && R.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState != 2)
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                if (target != null)
                {
                    if (E.IsReady() && Settings.UseE && target.IsValid() && target.IsValid && target.IsEnemy && !target.IsDead && !target.IsInvulnerable && !target.IsZombie && target.IsInRange(Player.Instance, E.Range))
                    {
                        E.Cast(target);
                    }
                    R.Cast(target);
                    SpellManager.RlastCast = R.GetPrediction(target).UnitPosition;
                    if (Settings.UseW && W.IsReady() && target.IsValid() && target.IsValid && target.IsEnemy && !target.IsDead && !target.IsInvulnerable && !target.IsZombie && target.IsInRange(Player.Instance, W.Range))
                    {
                        W.Cast(target);
                    }
                }
            }
            /*if (Settings.UseQ && Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 2)
            {
                var enemies = EntityManager.Heroes.Enemies.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance, 1500));
                foreach (var e in enemies)
                {
                    var missiles = ObjectManager.Get<MissileClient>().Where(missi => missi.SpellCaster.IsMe && missi.SData.AlternateName == "FlashFrostSpell");
                    foreach (var missile in missiles)
                    {
                        if (missile != null)
                        {
                            if (e.IsInRange(missile, 150))
                            {
                                Q.Cast(e);
                                if (E.IsReady() && Settings.UseE && e.IsValid() && e.IsValid && e.IsEnemy && !e.IsDead && !e.IsInvulnerable && !e.IsZombie && e.IsInRange(Player.Instance, E.Range))
                                {
                                    E.Cast(e);
                                }
                            }
                        }
                    }
                }
            }*/
            if (Settings.UseE && E.IsReady())
            {
                var target = TargetSelector.GetTarget(E.Range, DamageType.Magical);
                if (target != null)
                {
                    foreach (BuffInstance b in target.Buffs)
                    {
                        if (b.Name == "chilled")
                        {
                            E.Cast(target);
                        }
                    }
                }
            }
        }

        private void deactivateUlt()
        {
            if (Anivia.Config.Modes.Combo.deactiveR && Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState == 2)
            {
                var enemies = EntityManager.Heroes.Enemies.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsInRange(SpellManager.RlastCast, 400));
                if (enemies.Count() < 1)
                {
                    R.Cast(Player.Instance);
                }
            }
        }
    }
}
