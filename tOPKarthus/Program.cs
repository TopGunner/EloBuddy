using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace tOPKarthus
{
    public static class Program
    {
        // Change this line to the champion you want to make the addon for,
        // watch out for the case being correct!
        public const string ChampName = "Karthus";

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

            Chat.Print("<font color=\"#c71ef9\">tOP Karthus loaded.</font>");
            // Initialize the classes that we need
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();

            // Listen to events we need
            Drawing.OnDraw += OnDraw;
        }

        private static void OnDraw(EventArgs args)
        {
            Circle.Draw(SharpDX.Color.Red, SpellManager.Q.Range, Player.Instance.Position);

            var EnemiesTxt = "";

            //Show Enemies
            var enemies = HeroManager.Enemies;
            foreach (var enemy in enemies)
            {
                if (!(enemy.IsEnemy && enemy.IsValid))
                {
                    enemies.Remove(enemy);
                }
            }

            Vector2 WTS = Drawing.WorldToScreen(Player.Instance.Position);

            foreach (var enemy in enemies)
            {
                if ((enemy.Health - SpellManager.RDamage(enemy)) <= 0)
                {
                    if (!enemy.IsDead)
                    {
                        EnemiesTxt = enemy.BaseSkinName + " , ";

                    }
                }
            }
            if (EnemiesTxt != "")
            {
                if (SpellManager.R.IsLearned && SpellManager.R.IsReady())
                {
                    Drawing.DrawText(WTS[0] - 150, WTS[1] + 80, System.Drawing.Color.Red, "R: " + EnemiesTxt + "killable", 200);
                }
            }
        }
    }
}
