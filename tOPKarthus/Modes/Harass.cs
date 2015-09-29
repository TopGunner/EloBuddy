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
            var allMinions = ObjectManager.Get<Obj_AI_Base>().Where(t => Q.IsInRange(t) && t.IsValidTarget() && t.IsMinion && t.IsEnemy && t.Health < SpellManager.QMultitargetDamage(t)).OrderBy(t => t.Health);

            if (allMinions == null || allMinions.Count() == 0)
            {
                //LasthitSolotarget();
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
            Q.Cast(bestPred.CastPosition);
        }

        private void LasthitSolotarget()
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
            foreach (var pred in QLocation)
            {
                if (Q.IsInRange(pred.CastPosition) && pred.CollisionObjects.Count() == 1)
                {
                    Q.Cast(pred.CastPosition);
                }
            }
        }

        private void HarassChampion()
        {
            if (!Q.IsReady())
                return;

            var Target = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            if (Target != null && Target.IsValid)
            {
                var Pred = Q.GetPrediction(Target);
                Q.Cast(Pred.CastPosition);
            }
        }
    }
}
