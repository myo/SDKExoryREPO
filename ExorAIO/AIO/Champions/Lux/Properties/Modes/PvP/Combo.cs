using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;

namespace ExorAIO.Champions.Lux
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
            if (Bools.HasSheenBuff())
            {
                return;
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                !Vars.Q.IsReady() &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).ToggleState != 1 &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        t.IsValidTarget(Vars.R.Range) &&
                        Invulnerable.Check(Targets.Target) &&
                        t.HasBuff("luxilluminatingfraulein") &&
                        !Invulnerable.Check(t, DamageType.Magical)))
                {
                    Vars.R.Cast(target.ServerPosition);
                }
            }
            
            if (!Targets.Target.IsValidTarget())
            {
                return;
            }
            
            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).ToggleState != 1 &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast(Vars.E.GetPrediction(Targets.Target).CastPosition);
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                !Invulnerable.Check(Targets.Target) &&
                Targets.Target.IsValidTarget(Vars.Q.Range) &&
                !Targets.Target.HasBuff("luxilluminatingfraulein") &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                if (Vars.Q.GetPrediction(Targets.Target).CollisionObjects.Count(c => Targets.Minions.Contains(c)) <= 1)
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).UnitPosition);
                }
            }
        }
    }
}