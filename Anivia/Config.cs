using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass
namespace Anivia
{
    // I can't really help you with my layout of a good config class
    // since everyone does it the way they like it most, go checkout my
    // config classes I make on my GitHub if you wanna take over the
    // complex way that I use
    public static class Config
    {
        private const string MenuName = "Anivia";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to Untouchable Anivia by TopGunner");

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
            private static readonly CheckBox _tearStack;
            private static readonly CheckBox _autoBuyStartingItems;
            private static readonly CheckBox _useSeraphsDmg;
            private static readonly CheckBox _useSeraphsCC;
            private static readonly CheckBox _useZhonyasDmg;
            private static readonly CheckBox _useZhonyasCC;
            private static readonly CheckBox _autolevelskills;
            private static readonly CheckBox _autoInterrupt;
            private static readonly Slider _skinId;
            private static readonly CheckBox _cleanseStun;
            private static readonly Slider _cleanseEnemies;

            public static bool tearStack
            {
                get { return _tearStack.CurrentValue; }
            }
            public static bool autoBuyStartingItems
            {
                get { return _autoBuyStartingItems.CurrentValue; }
            }
            public static bool autolevelskills
            {
                get { return _autolevelskills.CurrentValue; }
            }
            public static bool autoInterrupt
            {
                get { return _autoInterrupt.CurrentValue; }
            }
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


            //Spells
            private static readonly CheckBox _onAhriE;
            private static readonly CheckBox _onAliKnockup;
            private static readonly CheckBox _onMumuUlt;
            private static readonly CheckBox _onAsheUlt;
            private static readonly CheckBox _onAnnieUlt;
            private static readonly CheckBox _onBlitzGrab;
            private static readonly CheckBox _onBraumUlt;
            private static readonly CheckBox _onCaitUlt;
            private static readonly CheckBox _onCassioUlt;
            private static readonly CheckBox _onChoQ;
            private static readonly CheckBox _onChoUlt;
            private static readonly CheckBox _onDariusUlt;
            private static readonly CheckBox _onDianaUlt;
            private static readonly CheckBox _onDravenUlt;
            private static readonly CheckBox _onEkkoUlt;
            private static readonly CheckBox _onEzUlt;
            private static readonly CheckBox _onEliseE;
            private static readonly CheckBox _onFioraUlt;
            private static readonly CheckBox _onFizzUlt;
            private static readonly CheckBox _onGalioUlt;
            private static readonly CheckBox _onGnarUlt;
            private static readonly CheckBox _onGragasUlt;
            private static readonly CheckBox _onGravesUlt;
            private static readonly CheckBox _onHecarimUlt;
            private static readonly CheckBox _onJinxUlt;
            private static readonly CheckBox _onKarthusUlt;
            private static readonly CheckBox _onKennenUlt;
            private static readonly CheckBox _onLeonaUlt;
            private static readonly CheckBox _onMalphiteUlt;
            private static readonly CheckBox _onMorganaBinding; 
            private static readonly CheckBox _onNamiBubble;
            private static readonly CheckBox _onNamiUlt;
            private static readonly CheckBox _onNautilusUlt;
            private static readonly CheckBox _onNautilusDrag;
            private static readonly CheckBox _onOriUlt;
            private static readonly CheckBox _onSejuaniUlt;
            private static readonly CheckBox _onShenE;
            private static readonly CheckBox _onShyvanaUlt;
            private static readonly CheckBox _onSkarnerUlt;
            private static readonly CheckBox _onSonaUlt;
            private static readonly CheckBox _onSyndraUlt;
            private static readonly CheckBox _onThreshGrab;
            private static readonly CheckBox _onVeigarUlt;
            private static readonly CheckBox _onViUlt;
            private static readonly CheckBox _onZedUlt;
            private static readonly CheckBox _onZiggsUlt;
            private static readonly CheckBox _onZyraUlt;

