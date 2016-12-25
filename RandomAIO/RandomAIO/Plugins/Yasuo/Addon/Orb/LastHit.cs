using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Common;
using System.Linq;

namespace RandomAIO.Plugins.Yasuo.Addon.Orb
{
    public static class LastHit
    {
        private static AIHeroClient Yasuo => Player.Instance;

        public static void Get()
        {
            if (SpellHandler.IsQ2Ready) return;

            if (!SpellHandler.IsQReady) return;
            var m =
                EntityManager.MinionsAndMonsters
                    .GetLaneMinions(
                        EntityManager.UnitTeam.Enemy, Yasuo.Position)
                    .Where(x => x.IsInRange(Yasuo, SpellHandler.Q.Range) &&
                                (x.HPrediction(SpellHandler.Q.CastDelay) < x.GetQDamage()));
            var p = SpellHandler.Q.GetBestLinearCastPosition(m);
            if (p.HitNumber >= MenuHandler.last["qmin"].Cast<Slider>().CurrentValue)
                SpellHandler.Q.Cast(p.CastPosition);
        }
    }
}