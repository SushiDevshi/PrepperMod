using EntityStates;
using RoR2;
using System;
using UnityEngine.Networking;

namespace Prepper
{
    public class HungeringCry : BaseState
    {
        public float baseDuration = 0.25f;
        public override void OnEnter()
        {
            base.OnEnter();
            bool isAuthority = base.isAuthority;
            if (isAuthority)
            { 
                base.GetComponent<PrepperController>().Hunger();
                new BlastAttack
                {
                    attacker = base.gameObject,
                    inflictor = base.gameObject,
                    teamIndex = TeamComponent.GetObjectTeam(base.gameObject),
                    baseDamage = this.damageStat * 2,
                    baseForce = 0,
                    position = base.transform.position,
                    radius = 20,
                    procCoefficient = 1f,
                    falloffModel = BlastAttack.FalloffModel.None,
                    damageType = DamageType.Stun1s & DamageType.SlowOnHit,
                    crit = RollCrit()
                }.Fire();
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            bool flag = base.fixedAge > this.baseDuration && base.isAuthority;
            if (flag)
            {
                this.outer.SetNextStateToMain();
            }
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }

    }
}
