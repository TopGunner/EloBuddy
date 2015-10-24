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
        bool stackingTear = true;
        int currentSkin = 0;
        public override bool ShouldBeExecuted()
        {
            // Since this is permaactive mode, always execute the loop
            return true;
        }

        public override void Execute()
        {
            cleanseMe();
            stackTear();
            stopStackMode();
            skinChanger();
            //TODO W CAST
        }

        private void cleanseMe()
        {
            return;
            /******** IsStunned Property not Working - WOULD CLEANSE KNOCKUP **********
            if (Settings.cleanseStun && cleanse != null)
            {
                if(Player.Instance.IsFeared|| Player.Instance.IsStunned|| Player.Instance.IsCharmed)
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
            */
        }

        private void skinChanger()
        {
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
                R.Cast(Player.Instance);
            }
            this.stackingTear = false;
        }

        private void stackTear()
        {
            if (Player.Instance.Spellbook.GetSpell(SpellSlot.R).ToggleState == 2)
                return;

            if (60 > Player.Instance.ManaPercent)
            {
                return;
            }
            if (!Settings.tearStack || !Player.Instance.IsInShopRange() || Game.MapId == GameMapId.HowlingAbyss)
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
                        R.Cast(Player.Instance);
                        this.stackingTear = true;
                    }
                }
            }

        }

        internal static void autoLevelSkills(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args)
        {
            if (Settings.autolevelskills)
            {
                if (!sender.IsMe)
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
    }
}
