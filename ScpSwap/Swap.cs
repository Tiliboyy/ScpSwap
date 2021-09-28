// -----------------------------------------------------------------------
// <copyright file="Swap.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using UnityEngine;

namespace ScpSwap
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using MEC;

    public class Swap
    {
        private static readonly List<Swap> Swaps = new List<Swap>();
        private static readonly List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
        private CoroutineHandle coroutine;

        private Swap(Player sender, Player receiver)
        {
            Sender = sender;
            Receiver = receiver;

            coroutine = Timing.RunCoroutine(RunTimeout());
            Coroutines.Add(coroutine);
        }

        public Player Sender { get; }

        public Player Receiver { get; }

        public static Swap FromSender(Player player)
        {
            foreach (Swap swap in Swaps)
            {
                if (swap.Sender == player)
                    return swap;
            }

            return null;
        }

        public static Swap FromReceiver(Player player)
        {
            foreach (Swap swap in Swaps)
            {
                if (swap.Receiver == player)
                    return swap;
            }

            return null;
        }

        public static void Send(Player sender, Player receiver)
        {
            Swaps.Add(new Swap(sender, receiver));
        }

        public static void Clear()
        {
            Swaps.Clear();
            foreach (CoroutineHandle coroutine in Coroutines)
                Timing.KillCoroutines(coroutine);

            Coroutines.Clear();
        }

        public void Run()
        {
            Sender.SendConsoleMessage("Swap successful!", "green");
            Receiver.SendConsoleMessage("Swap successful!", "green");

            RoleType senderRole = Sender.Role;
            Vector3 senderPosition = Sender.Position;
            float senderHealth = Sender.Health;

            RoleType receiverRole = Receiver.Role;
            Vector3 receiverPosition = Receiver.Position;
            float receiverHealth = Receiver.Health;

            Sender.Role = receiverRole;
            Sender.Position = receiverPosition;
            Sender.Health = receiverHealth;

            Receiver.Role = senderRole;
            Receiver.Position = senderPosition;
            Receiver.Health = senderHealth;

            Delete();
        }

        private IEnumerator<float> RunTimeout()
        {
            yield return Timing.WaitForSeconds(Plugin.Instance.Config.RequestTimeout);
            Delete();
        }

        private void Delete()
        {
            if (coroutine.IsRunning)
                Timing.KillCoroutines(coroutine);

            Swaps.Remove(this);
        }
    }
}