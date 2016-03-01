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
using Settings = HarleyJinx.Config.Modes.Combo;

namespace HarleyJinx.Modes
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
            if (Orbwalker.IsAutoAttacking)
                return;
            if (Settings.UseQ && Q.IsReady())
            {
                if (Player.Instance.CountEnemiesInRange(SpellManager.PowpowRange) > 0)
                {
                    //champion in powpowrange
                    SpellManager.toPowpow();
                }
                else if (Player.Instance.CountEnemiesInRange(SpellManager.FishbonesRange) > 0)
                {
                    //enemy in fishbones range
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
            if (Settings.UseE && E.IsReady())
            {
                var target = TargetSelector.GetTarget(SpellManager.E.Range, DamageType.Physical);
                var pred = E.GetPrediction(target);
                if (target != null && pred.HitChance >= HitChance.High)
                {
                    E.Cast(pred.CastPosition);
                }
            }
            if (Settings.useBOTRK)
            {
                if (!castBOTRK())
                    castBilgewater();
            }
            if (Settings.useYOUMOUS)
            {
                castYoumous();
            }
            getVision();
        }

        private void getVision()
        {
            if (Player.Instance.IsDead || Player.Instance.IsInvulnerable || !Player.Instance.IsTargetable || Player.Instance.IsZombie || Player.Instance.IsInShopRange())
                return;
            else if (Settings.useTrinketVision && Program.lastTarget != null && Program.lastTarget.Position.Distance(Player.Instance) < 600 && Game.Time - Program.lastSeen > 2)
            {
                InventorySlot[] inv = Player.Instance.InventoryItems;
                foreach (var item in inv)
                {
                    if (item.Id == ItemId.Greater_Stealth_Totem_Trinket || item.Id == ItemId.Greater_Vision_Totem_Trinket || item.Id == ItemId.Warding_Totem_Trinket )
                    {
                        item.Cast(Program.predictedPos);
                    }
                }
            }
            else if (Settings.useWardVision && Program.lastTarget != null && Program.lastTarget.Position.Distance(Player.Instance) < R.Range && Game.Time - Program.lastSeen > 2)
            {
                InventorySlot[] inv = Player.Instance.InventoryItems;
                foreach (var item in inv)
                {
                    if (item.Id == ItemId.Vision_Ward)
                    {
                        item.Cast(Program.predictedPos);
                    }
                }
            }
        }

        private bool castYoumous()
        {
            if (Player.Instance.IsDead || !Player.Instance.CanCast || Player.Instance.IsInvulnerable || !Player.Instance.IsTargetable || Player.Instance.IsZombie || Player.Instance.IsInShopRange())
                return false;
            InventorySlot[] inv = Player.Instance.InventoryItems;
            foreach (var item in inv)
            {
                if ((item.Id == ItemId.Youmuus_Ghostblade) && item.CanUseItem() && Player.Instance.CountEnemiesInRange(700) > 0)
                {
                    return item.Cast();
                }
            }
            return false;
        }

        private bool castBilgewater()
        {
            if (Player.Instance.IsDead || !Player.Instance.CanCast || Player.Instance.IsInvulnerable || !Player.Instance.IsTargetable || Player.Instance.IsZombie || Player.Instance.IsInShopRange())
                return false;
            InventorySlot[] inv = Player.Instance.InventoryItems;
            foreach (var item in inv)
            {
                if ((item.Id == ItemId.Bilgewater_Cutlass) && item.CanUseItem())
                {
                    var target = TargetSelector.GetTarget(550, DamageType.Magical);
                    if (target != null)
                        return item.Cast(target);
                }
            }
            return false;
        }

        private bool castBOTRK()
        {
            if (Player.Instance.IsDead || !Player.Instance.CanCast || Player.Instance.IsInvulnerable || !Player.Instance.IsTargetable || Player.Instance.IsZombie || Player.Instance.IsInShopRange())
                return false;
            InventorySlot[] inv = Player.Instance.InventoryItems;
            foreach (var item in inv)
            {
                if ((item.Id == ItemId.Blade_of_the_Ruined_King) && item.CanUseItem())
                {
                    var target = TargetSelector.GetTarget(550, DamageType.Physical);
                    if(target != null && Player.Instance.Health <= DamageLibrary.GetItemDamage(Player.Instance, target, ItemId.Blade_of_the_Ruined_King))
                        return item.Cast(target);
                }
            }
            return false;
        }
    }
}
