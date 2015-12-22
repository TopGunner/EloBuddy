using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK;


// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace SivirDamage
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "SivirDamage";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to SivirDamage by TopGunner");

            // Initialize the modes
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }


        public static class Misc
        {

            private static readonly Menu Menu;
            public static readonly CheckBox _drawQ;
            private static readonly CheckBox _ksQ;
            private static readonly CheckBox _useE;
            private static readonly CheckBox _useHeal;
            private static readonly CheckBox _useQSS;
            private static readonly CheckBox _autoBuyStartingItems;
            private static readonly CheckBox _autolevelskills;
            private static readonly Slider _skinId;
            private static readonly CheckBox[] _useHealOn = { new CheckBox("", false), new CheckBox("", false), new CheckBox("", false), new CheckBox("", false), new CheckBox("", false) };

            public static bool useHealOnI(int i)
            {
                return _useHealOn[i].CurrentValue;
            }
            public static bool ksQ
            {
                get { return _ksQ.CurrentValue; }
            }
            public static bool useE
            {
                get { return _useE.CurrentValue; }
            }
            public static int minDamage
            {
                get { return Menu["minDamageE"].Cast<Slider>().CurrentValue; }
            }
            public static bool useHeal
            {
                get { return _useHeal.CurrentValue; }
            }
            public static bool useQSS
            {
                get { return _useQSS.CurrentValue; }
            }
            public static bool autoBuyStartingItems
            {
                get { return _autoBuyStartingItems.CurrentValue; }
            }
            public static bool autolevelskills
            {
                get { return _autolevelskills.CurrentValue; }
            }
            public static int skinId
            {
                get { return _skinId.CurrentValue; }
            }


           static Misc()
            {
                // Initialize the menu values
                Menu = Config.Menu.AddSubMenu("Misc");
                _drawQ = Menu.Add("drawQ", new CheckBox("Draw Q"));
                Menu.AddSeparator();
                _ksQ = Menu.Add("ksQ", new CheckBox("Smart KS with Q"));
                _useE = Menu.Add("useE", new CheckBox("use E"));
                Menu.Add("minDamageE", new Slider("minimum damage to Spellshield", 100, 1, 1000));
                Menu.AddSeparator();
                _useHeal = Menu.Add("useHeal", new CheckBox("Use Heal Smart"));
                _useQSS = Menu.Add("useQSS", new CheckBox("Use QSS"));
                Menu.AddSeparator();
                for (int i = 0; i < EntityManager.Heroes.Allies.Count; i++)
                {
                    _useHealOn[i] = Menu.Add("useHeal" + i, new CheckBox("Use Heal to save " + EntityManager.Heroes.Allies[i].ChampionName));
                }
                Menu.AddSeparator();
                _autolevelskills = Menu.Add("autolevelskills", new CheckBox("Autolevelskills"));
                _autoBuyStartingItems = Menu.Add("autoBuyStartingItems", new CheckBox("Autobuy Starting Items (SR only)"));
                Menu.AddSeparator();
                _skinId = Menu.Add("skinId", new Slider("Skin ID", 5, 1, 9));
            }

            public static void Initialize()
            {
            }

        }

        public static class Modes
        {
            private static readonly Menu Menu;

            static Modes()
            {
                // Initialize the menu
                Menu = Config.Menu.AddSubMenu("Modes");

                // Initialize all modes
                // Combo
                Combo.Initialize();
                Menu.AddSeparator();

                // Harass
                Harass.Initialize();
                LaneClear.Initialize();
                Flee.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useR;
                private static readonly CheckBox _useBOTRK;
                private static readonly CheckBox _useYOUMOUS;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }
                public static bool useBOTRK
                {
                    get { return _useBOTRK.CurrentValue; }
                }
                public static bool useYOUMOUS
                {
                    get { return _useYOUMOUS.CurrentValue; }
                }
                public static int damageMin
                {
                    get { return Menu["comboDamageMin"].Cast<Slider>().CurrentValue; }
                }
                public static int useRCount
                {
                    get { return Menu["comboMinEnemiesR"].Cast<Slider>().CurrentValue; }
                }

                static Combo()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Combo");
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use W"));
                    _useR = Menu.Add("comboUseR", new CheckBox("Use R"));
                    Menu.Add("comboDamageMin", new Slider("Min. Percent Damage of Q", 40));
                    Menu.Add("comboMinEnemiesR", new Slider("Min. Enemies in 600 range to ult", 2, 0, 5));
                    Menu.AddSeparator();
                    _useBOTRK = Menu.Add("useBotrk", new CheckBox("Use Blade of the Ruined King (Smart) and Cutlass"));
                    _useYOUMOUS = Menu.Add("useYoumous", new CheckBox("Use Youmous"));
                }

                public static void Initialize()
                {
                }

            }

            public static class Harass
            {
                public static bool UseQ
                {
                    get { return Menu["harassUseQ"].Cast<CheckBox>().CurrentValue; }
                }
                public static bool UseW
                {
                    get { return Menu["harassUseW"].Cast<CheckBox>().CurrentValue; }
                }
                public static int Mana
                {
                    get { return Menu["harassMana"].Cast<Slider>().CurrentValue; }
                }
                public static int damageMin
                {
                    get { return Menu["harassDamageMin"].Cast<Slider>().CurrentValue; }
                }

                static Harass()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("Harass");
                    Menu.Add("harassUseQ", new CheckBox("Use Q"));
                    Menu.Add("harassUseW", new CheckBox("Use W"));

                    Menu.Add("harassDamageMin", new Slider("Min. Percent Damage of Q", 60));

                    // Adding a slider, we have a little more options with them, using {0} {1} and {2}
                    // in the display name will replace it with 0=current 1=min and 2=max value
                    Menu.Add("harassMana", new Slider("Maximum mana usage in percent ({0}%)", 40));
                }
                public static void Initialize()
                {
                }

            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _mana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static int mana
                {
                    get { return _mana.CurrentValue; }
                }

                static LaneClear()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Lane Clear");
                    _useQ = Menu.Add("clearUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("clearUseW", new CheckBox("Use W"));
                    _mana = Menu.Add("clearMana", new Slider("Maximum mana usage in percent ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }
            public static class Flee
            {
                private static readonly CheckBox _useR;

                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                static Flee()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Flee");
                    _useR = Menu.Add("fleeUseR", new CheckBox("Use R"));
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
