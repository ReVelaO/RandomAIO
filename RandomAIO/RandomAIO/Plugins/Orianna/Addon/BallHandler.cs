using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using System;
using System.Linq;

namespace RandomAIO.Plugins.Orianna.Addon
{
    public static class BallHandler
    {
        public static Vector3 Ball;
        public static bool IsInFloor;

        private static AIHeroClient Orianna => Player.Instance;

        public static bool HasBall(this Obj_AI_Base obj) => obj.HasBuff("orianaghostself");

        public static void Load()
        {
            Game.OnUpdate += Game_OnUpdate;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            //GameObject.OnCreate += OnCreate;
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Player.Instance.HasBall())
            {
                Ball = Orianna.Position;
                IsInFloor = false;
            }

            var ally = EntityManager.Heroes.Allies.FirstOrDefault(x => x.HasBuff("OrianaGhost"));
            if (ally != null)
            {
                Ball = ally.Position;
                IsInFloor = false;
            }
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsMe) return;

            if (args.Slot == SpellSlot.Q)
            {
                var time = args.End.Distance(Ball) * 1000 / SpellHandler.Q.Speed;
                Core.DelayAction(() =>
                {
                    Ball = args.End;
                    IsInFloor = true;
                }, (int)time);
            }
        }

        /*private static void OnCreate(GameObject obj, EventArgs args)
        {
            var particle = obj as Obj_GeneralParticleEmitter;
            if ((particle != null) && (particle.Name == "Orianna_Base_Q_yomu_ring_green.troy"))
            {
                Ball = particle.Position;
                IsInFloor = true;
            }
        }*/

        public static class WBall
        {
            public const int CastDelay = 250;
            public const int Width = 250;

            public static int CountEnemyMinionsNear
                => Ball != Vector3.Zero ? Ball.CountEnemyMinionsInRangeWithPrediction(250) : 0;

            public static int CountEnemyHeroesNear
                => Ball != Vector3.Zero ? Ball.CountEnemyHeroesInRangeWithPrediction(250) : 0;

            public static bool IsInBall(Obj_AI_Base e) => e.IsInRange(Ball, Width);
        }

        public static class RBall
        {
            public const int CastDelay = 750;
            public const int Width = 410;

            public static int CountEnemyMinionsNear
                => Ball != Vector3.Zero ? Ball.CountEnemyMinionsInRangeWithPrediction(410, 750) : 0;

            public static int CountEnemyHeroesNear
                => Ball != Vector3.Zero ? Ball.CountEnemyHeroesInRangeWithPrediction(410, 750) : 0;

            public static bool IsInBall(Obj_AI_Base e) => e.IsInRange(Ball, Width);
        }
    }
}