using HarmonyLib;
using System;
using UnityEngine;


namespace NoteSpawnEffectRemover.HarmonyPatches
{
    [HarmonyPatch(typeof(BeatEffect))]
    [HarmonyPatch("Init")]
    [HarmonyPatch(new Type[] { typeof(Color), typeof(float), typeof(Quaternion) })]
    class BeatEffectInitPatch
    {
        static void Prefix(Color color, ref float animationDuration, Quaternion rotation)
        {
            // Just skipping the init function also works, but will leave an unused BeatEffect object for every note spawn created by BeatEffectSpawner
            // So instead, just shorten the animation duration from one second to 0 (effectively alive for one tick)
            animationDuration = 0f;
            return;
        }
    }
}
