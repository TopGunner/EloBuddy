using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace HarleyJinx
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "HarleyJinx";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to HarleyJinx, the Ace Machine by TopGunner");

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
            public static readonly CheckBox _drawReadySpellsOnly;
            public static readonly CheckBox _ksW;
            public static readonly CheckBox _ksR;
            public static readonly CheckBox _EOnSlowedEnemy;
            public static readonly CheckBox _EOnImmobileEnemy;
            private static readonly CheckBox _useEOnGapcloser;
            private static readonly CheckBox _useEInterrupt;
            private static readonly Slider _interruptDangerLvl;
            private static readonly CheckBox _useHeal;
            private static readonly CheckBox _useQSS;
            private static readonly CheckBox _stealBaron;
            private static readonly CheckBox _stealDrake;
            private static readonly CheckBox _autoBuyStartingItems;
            private static readonly CheckBox _autolevelskills;
            private static readonly Slider _skinId;
            public static readonly CheckBox _useSkinHack;
            private static readonly CheckBox _cleanseStun;
            private static readonly Slider _cleanseEnemies;
            private static readonly CheckBox[] _useHealOn = { new CheckBox("", false), new CheckBox("", false), new CheckBox("", false), new CheckBox("", false), new CheckBox("", false) };

            public static bool useHealOnI(int i)
            {
                return _useHealOn[i].CurrentValue;
            }
            public static bool drawReadySpellsOnly
            {
                get { return _drawReadySpellsOnly.CurrentValue; }
            }
            public static bool ksW
            {
                get { return _ksW.CurrentValue; }
            }
            public static bool ksR
            {
                get { return _ksR.CurrentValue; }
            }
            public static bool useHeal
            {
                get { return _useHeal.CurrentValue; }
            }
            public static bool useQSS
            {
                get { return _useQSS.CurrentValue; }
            }
            public static bool useEOnGapcloser
            {
                get { return _useEOnGapcloser.CurrentValue; }
            }
            public static bool EOnSlowedEnemy
            {
                get { return _EOnSlowedEnemy.CurrentValue; }
            }
            public static bool EOnImmobileEnemy
            {
                get { return _EOnImmobileEnemy.CurrentValue; }
            }
            public static bool useEInterrupt
            {
                get { return _useEInterrupt.CurrentValue; }
            }
            public static DangerLevel interruptDangerLvl
            {
                get {
                    if (_interruptDangerLvl.CurrentValue == 1)
                        return DangerLevel.Low;
                    else if (_interruptDangerLvl.CurrentValue == 2)
                        return DangerLevel.Medium;
                    else
                        return DangerLevel.High;
                }
            }
            public static bool StealBaron
            {
                get { return _stealBaron.CurrentValue; }
            }
            public static bool StealDrake
            {
                get { return _stealDrake.CurrentValue; }
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
            public static bool UseSkinHack
            {
                get { return _useSkinHack.CurrentValue; }
            }


            static Misc()
            {
                // Initialize the menu values
                Menu = Config.Menu.AddSubMenu("Misc");
                _drawQ = Menu.Add("drawQ", new CheckBox("Draw Q"));
                _drawW = Menu.Add("drawW", new CheckBox("Draw W"));
                _drawE = Menu.Add("drawE", new CheckBox("Draw E"));
                _drawReadySpellsOnly = Menu.Add("drawReady", new CheckBox("Draw ready spells only"));
                Menu.AddSeparator();
                _ksW = Menu.Add("ksQ", new CheckBox("KS with W"));
                _ksR = Menu.Add("ksR", new CheckBox("KS with R"));
                Menu.AddSeparator();
                _useHeal = Menu.Add("useHeal", new CheckBox("Use Heal Smart"));
                _useQSS = Menu.Add("useQSS", new CheckBox("Use QSS"));
                Menu.AddSeparator();
                for (int i = 0; i < EntityManager.Heroes.Allies.Count; i++)
                {
                    _useHealOn[i] = Menu.Add("useHeal" + i, new CheckBox("Use Heal to save " + EntityManager.Heroes.Allies[i].ChampionName));
                }
                Menu.AddSeparator();
                _useEOnGapcloser = Menu.Add("useEOnGapcloser", new CheckBox("Use E on Gapcloser", false));
                _EOnImmobileEnemy = Menu.Add("EOnImmobile", new CheckBox("Use E on immobile enemy"));
                _EOnSlowedEnemy = Menu.Add("EOnSlowed", new CheckBox("Use E on slowed enemy", false));
                _useEInterrupt = Menu.Add("EToInterrupt", new CheckBox("Use E as interrupt"));
                _interruptDangerLvl = Menu.Add("InterruptDangerLvl", new Slider("Interrupt Danger Lvl", 2, 1, 3));
                Menu.AddSeparator();
                _stealDrake = Menu.Add("stealDrake", new CheckBox("Try to steal Dragon"));
                _stealBaron = Menu.Add("stealBaron", new CheckBox("Try to steal Baron"));
                Menu.AddSeparator();
                _autolevelskills = Menu.Add("autolevelskills", new CheckBox("Autolevelskills"));
                _autoBuyStartingItems = Menu.Add("autoBuyStartingItems", new CheckBox("Autobuy Starting Items (SR only)"));
                Menu.AddSeparator();
                _useSkinHack = Menu.Add("useSkinHack", new CheckBox("Use Skinhack", false));
                _skinId = Menu.Add("skinId", new Slider("Skin ID", 6, 1, 14));
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
                JungleClear.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useBOTRK;
                private static readonly CheckBox _useYOUMOUS;
                private static readonly CheckBox _useWardVision;
                private static readonly CheckBox _useTrinketVision;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool useWardVision
                {
                    get { return _useWardVision.CurrentValue; }
                }
                public static bool useTrinketVision
                {
                    get { return _useTrinketVision.CurrentValue; }
                }
                public static bool useBOTRK
                {
                    get { return _useBOTRK.CurrentValue; }
                }
                public static bool useYOUMOUS
                {
                    get { return _useYOUMOUS.CurrentValue; }
                }


                static Combo()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Combo");
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("comboUseW2", new CheckBox("Use W"));
                    _useE = Menu.Add("comboUseE", new CheckBox("Use E in normal fight", false));
                    _useBOTRK = Menu.Add("useBotrk", new CheckBox("Use Blade of the Ruined King (Smart) and Cutlass"));
                    _useYOUMOUS = Menu.Add("useYoumous", new CheckBox("Use Youmous"));
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
                public static bool UseW
                {
                    get { return Menu["harassUseW"].Cast<CheckBox>().CurrentValue; }
                }
                public static int Mana
                {
                    get { return Menu["harassMana"].Cast<Slider>().CurrentValue; }
                }

                static Harass()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("Harass");
                    Menu.Add("harassUseQ", new CheckBox("Use Q"));
                    Menu.Add("harassUseW", new CheckBox("Use W")); // Default false

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
                private static readonly Slider _minMinionsToFishbones;
                private static readonly Slider _mana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static int minMinionsToFishbones
                {
                    get { return _minMinionsToFishbones.CurrentValue; }
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
                    _minMinionsToFishbones = Menu.Add("clearFishbonesMinions", new Slider("Min. Minions to use Fishbones", 3, 1, 10));
                    _mana = Menu.Add("clearMana", new Slider("Maximum mana usage in percent ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }
            public static class JungleClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _mana;
                private static readonly Slider _minMinionsToFishbones;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static int minMinionsToFishbones
                {
                    get { return _minMinionsToFishbones.CurrentValue; }
                }
                public static int mana
                {
                    get { return _mana.CurrentValue; }
                }

                static JungleClear()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Jungle Clear");
                    _useQ = Menu.Add("jglUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("jglUseW", new CheckBox("Use W"));
                    _minMinionsToFishbones = Menu.Add("jglFishbonesMinions", new Slider("Min. Minions to use Fishbones", 3, 1, 10));
                    _mana = Menu.Add("jglMana", new Slider("Maximum mana usage in percent ({0}%)", 40));
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
