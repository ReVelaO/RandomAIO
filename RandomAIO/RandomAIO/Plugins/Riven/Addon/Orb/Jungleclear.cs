using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace RandomAIO.Plugins.Riven.Addon.Orb
{
    public static class Jungleclear
    {
        private static AIHeroClient Riven => Player.Instance;

        public static Obj_AI_Base Target;

        public static void Get()
        {
            var m =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(Riven.Position)
                    .FirstOrDefault(x => x.IsInRange(Riven, 450));

            if (MenuHandler.jungle["q"].Cast<CheckBox>().CurrentValue)
            {
                if (SpellHandler.Q.IsReady() && m.IsInRange(Riven, 450))
                {
                    Target = m;
                }
            }
            if (MenuHandler.jungle["w"].Cast<CheckBox>().CurrentValue)
            {
                if (m.IsInRange(Riven, SpellHandler.W.Range) && SpellHandler.W.IsReady())
                {
                    SpellHandler.W.Cast();
                }
            }
        }
    }
}