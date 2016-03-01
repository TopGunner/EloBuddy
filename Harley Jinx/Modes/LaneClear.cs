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

using Settings = HarleyJinx.Config.Modes.LaneClear;
namespace HarleyJinx.Modes
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
            if (Settings.mana >= Player.Instance.ManaPercent)
            {
                if (SpellManager.Fishbones)
                    Q.Cast();
                return;
            }

            if (Settings.UseQ && Q.IsReady() && !Orbwalker.IsAutoAttacking)
            {
                var lasthitminions = Orbwalker.LastHitMinionsList.Where(t => t.IsInRange(Player.Instance, SpellManager.Range));
                if (lasthitminions.Count() == 0)
                {
                    //no lasthit minions, continue with laneclear minions
                    lasthitminions.Concat(Orbwalker.LaneClearMinionsList.Where(t => t.IsInRange(Player.Instance, SpellManager.Range)));
                }
                if (lasthitminions.Count() == 0)
                {
                    SpellManager.toPowpow();
                    Orbwalker.ForcedTarget = null;
                }
                int count = 0;
                foreach (var m in lasthitminions)
                {
                    count = 1;
                    foreach (var m2 in Orbwalker.LaneClearMinionsList)
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
                        SpellManager.toFishBones();
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
                    SpellManager.toPowpow();
                    Orbwalker.ForcedTarget = null;
                }
            }
        }
    }
}
