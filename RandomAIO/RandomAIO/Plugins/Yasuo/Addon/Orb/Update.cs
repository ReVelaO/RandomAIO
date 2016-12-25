using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Common;
using System.Linq;

namespace RandomAIO.Plugins.Yasuo.Addon.Orb
{
    public static class Update
    {
        private static AIHeroClient Yasuo => Player.Instance;

        public static void KillSteal()
        {
            if (Orbwalker.CanAutoAttack)
            {
                var random =
                    EntityManager.Heroes.Enemies.FirstOrDefault(
                        x =>
                            !x.IsInvulnerable && !x.IsDead && x.IsInRange(Yasuo, Yasuo.GetAutoAttackRange()) &&
                            (x.HPrediction((int)Yasuo.AttackCastDelay) < Yasuo.GetAutoAttackDamage(x, true)));
                if (random != null)
                    Player.IssueOrder(GameObjectOrder.AttackUnit, random);
            }
            if (SpellHandler.IsQReady)
            {
                var random =
                    EntityManager.Heroes.Enemies.FirstOrDefault(
                        x =>
                            !x.IsInvulnerable && !x.IsDead && x.IsInRange(Yasuo, SpellHandler.Q.Range) &&
                            (x.HPrediction(SpellHandler.Q.CastDelay) < x.GetQDamage()));
                if (random != null)
                    SpellHandler.Q.CastMinimumHitchance(random, HitChance.Medium);
            }
            if (SpellHandler.IsQ2Ready)
            {
                var random =
                    EntityManager.Heroes.Enemies.FirstOrDefault(
                        x =>
                            !x.IsInvulnerable && !x.IsDead && x.IsInRange(Yasuo, SpellHandler.Q2.Range) &&
                            (x.HPrediction(SpellHandler.Q2.Time(x)) < x.GetQDamage()));
                if (random != null)
                    SpellHandler.Q2.CastMinimumHitchance(random, HitChance.High);
            }
        }

        public static void Misc()
        {
            if (MenuHandler.misc["r"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.R.IsReady())
                {
                    var n = MenuHandler.misc["rmin"].Cast<Slider>().CurrentValue;
                    var random =
                        EntityManager.Heroes.Enemies.Count(
                            x =>
                                x.IsInRange(Yasuo, SpellHandler.R.Range) &&
                                (x.HasBuffOfType(BuffType.Knockup) | x.HasBuffOfType(BuffType.Knockback)));
                    if (random >= n)
                        SpellHandler.R.Cast();
                }
            if (MenuHandler.misc["qst"].Cast<CheckBox>().CurrentValue)
            {
                if (SpellHandler.IsQ2Ready) return;

                if (SpellHandler.Q.IsReady() && (SpellHandler.Q.Name.ToLower() == "yasuoq2w"))
                {
                    var first =
                        EntityManager.Heroes.Enemies.FirstOrDefault(
                            x => !x.IsInvulnerable && !x.IsDead && x.IsInRange(Yasuo, SpellHandler.Q.Range));
                    if ((first != null) && !first.IsUnderTurret())
                    {
                        SpellHandler.Q.Cast(first);
                    }
                    else
                    {
                        var second =
                            EntityManager.MinionsAndMonsters.CombinedAttackable.FirstOrDefault(
                                x => !x.IsInvulnerable && !x.IsDead && x.IsInRange(Yasuo, SpellHandler.Q.Range));
                        if (second != null)
                            SpellHandler.Q.Cast(second);
                    }
                }
            }
        }
    }
}