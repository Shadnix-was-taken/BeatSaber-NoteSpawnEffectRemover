using Harmony;
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
            animationDuration = 0f;
            return;
        }
    }
}
