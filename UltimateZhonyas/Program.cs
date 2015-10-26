using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using SharpDX;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace UltimateZhonyas
{
    public static class Program
    {
        public static Spell.Skillshot Shield;
        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Initialize the classes that we need
            Config.Initialize();
            SaveMePls.Initialize();


            switch (Player.Instance.ChampionName)
            {
                case "Fiora":
                    Shield = new Spell.Skillshot(SpellSlot.W, 750, SkillShotType.Linear, 750, int.MaxValue, 75);
                    Shield.AllowedCollisionCount = int.MaxValue;
                    break;
                case "Sivir":
                    Shield = new Spell.Skillshot(SpellSlot.E, 0, SkillShotType.Circular, 0, int.MaxValue, 0);
                    break;
            }
            // Listen to events we need
            
        }
    }
}
