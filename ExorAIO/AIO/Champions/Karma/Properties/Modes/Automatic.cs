using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.Enumerations;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;

namespace ExorAIO.Champions.Karma
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Automatic(EventArgs args)
        {
            /// <summary>
            ///     The Support Mode Option.
            /// </summary>
            if (Vars.Menu["miscellaneous"]["support"].GetValue<MenuBool>().Value)
            {
                Variables.Orbwalker.SetAttackState(
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.Hybrid &&
                    Variables.Orbwalker.ActiveMode != OrbwalkingMode.LaneClear);
            }

            /// <summary>
            ///     The AoE E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                GameObjects.Player.CountEnemyHeroesInRange(2000f) >= 2 &&
                GameObjects.Player.CountAllyHeroesInRange(600f) >=
                    Vars.Menu["spells"]["e"]["engager"].GetValue<MenuSliderButton>().SValue + 1 &&
                Vars.Menu["spells"]["e"]["engager"].GetValue<MenuSliderButton>().BValue)
            {
                if (Vars.R.IsReady() &&
                    Vars.Menu["spells"]["r"]["empe"].GetValue<MenuBool>().Value)
                {
                    Vars.R.Cast();
                }

                Vars.E.CastOnUnit(GameObjects.Player);
            }
        }
    }
}