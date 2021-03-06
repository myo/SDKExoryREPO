using System;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;
using ExorAIO.Utilities;

namespace ExorAIO.Champions.Tryndamere
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
        public static void Automatic(EventArgs args)
        {
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >= 75 &&
                Health.GetPrediction(GameObjects.Player, (int)(250 + Game.Ping/2f)) <= GameObjects.Player.MaxHealth/2 &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast();
            }

            /// <summary>
            ///     The Lifesaver R Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                !Vars.Q.IsReady() &&
                GameObjects.Player.CountEnemyHeroesInRange(700f) > 0 &&
                Health.GetPrediction(GameObjects.Player, (int)(250 + Game.Ping/2f)) <= GameObjects.Player.MaxHealth/5 &&
                Vars.Menu["spells"]["r"]["lifesaver"].GetValue<MenuBool>().Value)
            {
                Vars.R.Cast();
            }
        }
    }
}