using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;
using LeagueSharp.Data.Enumerations;

namespace ExorAIO.Champions.Karma
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
        public static void Killsteal(EventArgs args)
        {
            /// <summary>
            ///     The KillSteal Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsValidTarget(Vars.Q.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical) &&
                        Vars.GetRealHealth(t) <
                            (float)GameObjects.Player.GetSpellDamage(t, SpellSlot.Q) +
                                (Vars.R.IsReady()
                                    ? (float)GameObjects.Player.GetSpellDamage(t, SpellSlot.Q, DamageStage.Empowered)
                                    : 0)))
                {
                    if (!Vars.Q.GetPrediction(target).CollisionObjects.Any(c => Targets.Minions.Contains(c)))
                    {
                        if (Vars.R.IsReady() &&
                            Vars.Menu["spells"]["r"]["empq"].GetValue<MenuBool>().Value)
                        {
                            Vars.R.Cast();
                        }

                        Vars.Q.Cast(Vars.Q.GetPrediction(target).UnitPosition);
                    }
                }
            }
        }
    }
}