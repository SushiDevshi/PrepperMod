using System;
using System.Collections.Generic;
using R2API;
using RoR2;
using UnityEngine;

namespace PrepperMod
{
    public class HUNGERINGBuff
    {
        public BuffIndex Hungering;
        public string BuffName = "HUNGER";
        public HUNGERINGBuff()
        {
            BuffIndex Hungering = (BuffIndex)ItemAPI.AddCustomBuff(new CustomBuff("HUNGERING", new RoR2.BuffDef
            {
                name = BuffName,
                buffColor = new Color32(188, 47, 47, 1),
                canStack = false,
                isDebuff = false,
            }));
            
            On.RoR2.CharacterBody.RecalculateStats   += delegate (On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
            {
                if (self.HasBuff(this.Hungering))
                {
                    //self.baseMoveSpeed = self.baseMoveSpeed + 10;
                    //self.baseAttackSpeed = self.baseAttackSpeed * 2;
                    //self.baseArmor = self.baseArmor + 10;
                }
                orig(self);
            };
        }
    }
}
/*var buffDef = new BuffDef
{
    name = BuffName,
    buffColor = new Color32(188, 47, 47, 1),
    canStack = false,
    isDebuff = false,
};
var buff = new Custom   Buff(BuffName, buffDef);*/
