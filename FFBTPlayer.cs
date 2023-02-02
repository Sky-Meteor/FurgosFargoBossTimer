using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FurgosFargoBossTimer
{
    public class FFBTPlayer : ModPlayer
    {
        public override void PostUpdateMiscEffects()
        {
            if (NPC.FindFirstNPC(NPCID.EaterofWorldsHead) != -1)
            {
                EaterofWorldsTimer++;
            }
            else if (EaterofWorldsTimer != 0 && NPC.FindFirstNPC(NPCID.EaterofWorldsTail) == -1)
            {
                Main.NewText(Lang.GetNPCNameValue(NPCID.EaterofWorldsHead) + "：", Color.Lerp(Color.YellowGreen, Color.Red, 0.65f));
                FFBTGlobalNPC.PrintTimer(1, EaterofWorldsTimer);
                EaterofWorldsTimer = 0;
            }
        }
        public int EaterofWorldsTimer = 0;
    }
}