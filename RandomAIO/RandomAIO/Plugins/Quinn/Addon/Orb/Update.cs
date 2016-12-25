using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Common;
using System.Linq;

namespace RandomAIO.Plugins.Quinn.Addon.Orb
{
    public static class Update
    {
        private static AIHeroClient Quinn => Player.Instance;

        public static void KillSteal()
        {
            if (MenuHandler.ks["aa"].Cast<CheckBox>().CurrentValue)
                if (Orbwalker.CanAutoAttack)
                {
                    var enemy =
                        EntityManager.Heroes.Enemies.FirstOrDefault(
                            x =>
                                x.IsInRange(Quinn, Quinn.GetAutoAttackRange()) &&
                                (x.HPrediction((int)Quinn.AttackCastDelay) < Quinn.GetAutoAttackDamage(x, true)));
                    if (enemy != null)
                        Player.IssueOrder(GameObjectOrder.AttackUnit, enemy);
                }

            if (MenuHandler.ks["q"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.Q.IsReady())
                {
                    var enemy =
                        EntityManager.Heroes.Enemies.FirstOrDefault(
                            x =>
                                x.IsInRange(Quinn, SpellHandler.Q.Range) &&
                                (x.HPrediction(SpellHandler.Q.Time(x)) < x.GetQDamage()));
                    if (enemy != null)
                    {
                        var p = SpellHandler.Q.GetPrediction(enemy);

                        if ((p != null) && (p.HitChance > HitChance.Medium))
                            SpellHandler.Q.Cast(enemy);
                    }
                }

            if (MenuHandler.ks["e"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.E.IsReady())
                {
                    var enemy =
                        EntityManager.Heroes.Enemies.FirstOrDefault(
                            x => x.IsInRange(Quinn, SpellHandler.E.Range) && (x.HPrediction(250) < x.GetEDamage()));
                    if ((enemy != null) && !Orbwalker.CanAutoAttack)
                        SpellHandler.E.Cast(enemy);
                }
        }
    }
}