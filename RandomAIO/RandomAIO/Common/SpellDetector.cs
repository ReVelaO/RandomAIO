using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using Timer = System.Timers.Timer;

namespace RandomAIO.Common
{
    //Full credits to apollyon ! x)
    public static class SpellDetector
    {
        private static readonly Dictionary<MissileClient, Geometry.Polygon> s = new Dictionary<MissileClient, Geometry.Polygon>();

        private static List<Geometry.Polygon> _joined = new List<Geometry.Polygon>();

        private static readonly Timer timer = new Timer(500);

        public static void Load()
        {
            GameObject.OnCreate += OnCreate;
            GameObject.OnDelete += OnDelete;

            timer.Elapsed += delegate
            {
                UpdatePolygons();
            };
        }

        private static void OnCreate(GameObject obj, EventArgs args)
        {
            var m = obj as MissileClient;
            if (m == null) return;

            var gd = SpellDatabase
                .GetSpellInfoList(m.SpellCaster)
                .FirstOrDefault(s => s.RealSlot == m.Slot);

            if (gd == null) return;

            switch (gd.Type)
            {
                case SpellType.Circle:
                    var c = new Geometry.Polygon.Circle(m.Position, gd.Radius);
                    s.Add(m, c);
                    break;

                case SpellType.Line:
                    var l = new Geometry.Polygon.Rectangle(m.StartPosition,
                        m.StartPosition.Extend(m.EndPosition, gd.Range).To3D(), 5);
                    s.Add(m, l);
                    break;

                case SpellType.Cone:
                    var ce = new Geometry.Polygon.Sector(m.StartPosition, m.EndPosition, gd.Radius, gd.Range, 80);
                    s.Add(m, ce);
                    break;

                case SpellType.MissileLine:
                    var ml = new Geometry.Polygon.Rectangle(m.StartPosition,
                        m.StartPosition.Extend(m.EndPosition, gd.Range).To3D(), 5);
                    s.Add(m, ml);
                    break;

                case SpellType.MissileAoe:
                    var ma = new Geometry.Polygon.Rectangle(m.StartPosition,
                        m.StartPosition.Extend(m.EndPosition, gd.Range).To3D(), 5);
                    s.Add(m, ma);
                    break;
            }

            var p = new List<Geometry.Polygon>();
            p.AddRange(s.Values);

            _joined = p.JoinPolygons();
        }

        private static void UpdatePolygons()
        {
            foreach (var m in s)
            {
                var gd =
                    SpellDatabase.GetSpellInfoList(m.Key.SpellCaster)
                        .FirstOrDefault(s => s.RealSlot == m.Key.Slot);
                if (gd == null) return;

                switch (gd.Type)
                {
                    case SpellType.Circle:
                        var c = new Geometry.Polygon.Circle(m.Key.Position, gd.Radius);
                        s[m.Key] = c;
                        break;

                    case SpellType.Line:
                        var l = new Geometry.Polygon.Rectangle(m.Key.StartPosition,
                            m.Key.StartPosition.Extend(m.Key.EndPosition, gd.Range).To3D(), 5);
                        s[m.Key] = l;
                        break;

                    case SpellType.Cone:
                        var ce = new Geometry.Polygon.Sector(m.Key.StartPosition, m.Key.EndPosition, gd.Radius, gd.Range, 80);
                        s[m.Key] = ce;
                        break;

                    case SpellType.MissileLine:
                        var ml = new Geometry.Polygon.Rectangle(m.Key.StartPosition,
                            m.Key.StartPosition.Extend(m.Key.EndPosition, gd.Range).To3D(), 5);
                        s[m.Key] = ml;
                        break;

                    case SpellType.MissileAoe:
                        var me = new Geometry.Polygon.Rectangle(m.Key.StartPosition,
                            m.Key.StartPosition.Extend(m.Key.EndPosition, gd.Range).To3D(), 5);
                        s[m.Key] = me;
                        break;
                }
            }

            var p = new List<Geometry.Polygon>();
            p.AddRange(s.Values);

            _joined = p.JoinPolygons();
        }

        private static void OnDelete(GameObject obj, EventArgs args)
        {
            var msl = obj as MissileClient;
            if (msl == null) return;

            var sm = s.FirstOrDefault(m => m.Key == msl);

            if (sm.Key == null) return;

            s.Remove(sm.Key);
        }

        public static bool IsInDanger(this AIHeroClient h, int percentHp = 30, int closestsrange = 300)
        {
            return h.HealthPercent < percentHp && s.Values.Any(p => p.IsInside(h)) &&
                   s.Keys.Any(m => m.IsInRange(h, closestsrange));
        }

        public static bool IsSkillCollisionable(this AIHeroClient h, int closestsrange = 300)
        {
            return s.Values.Any(a => a.IsInside(h)) && s.Keys.Any(a => a.IsInRange(h, closestsrange));
        }
    }
}