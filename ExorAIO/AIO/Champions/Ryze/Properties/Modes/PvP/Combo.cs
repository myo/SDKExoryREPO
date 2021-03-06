using System;
using ExorAIO.Utilities;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;

namespace ExorAIO.Champions.Ryze
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Combo(EventArgs args)
        {
            if (!Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target))
            {
                return;
            }
            
            if (Bools.HasSheenBuff())
            {
                if (Targets.Target.IsValidTarget(Vars.AARange))
                {
                    return;
                }
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                GameObjects.Player.ManaPercent > 20 &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!GameObjects.Player.HasBuff("RyzePassiveCharged") &&
                    GameObjects.Player.GetBuffCount("RyzePassiveStack") == 0)
                {
                    return;
                }

                if (!Vars.Q.IsReady() &&
                    GameObjects.Player.GetBuffCount("RyzePassiveStack") == 3)
                {
                    Vars.R.Cast();
                }
                else if (GameObjects.Player.GetBuffCount("RyzePassiveStack") < 3)
                {
                    Vars.R.Cast();
                }
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Targets.Target.IsValidTarget(Vars.W.Range) &&
                Vars.Menu["spells"]["w"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.Q.IsReady() &&
                    GameObjects.Player.GetBuffCount("RyzePassiveStack") == 1)
                {
                    return;
                }

                if (GameObjects.Player.HasBuff("RyzePassiveCharged") ||
                    GameObjects.Player.GetBuffCount("RyzePassiveStack") != 0)
                {
                    Vars.W.CastOnUnit(Targets.Target);
                }
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range-50f) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            { 
                Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).UnitPosition);
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.Q.IsReady() &&
                    GameObjects.Player.GetBuffCount("RyzePassiveStack") == 1)
                {
                    return;
                }

                if (GameObjects.Player.HasBuff("RyzePassiveCharged") ||
                    GameObjects.Player.GetBuffCount("RyzePassiveStack") != 0)
                {
                    Vars.E.CastOnUnit(Targets.Target);
                }
            }
        }
    }
}