using System;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.Enumerations;

namespace ExorAIO.Champions.Cassiopeia
{
    /// <summary>
    ///     The spells class.
    /// </summary>
    internal class Spells
    {
        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Vars.Q = new Spell(SpellSlot.Q, 750f);
            Vars.W = new Spell(SpellSlot.W, 800f);
            Vars.E = new Spell(SpellSlot.E, Vars.AARange);
            Vars.R = new Spell(SpellSlot.R, 800f);

            Vars.Q.SetSkillshot(0.75f, 100f, 1000f, false, SkillshotType.SkillshotCircle);
            Vars.W.SetSkillshot(0.75f, 160f, 1000f, false, SkillshotType.SkillshotCircle);
            Vars.E.SetTargetted(0.125f, float.MaxValue);
            Vars.R.SetSkillshot(0.3f, (float) (80 * Math.PI / 180), float.MaxValue, false, SkillshotType.SkillshotCone);
        }
    }
}