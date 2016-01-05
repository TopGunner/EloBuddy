using System;
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

using Settings = PurifierVayne.Config.Modes.LaneClear;

namespace PurifierVayne.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (E.IsReady() && Settings.UseE)
            {
                foreach (var e in EntityManager.MinionsAndMonsters.CombinedAttackable.Where(t => t.IsValidTarget(E.Range) && t.Health > 1000 && t.Team == GameObjectTeam.Neutral && t.IsVisible && !t.IsDead))
                {
                    if (e.HasBuffOfType(BuffType.SpellImmunity) || e.HasBuffOfType(BuffType.SpellShield))
                        continue;

                    Vector2 castTo = Prediction.Position.PredictUnitPosition(e, 500);
                    for (int i = 0; i < 475; i += 10)
                    {
                        var coll = Player.Instance.Position.Extend(castTo, Player.Instance.Distance(castTo) + i).To3D();
                        if ((coll.ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Wall) || coll.ToNavMeshCell().CollFlags.HasFlag(CollisionFlags.Building)))
                        {
                            E.Cast(e);
                            break;
                        }
                    }
                }
            }
        }

        internal static void Spellblade(AttackableUnit target, EventArgs args)
        {
            if (!Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                return;
            if (Settings.mana >= Player.Instance.ManaPercent || !Settings.UseQ)
                return;
            SpellManager.CastQ(false);
        }
    }
}