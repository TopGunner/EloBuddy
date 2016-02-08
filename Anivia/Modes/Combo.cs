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
using Settings = Anivia.Config.Modes.Combo;

namespace Anivia.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on combo mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            // TODO: Add combo logic here
            // See how I used the Settings.UseQ here, this is why I love my way of using
            // the menu in the Config class!
            ks();
            deactivateUlt();
            if (Settings.UseQ && Q.IsReady() && PermaActive.missile == null && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 1)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                HitChance h = HitChance.Medium;
                if (Settings.HitchanceQ == 1)
                    h = HitChance.Low;
                else if (Settings.HitchanceQ == 2)
                    h = HitChance.Medium;
                else if (Settings.HitchanceQ == 3)
                    h = HitChance.High;
                if (target != null && Q.GetPrediction(target).HitChance >= h)
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
                    if (E.IsReady() && Settings.UseE && target.IsValid && target.IsEnemy && !target.IsDead && !target.IsInvulnerable && !target.IsZombie && target.IsInRange(Player.Instance, E.Range))
                    {
                        E.Cast(target);
                    }
                    R.Cast(target);
                    SpellManager.RlastCast = R.GetPrediction(target).UnitPosition;
                    if (Settings.UseW && W.IsReady() && target.IsValid && target.IsEnemy && !target.IsDead && !target.IsInvulnerable && !target.IsZombie && target.IsInRange(Player.Instance, W.Range))
                    {
                        W.Cast(target);
                    }
                }
            }
            /*if (Settings.UseQ && Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 2)
            {
                casted = false;
                var enemies = EntityManager.Heroes.Enemies.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance, 1500));
                foreach(var e in enemies)
                {
                    if (e == null)
                        continue;
                    var missiles = ObjectManager.Get<MissileClient>().Where(missi => missi.SpellCaster.IsMe && missi.SData.AlternateName == "FlashFrostSpell");
                    foreach (var missile in missiles)
                    {
                        if (missile != null)
                        {
                            if (e.Distance(missile) <= 150)
                            {
                                Q.Cast(e);
                                if (E.IsReady() && Settings.UseE && e.IsValid && e.IsEnemy && !e.IsDead && !e.IsInvulnerable && !e.IsZombie && e.IsInRange(Player.Instance, E.Range))
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
                    if (Settings.UseEDoubleOnly)
                    {
                        foreach (BuffInstance b in target.Buffs)
                        {
                            if (b.Name == "chilled")
                            {
                                E.Cast(target);
                            }
                        }
                    }
                    else
                    {
                        E.Cast(target);
                    }
                }
            } 
            if (Settings.UseWOnEscapingEnemies && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Magical);
                if (target.IsValid && target.IsEnemy && !target.IsDead && !target.IsInvulnerable && !target.IsZombie && !target.IsInRange(Player.Instance, W.Range-200) && target.IsInRange(Player.Instance, W.Range-50))
                {
                    W.Cast(Prediction.Position.PredictUnitPosition(target, 100).To3D());
                }
            }
        }

        private void ks()
        {
            if (Settings.ksE && Settings.ksI && E.IsReady() && Ignite != null && Ignite.IsReady())
            {
                var enemy = EntityManager.Heroes.Enemies.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance.Position, Ignite.Range) && t.IsInRange(Player.Instance.Position, E.Range) && DamageLibrary.GetSummonerSpellDamage(Player.Instance, t, DamageLibrary.SummonerSpells.Ignite) + DamageLibrary.GetSpellDamage(Player.Instance, t, SpellSlot.E) > t.Health).FirstOrDefault();
                if (enemy != null)
                {
                    E.Cast(enemy);
                    Ignite.Cast(enemy);
                    return;
                }
                
            }
            if (Settings.ksE && E.IsReady())
            {
                var enemy = EntityManager.Heroes.Enemies.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance.Position, E.Range) && DamageLibrary.GetSpellDamage(Player.Instance, t, SpellSlot.E) > Prediction.Health.GetPrediction(t, 250)).FirstOrDefault();
                if(enemy != null)
                    E.Cast(enemy);
            }
            if (Settings.ksI && Ignite != null && Ignite.IsReady())
            {
                var enemy = EntityManager.Heroes.Enemies.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance.Position, Ignite.Range) && DamageLibrary.GetSummonerSpellDamage(Player.Instance, t, DamageLibrary.SummonerSpells.Ignite) > t.Health).FirstOrDefault();
                if (enemy != null)
                    Ignite.Cast(enemy);
            }
        }

        private void deactivateUlt()
        {
            if (Settings.deactiveR && Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState == 2)
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
