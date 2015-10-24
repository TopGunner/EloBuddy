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
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on harass mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            var allMinions = ObjectManager.Get<Obj_AI_Base>().Where(t => Q.IsInRange(t) && t.IsValidTarget() && t.IsMinion && t.IsEnemy && t.Health < SpellManager.QDamage(t)).OrderBy(t => t.Health);

            if (allMinions == null || allMinions.Count() == 0)
            {
                HarassChampion();
                return;
            }

            var QLocation = Prediction.Position.PredictCircularMissileAoe(allMinions.ToArray(), Q.Range, Q.Radius, Q.CastDelay, Q.Speed, Player.Instance.Position);

            if (QLocation == null || !Q.IsReady())
            {
                HarassChampion();
                return;
            }

            int collisionobjs = 0;
            var bestPred = QLocation[0];
            foreach (var pred in QLocation)
            {
                if (Q.IsInRange(pred.CastPosition) && pred.CollisionObjects.Count() > collisionobjs)
                {
                    collisionobjs = pred.CollisionObjects.Count();
                    bestPred = pred;
                }
            }
            if (bestPred != null)
                Q.Cast(bestPred.CastPosition);
        }

        private void HarassChampion()
        {

            if (!Q.IsReady() || (Player.Instance.Mana/Player.Instance.MaxMana < 0.4 && !SpellManager.hasTear()))
            {
                if ((Player.Instance.Mana / Player.Instance.MaxMana < 0.4 && !SpellManager.hasTear()))
                    Console.WriteLine("no mana and no tear");

                return;
            }

            var t = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            if (t != null && t.IsValid && t.IsEnemy && !t.IsDead && !t.IsInvulnerable && !t.IsZombie)
            {
                var Pred = Q.GetPrediction(t);
                if(Pred != null)
                    Q.Cast(Pred.CastPosition);
            }
        }
    }
}
