using System;
using ExorAIO.Utilities;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.Enumerations;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;

namespace ExorAIO.Champions.Cassiopeia
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Cassiopeia
    {
        /// <summary>
        ///     Loads Cassiopeia.
        /// </summary>
        public void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus.Initialize();

            /// <summary>
            ///     Initializes the spells.
            /// </summary>
            Spells.Initialize();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods.Initialize();

            /// <summary>
            ///     Initializes the drawings.
            /// </summary>
            Drawings.Initialize();
        }

        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (GameObjects.Player.IsDead)
            {
                return;
            }

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Logics.Automatic(args);

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Logics.Killsteal(args);

            if (GameObjects.Player.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    Logics.Combo(args);
                    break;

                case OrbwalkingMode.Hybrid:
                    Logics.Harass(args);
                    break;

                case OrbwalkingMode.LastHit:
                    Logics.LastHit(args);
                    break;

                case OrbwalkingMode.LaneClear:
                    Logics.Clear(args);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (Vars.W.IsReady() &&
                args.Sender.IsValidTarget(Vars.W.Range) &&
                GameObjects.Player.Distance(args.End) > 550 &&
                Vars.Menu["spells"]["w"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.W.Cast(args.End);
                return;
            }

            if (Vars.R.IsReady() &&
                !Invulnerable.Check(args.Sender) &&
                args.Sender.IsValidTarget(Vars.R.Range) &&
                args.Sender.IsFacing(GameObjects.Player) &&
                Vars.Menu["spells"]["r"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.R.Cast(args.End);
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public static void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (Vars.R.IsReady() &&
                !Invulnerable.Check(args.Sender) &&
                args.Sender.IsValidTarget(Vars.R.Range) &&
                args.Sender.IsFacing(GameObjects.Player) &&
                Vars.Menu["spells"]["r"]["interrupter"].GetValue<MenuBool>().Value)
            {
                Vars.R.Cast(args.Sender.ServerPosition);
            }
        }
    }
}