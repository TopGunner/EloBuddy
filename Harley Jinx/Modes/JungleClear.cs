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

using Settings = HarleyJinx.Config.Modes.JungleClear;

namespace HarleyJinx.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (Settings.mana >= Player.Instance.ManaPercent)
            {
                SpellManager.toPowpow();
                return;
            }

            if (Settings.UseQ && Q.IsReady())
            {
                var lasthitminions = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(t => t.IsInRange(Player.Instance, SpellManager.Range)).OrderByDescending(t => t.MaxHealth);
                if (lasthitminions.Count() == 0)
                {
                    if (SpellManager.Fishbones)
                        Q.Cast();
                    Orbwalker.ForcedTarget = null;
                }
                int count = 0;
                foreach (var m in lasthitminions)
                {
                    count = 1;
                    foreach (var m2 in lasthitminions)
                    {
                        if (m.Distance(m2) < 150)
                        {
                            count++;
                        }
                    }
                    //hitting m with Fisbones is gonna blow up *count* minions

                    //worth Fishbones
                    if (SpellManager.Powpow && Orbwalker.ForcedTarget == null && count >= Settings.minMinionsToFishbones)
                    {
                        Q.Cast();
                        Orbwalker.ForcedTarget = m;
                        return;
                    }
                    if (SpellManager.Fishbones && Orbwalker.ForcedTarget == null && count >= Settings.minMinionsToFishbones)
                    {
                        Orbwalker.ForcedTarget = m;
                        return;
                    }
                }
                //no fishbones worthy minion
                if (SpellManager.Fishbones && Orbwalker.ForcedTarget == null && count < Settings.minMinionsToFishbones)
                {
                    Q.Cast();
                    Orbwalker.ForcedTarget = null;
                }
            }

            if (Settings.UseW && W.IsReady())
            {
                var e = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(t => t.Health < DamageLibrary.GetSpellDamage(Player.Instance, t, SpellSlot.W) && t.IsInRange(Player.Instance, W.Range)).OrderByDescending(t => t.MaxHealth).FirstOrDefault();
                if (e != null)
                {
                    W.Cast(e);
                }

                e = EntityManager.MinionsAndMonsters.GetJungleMonsters().Where(t => !t.IsInRange(Player.Instance, SpellManager.Range) && t.IsInRange(Player.Instance, W.Range)).OrderByDescending(t => t.MaxHealth).FirstOrDefault();
                if (e != null)
                {
                    W.Cast(e);
                }
            }
        }
    }
}