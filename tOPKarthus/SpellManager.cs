using EloBuddy;
using EloBuddy.SDK;
using System;

namespace tOPKarthus
{
    public static class SpellManager
    {
        private static bool debug = true;
        // You will need to edit the types of spells you have for each champ as they
        // don't have the same type for each champ, for example Xerath Q is chargeable,
        // right now it's  set to Active.
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Skillshot R { get; private set; }
        public static Spell.Targeted Ignite {get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, /*Spell range*/ 875, EloBuddy.SDK.Enumerations.SkillShotType.Circular, 1000, int.MaxValue, 160);
            W = new Spell.Skillshot(SpellSlot.W, 1000, EloBuddy.SDK.Enumerations.SkillShotType.Circular, 100, int.MaxValue, 50);
            E = new Spell.Active(SpellSlot.E, 425);
            R = new Spell.Skillshot(SpellSlot.R, 30000, EloBuddy.SDK.Enumerations.SkillShotType.Circular, 3000, int.MaxValue, int.MaxValue);

            var slot = ObjectManager.Player.GetSpellSlotFromName("summonerdot");
            if (slot != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(slot, 600);
            }
        }

        public static void Initialize()
        {
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender == null || args == null)
                return;

            if (sender.GetAutoAttackDamage(Player.Instance) > Player.Instance.Health && !Player.Instance.IsDead && !Player.Instance.IsInvulnerable && !Player.Instance.IsZombie && args.Target.NetworkId == Player.Instance.NetworkId)
            {
                castZhonyasSeraphs();
            }
            var attacker = sender as AIHeroClient;
            if (attacker != null)
            {
                var slot = attacker.GetSpellSlotFromName(args.SData.Name);

                if (slot != SpellSlot.Unknown)
                {
                    float dmg = 0;
                    if (slot == attacker.GetSpellSlotFromName("SummonerDot") && args.Target != null && args.Target.NetworkId == Player.Instance.NetworkId)
                    {
                        dmg = attacker.GetSummonerSpellDamage(Player.Instance, DamageLibrary.SummonerSpells.Ignite);
                    }
                    else
                    {
                        if ((args.Target != null && args.Target.NetworkId == Player.Instance.NetworkId) || args.End.Distance(Player.Instance.ServerPosition) < Math.Pow(args.SData.LineWidth, 2))
                        {
                            dmg = attacker.GetSpellDamage(Player.Instance, slot);
                        }
                    }
                    if (dmg > Player.Instance.Health)
                    {
                        castZhonyasSeraphs();
                    }
                    else if (dmg > Player.Instance.MaxMana * 0.2)
                    {
                        castSeraphs();
                    }
                }
            }
        }

        private static void castZhonyasSeraphs()
        {
            if (!castZhonyas())
            {
                castSeraphs();
            }
        }

        private static bool castSeraphs()
        {
            InventorySlot[] inv = Player.Instance.InventoryItems;
            foreach (var item in inv)
            {
                if (item.Name.Contains("Seraphs"))
                {
                    Console.WriteLine("Seraphs found");
                    return item.Cast();
                }
            }
            return false;
        }

        private static bool castZhonyas()
        {
            InventorySlot[] inv = Player.Instance.InventoryItems;
            foreach (var item in inv)
            {
                if (item.Id == ItemId.Zhonyas_Hourglass)
                {
                    Console.WriteLine("Zhonyas found");
                    return item.Cast();
                }
            }
            return false;
        }
        public static float QDamage(Obj_AI_Base target)
        {
            float DMG = 0f;

            if (SpellManager.Q.Level == 1)
            {
                DMG = 80f + (0.6f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.Q.Level == 2)
            {
                DMG = 120f + (0.6f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.Q.Level == 3)
            {
                DMG = 160f + (0.6f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.Q.Level == 4)
            {
                DMG = 200f + (0.6f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.Q.Level == 5)
            {
                DMG = 240f + (0.6f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }

            return DMG;
        }

        public static float QMultitargetDamage(Obj_AI_Base target)
        {
            return QDamage(target) / 2;
        }

        public static float RDamage(Obj_AI_Base target)
        {
            float DMG = 0f;

            if (SpellManager.R.Level == 1)
            {
                DMG = 250f + (0.62f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.R.Level == 2)
            {
                DMG = 400f + (0.62f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }
            if (SpellManager.R.Level == 3)
            {
                DMG = 550f + (0.62f * Player.Instance.FlatMagicDamageMod);
                DMG = Player.Instance.CalculateDamageOnUnit(target, DamageType.Magical, DMG);
            }

            return DMG;
        }

        public static bool hasTear()
        {
            InventorySlot[] inv = Player.Instance.InventoryItems;
            foreach (var item in inv)
            {
                if (item.Id == ItemId.Archangels_Staff || item.Id == ItemId.Archangels_Staff_Crystal_Scar || item.Id == ItemId.Tear_of_the_Goddess || item.Id == ItemId.Tear_of_the_Goddess_Crystal_Scar || item.Name.Contains("Seraphs"))
                {
                    if (debug)
                    {
                        System.Console.WriteLine("Found: " + item.Name);
                        debug = false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
