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
using Anivia.Modes;
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

            // Listen to events we need
            Drawing.OnDraw += OnDraw;
            Player.OnLevelUp += Anivia.Modes.PermaActive.autoLevelSkills;
            Dash.OnDash += PermaActive.Dash_OnDash;
            Gapcloser.OnGapcloser += PermaActive.antiGapcloser;
            GameObject.OnCreate += PermaActive.GameObject_OnCreate;
            if (Settings._drawQ.CurrentValue || Settings._drawQ.CurrentValue || Settings._drawE.CurrentValue || Settings._drawR.CurrentValue)
                EloBuddy.SDK.Notifications.Notifications.Show(new EloBuddy.SDK.Notifications.SimpleNotification("Q missing", "If Q is missing, turn up the Q accuracy in the settings to about 140 - 150. Good Luck, Summoner"), 20000);
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

            if (Settings.drawComboDmg)
            {
                foreach (var e in EntityManager.Heroes.Enemies.Where(e => e.IsValidTarget() && e.IsHPBarRendered))
                {
                    //combodamage
                    float damage = SpellManager.DamageToHero(e);
                    if (damage <= 0)
                        continue;

                    var damagePercentage = ((e.Health - damage) > 0 ? (e.Health - damage) : 0) / e.MaxHealth;
                    var currentHealthPercentage = e.Health / e.MaxHealth;

                    var start = new Vector2((int)(e.HPBarPosition.X) + damagePercentage * 100 - 10, (int)(e.HPBarPosition.Y) -5);
                    var end = new Vector2((int)(e.HPBarPosition.X) + currentHealthPercentage * 100 - 10, (int)(e.HPBarPosition.Y) -5);

                    // Draw the line
                    Drawing.DrawLine(start, end, 20, System.Drawing.Color.Lime);
                    if (e.Health - damage < 0)
                    {
                        Drawing.DrawText(e.HPBarPosition.X, e.HPBarPosition.Y - 20, System.Drawing.Color.Lime, "KILLABLE", 18);
                    }
                }
            }
        }
    }
}
