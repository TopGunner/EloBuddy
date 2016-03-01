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
using Settings = HarleyJinx.Config.Misc;
using HarleyJinx.Modes;
namespace HarleyJinx
{
    public static class Program
    {
        // Change this line to the champion you want to make the addon for,
        // watch out for the case being correct!
        public const string ChampName = "Jinx";
        public static AIHeroClient lastTarget;
        public static float lastSeen = Game.Time;
        public static Vector3 predictedPos;

        public static void Main(string[] args)
        {
            // Wait till the loading screen has passed
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            // Verify the champion we made this addon for
            if (Player.Instance.ChampionName != ChampName)
            {
                // Champion is not the one we made this addon for,
                // therefore we return
                return;
            }

            // Initialize the classes that we need
            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
            SaveMePls.Initialize();
            if (Settings.autolevelskills)
            {
                Player.Instance.Spellbook.LevelSpell(SpellSlot.Q);
            }

            // Listen to events we need
            Drawing.OnDraw += OnDraw;
            Player.OnLevelUp += HarleyJinx.Modes.PermaActive.autoLevelSkills;
            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
            Dash.OnDash += Dash_OnDash;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            Player.OnBasicAttack += Player_OnBasicAttack;
            Game.OnTick += Game_OnTick;        
            Teleport.OnTeleport +=Teleport_OnTeleport;
        }

        private static void Teleport_OnTeleport(Obj_AI_Base sender, Teleport.TeleportEventArgs args)
        {
            if (args.Status == TeleportStatus.Start)
            {
                Core.DelayAction(() => SpellManager.E.Cast(sender.Position), 500);
            }
        }

        private static void Dash_OnDash(Obj_AI_Base sender, Dash.DashEventArgs e)
        {
            if (Settings.useEOnGapcloser && sender.IsEnemy)
            {
                Vector3 castTo = e.EndPos;
                SpellManager.E.Cast(castTo);
            }
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (Settings.useEInterrupt && sender.IsValidTarget() && sender.IsEnemy && e.DangerLevel >= Settings.interruptDangerLvl)
            {
                SpellManager.E.Cast(sender.Position);
            }
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (lastTarget != null)
            {
                if (lastTarget.IsVisible)
                {
                    predictedPos = Prediction.Position.PredictUnitPosition(lastTarget, 300).To3D();
                    lastSeen = Game.Time;
                }
                if (lastTarget.Distance(Player.Instance) > 700)
                {
                    lastTarget = null;
                }
            }
        }

        private static void Player_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender != Player.Instance)
                return;
            if (args.Target is AIHeroClient)
                lastTarget = (AIHeroClient)args.Target;
            else
                lastTarget = null;
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (Settings.useEOnGapcloser && sender.IsEnemy)
            {
                Vector3 castTo = e.End;
                SpellManager.E.Cast(castTo);
            }
        }

        private static void OnDraw(EventArgs args)
        {
            // Draw range circles of our spells
            if (Settings._drawQ.CurrentValue)
                Circle.Draw(Color.Black, SpellManager.FishbonesRange, Player.Instance.Position);
            if (Settings._drawQ.CurrentValue)
                Circle.Draw(Color.Black, SpellManager.PowpowRange, Player.Instance.Position);
            if (Settings._drawW.CurrentValue && (!Settings.drawReadySpellsOnly || SpellManager.W.IsReady()))
                Circle.Draw(Color.Aqua, SpellManager.W.Range, Player.Instance.Position);
            if (Settings._drawE.CurrentValue && (!Settings.drawReadySpellsOnly || SpellManager.E.IsReady()))
                Circle.Draw(Color.DarkGreen, SpellManager.E.Range, Player.Instance.Position);

        }
    }
}
