using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using RandomAIO.Plugins.Riven.Addon.Orb;
using System;
using System.Drawing;

namespace RandomAIO.Plugins.Riven.Addon
{
    public static class EventHandler
    {
        private static AIHeroClient Riven => Player.Instance;

        public static void Load()
        {
            Game.OnTick += OnTick;
            Game.OnTick += Update;
            Orbwalker.OnPostAttack += OnPostAttack;
            Drawing.OnDraw += OnDraw;
            //Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            //Spellbook.OnPostCastSpell += OnPostCastSpell;
        }

        private static void OnTick(EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
                Combo.Get();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                Laneclear.Get();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                Jungleclear.Get();
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee))
                Flee.Get();
        }

        private static void Update(EventArgs args)
        {
            Orb.Update.KillSteal();
        }

        private static void OnPostAttack(AttackableUnit unit, EventArgs args)
        {
            if (Orbwalker.ActiveModesFlags == Orbwalker.ActiveModes.Combo)
            {
                if (MenuHandler.combo["q"].Cast<CheckBox>().CurrentValue)
                {
                    var a = unit as AIHeroClient;
                    if ((a != null) && a.IsInRange(Riven, 450)
                        && SpellHandler.Q.IsReady() && a.IsValidTarget())
                        SpellHandler.Q.Cast(a.ServerPosition);
                }

                //Tiamat
                if (MenuHandler.combo["tiamat"].Cast<CheckBox>().CurrentValue)
                {
                    var a = unit as AIHeroClient;
                    if ((a != null) && SpellHandler.IsNotQReady
                        && a.IsInRange(Riven, 400)
                        && Item.HasItem(ItemId.Tiamat) && Item.CanUseItem(ItemId.Tiamat))
                        Item.UseItem(ItemId.Tiamat);
                }
                //Hydra
                if (MenuHandler.combo["hydra"].Cast<CheckBox>().CurrentValue)
                {
                    var a = unit as AIHeroClient;
                    if ((a != null) && SpellHandler.IsNotQReady
                        && a.IsInRange(Riven, 400)
                        && Item.HasItem(ItemId.Ravenous_Hydra) && Item.CanUseItem(ItemId.Ravenous_Hydra))
                        Item.UseItem(ItemId.Ravenous_Hydra);
                }
            }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear))
                if (MenuHandler.lane["q"].Cast<CheckBox>().CurrentValue)
                {
                    var a = unit as Obj_AI_Minion;
                    if ((a != null) && (a == Laneclear.Target) && a.IsInRange(Riven, 450)
                        && SpellHandler.Q.IsReady() && a.IsValidTarget())
                        SpellHandler.Q.Cast(a.ServerPosition);
                }
            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear))
                if (MenuHandler.jungle["q"].Cast<CheckBox>().CurrentValue)
                {
                    var a = unit as Obj_AI_Minion;
                    if ((a != null) && (a == Jungleclear.Target) && a.IsInRange(Riven, 450)
                        && SpellHandler.Q.IsReady() && a.IsValidTarget())
                        SpellHandler.Q.Cast(a.ServerPosition);
                }
        }

        private static void OnDraw(EventArgs args)
        {
            if (MenuHandler.draw["r2"].Cast<CheckBox>().CurrentValue)
                if (SpellHandler.IsR2Ready)
                    SpellHandler.R2.DrawRange(Color.FromArgb(130, Color.MediumSeaGreen));
        }

        /*private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                if (args.Slot == SpellSlot.W)
                {
                    Chat.Say("/d");
                }
            }
        }*/

        /*private static void OnPostCastSpell(Spellbook sender, SpellbookCastSpellEventArgs args)
        {
            if (sender.Owner.IsMe && args.Slot == SpellSlot.W)
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                Orbwalker.ResetAutoAttack();
            }
        }*/
    }
}