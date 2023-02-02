using FargowiltasSouls.NPCs.Challengers;
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
            BossPhaseTimer[Phase]++;
        }

        public override void OnKill(NPC npc)
        {
            if (!BossPhases.ContainsKey(npc.type) && !SpecialNPCs.Contains(npc.type))
                return;
            Main.NewText(Lang.GetNPCNameValue(npc.type) + "：", Color.Lerp(Color.YellowGreen, Color.Red, 0.65f));
            foreach (KeyValuePair<int, int> phaseTimer in BossPhaseTimer)
            {
                float second = (float)Math.Round((double)phaseTimer.Value / 60, 2);
                int minute = (int)(second / 60);
                string secondDisplay = second.ToString();
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
                Main.NewText($"第{phaseTimer.Key}阶段：{minuteDisplay}:{secondDisplay}", Color.Lerp(Color.Lime, Color.LightBlue, 0.5f));
            }
        }

        public override void ResetEffects(NPC npc)
        {
            EnteredP2 = false;
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
            /*{
                NPCID.BrainofCthulhu, new Dictionary<int, float>()
                {
                    { 1, 0.99f },
                    { 2, 0 }
                }
            },*/
        };
        public readonly static List<int> SpecialNPCs = new List<int>()
        {
            NPCType<TrojanSquirrel>(),
            NPCID.BrainofCthulhu,
        };
        #region Utils
        public void RegisterNPCTimer(NPC npc, int npcType, int phaseCount)
        {
            if (npc.type == npcType)
            {
                for (int i = 1; i <= phaseCount; i++)
                    BossPhaseTimer.Add(i, 0);
            }
        }
        #endregion
        #region Special Fields
        public bool EnteredP2;
        #endregion
    }
}