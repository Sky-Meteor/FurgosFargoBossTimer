using FargowiltasSouls.NPCs.Challengers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FurgosFargoBossTimer
{
    public class FFBTGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public int Phase;
        public Dictionary<int, int> BossPhaseTimer = new Dictionary<int, int>();

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (!BossPhases.ContainsKey(npc.type))
                return;
            int phaseCount = BossPhases[npc.type].Keys.Count;
            for (int i = 1; i <= phaseCount; i++)
                BossPhaseTimer.Add(i, 0);
            Phase = 1;
        }

        public override void PostAI(NPC npc)
        {
            if (!BossPhases.ContainsKey(npc.type))
                return;
            BossPhaseTimer[Phase]++;
            if (npc.life <= npc.lifeMax * BossPhases[npc.type][Phase])
                Phase++;
        }

        public override void OnKill(NPC npc)
        {
            if (!BossPhases.ContainsKey(npc.type))
                return;
            Main.NewText(Lang.GetNPCNameValue(npc.type) + "：", Color.Lerp(Color.YellowGreen, Color.Red, 0.65f));
            foreach (KeyValuePair<int, int> phaseTimer in BossPhaseTimer)
            {
                float second = (float)Math.Round((double)phaseTimer.Value / 60, 2);
                int minute = (int)(second / 60);
                string secondDisplay = second.ToString();
                if (!secondDisplay.Contains("."))
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
