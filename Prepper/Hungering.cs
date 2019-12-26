using System;
using System.Collections.Generic;
using R2API;
using RoR2;
using UnityEngine;

namespace PrepperMod
{
    public class HUNGERBuff
    {
        public string BuffName = "HUNGER";
        public HUNGERBuff()
        {
            var buffDef = new BuffDef
            {
                name = BuffName,
                buffColor = new Color32(188, 47, 47, 1),
                canStack = false,
                isDebuff = false,
            };
            var buff = new CustomBuff(BuffName, buffDef);
            On.RoR2.CharacterBody.RecalculateStats += delegate (On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
            {
                if (self.AddBuff(buff)
                orig(self);
            };
        }
    }
}