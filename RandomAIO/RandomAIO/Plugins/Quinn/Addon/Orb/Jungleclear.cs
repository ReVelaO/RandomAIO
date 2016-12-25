using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace RandomAIO.Plugins.Quinn.Addon.Orb
{
    public static class Jungleclear
    {
        private static AIHeroClient Quinn => Player.Instance;

        public static void Get()
        {
            if ((Quinn.ManaPercent < 59) || Quinn.IsDead) return;

            if (MenuHandler.jungle["q"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.Q.IsReady())
                {
                    var m =
                        EntityManager.MinionsAndMonsters.GetJungleMonsters(Quinn.Position)
                            .FirstOrDefault(x => x.IsInRange(Quinn, SpellHandler.Q.Range));

                    if (m != null)
                        SpellHandler.Q.Cast(m.Position);
                }

            if (MenuHandler.jungle["e"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.E.IsReady() && !Orbwalker.CanAutoAttack)
                {
                    var m =
                        EntityManager.MinionsAndMonsters.GetJungleMonsters(Quinn.Position)
                            .FirstOrDefault(x => x.IsInRange(Quinn, SpellHandler.E.Range));

                    if (m != null)
                        SpellHandler.E.Cast(m);
                }
        }
    }
}