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
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on jungleclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (Game.MapId == GameMapId.HowlingAbyss)
            {
                return;
            }
            var targt = ObjectManager.Get<Obj_AI_Minion>()
                    .Where(a => a.Team == GameObjectTeam.Neutral && a.Distance(Player.Instance) < Q.Range)
                    .OrderByDescending(a => a.MaxHealth);
            if (targt == null || targt.Count() == 0)
                return;

            var QLocation = Prediction.Position.PredictCircularMissileAoe(targt.ToArray(), Q.Range, Q.Radius, Q.CastDelay, Q.Speed, Player.Instance.Position);

            if (QLocation == null || !Q.IsReady())
            {
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
    }
}
