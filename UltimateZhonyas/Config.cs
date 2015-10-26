using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace UltimateZhonyas
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "UltimateZhonyas";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to Ultimate Zhonyas by TopGunner");

        }

        public static void Initialize()
        {
        }


        public static class Misc
        {

            private static readonly Menu Menu;
            private static readonly CheckBox _useSeraphsDmg;
            private static readonly CheckBox _useSeraphsCC;
            private static readonly CheckBox _useZhonyasDmg;
            private static readonly CheckBox _useZhonyasCC;
            private static readonly CheckBox _useSpellshields;

            public static bool useSeraphsDmg
            {
                get { return _useSeraphsDmg.CurrentValue; }
            }
            public static bool useSeraphsCC
            {
                get { return _useSeraphsCC.CurrentValue; }
            }
            public static bool useZhonyasDmg
            {
                get { return _useZhonyasDmg.CurrentValue; }
            }
            public static bool useZhonyasCC
            {
                get { return _useZhonyasCC.CurrentValue; }
            }
            public static bool useSpellshields
            {
                get { return _useSpellshields.CurrentValue; }
            }

            public static readonly CheckBox[] _skills = new CheckBox[EntityManager.Heroes.Enemies.Count()*4];

            static Misc()
            {
                // Initialize the menu values
                Menu = Config.Menu.AddSubMenu("Config");
                _useSeraphsDmg = Menu.Add("useSeraphsDmg", new CheckBox("Use Seraphs on incoming damage"));
                _useSeraphsCC = Menu.Add("useSeraphsCC", new CheckBox("Use Seraphs on incoming dangerous spells"));
                Menu.AddSeparator();
                _useZhonyasDmg = Menu.Add("useZhonyasDmg", new CheckBox("Use Zhonyas on incoming damage"));
                _useZhonyasCC = Menu.Add("useZhonyasCC", new CheckBox("Use Zhonyas on incoming dangerous spells"));
                Menu.AddSeparator();
                _useSpellshields = Menu.Add("useSpellshields", new CheckBox("Use Spellshields on incoming dangerous spells"));
                Menu.AddSeparator();

                var enemies = EntityManager.Heroes.Enemies;
                for (int j = 0; j < enemies.Count(); j++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        string skill = "";
                        switch(i)
                        {
                            case 0:
                                skill = "Q";
                                break;
                            case 1:
                                skill = "W";
                                break;
                            case 2:
                                skill = "E";
                                break;
                            case 3:
                                skill = "R";
                                break;
                        }
                        _skills[j * 4 + i] = Menu.Add("champ" + j + "" + i, new CheckBox("Block " + enemies[j].ChampionName + " " + skill, false));
                    }
                    Menu.AddSeparator();
                }
            }

            public static void Initialize()
            {
            }
        }
    }
}
