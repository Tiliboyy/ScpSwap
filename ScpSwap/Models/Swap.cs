// -----------------------------------------------------------------------
// <copyright file="Swap.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ScpSwap.Models
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;

    /// <summary>
    /// Handles the swapping of players.
    /// </summary>
    public class Swap
    {
        private static readonly List<Swap> Swaps = new List<Swap>();
        private static readonly List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
        private CoroutineHandle coroutine;

        private Swap(Player sender, Player receiver)
        {
            Sender = sender;
            Receiver = receiver;

            SendRequestMessages();
            coroutine = Timing.RunCoroutine(RunTimeout());
            Coroutines.Add(coroutine);
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
        }

        /// <summary>
        /// Gets the sender of the swap request.
        /// </summary>
        public Player Sender { get; }

        /// <summary>
        /// Gets the person who was sent the swap request.
        /// </summary>
        public Player Receiver { get; }

        /// <summary>
        /// Gets a swap request based on the sender.
        /// </summary>
        /// <param name="player">The sender of the request.</param>
        /// <returns>The sent swap request or null if one isn't found.</returns>
        public static Swap FromSender(Player player)
        {
            foreach (Swap swap in Swaps)
            {
                if (swap.Sender == player)
                    return swap;
            }

            return null;
        }

        /// <summary>
        /// Gets a swap request based on the receiver.
        /// </summary>
        /// <param name="player">The receiver of the request.</param>
        /// <returns>The sent swap request or null if one isn't found.</returns>
        public static Swap FromReceiver(Player player)
        {
            foreach (Swap swap in Swaps)
            {
                if (swap.Receiver == player)
                    return swap;
            }

            return null;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Swap"/> class with the given Sender and Receiver.
        /// </summary>
        /// <param name="sender">The sender of the request.</param>
        /// <param name="receiver">The receiver of the request.</param>
        public static void Send(Player sender, Player receiver)
        {
            Swaps.Add(new Swap(sender, receiver));
        }

        /// <summary>
        /// Clears all active swap requests.
        /// </summary>
        public static void Clear()
        {
            Swaps.Clear();
            foreach (CoroutineHandle coroutine in Coroutines)
                Timing.KillCoroutines(coroutine);

            Coroutines.Clear();
        }

        /// <summary>
        /// Performs the swap between the <see cref="Sender"/> and the <see cref="Receiver"/>.
        /// </summary>
        public void Run()
        {
            PartiallyDestroy();
            SwapData senderData = new SwapData(Sender);
            SwapData receiverData = new SwapData(Receiver);

            senderData.Swap(Receiver);
            receiverData.Swap(Sender);

            Sender.SendConsoleMessage("Swap successful!", "green");
            Receiver.SendConsoleMessage("Swap successful!", "green");
            Swaps.Remove(this);
        }

        /// <summary>
        /// Broadcasts the swap cancellation then destroys the swap.
        /// </summary>
        public void Cancel()
        {
            Sender.Broadcast(5, "Swap request cancelled!", shouldClearPrevious: true);
            Destroy();
        }

        /// <summary>
        /// Broadcasts the swap decline then destroys the swap.
        /// </summary>
        public void Decline()
        {
            Sender.Broadcast(5, $"{Receiver.Nickname} has declined your swap request.", shouldClearPrevious: true);
            Destroy();
        }

        private void PartiallyDestroy()
        {
            if (coroutine.IsRunning)
                Timing.KillCoroutines(coroutine);

            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
        }

        private void Destroy()
        {
            PartiallyDestroy();
            Swaps.Remove(this);
        }

        private void SendRequestMessages()
        {
            CustomSwap customSwap = ValidSwaps.GetCustom(Sender);
            string consoleMessage = Plugin.Instance.Translation.RequestConsoleMessage;
            consoleMessage = consoleMessage.Replace("$SenderName", Sender.Nickname);
            consoleMessage = consoleMessage.Replace("$RoleName", customSwap?.Name ?? Sender.Role.ToString());
            Receiver.SendConsoleMessage(consoleMessage, "yellow");
            Receiver.Broadcast(Plugin.Instance.Translation.RequestBroadcast);
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player == Sender || ev.Player == Receiver)
                Cancel();
        }

        private IEnumerator<float> RunTimeout()
        {
            yield return Timing.WaitForSeconds(Plugin.Instance.Config.RequestTimeout);
            Destroy();
        }
    }
}