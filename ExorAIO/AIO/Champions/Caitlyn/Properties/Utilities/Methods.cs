using LeagueSharp;
using LeagueSharp.SDKEx;

namespace ExorAIO.Champions.Caitlyn
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Caitlyn.OnUpdate;
            Events.OnGapCloser += Caitlyn.OnGapCloser;
        }
    }
}