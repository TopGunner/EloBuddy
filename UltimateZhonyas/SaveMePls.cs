using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using Settings = UltimateZhonyas.Config.Misc;

namespace UltimateZhonyas
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
                if (item.Id == ItemId.Zhonyas_Hourglass && item.CanUseItem())
                {
                    return item.Cast();
                }
            }
            return false;
        }

        private static void OnUpdate(EventArgs args)
        {
            if (Settings.useZhonyasDmg)
            {
                if (Player.Instance.HealthPercent < 5 && Player.Instance.CountEnemiesInRange(500) > 0 ||
                    IncomingDamage > Player.Instance.Health)
                    if (castZhonyas())
                        return;
            }
            if (Settings.useSeraphsDmg)
            {
                if (Player.Instance.HealthPercent < 5 && Player.Instance.CountEnemiesInRange(500) > 0 ||
                    IncomingDamage > Player.Instance.Health || IncomingDamage > Player.Instance.MaxMana*0.2)
                    if (castSeraphs())
                        return;
            }

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
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {

            if (sender.IsEnemy)
            {
                if (args.Target != null && args.Target.NetworkId == Player.Instance.NetworkId && sender is AIHeroClient && dangerousSpell(args.SData, (AIHeroClient)sender))
                {
                    var slot = ((AIHeroClient)sender).GetSpellSlotFromName(args.SData.Name);
                    //TODO TEST!
                    if (Player.Instance.ServerPosition.Distance(sender.ServerPosition) < 2500)
                    {
                        if (!castZhonyas())
                            castSeraphs();
                    }
                }
                if ((!(sender is AIHeroClient) || args.SData.IsAutoAttack()) && args.Target != null && args.Target.NetworkId == Player.Instance.NetworkId)
                {
                    IncDamage[Player.Instance.ServerPosition.Distance(sender.ServerPosition) / args.SData.MissileSpeed + Game.Time] = sender.GetAutoAttackDamage(Player.Instance);
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
            var enemies = EntityManager.Heroes.Enemies;
            int j2 = 0;
            for (int j = 0; j < enemies.Count(); j++)
            {
                if (sender.NetworkId == enemies[j].NetworkId)
                {
                    j2 = j;
                    break;
                }
            }
            int i = 0;
            switch (slot)
            {
                case SpellSlot.Q:
                    i = 0;
                    break;
                case SpellSlot.W:
                    i = 1;
                    break;
                case SpellSlot.E:
                    i = 2;
                    break;
                case SpellSlot.R:
                    i = 3;
                    break;
            }
            if (Settings._skills[j2 * 4 + i].CurrentValue)
                return true;
            return false;
        }
    }
}