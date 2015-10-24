using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Settings = Anivia.Config.Misc;
namespace Anivia
{
    public static class Program
    {
        // Change this line to the champion you want to make the addon for,
        // watch out for the case being correct!
        public const string ChampName = "Anivia";

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Verify the champion we made this addon for
            if (Player.Instance.ChampionName != ChampName)
            {
                // Champion is not the one we made this addon for,
                // therefore we return
                return;
            }

            // Initialize the classes that we need
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
            SaveMePls.Initialize();
            if (Settings.autolevelskills)
            {
                Player.Instance.Spellbook.LevelSpell(SpellSlot.Q);
            }
            if (Settings.autoBuyStartingItems)
            {
                if (Game.MapId == GameMapId.SummonersRift)
                {
                    Shop.BuyItem(ItemId.Dorans_Ring);
                    Shop.BuyItem(ItemId.Health_Potion);
                    Shop.BuyItem(ItemId.Health_Potion);
                }
            }

            // Listen to events we need
            Drawing.OnDraw += OnDraw;
            Player.OnLevelUp += Anivia.Modes.PermaActive.autoLevelSkills;
            
        }

        private static void OnDraw(EventArgs args)
        {
            // Draw range circles of our spells
            if(Settings._drawQ.CurrentValue)
                Circle.Draw(Color.Red, SpellManager.Q.Range, Player.Instance.Position);
            if (Settings._drawW.CurrentValue)
                Circle.Draw(Color.Aqua, SpellManager.W.Range, Player.Instance.Position);
            if (Settings._drawE.CurrentValue)
                Circle.Draw(Color.DarkGreen, SpellManager.E.Range, Player.Instance.Position);
            if (Settings._drawR.CurrentValue)
                Circle.Draw(Color.DarkOrange, SpellManager.R.Range, Player.Instance.Position);
        }
    }
}
