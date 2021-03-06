using LeagueSharp;
using LeagueSharp.SDKEx;

namespace ExorAIO.Champions.Ryze
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        /// <summary>
        ///     The methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Ryze.OnUpdate;
            Events.OnGapCloser += Ryze.OnGapCloser;
        }
    }
}