            public static bool onAhriE
            {
                get { return _onAhriE.CurrentValue; }
            }
            public static bool onAliKnockup
            {
                get { return _onAliKnockup.CurrentValue; }
            }
            public static bool onMumuUlt
            {
                get { return _onMumuUlt.CurrentValue; }
            }
            public static bool onAsheUlt
            {
                get { return _onAsheUlt.CurrentValue; }
            }
            public static bool onAnnieUlt
            {
                get { return _onAnnieUlt.CurrentValue; }
            }
            public static bool onBlitzGrab
            {
                get { return _onBlitzGrab.CurrentValue; }
            }
            public static bool onBraumUlt
            {
                get { return _onBraumUlt.CurrentValue; }
            }
            public static bool onCaitUlt
            {
                get { return _onCaitUlt.CurrentValue; }
            }
            public static bool onCassioUlt
            {
                get { return _onCassioUlt.CurrentValue; }
            }
            public static bool onChoQ
            {
                get { return _onChoQ.CurrentValue; }
            }
            public static bool onChoUlt
            {
                get { return _onChoUlt.CurrentValue; }
            }
            public static bool onDariusUlt
            {
                get { return _onDariusUlt.CurrentValue; }
            }
            public static bool onDianaUlt
            {
                get { return _onDianaUlt.CurrentValue; }
            }
            public static bool onDravenUlt
            {
                get { return _onDravenUlt.CurrentValue; }
            }
            public static bool onEkkoUlt
            {
                get { return _onEkkoUlt.CurrentValue; }
            }
            public static bool onEzUlt
            {
                get { return _onEzUlt.CurrentValue; }
            }
            public static bool onEliseE
            {
                get { return _onEliseE.CurrentValue; }
            }
            public static bool onFioraUlt
            {
                get { return _onFioraUlt.CurrentValue; }
            }
            public static bool onFizzUlt
            {
                get { return _onFizzUlt.CurrentValue; }
            }
            public static bool onGalioUlt
            {
                get { return _onGalioUlt.CurrentValue; }
            }
            public static bool onGnarUlt
            {
                get { return _onGnarUlt.CurrentValue; }
            }
            public static bool onGragasUlt
            {
                get { return _onGragasUlt.CurrentValue; }
            }
            public static bool onGravesUlt
            {
                get { return _onGravesUlt.CurrentValue; }
            }
            public static bool onHecarimUlt
            {
                get { return _onHecarimUlt.CurrentValue; }
            }
            public static bool onJinxUlt
            {
                get { return _onJinxUlt.CurrentValue; }
            }
            public static bool onKarthusUlt
            {
                get { return _onKarthusUlt.CurrentValue; }
            }
            public static bool onKennenUlt
            {
                get { return _onKennenUlt.CurrentValue; }
            }
            public static bool onLeonaUlt
            {
                get { return _onLeonaUlt.CurrentValue; }
            }
            public static bool onMalphiteUlt
            {
                get { return _onMalphiteUlt.CurrentValue; }
            }
            public static bool onMorganaBinding
            {
                get { return _onMorganaBinding.CurrentValue; }
            }
            public static bool onNamiBubble
            {
                get { return _onNamiBubble.CurrentValue; }
            }
            public static bool onNamiUlt
            {
                get { return _onNamiUlt.CurrentValue; }
            }
            public static bool onNautilusUlt
            {
                get { return _onNautilusUlt.CurrentValue; }
            }
            public static bool onNautilusDrag
            {
                get { return _onNautilusDrag.CurrentValue; }
            }
            public static bool onOriUlt
            {
                get { return _onOriUlt.CurrentValue; }
            }
            public static bool onSejuaniUlt
            {
                get { return _onSejuaniUlt.CurrentValue; }
            }
            public static bool onShenE
            {
                get { return _onShenE.CurrentValue; }
            }
            public static bool onShyvanaUlt
            {
                get { return _onShyvanaUlt.CurrentValue; }
            }
            public static bool onSkarnerUlt
            {
                get { return _onSkarnerUlt.CurrentValue; }
            }
            public static bool onSonaUlt
            {
                get { return _onSonaUlt.CurrentValue; }
            }
            public static bool onSyndraUlt
            {
                get { return _onSyndraUlt.CurrentValue; }
            }
            public static bool onThreshGrab
            {
                get { return _onThreshGrab.CurrentValue; }
            }
            public static bool onVeigarUlt
            {
                get { return _onVeigarUlt.CurrentValue; }
            }
            public static bool onViUlt
            {
                get { return _onViUlt.CurrentValue; }
            }
            public static bool onZedUlt
            {
                get { return _onZedUlt.CurrentValue; }
            }
            public static bool onZiggsUlt
            {
                get { return _onZiggsUlt.CurrentValue; }
            }
            public static bool onZyraUlt
            {
                get { return _onZyraUlt.CurrentValue; }
            }

