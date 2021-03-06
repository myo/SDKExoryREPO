using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.Enumerations;
using LeagueSharp.SDKEx.Utils;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.Data.Enumerations;

namespace ExorAIO.Champions.Lucian
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal class Lucian
    {
        /// <summary>
        ///     Loads Lucian.
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
        ///     Fired when the game is updated.
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

            if (GameObjects.Player.IsWindingUp ||
                GameObjects.Player.HasBuff("LucianR"))
            {
                return;
            }
            
            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Logics.Killsteal(args);

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    Logics.Combo(args);
                    break;

                case OrbwalkingMode.LaneClear:
                    Logics.Clear(args);
                    break;

                case OrbwalkingMode.Hybrid:
                    Logics.Harass(args);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        public static void OnDoCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe &&
                !GameObjects.Player.HasBuff("LucianR") &&
                AutoAttack.IsAutoAttack(args.SData.Name))
            {
                /// <summary>
                ///     Initializes the orbwalkingmodes.
                /// </summary>
                switch (Variables.Orbwalker.ActiveMode)
                {
                    case OrbwalkingMode.Combo:
                        Logics.Weaving(sender, args);
                        break;

                    case OrbwalkingMode.LaneClear:
                        Logics.JungleClear(sender, args);
                        Logics.BuildingClear(sender, args);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        ///     Celled on animation trigger.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="GameObjectPlayAnimationEventArgs" /> instance containing the event data.</param>
        public static void OnPlayAnimation(Obj_AI_Base sender, GameObjectPlayAnimationEventArgs args)
        {
            if (sender.IsMe &&
                Variables.Orbwalker.ActiveMode != OrbwalkingMode.None)
            {
                if (args.Animation.Equals("Spell1") ||
                    args.Animation.Equals("Spell2"))
                {
                    GameObjects.Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                }
            }
        }

        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (Vars.E.IsReady() &&
                args.Sender.IsMelee &&
                args.Sender.IsValidTarget(Vars.E.Range) &&
                args.SkillType == GapcloserType.Targeted &&
                GameObjects.Player.Distance(args.End) <
                    args.Sender.GetRealAutoAttackRange(args.Sender) &&
                Vars.Menu["spells"]["e"]["gapcloser"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(GameObjects.Player.ServerPosition.Extend(args.Sender.ServerPosition, -(Vars.E.Range - Vars.AARange)));
            }
        }
    }
}