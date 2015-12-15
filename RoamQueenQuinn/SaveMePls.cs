using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using Settings = RoamQueenQuinn.Config.Misc;

namespace RoamQueenQuinn
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
            if (Settings.useHeal)
            {
                if ((Player.Instance.HealthPercent < 5 && Player.Instance.CountEnemiesInRange(500) > 0) || (IncomingDamage > Player.Instance.Health && IncomingDamage < Player.Instance.Health + 75 + (15 * Player.Instance.Level)))
                {
                    if (SpellManager.heal != null && SpellManager.heal.Cast())
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
    }
}