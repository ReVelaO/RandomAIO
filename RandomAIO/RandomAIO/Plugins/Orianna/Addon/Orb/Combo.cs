using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Common;

namespace RandomAIO.Plugins.Orianna.Addon.Orb
{
    public static class Combo
    {
        public static void Get()
        {
            if (Player.Instance.IsDead) return;

            var t = TargetSelector.GetTarget(SpellHandler.Q.Range + 100, DamageType.Magical);
            if ((t == null) || !t.IsValidTarget()) return;

            if (MenuHandler.mcombo["q"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.Q.IsReady())
                {
                    var p = SpellHandler.Q.GetPrediction(t);
                    if (p.HitChance >= HitChance.High)
                        SpellHandler.Q.Cast(t);
                }
            if (MenuHandler.mcombo["w"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.W.IsReady())
                    if (BallHandler.WBall.CountEnemyHeroesNear > 0)
                        SpellHandler.W.Cast();

            if (MenuHandler.mcombo["r"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.R.IsReady())
                {
                    if (MenuHandler.mcombo["re"].Cast<CheckBox>().CurrentValue)
                    {
                        var hp = t.HPrediction(BallHandler.RBall.CastDelay);

                        if (BallHandler.RBall.IsInBall(t)
                            && (DamageHandler.R(t) + Player.Instance.GetAutoAttackDamage(t, true) > hp))
                            SpellHandler.R.Cast();
                    }
                    if (BallHandler.RBall.CountEnemyHeroesNear >=
                        MenuHandler.mcombo["minr"].Cast<Slider>().CurrentValue)
                        SpellHandler.R.Cast();
                }
        }
    }
}