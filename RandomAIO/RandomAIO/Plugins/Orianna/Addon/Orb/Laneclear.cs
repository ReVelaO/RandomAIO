using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace RandomAIO.Plugins.Orianna.Addon.Orb
{
    public static class Laneclear
    {
        private static AIHeroClient Orianna => Player.Instance;

        public static void Get()
        {
            if (Orianna.ManaPercent < MenuHandler.mlane["mmsli"].Cast<Slider>().CurrentValue || Orianna.IsDead) return;

            var m =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Orianna.ServerPosition)
                    .OrderBy(o => o.Health)
                    .Where(c => c.IsInRange(Orianna, SpellHandler.Q.Range));

            if (MenuHandler.mlane["q"].Cast<CheckBox>().CurrentValue)
            {
                var minhit = MenuHandler.mlane["minq"].Cast<Slider>().CurrentValue;

                if (SpellHandler.Q.IsReady())
                {
                    var p = SpellHandler.Q.GetBestCircularCastPosition(m);

                    if (p.HitNumber >= minhit)
                        SpellHandler.Q.Cast(p.CastPosition);
                }
            }
            if (MenuHandler.mlane["w"].Cast<CheckBox>().CurrentValue)
            {
                var minhit = MenuHandler.mlane["minw"].Cast<Slider>().CurrentValue;

                if (SpellHandler.W.IsReady())
                    if (BallHandler.WBall.CountEnemyMinionsNear >= minhit)
                        SpellHandler.W.Cast();
            }
        }
    }
}