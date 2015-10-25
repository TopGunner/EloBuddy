using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using Settings = Anivia.Config.Misc;

namespace Anivia
{
    public class SaveMePls
    {
        private static readonly Dictionary<float, float> IncDamage = new Dictionary<float, float>();
        private static readonly Dictionary<float, float> InstDamage = new Dictionary<float, float>();
        public static float IncomingDamage
        {
            get { return IncDamage.Sum(e => e.Value) + InstDamage.Sum(e => e.Value); }
        }

        public static void Initialize()
        {
            // Listen to related events
            Game.OnUpdate += OnUpdate;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
        }

        private static bool castSeraphs()
        {
            if (Player.Instance.IsDead || !Player.Instance.CanCast || Player.Instance.IsInvulnerable || !Player.Instance.IsTargetable || Player.Instance.IsZombie || Player.Instance.IsInShopRange())
                return true;
            foreach (InventorySlot item in Player.Instance.InventoryItems)
            {
                if (((int)item.Id == 3040 || (int)item.Id == 3048) && item.CanUseItem())
                {
                    return item.Cast();
                }
            }
            return false;
        }

        private static bool castZhonyas()
        {
            if (Player.Instance.IsDead || !Player.Instance.CanCast || Player.Instance.IsInvulnerable || !Player.Instance.IsTargetable || Player.Instance.IsZombie || Player.Instance.IsInShopRange())
                return true;
            InventorySlot[] inv = Player.Instance.InventoryItems;
            foreach (var item in inv)
            {
                if ((item.Id == ItemId.Zhonyas_Hourglass || item.Id == ItemId.Wooglets_Witchcap) && item.CanUseItem())
                {
                    return item.Cast();
                }
            }
            return false;
        }

