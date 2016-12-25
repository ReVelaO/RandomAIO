using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Common;
using System.Linq;

namespace RandomAIO.Plugins.Quinn.Addon.Orb
{
    public static class Laneclear
    {
        private static AIHeroClient Quinn => Player.Instance;

        public static void Get()
        {
            if ((Quinn.ManaPercent < 59) || Quinn.IsDead) return;

            if (MenuHandler.lane["q"].Cast<CheckBox>().CurrentValue)
            {
                if (!SpellHandler.Q.IsReady()) return;
                var m =
                    EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Quinn.Position)
                        .Where(x => x.IsInRange(Quinn, SpellHandler.Q.Range));
                {
                    var p = SpellHandler.Q.GetBestLinearCastPosition(m);

                    if (p.HitNumber <= 2)
                        SpellHandler.Q.Cast(p.CastPosition);
                }
            }
            if (MenuHandler.lane["e"].Cast<CheckBox>().CurrentValue)
            {
                if (!SpellHandler.E.IsReady()) return;
                var m =
                    EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Quinn.Position)
                        .Where(x => x.HPrediction(SpellHandler.E.CastDelay) < x.GetEDamage())
                        .FirstOrDefault(x => x.IsInRange(Quinn, SpellHandler.E.Range));

                if (m != null && !Orbwalker.CanAutoAttack)
                    SpellHandler.E.Cast(m);
            }
        }
    }
}