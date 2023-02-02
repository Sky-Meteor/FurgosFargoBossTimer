using FargowiltasSouls.NPCs.Challengers;
using FargowiltasSouls.NPCs.DeviBoss;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace FurgosFargoBossTimer
{
    public class FFBTGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int Phase;
        public Dictionary<int, int> BossPhaseTimer = new Dictionary<int, int>();

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (SpecialNPCs.Contains(npc.type))
            {
                RegisterNPCTimer(npc, NPCType<TrojanSquirrel>(), 2);
                RegisterNPCTimer(npc, NPCID.BrainofCthulhu, 2);
                Phase = 1;
            }
            else if (BossPhases.ContainsKey(npc.type))
            {
                int phaseCount = BossPhases[npc.type].Keys.Count;
                for (int i = 1; i <= phaseCount; i++)
                    BossPhaseTimer.Add(i, 0);
                Phase = 1;
            }
        }

        public override void PostAI(NPC npc)
        {
            if (SpecialNPCs.Contains(npc.type))
            {
                if ((npc.type == NPCType<TrojanSquirrel>() || npc.type == NPCID.BrainofCthulhu) && EnteredP2)
                {
                    Phase = 2;
                }
            }
            else if (BossPhases.ContainsKey(npc.type))
            {
                if (npc.life <= npc.lifeMax * BossPhases[npc.type][Phase])
                    Phase++;
            }
            else
                return;
            if (BossPhaseTimer.ContainsKey(Phase))
                BossPhaseTimer[Phase]++;
        }

        public override void OnKill(NPC npc)
        {
            if (BossPhases.ContainsKey(npc.type) || SpecialNPCs.Contains(npc.type))
            {
                Main.NewText(Lang.GetNPCNameValue(npc.type) + "：", Color.Lerp(Color.YellowGreen, Color.Red, 0.65f));
                foreach (KeyValuePair<int, int> phaseTimer in BossPhaseTimer)
                {
                    PrintTimer(phaseTimer.Key, phaseTimer.Value);
                }
            }
        }

        public override void ResetEffects(NPC npc)
        {
            EnteredP2 = false;
        }
        #region NPC Statistics
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
            /*{
                NPCID.BrainofCthulhu, new Dictionary<int, float>()
                {
                    { 1, 0.99f },
                    { 2, 0 }
                }
            },*/
            /*{
                NPCID.EaterofWorldsHead, new Dictionary<int, float>()
                {
                    { 1,0 }
                }
            }*/ //needs special
            {
                NPCID.QueenBee, new Dictionary<int, float>()
                {
                    { 1, 0.5f },
                    { 2, 0 }
                    //to be decided: divide by Subjects or Attacks
                }
            },
            {
                NPCID.SkeletronHead, new Dictionary<int, float>()
                {
                    { 1, 0.75f },
                    { 2, 0.50f },
                    { 3, 0 }
                }
            },
            {
                NPCID.Deerclops, new Dictionary<int, float>()
                {
                    { 1, 0.66f },
                    { 2, 0.33f },
                    { 3, 0 }
                }
            },
            {
                NPCType<DeviBoss>(), new Dictionary<int, float>()
                {
                    { 1, 0.66f },
                    { 2, 0 }
                }
            }
        };
        public readonly static Dictionary<int, Dictionary<int, float>> MasoBossPhases = new Dictionary<int, Dictionary<int, float>>() // this doesn't work rn
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
            /*{
                NPCID.BrainofCthulhu, new Dictionary<int, float>()
                {
                    { 1, 0.99f },
                    { 2, 0 }
                }
            },*/
            /*{
                NPCID.EaterofWorldsHead, new Dictionary<int, float>()
                {
                    { 1,0 }
                }
            }*/ //needs special
            {
                NPCID.QueenBee, new Dictionary<int, float>()
                {
                    { 1, 0.5f }
                    //to be decided: divide by Subjects or Attacks
                }
            },
            {
                NPCID.SkeletronHead, new Dictionary<int, float>()
                {
                    { 1, 0.75f },
                    { 2, 0.50f }
                }
            },
            {
                NPCID.Deerclops, new Dictionary<int, float>()
                {
                    { 1, 0.66f },
                    { 2, 0.33f }
                }
            },
            {
                NPCType<DeviBoss>(), new Dictionary<int, float>()
                {
                    { 1, 0.5f }
                } // for masochist
            }
        };
        public readonly static List<int> SpecialNPCs = new List<int>()
        {
            NPCType<TrojanSquirrel>(),
            NPCID.BrainofCthulhu,
        };
        #endregion
        #region Utils
        public void RegisterNPCTimer(NPC npc, int npcType, int phaseCount)
        {
            if (npc.type == npcType)
            {
                for (int i = 1; i <= phaseCount; i++)
                    BossPhaseTimer.Add(i, 0);
            }
        }
        public static void PrintTimer(int phase, int time)
        {
            float second = (float)Math.Round((double)time / 60, 2);
            int minute = (int)(second / 60);
            string secondDisplay = Math.Round(second % 60, 2).ToString();
            if (!secondDisplay.Contains('.'))
                secondDisplay += ".00";
            string[] splitSecond = secondDisplay.Split(".");
            if (splitSecond[0].Length == 1)
                secondDisplay = "0" + secondDisplay;
            if (splitSecond[1].Length == 1)
                secondDisplay += "0";
            string minuteDisplay = minute.ToString();
            if (minuteDisplay.Length == 1)
                minuteDisplay = "0" + minuteDisplay;
            Main.NewText($"第{phase}阶段：{minuteDisplay}:{secondDisplay}", Color.Lerp(Color.Lime, Color.LightBlue, 0.5f));
        }
        #endregion
        #region Special Fields
        public bool EnteredP2;
        #endregion
    }
}