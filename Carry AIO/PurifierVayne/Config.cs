using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace PurifierVayne
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "Purifier Vayne";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to Purifier Vayne by TopGunner");

            // Initialize the modes
            Misc.Initialize();
            Modes.Initialize();
            ESettings.Initialize();
            QSettings.Initialize();

        }

        public static void Initialize()
        {
        }

        public static class QSettings
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _useQToMouse;
            private static readonly CheckBox _gapcloserQ;
            private static readonly CheckBox _fleeQ;
            private static readonly Slider _defQ;
            private static readonly Slider _lowHPQ;
            private static readonly CheckBox _unkillableMinion;
            private static readonly CheckBox _useQToMouseCombo;
            private static readonly CheckBox _useQToMouseHarass;
            private static readonly CheckBox _useQToMouseLaneClear;
            private static readonly CheckBox _useQToMouseLastHit;

            public static bool useQToMouse
            {
                get { return _useQToMouse.CurrentValue; }
            }
            public static bool unkillableMinion
            {
                get { return _unkillableMinion.CurrentValue; }
            }
            public static bool comboQToMouse
            {
                get { return _useQToMouseCombo.CurrentValue; }
            }
            public static bool harassQToMouse
            {
                get { return _useQToMouseHarass.CurrentValue; }
            }
            public static bool laneClearQToMouse
            {
                get { return _useQToMouseLaneClear.CurrentValue; }
            }
            public static bool lastHitQToMouse
            {
                get { return _useQToMouseLastHit.CurrentValue; }
            }
            public static bool GapcloserQ
            {
                get { return _gapcloserQ.CurrentValue; }
            }
            public static bool fleeQ
            {
                get { return _fleeQ.CurrentValue; }
            }
            public static int defQ
            {
                get { return _defQ.CurrentValue; }
            }
            public static int lowHPQ
            {
                get { return _lowHPQ.CurrentValue; }
            }



            static QSettings()
            {
                Menu = Config.Menu.AddSubMenu("Q Settings");
                Menu.AddSeparator();
                _useQToMouse = Menu.Add("useQToMouse", new CheckBox("Use Q to mouse", false));
                _gapcloserQ = Menu.Add("gapcloserQ", new CheckBox("Use Q to Gapclose"));
                _fleeQ = Menu.Add("fleeQ", new CheckBox("Use Q in Flee Mode (to mouse)"));
                _unkillableMinion = Menu.Add("unkillableMinion", new CheckBox("use Q for unkillable Minons"));
                Menu.AddSeparator();
                _useQToMouseCombo = Menu.Add("useQToMouseCombo", new CheckBox("Use Q to mouse in Combo", false));
                _useQToMouseHarass = Menu.Add("useQToMouseHarass", new CheckBox("Use Q to mouse in Harass", false));
                _useQToMouseLaneClear = Menu.Add("useQToMouseLaneClear", new CheckBox("Use Q to mouse in LaneClear", false));
                _useQToMouseLastHit = Menu.Add("useQToMouselastHit", new CheckBox("Use Q to mouse in LastHit", false));
                Menu.AddSeparator();
                _defQ = Menu.Add("defQ", new Slider("Defensive Q for at least ({0}) more enemies than allies", 1, 0, 4));
                _lowHPQ = Menu.Add("lowHPQ", new Slider("Defensive Q if less than ({0}%) hp", 10, 1, 100));
            }

            public static void Initialize()
            {
            }
        }
        public static class ESettings
        {
            private static readonly Menu Menu;
            private static readonly CheckBox _harassEProcW;
            private static readonly CheckBox _harassPinToWall;
            private static readonly CheckBox _comboEProcW;
            private static readonly CheckBox _comboPinToWall;
            public static readonly CheckBox _ksE;
            public static readonly CheckBox _interruptE;
            public static readonly CheckBox _useEOnGapcloser;

            public static bool harassEProcW
            {
                get { return _harassEProcW.CurrentValue; }
            }
            public static bool harassPinToWall
            {
                get { return _harassPinToWall.CurrentValue; }
            }
            public static bool comboEProcW
            {
                get { return _comboEProcW.CurrentValue; }
            }
            public static bool comboPinToWall
            {
                get { return _comboPinToWall.CurrentValue; }
            }
            public static bool ksE
            {
                get { return _ksE.CurrentValue; }
            }
            public static bool interruptE
            {
                get { return _interruptE.CurrentValue; }
            }
            public static bool useEOnGapcloser
            {
                get { return _useEOnGapcloser.CurrentValue; }
            }
            public static bool condemnAfterNextAA
            {
                get { return Menu["CondemnHotkey"].Cast<KeyBind>().CurrentValue; }
            }
            static ESettings()
            {
                Menu = Config.Menu.AddSubMenu("E Settings");
                Menu.AddGroupLabel("Harass");
                _harassEProcW = Menu.Add("harassEProcW", new CheckBox("Proc W Stacks", false));
                _harassPinToWall = Menu.Add("harassPinToWall", new CheckBox("Pin to wall"));
                Menu.AddGroupLabel("Combo");
                _comboEProcW = Menu.Add("comboEProcW", new CheckBox("Proc W Stacks", false));
                _comboPinToWall = Menu.Add("comboPinToWall", new CheckBox("Pin to wall"));
                Menu.AddGroupLabel("Others");
                _interruptE = Menu.Add("interruptE", new CheckBox("Interrupt with E"));
                _ksE = Menu.Add("ksE", new CheckBox("KS with E", false));
                _useEOnGapcloser = Menu.Add("useEOnGapcloser", new CheckBox("use E on Gapcloser", false));
                Menu.Add("CondemnHotkey", new KeyBind("Condemn After Next AA", false, KeyBind.BindTypes.HoldActive, 'Y'));
            }

            public static void Initialize()
            {
            }
        }

        public static class Misc
        {

            private static readonly Menu Menu;
            public static readonly CheckBox _drawQ;
            public static readonly CheckBox _drawE;
            public static readonly CheckBox _drawReady;
            private static readonly CheckBox _useHeal;
            private static readonly CheckBox _useQSS;
            private static readonly CheckBox _useQOnGapcloser;
            private static readonly CheckBox _autoBuyStartingItems;
            private static readonly CheckBox _autolevelskills;
            private static readonly Slider _skinId;
            public static readonly CheckBox _useSkinHack;
            private static readonly CheckBox[] _useHealOn = { new CheckBox("", false), new CheckBox("", false), new CheckBox("", false), new CheckBox("", false), new CheckBox("", false) };

            public static bool useHealOnI(int i)
            {
                return _useHealOn[i].CurrentValue;
            }
            public static bool useHeal
            {
                get { return _useHeal.CurrentValue; }
            }
            public static bool useQSS
            {
                get { return _useQSS.CurrentValue; }
            }
            public static bool useQOnGapcloser
            {
                get { return _useQOnGapcloser.CurrentValue; }
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
            public static bool drawReady
            {
                get { return _drawReady.CurrentValue; }
            }


            static Misc()
            {
                // Initialize the menu values
                Menu = Config.Menu.AddSubMenu("Misc");
                _drawQ = Menu.Add("drawQ", new CheckBox("Draw Q"));
                _drawE = Menu.Add("drawE", new CheckBox("Draw E"));
                _drawReady = Menu.Add("drawReady", new CheckBox("Draw Ranges if only if skills are ready"));
                Menu.AddSeparator();
                _useHeal = Menu.Add("useHeal", new CheckBox("Use Heal"));
                _useQSS = Menu.Add("useQSS", new CheckBox("Use QSS"));
                Menu.AddSeparator();
                for (int i = 0; i < EntityManager.Heroes.Allies.Count; i++)
                {
                    _useHealOn[i] = Menu.Add("useHeal" + i, new CheckBox("Use Heal to save " + EntityManager.Heroes.Allies[i].ChampionName));
                }
                Menu.AddSeparator();
                _useQOnGapcloser = Menu.Add("useQOnGapcloser", new CheckBox("Use Q on Gapcloser", false));
                Menu.AddSeparator();
                _autolevelskills = Menu.Add("autolevelskills", new CheckBox("Autolevelskills"));
                _autoBuyStartingItems = Menu.Add("autoBuyStartingItems", new CheckBox("Autobuy Starting Items (SR only)", false));
                Menu.AddSeparator();
                _useSkinHack = Menu.Add("useSkinHack", new CheckBox("Use Skinhack", false));
                _skinId = Menu.Add("skinId", new Slider("Skin ID", 6, 1, 10));
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
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;
                private static readonly Slider _useREnemies;
                private static readonly CheckBox _useBOTRK;
                private static readonly CheckBox _useYOUMOUS;
                private static readonly CheckBox _useWardVision;
                private static readonly CheckBox _useTrinketVision;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }
                public static int UseREnemies
                {
                    get { return _useREnemies.CurrentValue; }
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
                    _useE = Menu.Add("comboUseE", new CheckBox("Use E"));
                    _useR = Menu.Add("comboUseR", new CheckBox("Use R"));
                    _useREnemies = Menu.Add("useREnemies", new Slider("Use R if x enemies are nearby", 2, 1, 5));
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
                public static bool UseE
                {
                    get { return Menu["harassUseE"].Cast<CheckBox>().CurrentValue; }
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
                    Menu.Add("harassUseE", new CheckBox("Use E"));

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

            public static class JungleClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;
                private static readonly Slider _mana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
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
                    _mana = Menu.Add("jglMana", new Slider("Maximum mana usage in percent ({0}%)", 40));
                    _useE = Menu.Add("jglUseE", new CheckBox("Use E"));
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}
