using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace RandomAIO.Plugins.Yasuo.Addon.Orb
{
    public static class Flee
    {
        private static readonly AIHeroClient Yasuo = Player.Instance;

        public static void Get()
        {
            if (MenuHandler.flee["e"].Cast<CheckBox>().CurrentValue)
            {
                var p1 = Game.CursorPos;
                var m =
                    EntityManager.MinionsAndMonsters.CombinedAttackable.OrderBy(o => p1.Distance(o))
                        .ThenByDescending(d => d.Distance(Yasuo))
                        .FirstOrDefault(x => x.IsInRange(Yasuo, SpellHandler.E.Range) && !x.IsDashed());
                if (m != null)
                {
                    if (SpellHandler.E.IsReady()
                        && Yasuo.IsFacing(m) && Yasuo.Distance(m.ServerPosition) >= 30) SpellHandler.E.Cast(m);
                }
                else
                {
                    var h = EntityManager.Heroes.Enemies.OrderBy(o => p1.Distance(o))
                        .ThenByDescending(d => d.Distance(Yasuo))
                        .FirstOrDefault(x => x.IsInRange(Yasuo, SpellHandler.E.Range) && !x.IsDashed() && x.IsValidTarget());
                    if (h != null)
                        if (SpellHandler.E.IsReady()
                            && Yasuo.IsFacing(h) && Yasuo.Distance(h.ServerPosition) >= 30) SpellHandler.E.Cast(h);
                }
            }
        }
    }
}