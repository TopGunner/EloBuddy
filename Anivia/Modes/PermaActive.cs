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

using Settings = Anivia.Config.Misc;

namespace Anivia.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public static bool castedForChampion = false;
        public static bool castedForMinions = false;
        public static AIHeroClient castedOn = null;
        public static MissileClient missile = null;
        Vector2 perfectCast = new Vector2(0,0);

        bool stackingTear = false;
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
            cleanseMe();
            stackTear();
            stopStackMode();
            skinChanger();
            missileTracker();
        }

        private void missileTracker()
        {
            if (missile == null || !Settings.activateQ || missile.IsDead)
            {
                missile = null;
                return;
            }
            Vector2 posi = missile.Position.Extend(missile.EndPosition, Q.Speed * (Game.Ping / 500));
            if (!castedForChampion && !castedForMinions)
            {
                foreach (var e in EntityManager.Heroes.Enemies.Where(t => Prediction.Position.Collision.LinearMissileCollision(t, missile.StartPosition.To2D(), missile.StartPosition.Extend(missile.EndPosition, Q.Range), Q.Speed, Q.Width, Q.CastDelay)).OrderBy(t => t.MaxHealth))
                {
                    castedOn = e;
                    castedForChampion = true;
                    break;
                }
                if (!castedForChampion)
                    castedForMinions = true;
            }
            if (castedForChampion)
            {
                if (castedOn == null)
                    return;

                /*if (Prediction.Position.Collision.LinearMissileCollision(castedOn, missile.StartPosition.To2D(), missile.StartPosition.Extend(missile.EndPosition, Q.Range), Q.Speed, Q.Width, Q.CastDelay))
                {
                    if (missile.StartPosition.Distance(castedOn) <= missile.StartPosition.Distance(posi) - 50)
                    {
                        Q.Cast(castedOn);
                        missile = null;
                        castedForChampion = false;
                        castedForMinions = false;
                        castedOn = null;
                        perfectCast = new Vector2(0, 0);
                    }
                }
                else */if (Prediction.Position.Collision.LinearMissileCollision(castedOn, missile.StartPosition.To2D(), missile.StartPosition.Extend(missile.EndPosition, Q.Range), Q.Speed, 150, Q.CastDelay))
                {
                    if (posi.Distance(castedOn) < 150)
                    {
                        Q.Cast(castedOn);
                        missile = null;
                        castedForChampion = false;
                        castedForMinions = false;
                        castedOn = null;
                        perfectCast = new Vector2(0, 0);
                    }
                }
                if (missile != null && missile.Position.CountEnemiesInRange(150) > 0)
                {
                    Q.Cast(castedOn);
                    missile = null;
                    castedForChampion = false;
                    castedForMinions = false;
                    castedOn = null;
                    perfectCast = new Vector2(0, 0);
                }
            }
            else
            {
                //Casted for farming issues
                Vector2 pos = posi;
                int count = 0;
                int i = 0;
                while (pos.Distance(missile.EndPosition) > Settings.accuracyQ && !missile.IsDead)
                {
                    int amount = 0;
                    foreach (var e in EntityManager.MinionsAndMonsters.CombinedAttackable.Where(t => t.IsInRange(pos, 150) && t.HealthPercent > 0))
                    {
                        amount++;
                    }
                    if (amount >= count)
                    {
                        perfectCast = pos;
                        count = amount;
                    }
                    i++;
                    pos = posi.Extend(missile.EndPosition, Settings.accuracyQ * i);
                }
                if (perfectCast.Distance(posi) <= Settings.accuracyQ*2)
                {
                    Q.Cast(Player.Instance);
                    missile = null;
                    castedForChampion = false;
                    castedForMinions = false;
                    castedOn = null;
                    perfectCast = new Vector2(0, 0);
                }
            }
        }

        private void autoBuyStartingItems()
        {
            
            if (bought || ticks / Game.TicksPerSecond < 5)
            {
                ticks++;
                return;
            }

            bought = true;
            if (Settings.autoBuyStartingItems)
            {
                if (Game.MapId == GameMapId.SummonersRift)
                {
                    Shop.BuyItem(ItemId.Dorans_Ring);
                    Shop.BuyItem(ItemId.Health_Potion);
                    Shop.BuyItem(ItemId.Health_Potion);
                    Shop.BuyItem(ItemId.Warding_Totem_Trinket);
                }
            }
        }

        private void cleanseMe()
        {
            if (Settings.cleanseStun && cleanse != null)
            {
                if(Player.HasBuff("PoppyDiplomaticImmunity") || Player.HasBuff("MordekaiserChildrenOfTheGrave") || Player.HasBuff("FizzMarinerDoom") || Player.HasBuff("VladimirHemoplague") || 
                        Player.HasBuff("zedulttargetmark") || Player.HasBuffOfType(BuffType.Suppression) || Player.HasBuffOfType(BuffType.Charm) || Player.HasBuffOfType(BuffType.Flee) || Player.HasBuffOfType(BuffType.Blind) || 
                        Player.HasBuffOfType(BuffType.Polymorph) || Player.HasBuffOfType(BuffType.Snare) || Player.HasBuffOfType(BuffType.Stun) || Player.HasBuffOfType(BuffType.Taunt))
                {
                    if (Player.Instance.CountEnemiesInRange(1000) >= Settings.cleanseEnemies)
                    {
                        if (cleanse.IsReady())
                        {
                            cleanse.Cast();
                        }
                    }
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

        private void stopStackMode()
        {
            if (!Settings.tearStack || !this.stackingTear)
            {
                return;
            }
            if (!Player.Instance.IsInShopRange() && Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState == 2)
            {
                if (R.IsReady() && R.IsLearned)
                {
                    R.Cast(Player.Instance);
                    this.stackingTear = false;
                }
            }
        }

        private void stackTear()
        {
            if (Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState == 2)
                return;

            if (60 > Player.Instance.ManaPercent)
            {
                return;
            }
            if (!Settings.tearStack || this.stackingTear || !Player.Instance.IsInShopRange() || Game.MapId == GameMapId.HowlingAbyss)
            {
                return;
            }
            InventorySlot[] inv = Player.Instance.InventoryItems;
            foreach (var item in inv)
            {
                if (item.Id == ItemId.Archangels_Staff || item.Id == ItemId.Archangels_Staff_Crystal_Scar || item.Id == ItemId.Tear_of_the_Goddess || item.Id == ItemId.Tear_of_the_Goddess_Crystal_Scar)
                {
                    if (item.Charges < 700)
                    {
                        if (R.IsReady() && R.IsLearned)
                        {
                            R.Cast(Player.Instance);
                            this.stackingTear = true;
                        }
                    }
                }
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
                int[] leveler = new int[] { 1, 3, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2 };
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

        internal static void Dash_OnDash(Obj_AI_Base sender, Dash.DashEventArgs e)
        {
            Spell.Skillshot W = SpellManager.W;
            if (Settings.antiDash && W.IsReady() && sender.IsValid && sender.IsEnemy && !sender.IsDead && !sender.IsInvulnerable && !sender.IsZombie && sender.IsInRange(Player.Instance, W.Range))
            {
                if (Player.Instance.Distance(e.EndPos) < Player.Instance.Distance(e.StartPos))
                    W.Cast(sender);
                else if (Settings.antiDashOffensive)
                    W.Cast(sender);
            }
        }

        internal static void antiGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            Spell.Skillshot W = SpellManager.W;
            if (Settings.antiDash && W.IsReady() && sender.IsValid && sender.IsEnemy && !sender.IsDead && !sender.IsInvulnerable && !sender.IsZombie && e.End.IsInRange(Player.Instance, W.Range))
            {
                if (Player.Instance.Distance(e.End) < Player.Instance.Distance(e.Start))
                    W.Cast(e.End);
                else if (Settings.antiDashOffensive)
                    W.Cast(e.End);
            }
        }

        internal static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            bool isMissile = sender.GetType() == typeof(MissileClient);
            if (!isMissile)
                return;

            var miss = sender as MissileClient;
            if (!miss.IsValidMissile() || miss.SpellCaster.NetworkId != Player.Instance.NetworkId || miss.SData.Name != "FlashFrostSpell")
            {
                return;
            }
            missile = miss;
        }
    }
}