            static Misc()
            {
                // Initialize the menu values
                Menu = Config.Menu.AddSubMenu("Misc");
                _drawQ = Menu.Add("drawQ", new CheckBox("Draw Q"));
                _drawW = Menu.Add("drawW", new CheckBox("Draw W"));
                _drawE = Menu.Add("drawE", new CheckBox("Draw E"));
                _drawR = Menu.Add("drawR", new CheckBox("Draw R"));
                Menu.AddSeparator();
                _tearStack = Menu.Add("tearStack", new CheckBox("Tearstacking Mode"));
                _autolevelskills = Menu.Add("autolevelskills", new CheckBox("Autolevelskills"));
                _autoBuyStartingItems = Menu.Add("autoBuyStartingItems", new CheckBox("Autobuy Starting Items (SR only)"));
                _autoInterrupt = Menu.Add("autoInterrupt", new CheckBox("Autointerrup dangerous channeling skills (e.g. Fiddle Ult, ...)"));
                _skinId = Menu.Add("skinId", new Slider("Skin ID", 5, 1, 7));
                _cleanseStun = Menu.Add("cleanseStun", new CheckBox("Cleanse if x or more enemies are around"));
                _cleanseEnemies = Menu.Add("cleanseEnemies", new Slider("x enemies in range for Cleanse", 2, 0, 5));
                Menu.AddSeparator();
                _useSeraphsDmg = Menu.Add("useSeraphsDmg", new CheckBox("Use Seraphs on incoming damage"));
                _useSeraphsCC = Menu.Add("useSeraphsCC", new CheckBox("Use Seraphs on incoming dangerous spells"));
                Menu.AddSeparator();
                _useZhonyasDmg = Menu.Add("useZhonyasDmg", new CheckBox("Use Zhonyas on incoming damage"));
                _useZhonyasCC = Menu.Add("useZhonyasCC", new CheckBox("Use Zhonyas on incoming dangerous spells"));
                Menu.AddSeparator();
                _onAhriE = Menu.Add("onAhriE", new CheckBox("Block Ahri Charm", false));
                _onAliKnockup = Menu.Add("onAliKnockup", new CheckBox("Block Alistar Pulverize", false));
                _onMumuUlt = Menu.Add("onMumuUlt", new CheckBox("Block Amumu Curse of the Sad Mummy"));
                _onAnnieUlt = Menu.Add("onAnnieUlt", new CheckBox("Block Annie Tibbers"));
                _onAsheUlt = Menu.Add("onAsheUlt", new CheckBox("Block Ashe Arrow"));
                _onBlitzGrab = Menu.Add("onBlitzGrab", new CheckBox("Block Blitz Grab", false));
                _onBraumUlt = Menu.Add("onBraumUlt", new CheckBox("Block Braum Glacial Fissure", false));
                _onCaitUlt = Menu.Add("onCaitUlt", new CheckBox("Block Caitlyn Ace in the Hole", false));
                _onCassioUlt = Menu.Add("onCassioUlt", new CheckBox("Block Cassiopeia Petrifying Gaze", false));
                _onChoQ = Menu.Add("onChoQ", new CheckBox("Block ChoGath Rupture", false));
                _onChoUlt = Menu.Add("onChoUlt", new CheckBox("Block ChoGath Omnomnom"));
                _onDariusUlt = Menu.Add("onDariusUlt", new CheckBox("Block Darius SchlitzDown"));
                _onDianaUlt = Menu.Add("onDianaUlt", new CheckBox("Block Diana Jump", false));
                _onDravenUlt = Menu.Add("onDravenUlt", new CheckBox("Block Draven Rolling Axes", false));
                _onEkkoUlt = Menu.Add("onEkkoUlt", new CheckBox("Block Ekko Chronobreak", false));
                _onEzUlt = Menu.Add("onEzUlt", new CheckBox("Block Ezreal TrueShot", false));
                _onEliseE = Menu.Add("onEliseE", new CheckBox("Block Elise Cocoon"));
                _onFioraUlt = Menu.Add("onFioraUlt", new CheckBox("Block Fiora Ult Marks"));
                _onFizzUlt = Menu.Add("onFizzUlt", new CheckBox("Block Fizz Shark"));
                _onGalioUlt = Menu.Add("onGalioUlt", new CheckBox("Block Galio Idol of Durand"));
                _onGnarUlt = Menu.Add("onGnarUlt", new CheckBox("Block Gnar Ult"));
                _onGragasUlt = Menu.Add("onGragasUlt", new CheckBox("Block Gragas Explosive Cask"));
                _onGravesUlt = Menu.Add("onGravesUlt", new CheckBox("Block Graves Collateral Damage"));
                _onHecarimUlt = Menu.Add("onHecaUlt", new CheckBox("Block Hecarim Ult"));
                _onJinxUlt = Menu.Add("onJinxUlt", new CheckBox("Block Jinx Ult"));
                _onKarthusUlt = Menu.Add("onKarthusUlt", new CheckBox("Block Karthus R2Pentakill"));
                _onKennenUlt = Menu.Add("onKennenUlt", new CheckBox("Block Kennen Slicing Maelstrom", false));
                _onLeonaUlt = Menu.Add("onLeonaUlt", new CheckBox("Block Leona Solar Flare"));
                _onMalphiteUlt = Menu.Add("onMalphiteUlt", new CheckBox("Block Malphite Unstoppable Force"));
                _onMorganaBinding = Menu.Add("onMorganaBinding", new CheckBox("Block Morgana Binding", false));
                _onNamiBubble = Menu.Add("onNamiBubble", new CheckBox("Block Nami Bubble", false));
                _onNamiUlt = Menu.Add("onNamiUlt", new CheckBox("Block Nami Ult"));
                _onNautilusUlt = Menu.Add("onNautilusUlt", new CheckBox("Block Nautilus Ult"));
                _onNautilusDrag = Menu.Add("onNautilusDrag", new CheckBox("Block Nautilus Grab", false));
                _onOriUlt = Menu.Add("onOriUlt", new CheckBox("Block Orianna Shockwave"));
                _onSejuaniUlt = Menu.Add("onSejuaniUlt", new CheckBox("Block Seuani Glacian Prison (Ult)"));
                _onShenE = Menu.Add("onShenE", new CheckBox("Block Shen Dash"));
                _onShyvanaUlt = Menu.Add("onShyvanaUlt", new CheckBox("Block Shyvana Ult"));
                _onSkarnerUlt = Menu.Add("onSkarnerUlt", new CheckBox("Block Skarner Grab of the Scorpion"));
                _onSonaUlt = Menu.Add("onSonaUlt", new CheckBox("Block Sona Make Me Dance"));
                _onSyndraUlt = Menu.Add("onSyndraUlt", new CheckBox("Block Syndra Ult"));
                _onThreshGrab = Menu.Add("onThreshGrab", new CheckBox("Block Thresh Grab", false));
                _onVeigarUlt = Menu.Add("onVeigarUlt", new CheckBox("Block Veigar Ult"));
                _onViUlt = Menu.Add("onViUlt", new CheckBox("Block Vi Ult"));
                _onZedUlt = Menu.Add("onZedUlt", new CheckBox("Block Zed Ult"));
                _onZiggsUlt = Menu.Add("onZiggsUlt", new CheckBox("Block Ziggs Ult"));
                _onZyraUlt = Menu.Add("onZyraUlt", new CheckBox("Block Zyra Ult"));
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
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;
                private static readonly CheckBox _deactiveR;
                private static readonly CheckBox _ksE;
                private static readonly CheckBox _ksI;

                public static bool ksE
                {
                    get { return _ksE.CurrentValue; }
                }
                public static bool ksI
                {
                    get { return _ksI.CurrentValue; }
                }
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
                public static bool UseEDoubleOnly
                {
                    get { return _useE.CurrentValue; }
                }
                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }
                public static bool deactiveR
                {
                    get { return _deactiveR.CurrentValue; }
                }


