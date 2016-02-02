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

using Settings = Anivia.Config.Modes.JungleClear;

namespace Anivia.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            deactivateUlt();
            if (Settings.mana >= Player.Instance.ManaPercent)
                return;
            if (Settings.UseR && R.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState != 2)
            {
                var minions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, R.Range).Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable);
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
                    if (counter >= minions.Count() - 1)
                    {
                        R.Cast(middle);
                        SpellManager.RlastCast = middle;
                    }
                }
            }

            if (Settings.UseQ && Q.IsReady() && Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 1)
            {
                var minions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, R.Range).Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable).OrderByDescending(t => t.MaxHealth);
                foreach (var m in minions)
                {
                    if (Q.GetPrediction(m).CollisionObjects.Where(t => t.IsEnemy && !t.IsDead && t.IsValid && !t.IsInvulnerable).Count() >= minions.Count() - 2)
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
                var minions = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, R.Range).Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable);
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
                    if (counter >= minions.Count())
                    {
                        Q.Cast(e);
                        break;
                    }
                }
            }*/
            if (Settings.UseE && E.IsReady())
            {
                var minion = EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position, Q.Range).Where(t => !t.IsDead && t.IsValid && !t.IsInvulnerable).OrderByDescending(t => t.MaxHealth).FirstOrDefault();
                if (minion != null)
                {
                    foreach (BuffInstance b in minion.Buffs)
                    {
                        if (b.Name == "chilled")
                        {
                            E.Cast(minion);
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
                var enemies = EntityManager.MinionsAndMonsters.EnemyMinions.Where(t => t.IsEnemy && !t.IsZombie && !t.IsDead && t.IsInRange(SpellManager.RlastCast, 400));
                if (minions.Count() < 1 && enemies.Count() < 1)
                {
                   
                    R.Cast(Player.Instance);
                }
            }
        }
    }
}