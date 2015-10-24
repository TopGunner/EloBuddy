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
            onDeath();

        }

        private void onDeath()
        {

            if (Player.Instance.IsZombie)
            {
                var enemies = HeroManager.Enemies.Where(t => t.IsEnemy && t.IsValid && !t.IsDead && !t.IsInvulnerable && !t.IsZombie);
                if (!R.IsLearned || !R.IsReady() || (enemies == null) || enemies.Count() == 0)
                {
                    if (!R.IsLearned)
                        Console.WriteLine("R not learned");
                    if (!R.IsReady())
                        Console.WriteLine("R not ready");
                    if ((enemies == null) || enemies.Count() == 0)
                        Console.WriteLine("no enemies alive");
                    return;
                }
                foreach (AIHeroClient enemy in enemies)
                {
                    if ((SpellManager.RDamage(enemy) > enemy.Health) && R.IsReady())
                    {
                        Console.WriteLine("R casted for " + enemy.Name);
                        R.Cast(enemy);
                        return;
                    }
                }
            }
        }
    }
}
