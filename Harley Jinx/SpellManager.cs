﻿using System;
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
using Settings = HarleyJinx.Config.Misc;

namespace HarleyJinx
{
    public static class SpellManager
    {
        // You will need to edit the types of spells you have for each champ as they
        // don't have the same type for each champ, for example Xerath Q is chargeable,
        // right now it's  set to Active.
        public static Spell.Active Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Skillshot R { get; private set; }
        public static Spell.Active heal { get; private set; }
        public static Spell.Targeted ignite { get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Active(SpellSlot.Q);
            W = new Spell.Skillshot(SpellSlot.W, 1450, SkillShotType.Linear, 600, 3200, 80);
            W.AllowedCollisionCount = 0;
            E = new Spell.Skillshot(SpellSlot.E, 900, SkillShotType.Circular, 700, int.MaxValue, 100);
            R = new Spell.Skillshot(SpellSlot.R, int.MaxValue, SkillShotType.Linear, 1000, 2500, 225);
            R.AllowedCollisionCount = int.MaxValue;
            var slot = ObjectManager.Player.GetSpellSlotFromName("summonerheal");
            if (slot != SpellSlot.Unknown)
            {
                heal = new Spell.Active(slot, 850);
            }
            var slot2 = ObjectManager.Player.GetSpellSlotFromName("summonerdot");
            if (slot2 != SpellSlot.Unknown)
            {
                ignite = new Spell.Targeted(slot2, 600);
            }

        }
        public static void Initialize()
        {
        }

        public static bool Fishbones
        {
            get { return Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 2; }
        }

        public static float Range
        {
            get {
                if (Fishbones)
                    return Player.Instance.Spellbook.GetSpell(SpellSlot.Q).Level * 25 + 50 + PowpowRange;
                else
                    return PowpowRange;
            }
        }

        public static float PowpowRange
        {
            get
            {
                return 525;
            }
        }

        public static float FishbonesRange
        {
            get
            {
                return Player.Instance.Spellbook.GetSpell(SpellSlot.Q).Level * 25 + 50 + PowpowRange;
            }
        }

        public static bool Powpow
        {
            get { return Player.Instance.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 1; }
        }

        public static void toPowpow()
        {
            if (Fishbones)
                Q.Cast();
        }
        public static void toFishBones()
        {
            if (Powpow)
                Q.Cast();
        }

        public static double getRDamage(Obj_AI_Base enemy)
        {
            if (enemy == null)
                return 0;
            float bonushp = enemy.TotalShieldHealth() + enemy.HPRegenRate * 2;
            float factor = Player.Instance.Distance(enemy) / 1500;
            if(factor > 1)
                factor = 1;
            if (!GetRCollision().Contains(enemy))
                return 0;
            double dmg = 0;
            {
                //base damage
                dmg = 200 + Player.Instance.Spellbook.GetSpell(SpellSlot.R).Level * 100;
                dmg *= factor;
                //ad scaling
                dmg += ((0.9 * factor) + 0.1) * Player.Instance.TotalAttackDamage;
                //missing HP
                dmg += (0.2 + Player.Instance.Spellbook.GetSpell(SpellSlot.R).Level * 0.05)*(enemy.MaxHealth - enemy.Health);
            }
            return dmg;
        }

        private static List<Obj_AI_Base> GetRCollision()
        {
            var collisionList = new List<Obj_AI_Base>();
            foreach (var unit in EntityManager.Heroes.Enemies.Where(t => Player.Instance.Distance(t) < 2000))
            {
                var pred = Prediction.Position.PredictLinearMissile(unit, 2000, R.Radius, R.CastDelay, R.Speed, -1);
                var endpos = Player.Instance.ServerPosition.Extend(Fountain(), 2000);
                var projectOn = pred.UnitPosition.To2D().ProjectOn(Player.Instance.ServerPosition.To2D(), endpos);
                if (projectOn.SegmentPoint.Distance(endpos) < R.Width + unit.BoundingRadius)
                {
                    collisionList.Add(unit);
                }
            }

            return collisionList;
        }
        private static Vector3 Fountain()
        {
            switch (Game.MapId)
            {
                case GameMapId.SummonersRift:
                    {
                        return Player.Instance.Team == GameObjectTeam.Order
                            ? new Vector3(14296, 14362, 171)
                            : new Vector3(408, 414, 182);
                    }
                case GameMapId.CrystalScar:
                    {
                        return Player.Instance.Team == GameObjectTeam.Order
                            ? new Vector3(524, 4164, 35)
                            : new Vector3(13323, 4105, 36);
                    }
                case GameMapId.TwistedTreeline:
                    {
                        return Player.Instance.Team == GameObjectTeam.Order
                            ? new Vector3(1060, 7297, 150)
                            : new Vector3(14353, 7297, 150);
                    }
            }
            return new Vector3();
        }
        public static bool RoverkillCheck(AIHeroClient enemy)
        {
            //TODO check if enemy.Allies = my allies??
            if (enemy.CountAlliesInRange(650) > 1)
            {
                return false;
            }
            if (enemy.IsInRange(Player.Instance, Range) && Player.Instance.CountEnemiesInRange(Range) == 1 && Player.Instance.HealthPercent < 20)
            {
                return true;
            }
            if (Player.Instance.CountEnemiesInRange(Range) > 0)
            {
                return false;
            }
            return true;
        }
    }
}
