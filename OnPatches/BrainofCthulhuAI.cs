using FargowiltasSouls.EternityMode.Content.Boss.PHM;
using System;
using System.Reflection;
using Terraria;

namespace FurgosFargoBossTimer.OnPatches
{
    public class BrainofCthulhuAI : BaseOnPatch
    {
        protected override MethodBase method => typeof(BrainofCthulhu).GetMethod("SafePreAI");
        protected override Delegate Delegate => SafePreAI;

        private bool SafePreAI(BrainofCthulhuAIDelegate orig, BrainofCthulhu brainofCthulhu, NPC npc)
        {
            if (brainofCthulhu.EnteredPhase2)
                npc.GetGlobalNPC<FFBTGlobalNPC>().EnteredP2 = true;
            return orig.Invoke(brainofCthulhu, npc);
        }
        private delegate bool BrainofCthulhuAIDelegate(BrainofCthulhu brainofCthulhu, NPC npc);
    }
}