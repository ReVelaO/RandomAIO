using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Spells;

namespace RandomAIO.Plugins.Riven.Addon
{
    public static class SpellHandler
    {
        public static Spell.SimpleSkillshot Q, E;
        public static Spell.Active W, R;
        public static Spell.Skillshot R2;
        public static Spell.Targeted Ignite;

        public static bool IsRReady => R.Name.ToLower() == "rivenfengshuiengine" && R.IsReady();
        public static bool IsR2Ready => R2.Name.ToLower() == "rivenizunablade" && R2.IsReady();
        public static bool IsNotQReady => !Q.IsReady() && !Player.Instance.HasBuff("RivenTriCleave");

        public static void Load()
        {
            Q = new Spell.SimpleSkillshot(SpellSlot.Q, 275);
            W = new Spell.Active(SpellSlot.W, 250);
            E = new Spell.SimpleSkillshot(SpellSlot.E, 310);
            R = new Spell.Active(SpellSlot.R);
            R2 = new Spell.Skillshot(SpellSlot.R, 900, SkillShotType.Cone, 250, 1600, 125) { AllowedCollisionCount = -1 };
            Ignite = new Spell.Targeted(SummonerSpells.Ignite.Slot, 600);
        }
    }
}