using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace RandomAIO.Plugins.Yasuo.Addon
{
    public static class SpellHandler
    {
        public static Spell.Skillshot Q, Q2;
        public static Spell.SimpleSkillshot W;
        public static Spell.Targeted E;
        public static Spell.Active Qaoe, R;

        public static bool IsQReady
            => (Q.Name.ToLower() == "yasuoqw") || ((Q.Name.ToLower() == "yasuoq2w") && Q.IsReady());

        public static bool IsQ2Ready => (Q.Name.ToLower() == "yasuoq3w") && Q.IsReady();

        public static bool IsDashed(this Obj_AI_Base obj)
        {
            return obj.HasBuff("yasuodashwrapper");
        }

        public static void Load()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 475, SkillShotType.Linear, 400, int.MaxValue, 40);
            Q2 = new Spell.Skillshot(SpellSlot.Q, 975, SkillShotType.Linear, 300, 1250, 90);
            Qaoe = new Spell.Active(SpellSlot.Q, 375);
            W = new Spell.SimpleSkillshot(SpellSlot.W, 400);
            E = new Spell.Targeted(SpellSlot.E, 475);
            R = new Spell.Active(SpellSlot.R, 1200);

            Q.AllowedCollisionCount = -1;
            Q2.AllowedCollisionCount = -1;
        }
    }
}