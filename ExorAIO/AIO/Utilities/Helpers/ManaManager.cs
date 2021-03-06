using LeagueSharp;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;

namespace ExorAIO.Utilities
{
    /// <summary>
    ///     The Mana manager class.
    /// </summary>
    internal class ManaManager
    {
        /// <summary>
        ///     The minimum mana needed to cast the given Spell.
        /// </summary>
        public static int GetNeededHealth(SpellSlot slot, AMenuComponent value)
            =>
                value.GetValue<MenuSliderButton>().SValue +
                (int)(GameObjects.Player.Spellbook.GetSpell(slot).ManaCost / GameObjects.Player.MaxHealth * 100);

        /// <summary>
        ///     The minimum mana needed to cast the given Spell.
        /// </summary>
        public static int GetNeededMana(SpellSlot slot, AMenuComponent value)
            =>
                value.GetValue<MenuSliderButton>().SValue +
                (int)(GameObjects.Player.Spellbook.GetSpell(slot).ManaCost / GameObjects.Player.MaxMana * 100);
    }
}