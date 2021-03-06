using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.Enumerations;
using LeagueSharp.SDKEx.UI;
using LeagueSharp.SDKEx.Utils;

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
        public static void Automatic(EventArgs args)
        {
            if (GameObjects.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The R Stacking Manager.
            /// </summary>
            if (GameObjects.Player.InFountain() &&
                Bools.HasTear(GameObjects.Player) &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.R).ToggleState == 1 &&
                Vars.Menu["miscellaneous"]["tear"].GetValue<MenuBool>().Value)
            {
                Vars.R.Cast(Game.CursorPos);
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.Q).ToggleState == 1 &&
                Vars.Menu["spells"]["q"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.Q.Range)))
                {
                    Vars.Q.Cast(Vars.Q.GetPrediction(target).UnitPosition);
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.W.Range)))
                {
                    Vars.W.Cast(
                        GameObjects.Player.ServerPosition.Extend(
                            target.ServerPosition, GameObjects.Player.Distance(target)+20f));
                }
            }

            /// <summary>
            ///     The Q Missile Manager.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Anivia.QMissile != null &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.Q).ToggleState != 1)
            {
                switch (Variables.Orbwalker.ActiveMode)
                {
                    /// <summary>
                    ///     The Q Clear Logic.
                    /// </summary>
                    case OrbwalkingMode.LaneClear:

                        if (!Vars.Menu["spells"]["q"]["clear"].GetValue<MenuSliderButton>().BValue)
                        {
                            return;
                        }

                        if (Anivia.QMissile.Position.CountEnemyHeroesInRange(Vars.Q.Width*2-10f) > 0)
                        {
                            Vars.Q.Cast();
                        }
                        else if (Targets.QMinions.Count() >= 2)
                        {
                            Vars.Q.Cast();
                        }
                        break;

                    /// <summary>
                    ///     The Default Q Logic.
                    /// </summary>
                    default:

                        if (!Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
                        {
                            return;
                        }

                        if (Anivia.QMissile.Position.CountEnemyHeroesInRange(Vars.Q.Width*2-10f) > 0)
                        {
                            Vars.Q.Cast();
                        }
                        
                        break;
                }
            }

            /// <summary>
            ///     The R Missile Manager.
            /// </summary>
            if (Vars.R.IsReady() &&
                Anivia.RMissile != null &&
                !GameObjects.Player.InFountain() &&
                GameObjects.Player.Spellbook.GetSpell(SpellSlot.R).ToggleState != 1)
            {
                switch (Variables.Orbwalker.ActiveMode)
                {
                    /// <summary>
                    ///     The R Clear Logic.
                    /// </summary>
                    case OrbwalkingMode.LaneClear:

                        if (!Vars.Menu["spells"]["r"]["clear"].GetValue<MenuSliderButton>().BValue)
                        {
                            return;
                        }

                        if (GameObjects.Player.ManaPercent <
                                ManaManager.GetNeededMana(Vars.R.Slot, Vars.Menu["spells"]["r"]["clear"]))
                        {
                            Vars.R.Cast();
                        }
                        if (!Targets.RMinions.Any())
                        {
                            Vars.R.Cast();
                        }
                        break;

                    /// <summary>
                    ///     The Default R Logic.
                    /// </summary>
                    default:

                        if (!Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
                        {
                            return;
                        }

                        if (Anivia.RMissile.Position.CountEnemyHeroesInRange(Vars.R.Width+250f) < 1)
                        {
                            Vars.R.Cast();
                        }
                        break;
                }
            }
        }
    }
}