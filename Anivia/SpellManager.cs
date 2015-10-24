using System;
using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using Settings = Anivia.Config.Misc;

namespace Anivia
{
    public static class SpellManager
    {
        // You will need to edit the types of spells you have for each champ as they
        // don't have the same type for each champ, for example Xerath Q is chargeable,
        // right now it's  set to Active.
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Skillshot R { get; private set; }
        public static Spell.Active cleanse { get; private set; }
        public static Spell.Targeted ignite { get; private set; }
        public static Vector3 RlastCast;

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, 1075, EloBuddy.SDK.Enumerations.SkillShotType.Linear, 0, 850, 110);
            Q.AllowedCollisionCount = int.MaxValue;
            W = new Spell.Skillshot(SpellSlot.W, 1000, EloBuddy.SDK.Enumerations.SkillShotType.Circular, 0, int.MaxValue, 1);
            E = new Spell.Targeted(SpellSlot.E, 650);
            R = new Spell.Skillshot(SpellSlot.R, 625, EloBuddy.SDK.Enumerations.SkillShotType.Circular, 0, int.MaxValue, 400);
            var slot = ObjectManager.Player.GetSpellSlotFromName("summonerboost");
            if (slot != SpellSlot.Unknown)
            {
                cleanse = new Spell.Active(slot);
            }
            var slot2 = ObjectManager.Player.GetSpellSlotFromName("summonerdot");
            if (slot2 != SpellSlot.Unknown)
            {
                ignite = new Spell.Targeted(slot2, 600);
            }
        }

        public static void Initialize()
        {
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (Settings.autoInterrupt && sender.IsEnemy && sender is AIHeroClient && sender.IsInRange(Player.Instance, W.Range))
            {
                var attacker = sender as AIHeroClient;
                if (attacker == null)
                    return;

                var slot = attacker.GetSpellSlotFromName(args.SData.Name);
                if (slot == SpellSlot.Unknown)
                    return;

                switch (attacker.ChampionName)
                {
                    case "Fiddlesticks":
                    case "Galio":
                    case "Janna":
                    case "Karthus":
                    case "Katarina":
                    case "Malzahar":
                    case "MissFortune":
                    case "Nunu":
                    case "Pantheon":
                    case "TwistedFate":
                    case "Warwick":
                    case "Caitlyn":
                    case "Shen":
                        if (slot == SpellSlot.R)
                        {
                            if(W.IsReady())
                                W.Cast(attacker.Position);
                        }
                        break;
                }

            }
        }

        public static float DamageToHero(AIHeroClient unit)
        {
            float dmg = 0;
            if(unit.IsEnemy && !unit.IsZombie && !unit.IsDead && unit.IsValid && !unit.IsInvulnerable)
            {
                if(ignite != null && ignite.IsReady())
                    dmg += DamageLibrary.GetSummonerSpellDamage(Player.Instance, unit, DamageLibrary.SummonerSpells.Ignite);
                if (Q.IsReady())
                    dmg += DamageLibrary.GetSpellDamage(Player.Instance, unit, SpellSlot.Q) * 2;
                if (E.IsReady() && (Q.IsReady() || R.IsReady()))
                    dmg += DamageLibrary.GetSpellDamage(Player.Instance, unit, SpellSlot.E);
                if (E.IsReady())
                    dmg += DamageLibrary.GetSpellDamage(Player.Instance, unit, SpellSlot.E);
                if (R.IsReady())
                    dmg += DamageLibrary.GetSpellDamage(Player.Instance, unit, SpellSlot.R);
            }
            return dmg;
        }
    }
}
