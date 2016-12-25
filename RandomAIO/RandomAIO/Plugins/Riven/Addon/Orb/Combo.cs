using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Common;
using System.Linq;

namespace RandomAIO.Plugins.Riven.Addon.Orb
{
    public static class Combo
    {
        private static AIHeroClient Riven => Player.Instance;

        public static void Get()
        {
            var target = TargetSelector.GetTarget(600, DamageType.Physical);

            if ((target == null) || target.IsInvulnerable) return;

            if (MenuHandler.combo["e"].Cast<CheckBox>().CurrentValue)
            {
                if (target.IsInMinimumRange(Riven.GetAutoAttackRange(), SpellHandler.E.Range)
                    && SpellHandler.E.IsReady())
                {
                    SpellHandler.E.Cast(target.ServerPosition);
                    if (target.IsInRange(Riven, SpellHandler.W.Range) && !SpellHandler.IsRReady)
                        SpellHandler.W.Cast();
                }
            }

            if (MenuHandler.combo["q"].Cast<CheckBox>().CurrentValue)
            {
                if (target.IsInRange(Riven, 450) && SpellHandler.Q.IsReady()
                    && !Riven.HasBuff("RivenTriCleave"))
                {
                    SpellHandler.Q.Cast(target.ServerPosition);
                }
            }

            if (MenuHandler.combo["w"].Cast<CheckBox>().CurrentValue)
            {
                if (target.IsInRange(Riven, SpellHandler.W.Range)
                    && !SpellHandler.IsRReady && SpellHandler.W.IsReady()
                    && SpellHandler.IsNotQReady)
                    SpellHandler.W.Cast();
            }

            if (MenuHandler.combo["r"].Cast<CheckBox>().CurrentValue)
            {
                if (target.HPrediction(SpellHandler.R2.Time(target)) < target.GetRDamage()
                    && SpellHandler.IsRReady)
                {
                    SpellHandler.R.Cast();
                    if (SpellHandler.IsR2Ready)
                    {
                        var p = SpellHandler.R2.GetPrediction(target);
                        if (p.HitChance >= HitChance.High)
                            SpellHandler.R2.Cast(p.CastPosition);
                    }
                }
                else
                {
                    var enemies =
                        EntityManager.Heroes.Enemies.Count(x => x.IsInRange(Riven, SpellHandler.R2.Range) && !x.IsDead);
                    if (enemies >= 2)
                    {
                        if (SpellHandler.IsRReady) SpellHandler.R.Cast();
                        if (SpellHandler.IsR2Ready)
                        {
                            var e2 =
                                EntityManager.Heroes.Enemies.Where(
                                    x => x.IsInRange(Riven, SpellHandler.R2.Range) && !x.IsDead);
                            var p = SpellHandler.R2.GetBestConeCastPosition(e2);

                            if (p.HitNumber >= 2) SpellHandler.R2.Cast(p.CastPosition);
                        }
                    }
                }
            }
        }
    }
}