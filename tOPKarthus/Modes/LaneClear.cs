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
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            // Only execute this mode when the orbwalker is on laneclear mode
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            var allMinions = ObjectManager.Get<Obj_AI_Base>().Where(t => Q.IsInRange(t) && t.IsValidTarget() && t.IsMinion && t.IsEnemy && t.Health < SpellManager.QDamage(t)).OrderBy(t => t.Health);

            if (allMinions == null || allMinions.Count() == 0)
            {
                allMinions = ObjectManager.Get<Obj_AI_Base>().Where(t => Q.IsInRange(t) && t.IsValidTarget() && t.IsMinion && t.IsEnemy).OrderBy(t => t.Health);
                if (allMinions == null || allMinions.Count() == 0)
                    return;
            }
            var QLocation = Prediction.Position.PredictCircularMissileAoe(allMinions.ToArray(), Q.Range, Q.Radius, Q.CastDelay, Q.Speed, Player.Instance.Position);

            if (QLocation == null || !Q.IsReady())
                return;

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

            var allMinionsE = ObjectManager.Get<Obj_AI_Base>().Where(t => E.IsInRange(t) && t.IsValidTarget() && t.IsMinion && t.IsEnemy).OrderBy(t => t.Health);
            if (allMinionsE == null)
            {
                if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 2) // 1 = off , 2 = on
                    E.Cast();
                return;
            }

            if (allMinionsE.Count() >= 3)
            {
                if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1) // 1 = off , 2 = on
                    E.Cast();
            }
            if (allMinionsE.Count() < 2)
            {
                if (Player.Instance.Spellbook.GetSpell(SpellSlot.E).ToggleState == 2) // 1 = off , 2 = on
                    E.Cast();
            }
        }
    }
}
