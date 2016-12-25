using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Common;
using System.Linq;

namespace RandomAIO.Plugins.Yasuo.Addon.Orb
{
    public static class Laneclear
    {
        private static AIHeroClient Yasuo => Player.Instance;

        public static void Get()
        {
            if (MenuHandler.lane["q2"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.IsQ2Ready)
                {
                    var m = API.GetEnemyMinions(SpellHandler.Q2.Range);
                    if (m != null)
                    {
                        var p = SpellHandler.Q2.GetBestLinearCastPosition(m);
                        if (p.HitNumber >= MenuHandler.lane["q2sli"].Cast<Slider>().CurrentValue)
                            if (!Yasuo.IsDashing())
                                SpellHandler.Q2.Cast(p.CastPosition);
                    }
                }
            if (MenuHandler.lane["qaoe"].Cast<CheckBox>().CurrentValue)
            {
                if (SpellHandler.IsQ2Ready) return;

                if (SpellHandler.E.IsReady() && SpellHandler.Qaoe.IsReady())
                {
                    var m =
                        EntityManager.MinionsAndMonsters
                            .GetLaneMinions(EntityManager.UnitTeam.Enemy, Yasuo.Position)
                            .FirstOrDefault(x => x.IsInRange(Yasuo, SpellHandler.E.Range) && !x.IsDashed());
                    if (m != null)
                        if (m.CountEnemyMinionsInRange(SpellHandler.Qaoe.Range) >=
                            MenuHandler.lane["qaoesli"].Cast<Slider>().CurrentValue)
                            SpellHandler.E.Cast(m);
                }
            }
            if (MenuHandler.lane["q"].Cast<CheckBox>().CurrentValue)
            {
                if (SpellHandler.IsQ2Ready) return;

                if (SpellHandler.IsQReady)
                {
                    var m =
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Yasuo.Position)
                            .Where(x => x.IsInRange(Yasuo, SpellHandler.Q.Range));
                    {
                        var p = SpellHandler.Q.GetBestLinearCastPosition(m);
                        if (p.HitNumber >= MenuHandler.lane["qsli"].Cast<Slider>().CurrentValue)
                        {
                            if (!Yasuo.IsDashing())
                                SpellHandler.Q.Cast(p.CastPosition);
                        }
                        else
                        {
                            var m1 =
                                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                                    Yasuo.Position).FirstOrDefault(x => x.IsInRange(Yasuo, SpellHandler.Q.Range));
                            if (m1 != null)
                                if (!Yasuo.IsDashing())
                                    SpellHandler.Q.Cast(m1.Position);
                        }
                    }
                }
            }
            if (MenuHandler.lane["e"].Cast<CheckBox>().CurrentValue)
            {
                if (!SpellHandler.E.IsReady()) return;

                var m =
                    EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Yasuo.Position)
                        .FirstOrDefault(
                            x =>
                                x.IsInRange(Yasuo, SpellHandler.E.Range) && !x.IsDashed() &&
                                (x.HPrediction(150) < x.GetEDamage()));
                if ((m != null) && !SpellHandler.Q.IsReady())
                {
                    if (MenuHandler.lane["et"].Cast<CheckBox>().CurrentValue)
                    {
                        if (m.IsUnderTurret())
                            return;
                        SpellHandler.E.Cast(m);
                    }
                    else
                    {
                        SpellHandler.E.Cast(m);
                    }
                }
                else
                {
                    var m0 =
                        EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Yasuo.Position)
                            .FirstOrDefault(x => x.IsInRange(Yasuo, SpellHandler.E.Range) && !x.IsDashed());
                    if ((m0 != null) && !SpellHandler.Q.IsReady())
                        if (MenuHandler.lane["et"].Cast<CheckBox>().CurrentValue)
                        {
                            if (m0.IsUnderTurret())
                                return;
                            SpellHandler.E.Cast(m0);
                        }
                        else
                        {
                            SpellHandler.E.Cast(m0);
                        }
                }
            }
        }
    }
}