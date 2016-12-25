using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Common;
using System.Linq;

namespace RandomAIO.Plugins.Yasuo.Addon.Orb
{
    public static class Combo
    {
        private static AIHeroClient Yasuo => Player.Instance;

        public static void Get()
        {
            if (MenuHandler.combo["q"].Cast<CheckBox>().CurrentValue)
            {
                if (SpellHandler.Q.IsReady())
                {
                    AIHeroClient qTarget = TargetSelector.GetTarget(SpellHandler.Q.Range, DamageType.Physical);
                    if (qTarget != null && !qTarget.IsInvulnerable)
                    {
                        SpellHandler.Q.CastMinimumHitchance(qTarget, HitChance.High);
                    }
                }
            }
            if (MenuHandler.combo["q2"].Cast<CheckBox>().CurrentValue)
            {
                if (SpellHandler.IsQ2Ready)
                {
                    var qTarget = TargetSelector.GetTarget(SpellHandler.Q2.Range, DamageType.Physical);

                    if (qTarget != null && !qTarget.IsInvulnerable)
                    {
                        if (MenuHandler.combo["q2block"].Cast<CheckBox>().CurrentValue)
                        {
                            if (qTarget.IsInMinimumRange(Yasuo.GetAutoAttackRange(), SpellHandler.Q2.Range))
                            {
                                if (!Yasuo.IsDashing())
                                {
                                    SpellHandler.Q2.CastMinimumHitchance(qTarget, HitChance.High);
                                }
                            }
                        }
                        else
                        {
                            if (!Yasuo.IsDashing())
                            {
                                SpellHandler.Q2.CastMinimumHitchance(qTarget, HitChance.High);
                            }
                        }
                        if (MenuHandler.combo["r"].Cast<CheckBox>().CurrentValue)
                        {
                            if (SpellHandler.R.IsReady())
                            {
                                if (MenuHandler.combo["rexe"].Cast<CheckBox>().CurrentValue)
                                {
                                    if (qTarget.HasBuffOfType(BuffType.Knockup) || qTarget.HasBuffOfType(BuffType.Knockback)
                                        && qTarget.HPrediction(1100) < qTarget.GetRDamage())
                                    {
                                        SpellHandler.R.Cast();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (MenuHandler.combo["qe"].Cast<CheckBox>().CurrentValue)
            {
                if (SpellHandler.Qaoe.IsReady() && SpellHandler.E.IsReady())
                {
                    AIHeroClient target = TargetSelector.GetTarget(SpellHandler.E.Range, DamageType.Mixed);
                    if (target != null && !target.IsInvulnerable)
                    {
                        if (MenuHandler.combo["eblock1"].Cast<CheckBox>().CurrentValue)
                        {
                            if (target.IsUnderTurret())
                            {
                                return;
                            }
                            if (!target.IsDashed() && target.CountEnemiesInRange(SpellHandler.Qaoe.Range) >= MenuHandler.combo["qaoesli"].Cast<Slider>().CurrentValue)
                            {
                                SpellHandler.E.Cast(target);
                                if (Yasuo.IsDashing())
                                {
                                    switch (SpellHandler.IsQ2Ready)
                                    {
                                        case true:
                                            SpellHandler.Qaoe.Cast();
                                            break;

                                        case false:
                                            SpellHandler.Qaoe.Cast();
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!target.IsDashed() && target.CountEnemiesInRange(SpellHandler.Qaoe.Range) >= MenuHandler.combo["qaoesli"].Cast<Slider>().CurrentValue)
                            {
                                SpellHandler.E.Cast(target);
                                if (Yasuo.IsDashing())
                                {
                                    if (SpellHandler.IsQ2Ready)
                                    {
                                        SpellHandler.Qaoe.Cast();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (MenuHandler.combo["e"].Cast<CheckBox>().CurrentValue)
            {
                if (SpellHandler.E.IsReady() && !SpellHandler.IsQReady && !SpellHandler.IsQ2Ready)
                {
                    AIHeroClient eTarget = TargetSelector.GetTarget(SpellHandler.E.Range, DamageType.Magical);
                    if (eTarget != null && !eTarget.IsInvulnerable
                        && !eTarget.IsDashed() && !eTarget.IsInRange(Yasuo, Yasuo.GetAutoAttackRange()))
                    {
                        if (MenuHandler.combo["eblock2"].Cast<CheckBox>().CurrentValue)
                        {
                            if (eTarget.IsUnderTurret())
                            {
                                return;
                            }
                            SpellHandler.E.Cast(eTarget);
                        }
                        else
                        {
                            SpellHandler.E.Cast(eTarget);
                        }
                    }
                }
            }
            if (MenuHandler.combo["r"].Cast<CheckBox>().CurrentValue)
            {
                if (SpellHandler.R.IsReady())
                {
                    var e = EntityManager.Heroes.Enemies.Count(x => !x.IsInvulnerable && !x.IsDead && x.HasBuffOfType(BuffType.Knockup) | x.HasBuffOfType(BuffType.Knockback) && x.IsInRange(Yasuo, SpellHandler.R.Range));
                    if (e >= MenuHandler.combo["rsli"].Cast<Slider>().CurrentValue)
                    {
                        SpellHandler.R.Cast();
                    }
                    if (MenuHandler.combo["rexe"].Cast<CheckBox>().CurrentValue)
                    {
                        var targeted = TargetSelector.GetTarget(SpellHandler.R.Range, DamageType.Physical);
                        if (targeted != null && !targeted.IsInvulnerable && targeted.HasBuffOfType(BuffType.Knockup) | targeted.HasBuffOfType(BuffType.Knockback)
                            && targeted.HPrediction(1100) < targeted.GetRDamage())
                        {
                            if (targeted.IsInMinimumRange(Yasuo.GetAutoAttackRange(), SpellHandler.R.Range))
                            {
                                SpellHandler.R.Cast();
                            }
                        }
                    }
                }
            }
        }
    }
}