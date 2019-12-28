using BepInEx;
using RoR2;
using R2API.Utils;
using UnityEngine;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using R2API;
using R2API.AssetPlus;
using RoR2.Skills;
using EntityStates;
using System.Reflection;
using Prepper.Weapon;

namespace Prepper
{
    [BepInDependency(R2API.R2API.PluginGUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInPlugin("com.TheBoys.Prepper", "Prepper", "1.0.0")]
    public class Prepper : BaseUnityPlugin
    {
        public void Awake()
        {
            {   
                GameObject Prepper = Resources.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody").InstantiateClone("Prepper", true);
                PrefabAPI.RegisterNetworkPrefab(Prepper);
                var display = Prepper.GetComponent<RoR2.ModelLocator>().modelTransform.gameObject;
                SurvivorDef item = new SurvivorDef
                {
                    //We're finding the body prefab here,
                    bodyPrefab = Prepper,
                    //Description
                    descriptionToken = "gay baby",
                    //Display 
                    displayPrefab = display,
                    //Color on the select screen
                    primaryColor = new Color(0.8039216f, 0.482352942f, 0.843137264f),
                    //does literally nothing useful 
                };
                Languages.AddToken("PREPPER_BODY_NAME", "Prepper");
                //fetchin' components
                CharacterBody P_CB = item.bodyPrefab.GetComponent<CharacterBody>();
                CharacterMotor P_CM = item.bodyPrefab.GetComponent<CharacterMotor>();
                EntityStateMachine P_ESM = item.bodyPrefab.GetComponent<EntityStateMachine>();
                CharacterModel P_CML = item.bodyPrefab.GetComponent<CharacterModel>();
                CharacterDirection P_CD = item.bodyPrefab.GetComponent<CharacterDirection>();
                ModelLocator P_ML = item.bodyPrefab.GetComponent<ModelLocator>();
                SkillLocator P_SL = item.bodyPrefab.GetComponent<SkillLocator>();
                ModelSkinController P_MSC = item.bodyPrefab.GetComponent<ModelSkinController>();
                RagdollController P_RC = item.bodyPrefab.GetComponent<RagdollController>();
                Rigidbody P_RB = item.bodyPrefab.GetComponent<Rigidbody>();
                SkillFamily P_SF = item.bodyPrefab.GetComponent<SkillFamily>();
                P_CB.baseNameToken = "PREPPER_BODY_NAME";
                //big ol' block of skil code
                SkillFamily PY_skillFamily = P_SL.primary.skillFamily;
                SkillFamily SY_skillFamily = P_SL.secondary.skillFamily;
                SkillFamily UY_skillFamily = P_SL.utility.skillFamily;
                SkillFamily SL_skillFamily = P_SL.special.skillFamily;
                SkillDef PY = PY_skillFamily.variants[PY_skillFamily.defaultVariantIndex].skillDef;
                SkillDef SY = SY_skillFamily.variants[SY_skillFamily.defaultVariantIndex].skillDef;
                SkillDef UY = UY_skillFamily.variants[UY_skillFamily.defaultVariantIndex].skillDef;
                SkillDef SL = SL_skillFamily.variants[SL_skillFamily.defaultVariantIndex].skillDef;
                //actual stuff
                PY.activationState = new EntityStates.SerializableEntityStateType(typeof(Shotgun));
                var PYfield = typeof(EntityStates.SerializableEntityStateType)?.GetField("_typeName", BindingFlags.NonPublic | BindingFlags.Instance);
                PYfield?.SetValue(PY.activationState, typeof(Shotgun)?.AssemblyQualifiedName);
                UY.activationState = new EntityStates.SerializableEntityStateType(typeof(HungeringCry));
                var UYfield = typeof(EntityStates.SerializableEntityStateType)?.GetField("_typeName", BindingFlags.NonPublic | BindingFlags.Instance);
                UYfield?.SetValue(UY.activationState, typeof(HungeringCry)?.AssemblyQualifiedName);

            }
        }
    }
}