                static Combo()
                {
                    // Initialize the menu values
                    Menu.AddGroupLabel("Combo");
                    _useQ = Menu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use Smart W"));
                    _useE = Menu.Add("comboUseE", new CheckBox("Use E"));
                    _useE = Menu.Add("comboUseEDoubleOnly", new CheckBox("Use E only for doubled damage"));
                    _useR = Menu.Add("comboUseR", new CheckBox("Use R"));
                    _deactiveR = Menu.Add("deactiveR", new CheckBox("Autocancel Ult"));
                    _ksE = Menu.Add("ksE", new CheckBox("Killsteal with E"));
                    _ksI = Menu.Add("ksI", new CheckBox("Killsteal/Finish with Ignite"));
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

                static Harass()
                {
                    // Here is another option on how to use the menu, but I prefer the
                    // way that I used in the combo class
                    Menu.AddGroupLabel("Harass");
                    Menu.Add("harassUseQ", new CheckBox("Use Q"));
                    Menu.Add("harassUseW", new CheckBox("Use W (only to block enemy)"));
                    Menu.Add("harassUseE", new CheckBox("Use E (double damage only)"));
                    Menu.Add("harassUseR", new CheckBox("Use R", false)); // Default false

                    // Adding a slider, we have a little more options with them, using {0} {1} and {2}
                    // in the display name will replace it with 0=current 1=min and 2=max value
                    Menu.Add("harassMana", new Slider("Maximum mana usage in percent ({0}%)", 40));
                }
                public static void Initialize()
                {
                }
            }
        }
    }
}
