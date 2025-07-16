// Stolen from https://github.com/Graicc/PracticeMod/blob/617a9f758077ea06cf0407a776580d6b021bcc35/PracticeMod/Patches/PlayerTeleportPatch.cs#L61
// Used without permission, but what are you gonna do, sue me?

using FiveNightsAtGorillas.Managers.Refrences;
using GorillaLocomotion;
using HarmonyLib;
using OculusSampleFramework;
using System;
using System.Reflection;
using UnityEngine;

namespace FiveNightsAtGorillas.Managers.Teleport
{
    [HarmonyPatch(typeof(GTPlayer))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    internal class Teleport
    {
        internal static Vector3 World2Player(Vector3 world) =>
            world - GorillaTagger.Instance.bodyCollider.transform.position + GorillaTagger.Instance.transform.position;

        internal static int teleportFrame;
        internal static void TeleportPlayer(Vector3 destinationPosition, float destinationRotation, bool killVelocity = true)
        {
            if (teleportFrame == Time.frameCount)
                return;
            GTPlayer.Instance.TeleportTo(World2Player(destinationPosition), Quaternion.Euler(0f, destinationRotation, 0f));
            teleportFrame = Time.frameCount;
            RefrenceManager.Data.FNAGMAP.SetActive(true);
        }
    }
}