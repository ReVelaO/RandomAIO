using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace RandomAIO.Plugins.Orianna.Addon
{
    public static class SpellHandler
    {
        public static Spell.Skillshot Q;
        public static Spell.Active W, R;
        public static Spell.Targeted E;

        public static void Load()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 810, SkillShotType.Circular, 100, 1200, 80);
            W = new Spell.Active(SpellSlot.W);
            E = new Spell.Targeted(SpellSlot.E, 1100);
            R = new Spell.Active(SpellSlot.R);
        }
    }
}