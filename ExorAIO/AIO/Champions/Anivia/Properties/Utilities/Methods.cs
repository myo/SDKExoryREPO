using LeagueSharp;
using LeagueSharp.SDKEx;

namespace ExorAIO.Champions.Anivia
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
            Game.OnUpdate += Anivia.OnUpdate;
            GameObject.OnCreate += Anivia.OnCreate;
            GameObject.OnDelete += Anivia.OnDelete;
            Events.OnGapCloser += Anivia.OnGapCloser;
            Events.OnInterruptableTarget += Anivia.OnInterruptableTarget;
        }
    }
}