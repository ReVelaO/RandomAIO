using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Spells;
using RandomAIO.Common;
using System.Linq;

namespace RandomAIO.Plugins.Riven.Addon.Orb
{
    public static class Update
    {
        private static AIHeroClient Riven => Player.Instance;

        public static void KillSteal()
        {
            if (MenuHandler.ks["aa"].Cast<CheckBox>().CurrentValue)
                if (Orbwalker.CanAutoAttack)
                {
                    var random =
                        EntityManager.Heroes.Enemies.FirstOrDefault(
                            x => x.IsInRange(Riven, Riven.GetAutoAttackRange()) &&
                                 (x.HPrediction((int)Riven.AttackCastDelay) < Riven.GetAutoAttackDamage(x)) &&
                                 x.IsValidTarget());
                    if (random != null)
                        Player.IssueOrder(GameObjectOrder.AttackUnit, random);
                }
            if (MenuHandler.ks["w"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.W.IsReady())
                {
                    var random =
                        EntityManager.Heroes.Enemies.FirstOrDefault(x => x.IsInRange(Riven, SpellHandler.W.Range) &&
                                                                         (x.HPrediction((int)Riven.AttackCastDelay) <
                                                                          Riven.GetAutoAttackDamage(x)) &&
                                                                         x.IsValidTarget());
                    if (random != null)
                        SpellHandler.W.Cast();
                }
            if (MenuHandler.ks["r2"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.IsR2Ready)
                {
                    var random =
                        EntityManager.Heroes.Enemies.FirstOrDefault(x => x.IsInRange(Riven, SpellHandler.R2.Range) &&
                                                                         (x.HPrediction(SpellHandler.R2.Time(x)) <
                                                                          x.GetRDamage()) && x.IsValidTarget());
                    if (random != null)
                        SpellHandler.R2.CastMinimumHitchance(random, HitChance.High);
                }
            if (MenuHandler.ks["i"].Cast<CheckBox>().CurrentValue)
                if (SummonerSpells.PlayerHas(SummonerSpellsEnum.Ignite)
                    && SpellHandler.Ignite.IsReady())
                {
                    var random =
                        EntityManager.Heroes.Enemies.FirstOrDefault(
                            x => x.IsInRange(Riven, SpellHandler.Ignite.Range) &&
                                 (x.HPrediction(SpellHandler.Ignite.CastDelay) <
                                  Riven.GetSummonerSpellDamage(x, DamageLibrary.SummonerSpells.Ignite)) &&
                                 x.IsValidTarget());
                    if (random != null)
                        SpellHandler.Ignite.Cast(random);
                }
        }
    }
}