using FargowiltasSouls.NPCs.Challengers;
using System;
using System.Reflection;

namespace FurgosFargoBossTimer.OnPatches
{
    public class TrojanSquirrelAI : BaseOnPatch
    {
        protected override MethodBase method => typeof(TrojanSquirrel).GetMethod("PostAI");
        protected override Delegate Delegate => PostAI;

        private void PostAI(TrojanSquirrelAIDelegate orig, TrojanSquirrel trojanSquirrel)
        {
            if (trojanSquirrel.head == null && trojanSquirrel.arms == null)
                trojanSquirrel.NPC.GetGlobalNPC<FFBTGlobalNPC>().EnteredP2 = true;
        }
        private delegate void TrojanSquirrelAIDelegate(TrojanSquirrel trojanSquirrel);
    }
}