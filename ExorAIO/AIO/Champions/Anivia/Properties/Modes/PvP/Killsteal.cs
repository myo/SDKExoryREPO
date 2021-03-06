using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;
using LeagueSharp.Data.Enumerations;

namespace ExorAIO.Champions.Anivia
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
            ///     The KillSteal E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.E.Range) &&
                        Vars.GetRealHealth(t) <
                            (float)GameObjects.Player.GetSpellDamage(t, SpellSlot.E)))
                {
                    Vars.E.CastOnUnit(target);
                    return;
                }
            }

            /// <summary>
            ///     The KillSteal Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 1 &&
                Vars.Menu["spells"]["q"]["killsteal"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.Q.Range) &&
                        Vars.GetRealHealth(t) <
                            (float)GameObjects.Player.GetSpellDamage(t, SpellSlot.Q)))
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(target).UnitPosition);
                }
            }
        }
    }
}