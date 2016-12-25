using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace RandomAIO.Common
{
    public static class API
    {
        public static void Welcome(string addonname, string notifimessage = null)
        {
            Chat.Print("<font color='#FFFFFF'>[" + addonname + " loaded. Enjoy!</font>");
            Notifications.Show(new SimpleNotification(addonname, notifimessage ?? "Welcome Back Buddy!"), 20000);
        }

        public static AIHeroClient Target(this Spell.SpellBase spell)
        {
            return TargetSelector.GetTarget(spell.Range, spell.DamageType);
        }

        public static IEnumerable<Obj_AI_Minion> GetEnemyMinions(float range)
        {
            return
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Player.Instance.Position)
                    .Where(x => x.IsInRange(Player.Instance, range));
        }

        public static Obj_AI_Base GetJungleMinions(float range)
        {
            return
                EntityManager.MinionsAndMonsters.GetJungleMonsters(Player.Instance.Position)
                    .FirstOrDefault(x => x.IsInRange(Player.Instance, range));
        }

        public static bool IsInMinimumRange(this Obj_AI_Base obj, float min, float max)
        {
            return !obj.IsInRange(Player.Instance, min) && obj.IsInRange(Player.Instance, max);
        }

        public static float HPrediction(this Obj_AI_Base obj, int time)
        {
            return Prediction.Health.GetPrediction(obj, time);
        }

        public static int Time(this Spell.Skillshot spell, Obj_AI_Base endPos)
        {
            return (int)(Player.Instance.Distance(endPos.ServerPosition) / spell.Speed * 1000) + spell.CastDelay;
        }
    }
}