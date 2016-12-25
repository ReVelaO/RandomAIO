using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace RandomAIO.Plugins.Yasuo.Addon.Orb
{
    public static class Jungleclear
    {
        private static AIHeroClient Yasuo => Player.Instance;

        public static void Get()
        {
            var m =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(Yasuo.Position)
                    .FirstOrDefault(x => x.IsInRange(Yasuo, SpellHandler.Q.Range));
            if (m == null) return;
            if (MenuHandler.jungle["q"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.IsQReady) SpellHandler.Q.CastMinimumHitchance(m, HitChance.Medium);
            if (MenuHandler.jungle["q2"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.IsQ2Ready) SpellHandler.Q2.CastMinimumHitchance(m, HitChance.High);
            if (MenuHandler.jungle["e"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.E.IsReady() && !m.IsDashed()) SpellHandler.E.Cast(m);
        }
    }
}