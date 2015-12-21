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

using Settings = Anivia.Config.Modes.LaneClear;
namespace Anivia.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on laneclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            deactivateUlt();
            if (Settings.mana >= Player.Instance.ManaPercent)
                return;
            if (Settings.UseR && R.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState != 2)
            {
                var minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance.Position, Q.Range));
                if (minions != null && minions.Count() > 0)
                {
                    Vector3 middle = new Vector3();
                    foreach (var minion in minions)
                    {
                        middle.X += minion.Position.X;
                        middle.Y += minion.Position.Y;
                        middle.Z += minion.Position.Z;
                    }
                    middle.X /= minions.Count();
                    middle.Y /= minions.Count();
                    middle.Z /= minions.Count();

                    int counter = 0;
                    foreach (var minion in minions)
                    {
                        if (minion.IsInRange(middle, 400))
                            counter++;
                    }
                    if (counter >= 3)
                    {
                        R.Cast(middle);
                        SpellManager.RlastCast = middle;
                    }
                }
            }

            if (Settings.UseQ && Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 1)
            {
                var minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance.Position, Q.Range));
                foreach (var m in minions)
                {
                    if (Q.GetPrediction(m).CollisionObjects.Where(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable).Count() >= 4)
                    {
                        PermaActive.castedForChampion = false;
                        PermaActive.castedForMinions = true;
                        PermaActive.castedOn = null;
                        Q.Cast(m);
                        break;
                    }
                }
            }
            /*if (Settings.UseQ && Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 2)
            {
                int counter = 0;
                var minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance.Position, Q.Range));
                foreach (var e in minions)
                {
                    var missiles = ObjectManager.Get<MissileClient>().Where(missi => missi.SpellCaster.IsMe && missi.SData.AlternateName == "FlashFrostSpell");
                    foreach (var missile in missiles)
                    {
                        if (missile != null)
                        {
                            if (e.IsInRange(missile, 150))
                            {
                                counter++;
                            }
                        }
                    }
                    if (counter >= 4)
                    {
                        Q.Cast(e);
                        break;
                    }
                }
            }*/
            if (Settings.UseE && E.IsReady())
            {
                var minions = EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable && t.IsInRange(Player.Instance.Position, E.Range)).OrderByDescending(t => t.Health);
                foreach (var e in minions)
                {
                    foreach (BuffInstance b in e.Buffs)
                    {
                        if (b.Name == "chilled")
                        {
                            E.Cast(e);
                        }
                    }
                }
            }
        }


        private void deactivateUlt()
        {
            if (Anivia.Config.Modes.Combo.deactiveR && Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState == 2)
            {
                var minions = EntityManager.MinionsAndMonsters.GetJungleMonsters(SpellManager.RlastCast, 400);
                var enemies = EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsInRange(SpellManager.RlastCast, 395));
                if (minions.Count() < 1 && enemies.Count() < 1)
                {
                    R.Cast(Player.Instance);
                }
            }
        }
    }
}
