using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace RandomAIO.Plugins.Orianna.Addon.Orb
{
    public static class Jungleclear
    {
        private static AIHeroClient Orianna => Player.Instance;

        public static void Get()
        {
            if (Orianna.IsDead) return;

            var m =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(Orianna.Position)
                    .OrderByDescending(o => o.MaxHealth)
                    .FirstOrDefault(x => x.IsInRange(Orianna, SpellHandler.Q.Range));
            if (m != null)
            {
                if (MenuHandler.mjungle["q"].Cast<CheckBox>().CurrentValue)
                    if (SpellHandler.Q.IsReady())
                    {
                        var p = SpellHandler.Q.GetPrediction(m);
                        if (p.HitChance >= HitChance.Medium)
                            SpellHandler.Q.Cast(p.CastPosition);
                    }
                if (MenuHandler.mjungle["w"].Cast<CheckBox>().CurrentValue)
                    if (SpellHandler.W.IsReady())
                        if (BallHandler.WBall.IsInBall(m))
                            SpellHandler.W.Cast();
            }
        }
    }
}