        private static void OnUpdate(EventArgs args)
        {
            // Check spell arrival
            foreach (var entry in IncDamage.Where(entry => entry.Key < Game.Time).ToArray())
            {
                IncDamage.Remove(entry.Key);
            }

            // Instant damage removal
            foreach (var entry in InstDamage.Where(entry => entry.Key < Game.Time).ToArray())
            {
                InstDamage.Remove(entry.Key);
            }
            if (Settings.useZhonyasDmg)
            {
                if ((Player.Instance.HealthPercent < 5 && Player.Instance.CountEnemiesInRange(500) > 0))
                {
                    if (castZhonyas())
                    {
                        return;
                    }
                }
                if(IncomingDamage > Player.Instance.Health)
                {
                    if (castZhonyas())
                    {
                        return;
                    }
                }
            }
            if (Settings.useSeraphsDmg)
            {
                if ((Player.Instance.HealthPercent < 5 && Player.Instance.CountEnemiesInRange(500) > 0) ||
                    IncomingDamage > Player.Instance.Health || IncomingDamage > Player.Instance.MaxMana * 0.2)
                {
                    if (castSeraphs())
                    {
                        return;
                    }
                }
            }
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {

            if (sender.IsEnemy)
            {
                if (args.Target != null && args.Target.NetworkId == Player.Instance.NetworkId && sender is AIHeroClient && dangerousSpell(args.SData, (AIHeroClient)sender))
                {
                    //TODO TEST!
                    if (Player.Instance.Distance(sender) < 2500)
                    {
                        if (!castZhonyas())
                            castSeraphs();
                    }
                }
                if ((!(sender is AIHeroClient) || args.SData.IsAutoAttack()) && args.Target != null && args.Target.NetworkId == Player.Instance.NetworkId)
                {
                    IncDamage[Player.Instance.ServerPosition.Distance(sender.ServerPosition) / args.SData.MissileSpeed + Game.Time] = sender.GetAutoAttackDamage(Player.Instance);
                }
                else if (!(sender is AIHeroClient))
                {
                    return;
                }
                else
                {
                    var attacker = sender as AIHeroClient;
                    if (attacker != null)
                    {
                        var slot = attacker.GetSpellSlotFromName(args.SData.Name);

                        if (slot != SpellSlot.Unknown)
                        {
                            if (slot == attacker.GetSpellSlotFromName("SummonerDot") && args.Target != null && args.Target.NetworkId == Player.Instance.NetworkId)
                            {
                                InstDamage[Game.Time + 2] = attacker.GetSummonerSpellDamage(Player.Instance, DamageLibrary.SummonerSpells.Ignite);
                            }
                            else
                            {
                                switch (slot)
                                {
                                    case SpellSlot.Q:
                                    case SpellSlot.W:
                                    case SpellSlot.E:
                                    case SpellSlot.R:

                                        if ((args.Target != null && args.Target.NetworkId == Player.Instance.NetworkId) || args.End.Distance(Player.Instance.ServerPosition) < Math.Pow(args.SData.LineWidth, 2))
                                        {
                                            // Instant damage to target
                                            InstDamage[Game.Time + 2] = attacker.GetSpellDamage(Player.Instance, slot);
                                        }

                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool dangerousSpell(SpellData spellData, AIHeroClient sender)
        {
            var slot = sender.GetSpellSlotFromName(spellData.Name);
            if (Settings.onAhriE && sender.ChampionName == "Ahri" && slot == SpellSlot.E)
            {
                return true;
            }
            if (Settings.onAliKnockup && sender.ChampionName == "Alistar" && slot == SpellSlot.Q)
            {
                return true;
            } 
            if (Settings.onAnnieUlt && sender.ChampionName == "Annie" && slot == SpellSlot.R)
            {
                return true;
            } 
            if (Settings.onAsheUlt && sender.ChampionName == "Ashe" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onBlitzGrab && sender.ChampionName == "Blitzcrank" && slot == SpellSlot.Q)
            {
                return true;
            }
            if (Settings.onBraumUlt && sender.ChampionName == "Braum" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onMumuUlt && sender.ChampionName == "Amumu" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onCaitUlt && sender.ChampionName == "Caitlyn" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onCassioUlt && sender.ChampionName == "Cassiopeia" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onChoQ && sender.ChampionName == "Chogath" && slot == SpellSlot.Q)
            {
                return true;
            }
            if (Settings.onChoUlt && sender.ChampionName == "Chogath" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onDariusUlt && sender.ChampionName == "Darius" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onDianaUlt && sender.ChampionName == "Diana" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onDravenUlt && sender.ChampionName == "Draven" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onEkkoUlt && sender.ChampionName == "Ekko" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onEliseE && sender.ChampionName == "Elise" && slot == SpellSlot.E && spellData.Name == "EliseHumanE")
            {
                return true;
            }
            if (Settings.onEzUlt && sender.ChampionName == "Ezreal" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onFioraUlt && sender.ChampionName == "Fiora" && slot == SpellSlot.R)
            {
                return true;
            } 
            if (Settings.onFizzUlt && sender.ChampionName == "Fizz" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onGalioUlt && sender.ChampionName == "Galio" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onGnarUlt && sender.ChampionName == "Gnar" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onGragasUlt && sender.ChampionName == "Gragas" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onGravesUlt && sender.ChampionName == "Graves" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onHecarimUlt && sender.ChampionName == "Hecarim" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onJinxUlt && sender.ChampionName == "Jinx" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onKarthusUlt && sender.ChampionName == "Karthus" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onKennenUlt && sender.ChampionName == "Kennen" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onLeonaUlt && sender.ChampionName == "Leona" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onMalphiteUlt && sender.ChampionName == "Malphite" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onMorganaBinding && sender.ChampionName == "Morgana" && slot == SpellSlot.Q)
            {
                return true;
            }
            if (Settings.onNamiBubble && sender.ChampionName == "Nami" && slot == SpellSlot.Q)
            {
                return true;
            }
            if (Settings.onNamiUlt && sender.ChampionName == "Nami" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onNautilusUlt && sender.ChampionName == "Nautilus" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onNautilusDrag && sender.ChampionName == "Nautilus" && slot == SpellSlot.Q)
            {
                return true;
            }
            if (Settings.onOriUlt && sender.ChampionName == "Orianna" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onSejuaniUlt && sender.ChampionName == "Sejuani" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onShenE && sender.ChampionName == "Shen" && slot == SpellSlot.E)
            {
                return true;
            }
            if (Settings.onShyvanaUlt && sender.ChampionName == "Shyvana" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onSkarnerUlt && sender.ChampionName == "Skarner" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onSonaUlt && sender.ChampionName == "Sona" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onSyndraUlt && sender.ChampionName == "Syndra" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onThreshGrab && sender.ChampionName == "Thresh" && slot == SpellSlot.Q)
            {
                return true;
            }
            if (Settings.onVeigarUlt && sender.ChampionName == "Veigar" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onViUlt && sender.ChampionName == "Vi" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onZedUlt && sender.ChampionName == "Zed" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onZiggsUlt && sender.ChampionName == "Ziggs" && slot == SpellSlot.R)
            {
                return true;
            }
            if (Settings.onZyraUlt && sender.ChampionName == "Zyra" && slot == SpellSlot.R)
            {
                return true;
            }



            return false;
        }
    }
}