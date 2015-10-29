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
using Settings = Kitelyn.Config.Modes.Combo;

namespace Kitelyn.Modes
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
            if (Settings.UseQ && Q.IsReady())
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                if (target != null && Q.GetPrediction(target).HitChance >= HitChance.High && DamageLibrary.GetSpellDamage(Player.Instance, target, SpellSlot.Q) > Player.Instance.GetAutoAttackDamage(target) && Player.Instance.Level < 11)
                {
                    Q.Cast(target);
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
            castE();
        }
 
        private void castE()
        {
            if (Settings.UseE && E.IsReady())
            {
                var enemies = EntityManager.Heroes.Enemies.Where(t => t.IsInRange(Player.Instance, 200) && !t.IsRanged && t.IsValid && t.IsTargetable && !t.IsInvulnerable && !t.IsDead);
                AIHeroClient bestShot = new AIHeroClient();
                if(enemies != null && enemies.Count() > 0)
                    bestShot = enemies.First();
                int enemiesInRange = Player.Instance.CountEnemiesInRange(400);
                int enemiesInLargeRange = Player.Instance.CountEnemiesInRange(1000);
                Vector3 newPos = new Vector3(0,0,0);
                foreach (var e in enemies)
                {
                    var pos = Player.Instance.Position.Extend(e, -400).To3D();
                    if (pos.CountEnemiesInRange(400) <= enemiesInRange && pos.CountEnemiesInRange(1000) <= enemiesInLargeRange)
                    {
                        if (!intowerrange(pos))
                        {
                            E.Cast(e);
                            return;
                        }
                    }
                }
            }
        }

        private bool intowerrange(Vector3 pos)
        {
            foreach (var t in EntityManager.Turrets.Enemies)
            {
                if (pos.IsInRange(t, 775))
                {
                    return true;
                }
            }
            return false;
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
