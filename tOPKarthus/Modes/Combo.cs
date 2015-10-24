using EloBuddy;
using EloBuddy.SDK;

namespace tOPKarthus.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on combo mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            if (Q.IsReady())
            {
                var Target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (Target != null && Target.IsValid)
                {
                    var Pred = Q.GetPrediction(Target);
                    Q.Cast(Pred.CastPosition);
                }
            }
            if (W.IsReady())
            {
                var Target = TargetSelector.GetTarget(W.Range, DamageType.Magical);
                if (Target != null && Target.IsValid)
                {
                    var Pred = W.GetPrediction(Target);
                    W.Cast(Pred.CastPosition);
                }
            }
            if (SpellManager.Ignite != null && SpellManager.Ignite.IsReady())
            {
                foreach (AIHeroClient enemy in HeroManager.Enemies)
                {
                    if (enemy.IsValidTarget(SpellManager.Ignite.Range) && ObjectManager.Player.GetSummonerSpellDamage(enemy, DamageLibrary.SummonerSpells.Ignite) >= enemy.Health)
                        SpellManager.Ignite.Cast(enemy);
                }
            }
        }
    }
}
