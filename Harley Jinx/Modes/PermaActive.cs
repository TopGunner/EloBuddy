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

using Settings = HarleyJinx.Config.Misc;

namespace HarleyJinx.Modes
{
    public sealed class PermaActive : ModeBase
    {
        int currentSkin = 0;
        bool bought = false;
        int ticks = 0;
        public override bool ShouldBeExecuted()
        {
            // Since this is permaactive mode, always execute the loop
            return true;
        }

        public override void Execute()
        {
            autoBuyStartingItems();
            skinChanger();
            castQSS();
            ks();
            EOnImmobile();
            BaronDrakeSteal();
        }

        private void BaronDrakeSteal()
        {
            if (!R.IsReady())
                return;
            if (Settings.StealDrake)
            {
                Obj_AI_Base drake = EntityManager.MinionsAndMonsters.Monsters.Where(t => t.Name.Contains("Dragon")).FirstOrDefault();
                steal(drake);
            }
            if (Settings.StealBaron)
            {
                Obj_AI_Base baron = EntityManager.MinionsAndMonsters.Monsters.Where(t => t.Name.Contains("Baron")).FirstOrDefault();
                steal(baron);
            }

        }

        private void steal(Obj_AI_Base monster)
        {
            if (monster == null || monster.CountEnemiesInRange(600) == 0 || monster.CountAlliesInRange(500) > 0)
                return;
            float distance = Player.Instance.Position.Distance(monster.Position);
            int travelTime = 0;
            if (distance > 1500)
            {
                travelTime = 1000;
                distance -= 1500;
            }
            else
            {
                travelTime = (int)distance / 1500 * 1000;
                distance = 0;
            }
            travelTime += (int)(distance / 2500) * 1000;
            float health = Prediction.Health.GetPrediction(monster, travelTime);
            if (health - 300 > 0 && health < SpellManager.getRDamage(monster) * 0.8)
            {
                R.Cast(monster);
            }
        }


        private void EOnImmobile()
        {
            if(Settings.EOnImmobileEnemy)
            {
                foreach (var e in EntityManager.Heroes.Enemies.Where(t => t.HealthPercent > 0 && !t.IsZombie && t.IsInRange(Player.Instance, E.Range)).OrderBy(t => t.MaxHealth))
                {
                    if (e.GetMovementBlockedDebuffDuration() > E.CastDelay)
                    {
                        E.Cast(e);
                        return;
                    }
                }
            }
            if (Settings.EOnSlowedEnemy)
            {
                foreach (var e in EntityManager.Heroes.Enemies.Where(t => t.HealthPercent > 0 && !t.IsZombie && t.IsInRange(Player.Instance, E.Range)).OrderBy(t => t.MaxHealth))
                {
                    if (e.GetMovementReducedDebuffDuration() > E.CastDelay)
                    {
                        E.Cast(E.GetPrediction(e).CastPosition);
                        return;
                    }
                }
            }
        }

        private void ks()
        {
            foreach (var enemy in EntityManager.Heroes.Enemies.Where(target => target.IsVisible && target.HealthPercent > 0 && !target.IsInvulnerable && target.IsEnemy && !target.IsPhysicalImmune && !target.IsZombie))
            {
                if (enemy.IsInRange(Player.Instance, W.Range) && Settings.ksW && W.IsReady())
                {
                    if (Prediction.Health.GetPrediction(enemy, W.CastDelay) < DamageLibrary.GetSpellDamage(Player.Instance, enemy, SpellSlot.W))
                    {
                        W.Cast(W.GetPrediction(enemy).CastPosition);
                        return;
                    }
                }
                if (enemy.IsInRange(Player.Instance, R.Range) && Settings.ksR && R.IsReady())
                {
                    Console.WriteLine("dmg" + SpellManager.getRDamage(enemy));
                    if (enemy.Health < SpellManager.getRDamage(enemy) && Settings.ksR && R.IsReady() && SpellManager.RoverkillCheck(enemy))
                    {
                        R.Cast(R.GetPrediction(enemy).CastPosition);
                    }
                }
            }
        }



        private bool castQSS()
        {
            if (!Settings.useQSS)
                return false;
            if (Player.Instance.IsDead || Player.Instance.IsInvulnerable || !Player.Instance.IsTargetable || Player.Instance.IsZombie || Player.Instance.IsInShopRange())
                return false;
            InventorySlot[] inv = Player.Instance.InventoryItems;
            foreach (var item in inv)
            {
                if ((item.Id == ItemId.Quicksilver_Sash || item.Id == ItemId.Mercurial_Scimitar) && item.CanUseItem() && Player.Instance.CountEnemiesInRange(700) > 0)
                {
                    if(Player.HasBuff("PoppyDiplomaticImmunity") || Player.HasBuff("MordekaiserChildrenOfTheGrave") || Player.HasBuff("FizzMarinerDoom") || Player.HasBuff("VladimirHemoplague") || 
                        Player.HasBuff("zedulttargetmark") || Player.HasBuffOfType(BuffType.Suppression) || Player.HasBuffOfType(BuffType.Charm) || Player.HasBuffOfType(BuffType.Flee) || Player.HasBuffOfType(BuffType.Blind) || 
                        Player.HasBuffOfType(BuffType.Polymorph) || Player.HasBuffOfType(BuffType.Snare) || Player.HasBuffOfType(BuffType.Stun) || Player.HasBuffOfType(BuffType.Taunt))
                    {
                        Core.DelayAction(() => item.Cast(), 110);
                        return true;
                    }
                }
            }
            return false;
        }

        private void autoBuyStartingItems()
        {
            if (bought || ticks / Game.TicksPerSecond < 3)
            {
                ticks++;
                return;
            }
            bought = true;
            if (Settings.autoBuyStartingItems)
            {
                if (Game.MapId == GameMapId.SummonersRift)
                {
                    Shop.BuyItem(ItemId.Dorans_Blade);
                    Shop.BuyItem(ItemId.Health_Potion);
                    Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                }
            }
        }

        private void skinChanger()
        {
            if (!Settings.UseSkinHack)
                return;
            if (Settings.skinId != currentSkin)
            {
                Player.Instance.SetSkinId(Settings.skinId);
                this.currentSkin = Settings.skinId;
            }
        }

        internal static void autoLevelSkills(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args)
        {
            if (Settings.autolevelskills)
            {
                if (!sender.IsMe || args.Level > 17)
                {
                    return;
                }
                int[] leveler = new int[] { 1, 2, 3, 1, 1, 4, 1, 2, 1, 2, 4, 2, 2, 3, 3, 4, 3, 3 };
                int skill = leveler[Player.Instance.Level];

                if (skill == 1)
                    Player.Instance.Spellbook.LevelSpell(SpellSlot.Q);
                else if (skill == 2)
                    Player.Instance.Spellbook.LevelSpell(SpellSlot.W);
                else if (skill == 3)
                    Player.Instance.Spellbook.LevelSpell(SpellSlot.E);
                else if (skill == 4)
                    Player.Instance.Spellbook.LevelSpell(SpellSlot.R);
                else
                    return;
            }
        }
    }
}
