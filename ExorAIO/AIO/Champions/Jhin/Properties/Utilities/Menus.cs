using ExorAIO.Utilities;
using LeagueSharp.SDKEx;
using LeagueSharp.SDKEx.UI;

namespace ExorAIO.Champions.Jhin
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     Sets the spells menu.
            /// </summary>
            Vars.SpellsMenu = new Menu("spells", "Spells");
            {
                /// <summary>
                ///     Sets the menu for the Q.
                /// </summary>
                Vars.QMenu = new Menu("q", "Use Q to:");
                {
                    Vars.QMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.QMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.QMenu.Add(new MenuSliderButton("lasthit", "LastHit / if Mana >= x%",  0, 0, 99, true));
                    Vars.QMenu.Add(new MenuSliderButton("harass",  "Harass / if Mana >= x%",  50, 0, 99, true));
                    Vars.QMenu.Add(new MenuSliderButton("clear",   "Clear / if Mana >= x%",   50, 0, 99, true));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("logical",   "Logical",   true));
                    Vars.WMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.WMenu.Add(new MenuSliderButton("clear", "Clear / if Mana >= x%", 50, 0, 99, true));
                    {
                        /// <summary>
                        ///     Sets the menu for the W Whitelist.
                        /// </summary>
                        Vars.WhiteListMenu = new Menu("whitelist", "W: Whitelist Menu", true);
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                Vars.WhiteListMenu.Add(
                                    new MenuBool(
                                        target.ChampionName.ToLower(),
                                        $"Use against: {target.ChampionName}",
                                        true));
                            }
                        }
                        Vars.WMenu.Add(Vars.WhiteListMenu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("logical",   "Logical",        true));
                    Vars.EMenu.Add(new MenuBool("gapcloser", "Anti-Gapcloser", true));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                Vars.RMenu = new Menu("r", "Use R to:");
                {
                    Vars.RMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.RMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.RMenu.Add(new MenuBool("nearmouse", "Focus the enemy nearest to your cursor"));
                    Vars.RMenu.Add(
                        new MenuSeparator("separator", "- You need to manually start the Ultimate. -"));
                    {
                        /// <summary>
                        ///     Sets the menu for the R Whitelist.
                        /// </summary>
                        Vars.WhiteList2Menu = new Menu("whitelist", "R: Whitelist Menu", true);
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                Vars.WhiteList2Menu.Add(
                                    new MenuBool(
                                        target.ChampionName.ToLower(),
                                        $"Use against: {target.ChampionName}",
                                        true));
                            }
                        }
                        Vars.RMenu.Add(Vars.WhiteList2Menu);
                    }
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
                Vars.DrawingsMenu.Add(new MenuBool("r", "R Range"));
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}