using FargowiltasSouls.NPCs.Challengers;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FurgosFargoBossTimer
{
    public class FFBTGlobalNPC : GlobalNPC
    {
        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            base.OnSpawn(npc, source);
        }

        public override void PostAI(NPC npc)
        {
            base.PostAI(npc);
        }

        public override void OnKill(NPC npc)
        {
            base.OnKill(npc);
        }

        public readonly static Dictionary<int, Dictionary<int, float>> BossPhases = new Dictionary<int, Dictionary<int, float>>()
        {
            /*{
                ModContent.NPCType<TrojanSquirrel>()
            },*/
            {
                NPCID.KingSlime, new Dictionary<int, float>()
                {
                    { 1, 0.66f },
                    { 2, 0 }
                }
            },
            {
                NPCID.EyeofCthulhu, new Dictionary<int, float>()
                {
                    { 1, 0.5f },
                    { 2, 0.1f },
                    { 3, 0 }
                }
            },
            {
                NPCID.BrainofCthulhu, new Dictionary<int, float>()
                {
                    { 1, 0.99f },
                    { 2, 0 }
                }
            },
        };
    }
}
