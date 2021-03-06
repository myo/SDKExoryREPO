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
        public static void Harass(EventArgs args)
        {
            if (!Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target) ||
                Targets.Target.HasBuff("luxilluminatingfraulein"))
            {
                return;
            }

            /// <summary>
            ///     The E Harass Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.E.Range) &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.E).ToggleState == 1 &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.E.Slot, Vars.Menu["spells"]["e"]["harass"]) &&
                Vars.Menu["spells"]["e"]["harass"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.E.Cast(Vars.E.GetPrediction(Targets.Target).UnitPosition);
            }
        }
    }
}