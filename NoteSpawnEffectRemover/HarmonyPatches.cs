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
        static bool Prefix(Color color, float animationDuration, Quaternion rotation)
        {
            // stop the effect from Initializing
            return false;
        }
    }
}
