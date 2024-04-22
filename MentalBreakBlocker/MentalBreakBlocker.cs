using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace MentalBreakBlocker {
    [StaticConstructorOnStartup]
    public class MentalBreakBlocker {
        static MentalBreakBlocker() {
            Log.Message("[MentalBreakBlocker] Now active");
            var harmony = new Harmony("kaitorisenkou.MentalBreakBlocker");
            
            harmony.Patch(
                AccessTools.Method(typeof(MentalStateHandler), nameof(MentalStateHandler.TryStartMentalState), null, null),
                new HarmonyMethod(typeof(MentalBreakBlocker), nameof(Patch_TryStartMentalState), null),
                null,
                null,
                null
                );
            Log.Message("[MentalBreakBlocker] Harmony patch complete!");
        }

        public static bool Patch_TryStartMentalState(ref bool __result, Pawn ___pawn, MentalStateDef stateDef, bool causedByMood, bool causedByDamage,bool causedByPsycast) {
            if (___pawn == null)
                return true;
            foreach (Hediff hediff in ___pawn.health.hediffSet.hediffs) {
                var ex = hediff.def.GetModExtension<ModExtension_MentalBreakBlocker>();
                if (ex != null) {
                    if (ex.IsBlocked(causedByMood, causedByDamage, causedByPsycast)) {
                        __result = false;
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
