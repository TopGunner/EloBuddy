using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace Kitelyn
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "Kitelyn";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to Kitelyn by TopGunner");

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
            public static readonly CheckBox _drawW;
            public static readonly CheckBox _drawE;
            public static readonly CheckBox _drawR;
            public static readonly CheckBox _drawCombo;
            private static readonly CheckBox _useR;
            private static readonly CheckBox _useRAlways;
            private static readonly CheckBox _useScryingOrbMarker;
            private static readonly CheckBox _useHeal;
            private static readonly CheckBox _useQSS;
            private static readonly CheckBox _useWOnTP;
            private static readonly CheckBox _useWOnZhonyas;
            private static readonly CheckBox _useWOnGapcloser;
            private static readonly CheckBox _useEOnGapcloser;
            public static readonly CheckBox _useEFlee;
            private static readonly CheckBox _autoBuyStartingItems;
            private static readonly CheckBox _autolevelskills;
            private static readonly Slider _skinId;
            private static readonly CheckBox _cleanseStun;
            private static readonly Slider _cleanseEnemies;


            public static bool UseR
            {
                get { return _useR.CurrentValue; }
            }
            public static bool UseRAlways
            {
                get { return _useRAlways.CurrentValue; }
            }
            public static bool useScryingOrbMarker
            {
                get { return _useScryingOrbMarker.CurrentValue; }
            }
            public static bool useHeal
            {
                get { return _useHeal.CurrentValue; }
            }
            public static bool useQSS
            {
                get { return _useQSS.CurrentValue; }
            }
            public static bool useWOnTP
            {
                get { return _useWOnTP.CurrentValue; }
            }
            public static bool useWOnZhonyas
            {
                get { return _useWOnZhonyas.CurrentValue; }
            }
            public static bool useWOnGapcloser
            {
                get { return _useWOnGapcloser.CurrentValue; }
            }
            public static bool useEOnGapcloser
            {
                get { return _useEOnGapcloser.CurrentValue; }
            }
            public static bool useEFlee
            {
                get { return _useEFlee.CurrentValue; }
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
            public static int cleanseEnemies
            {
                get { return _cleanseEnemies.CurrentValue; }
            }
            public static bool cleanseStun
            {
                get { return _cleanseStun.CurrentValue; }
            }
            public static bool drawComboDmg
            {
                get { return _drawCombo.CurrentValue; }
            }


            static Misc()
            {
                // Initialize the menu values
                Menu = Config.Menu.AddSubMenu("Misc");
                _drawQ = Menu.Add("drawQ", new CheckBox("Draw Q"));
                _drawW = Menu.Add("drawW", new CheckBox("Draw W"));
                _drawE = Menu.Add("drawE", new CheckBox("Draw E"));
                _drawR = Menu.Add("drawR", new CheckBox("Draw R"));
                _drawCombo = Menu.Add("drawCombo", new CheckBox("Draw Combo Damge"));
                Menu.AddSeparator();
                _useR = Menu.Add("useR", new CheckBox("Use R to kill out of range targets"));
                _useRAlways = Menu.Add("useRAlways", new CheckBox("Always use R if killable", false));
                _useScryingOrbMarker = Menu.Add("useScryingOrbMarker", new CheckBox("Use Scrying Orb to mark enemies for later ult"));
                Menu.AddSeparator();
                _useHeal = Menu.Add("useHeal", new CheckBox("Use Heal Smart"));
                _useQSS = Menu.Add("useQSS", new CheckBox("Use QSS"));
                Menu.AddSeparator();
                _useWOnTP = Menu.Add("useWOnTP", new CheckBox("Use W on Teleport"));
                _useWOnZhonyas = Menu.Add("useWOnZhonyas", new CheckBox("Use W on Zhonyas"));
                _useWOnGapcloser = Menu.Add("useWOnGapcloser", new CheckBox("Use W on Gapcloser"));
                _useEOnGapcloser = Menu.Add("useEOnGapcloser", new CheckBox("Use E on Gapcloser", false));
                _useEFlee = Menu.Add("useEFlee", new CheckBox("Use E in Fleemode to mouse"));
                Menu.AddSeparator();
                _autolevelskills = Menu.Add("autolevelskills", new CheckBox("Autolevelskills"));
                _autoBuyStartingItems = Menu.Add("autoBuyStartingItems", new CheckBox("Autobuy Starting Items (SR only)"));
                Menu.AddSeparator();
                _skinId = Menu.Add("skinId", new Slider("Skin ID", 6, 1, 13));
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
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useQNotStunned;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useBOTRK;
                private static readonly CheckBox _useYOUMOUS;
                private static readonly CheckBox _useWVision;
                private static readonly CheckBox _useWardVision;
                private static readonly CheckBox _useTrinketVision;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseQNotStunned
                {
                    get { return _useQNotStunned.CurrentValue; }
                }
                public static int ManaQAlways
                {
                    get { return Menu["comboManaQAlways"].Cast<Slider>().CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool useWVision
                {
                    get { return _useWVision.CurrentValue; }
                }
                public static bool useWardVision
                {
                    get { return _useWardVision.CurrentValue; }
                }
                public static bool useTrinketVision
                {
                    get { return _useTrinketVision.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool useBOTRK
                {
                    get { return _useBOTRK.CurrentValue; }
                }
                public static bool useYOUMOUS
                {
                    get { return _useYOUMOUS.CurrentValue; }
                }
                public static int StockW
                {
                    get { return Menu["comboStockW"].Cast<Slider>().CurrentValue; }
                }


                static Combo()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Combo");
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use Smart W"));
                    Menu.Add("comboStockW", new Slider("Keep at least x traps for CC", 1, 0, 5));
                    _useQNotStunned = Menu.Add("comboUseQNotStunned", new CheckBox("Use Q always", false));
                    Menu.Add("comboManaQAlways", new Slider("Use Q always if Mana > ", 75, 0, 100));
                    _useE = Menu.Add("comboUseE", new CheckBox("Use Smart E"));
                    _useBOTRK = Menu.Add("useBotrk", new CheckBox("Use Blade of the Ruined King (Smart) and Cutlass"));
                    _useYOUMOUS = Menu.Add("useYoumous", new CheckBox("Use Youmous"));
                    _useWVision = Menu.Add("useWVision", new CheckBox("Use W for vision"));
                    _useWardVision = Menu.Add("useWardVision", new CheckBox("Use Wards for Vision"));
                    _useTrinketVision = Menu.Add("useTrinketVision", new CheckBox("Use Trinkets for Vision"));
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
                public static bool UseQNotStunned
                {
                    get { return Menu["harassUseQNotStunned"].Cast<CheckBox>().CurrentValue; }
                }
                public static int ManaQAlways
                {
                    get { return Menu["harassManaQAlways"].Cast<Slider>().CurrentValue; }
                }
                public static bool UseW
                {
                    get { return Menu["harassUseW"].Cast<CheckBox>().CurrentValue; }
                }
                public static bool UseE
                {
                    get { return Menu["harassUseE"].Cast<CheckBox>().CurrentValue; }
                }
                public static bool UseR
                {
                    get { return Menu["harassUseR"].Cast<CheckBox>().CurrentValue; }
                }
                public static int Mana
                {
                    get { return Menu["harassMana"].Cast<Slider>().CurrentValue; }
                }
                public static int StockW
                {
                    get { return Menu["harassStockW"].Cast<Slider>().CurrentValue; }
                }

                static Harass()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("Harass");
                    Menu.Add("harassUseQ", new CheckBox("Use Q"));
                    Menu.Add("harassUseQNotStunned", new CheckBox("Use Q always"));
                    Menu.Add("harassManaQAlways", new Slider("Use Q always if Mana > ", 75, 0, 100));
                    Menu.Add("harassUseW", new CheckBox("Use Smart W"));
                    Menu.Add("harassStockW", new Slider("Keep at least x traps for CC", 1, 0, 5));
                    Menu.Add("harassUseR", new CheckBox("Use R", false)); // Default false

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
                private static readonly Slider _mana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
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
                    _mana = Menu.Add("clearMana", new Slider("Maximum mana usage in percent ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
