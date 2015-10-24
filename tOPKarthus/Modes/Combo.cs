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
                var t = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
                if (t != null && t.IsValid && t.IsEnemy && !t.IsDead && !t.IsInvulnerable && !t.IsZombie)
                {
                    var Pred = Q.GetPrediction(t);
                    Q.Cast(Pred.CastPosition);
                }
            }
            if (W.IsReady())
            {
                var t = TargetSelector.GetTarget(W.Range, DamageType.Magical);
                if (t != null && t.IsValid && t.IsEnemy && !t.IsDead && !t.IsInvulnerable && !t.IsZombie)
                {
                    var Pred = W.GetPrediction(t);
                    W.Cast(Pred.CastPosition);
                }
            }
            if (SpellManager.Ignite != null && SpellManager.Ignite.IsReady())
            {
                foreach (AIHeroClient enemy in HeroManager.Enemies)
                {
                    if (enemy != null && enemy.IsValid && enemy.IsEnemy && !enemy.IsDead && !enemy.IsInvulnerable && !enemy.IsZombie)
                    {
                        if (enemy.IsValidTarget(SpellManager.Ignite.Range) && ObjectManager.Player.GetSummonerSpellDamage(enemy, DamageLibrary.SummonerSpells.Ignite) >= enemy.Health)
                            SpellManager.Ignite.Cast(enemy);
                    }
                }
            }
        }
    }
}
