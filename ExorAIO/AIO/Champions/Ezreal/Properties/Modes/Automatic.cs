using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.Enumerations;
using LeagueSharp.SDKEx.UI;

namespace ExorAIO.Champions.Ezreal
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
            ///     The Q LastHit Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["farmhelper"]) &&
                Vars.Menu["spells"]["q"]["farmhelper"].GetValue<MenuSliderButton>().BValue)
            {
                foreach (var minion in Targets.Minions.Where(
                    m =>
                        !m.IsValidTarget(Vars.AARange) &&
                        Vars.GetRealHealth(m) > GameObjects.Player.GetAutoAttackDamage(m) &&
                        Vars.GetRealHealth(m) < (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)).OrderBy(
                            o =>
                                o.MaxHealth))
                {
                    if (!Vars.Q.GetPrediction(minion).CollisionObjects.Any(c => Targets.Minions.Contains(c)))
                    {
                        Vars.Q.Cast(Vars.Q.GetPrediction(minion).UnitPosition);
                    }
                }
            }

            /// <summary>
            ///     The Tear Stacking Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                !Targets.Minions.Any() &&
                Bools.HasTear(GameObjects.Player) &&
                Variables.Orbwalker.ActiveMode == OrbwalkingMode.None &&
                GameObjects.Player.CountEnemyHeroesInRange(1500) == 0 &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["miscellaneous"]["tear"]) &&
                Vars.Menu["miscellaneous"]["tear"].GetValue<MenuSliderButton>().BValue)
            {
                Vars.Q.Cast(Game.CursorPos);
            }

            if (GameObjects.Player.TotalMagicalDamage > GameObjects.Player.TotalAttackDamage)
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Variables.Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    if (Variables.Orbwalker.GetTarget() as Obj_AI_Hero == null)
                    {
                        return;
                    }
                    break;

                case OrbwalkingMode.LaneClear:
                    if (Variables.Orbwalker.GetTarget() as Obj_HQ == null &&
                        Variables.Orbwalker.GetTarget() as Obj_AI_Turret  == null &&
                        Variables.Orbwalker.GetTarget() as Obj_BarracksDampener == null)
                    {
                        return;
                    }
                    break;

                default:
                    break;
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                ObjectManager.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.W.Slot, Vars.Menu["spells"]["w"]["logical"]) &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuSliderButton>().BValue)
            {
                foreach (var target in GameObjects.AllyHeroes.Where(
                    t =>
                        !t.IsMe &&
                        t.IsWindingUp &&
                        t.IsValidTarget(Vars.W.Range, false)))
                {
                    Vars.W.Cast(Vars.W.GetPrediction(target).UnitPosition);
                }
            }
        }
    }
}