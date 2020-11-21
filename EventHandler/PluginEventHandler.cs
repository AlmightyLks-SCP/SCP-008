using MEC;
using Synapse.Api;
using Synapse.Api.Events.SynapseEventArguments;
using System.Collections.Generic;
using UnityEngine;
using SynEvents = Synapse.Api.Events.EventHandler;

namespace SCP_008
{
    internal class PluginEventHandler
    {
        private HashSet<Player> infectedPlayers;
        private List<CoroutineHandle> runningCoroutines;
        public PluginEventHandler(HashSet<Player> InfectedPlayers)
        {
            SynEvents.Get.Player.PlayerDamageEvent += Player_PlayerDamageEvent;
            SynEvents.Get.Player.PlayerDeathEvent += Player_PlayerDeathEvent;
            SynEvents.Get.Round.RoundEndEvent += Round_RoundEndEvent;
            SynEvents.Get.Round.RoundRestartEvent += Round_RoundRestartEvent;
            SynEvents.Get.Player.PlayerLeaveEvent += Player_PlayerLeaveEvent;
            SynEvents.Get.Player.PlayerItemUseEvent += Player_PlayerItemUseEvent;

            infectedPlayers = InfectedPlayers;
            runningCoroutines = new List<CoroutineHandle>();
            runningCoroutines.Add(Timing.RunCoroutine(DamageOverTime()));
        }

        private void Player_PlayerItemUseEvent(PlayerItemInteractEventArgs ev)
        {
            if (!SCP_008.Config.Scp008Configs.InfectionHealItems.Contains(ev.CurrentItem.ID))
                return;

            if (ev.State == ItemInteractState.Finalizing)
                infectedPlayers.Remove(ev.Player);
        }
        private void Player_PlayerLeaveEvent(PlayerLeaveEventArgs ev)
            => infectedPlayers.Remove(ev.Player);
        private void Round_RoundRestartEvent()
        {
            runningCoroutines.ForEach((_) => Timing.KillCoroutines(_));
            runningCoroutines.Clear();
            infectedPlayers.Clear();
        }
        private void Round_RoundEndEvent()
        {
            runningCoroutines.ForEach((_) => Timing.KillCoroutines(_));
            runningCoroutines.Clear();
            infectedPlayers.Clear();
        }
        private void Player_PlayerDeathEvent(PlayerDeathEventArgs ev)
            => infectedPlayers.Remove(ev.Victim);
        private void Player_PlayerDamageEvent(PlayerDamageEventArgs ev)
        {
            //If the victim inflicted dmg to themselves / SCP-008 damage tick
            if (ev.Killer == ev.Victim || ev.HitInfo.GetDamageType().name == "FALLDOWN")
                return;

            //If the attacker is neither one of the infecting roles nor the doctor, leave.
            if (!SCP_008.Config.Scp008Configs.InfectingRoles.Contains(ev.Killer.RoleID) && ev.Killer.RoleType != RoleType.Scp049)
                return;

            //If the attacker was from one of the infecting roles, add to infectedPlayers
            if (SCP_008.Config.Scp008Configs.InfectingRoles.Contains(ev.Killer.RoleID))
                infectedPlayers.Add(ev.Killer);

            //If doctor only one hit k.o.'s, leave
            if (SCP_008.Config.Scp049Configs.NonOHKChance == 0.0f)
                return;

            //Chance for non-one hit k.o.
            if (Random.Range(0f, 100f) <= SCP_008.Config.Scp049Configs.NonOHKChance)
                ev.DamageAmount = (ev.Victim.MaxHealth * (SCP_008.Config.Scp049Configs.NonOHKDamage / 100));

            //If victim will die from it, leave.
            if ((ev.Victim.Health - ev.DamageAmount) <= 0.0f)
                return;

            //If doctor cannot infect victims, leave.
            if (SCP_008.Config.Scp049Configs.InfectChanceOnNonOHK == 0.0f)
                return;

            //Chance for infecting
            if (Random.Range(0f, 100f) <= SCP_008.Config.Scp049Configs.InfectChanceOnNonOHK)
                infectedPlayers.Add(ev.Victim);

        }
        private IEnumerator<float> DamageOverTime()
        {
            var infected = new Player[infectedPlayers.Count];
            while (true)
            {
                try
                {
                    infected = new Player[infectedPlayers.Count];
                    infectedPlayers.CopyTo(infected);

                    foreach (var player in infected)
                    {
                        if (SCP_008.Config.Scp008Configs.DamagerPerTickPercentage.Enabled)
                        {
                            player.Hurt((int)(player.MaxHealth * (SCP_008.Config.Scp008Configs.DamagerPerTickPercentage.Amount / 100)));
                        }
                        else if (SCP_008.Config.Scp008Configs.DamagerPerTickStatic.Enabled)
                        {
                            player.Hurt((int)SCP_008.Config.Scp008Configs.DamagerPerTickStatic.Amount);
                        }
                    }
                }
                catch (System.Exception e)
                {
                    SynapseController.Server.Logger.Info(e.StackTrace);
                }
                yield return Timing.WaitForSeconds(SCP_008.Config.Scp008Configs.DamagerOverTimeInterval);
            }
        }
    }
}