using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace tOPKarthus.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Since this is permaactive mode, always execute the loop
            return true;
        }

        public override void Execute()
        {
            var Target = TargetSelector.GetTarget(E.Range + 30, DamageType.Magical);
            if (Target != null && Target.IsValid)
            {
                if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1) // 1 = off , 2 = on
                    E.Cast();
            }
            else
            {
                if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 2 
                    && !( Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear) 
                    || Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear) )) // 1 = off , 2 = on
                    E.Cast();
            }
            if (Player.Instance.IsZombie)
            {
                var enemies = HeroManager.Enemies.Where(t => t.IsEnemy && t.IsValid && (t.Health - SpellManager.RDamage(t)) <= 0 && !t.IsDead);
                if(R.IsLearned && R.IsReady())
                    R.Cast();
            }

        }
    }
}
