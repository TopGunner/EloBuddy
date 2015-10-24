using EloBuddy;
using EloBuddy.SDK;

namespace tOPKarthus
{
    public static class SpellManager
    {
        // You will need to edit the types of spells you have for each champ as they
        // don't have the same type for each champ, for example Xerath Q is chargeable,
        // right now it's  set to Active.
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Chargeable R { get; private set; }
        public static Spell.Targeted Ignite {get; private set; }

        static SpellManager()
        {
            // Initialize spells
            Q = new Spell.Skillshot(SpellSlot.Q, /*Spell range*/ 875, EloBuddy.SDK.Enumerations.SkillShotType.Circular, 500);
            W = new Spell.Skillshot(SpellSlot.W, 1000, EloBuddy.SDK.Enumerations.SkillShotType.Circular, 0);
            E = new Spell.Active(SpellSlot.E, 425);
            R = new Spell.Chargeable(SpellSlot.R, int.MaxValue, int.MaxValue, 3000);

            var slot = ObjectManager.Player.GetSpellSlotFromName("summonerdot");
            if (slot != SpellSlot.Unknown)
            {
                Ignite = new Spell.Targeted(slot, 600);
            }

            // TODO: Uncomment the other spells to initialize them
            //W = new Spell.Chargeable(SpellSlot.W);
            //E = new Spell.Skillshot(SpellSlot.E);
            //R = new Spell.Targeted(SpellSlot.R);
        }

        public static void Initialize()
        {
            // Let the static initializer do the job, this way we avoid multiple init calls aswell
        }

        public static float QDamage(Obj_AI_Base target)
        {
            var DMG = 0f;

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
            var DMG = 0f;

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
    }
}
