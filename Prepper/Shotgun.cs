using EntityStates;
using RoR2;
using UnityEngine;

namespace Prepper.weapon
{
    public class Shotgun : BaseState
    {
        public GameObject effectPrefab = Resources.Load<GameObject>("prefabs/effects/muzzleflashes/muzzleflashfmj");
        public GameObject hitEffectPrefab = Resources.Load<GameObject>("prefabs/effects/impacteffects/fmjimpact");
        public GameObject tracerEffectPrefab = Resources.Load<GameObject>("prefabs/effects/tracers/tracerbarrage");
        public float force = 0f;
        public float duration;
        public float bulletCount = 3;
        public float baseDuration = 0.5f;
        public float recoilAmplitude = 0f;
        public bool buttonReleased;
        public override void OnEnter()
        {
            base.OnEnter();
            this.duration = this.baseDuration / base.attackSpeedStat;
            Ray aimRay = base.GetAimRay();
            base.StartAimMode(aimRay, 2f, false);
            Util.PlaySound("Play_wFeralShoot1", base.gameObject);
            string muzzleName = "MuzzleShotgun";
            bool flag = this.effectPrefab;
            if (flag)
            {
                EffectManager.SimpleMuzzleFlash(this.effectPrefab, base.gameObject, muzzleName, false);
            }
            bool isAuthority = base.isAuthority;
            bool flag2 = isAuthority;
            if (flag2)
            {
                new BulletAttack
                {
                    owner = base.gameObject,
                    weapon = base.gameObject,
                    origin = aimRay.origin,
                    aimVector = aimRay.direction,
                    minSpread = 2,
                    maxSpread = base.characterBody.spreadBloomAngle,
                    bulletCount = 4,
                    procCoefficient = 1,
                    damage = 4.2f,
                    falloffModel = BulletAttack.FalloffModel.Buckshot,
                    tracerEffectPrefab = this.tracerEffectPrefab,
                    muzzleName = muzzleName,
                    hitEffectPrefab = this.hitEffectPrefab,
                    isCrit = RollCrit(),
                }.Fire();
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            this.outer.SetNextStateToMain();
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            this.buttonReleased |= !base.inputBank.skill1.down;
            bool flag = base.fixedAge >= this.duration && base.isAuthority;
            if (flag)
            {
                this.outer.SetNextStateToMain();
            }
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            bool flag = this.buttonReleased && base.fixedAge >= this.duration;
            InterruptPriority result;
            if (flag)
            {
                result = InterruptPriority.Any;
            }
            else
            {
                result = InterruptPriority.Skill;
            }
            return result;
        }
    }
}