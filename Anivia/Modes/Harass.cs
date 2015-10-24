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

// Using the config like this makes your life easier, trust me
using Settings = Anivia.Config.Modes.Harass;

namespace Anivia.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on harass mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {

            if (Settings.Mana < Player.Instance.ManaPercent)
            {
                return;
            }
            ks();
            deactivateUlt();
            if (Settings.UseQ && Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 1)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (target != null && Q.GetPrediction(target).HitChance >= HitChance.Medium)
                {
                    Q.Cast(target);
                }
            }
            if (Settings.UseR && R.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState != 2)
            {
                var target = TargetSelector.GetTarget(R.Range, DamageType.Magical);
                if (target != null)
                {
                    R.Cast(target);
                    SpellManager.RlastCast = R.GetPrediction(target).UnitPosition;
                    if (E.IsReady() && Settings.UseE && target.IsValid() && target.IsValid && target.IsEnemy && !target.IsDead && !target.IsInvulnerable && !target.IsZombie && target.IsInRange(Player.Instance, E.Range))
                    {
                        E.Cast(target);
                    }
                    if (Settings.UseW && W.IsReady() && target.IsValid() && target.IsValid && target.IsEnemy && !target.IsDead && !target.IsInvulnerable && !target.IsZombie && target.IsInRange(Player.Instance, W.Range))
                    {
                        W.Cast(target);
                    }
                }
            }
            if (Settings.UseQ && Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 2)
            {
                var enemies = EntityManager.Heroes.Enemies.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsValid && !t.IsInvulnerable);
                foreach (var e in enemies)
                {
                    var missiles = ObjectManager.Get<MissileClient>().Where(missi => missi.SpellCaster.IsMe);
                    foreach (var missile in missiles)
                    {
                        if (missile != null && missile.SData.AlternateName == "FlashFrostSpell")
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
            }
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

        private void ks()
        {
            var enemy = EntityManager.Heroes.Enemies.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance.Position, E.Range) && DamageLibrary.GetSpellDamage(Player.Instance, t, SpellSlot.E) > t.Health).FirstOrDefault();
            if (enemy != null)
                if (E.IsReady())
                {
                    E.Cast(enemy);
                }
        }

        private void deactivateUlt()
        {
            if (Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState == 2)
            {
                var enemies = EntityManager.Heroes.Enemies.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsInRange(SpellManager.RlastCast, 220));
                if (enemies.Count() < 1)
                {
                    R.Cast(Player.Instance);
                }
            }
        }
    }
}
