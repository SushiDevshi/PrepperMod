using RoR2;
using System.Collections.Generic;
using UnityEngine;

public class PrepperController : MonoBehaviour
{
    public bool rechargingOnHit = false;
    public bool hungering = false;
    public float maxDuration = 3f;
    public float durationOnHit = 0.32f;
    public float overclockTargetArmor = 30f;
    private CharacterBody characterBody;
    private TeamComponent teamComponent;
    private float duration;
    public bool useGroundPound = false;
    public bool executeGroundPound = false;
    private CharacterMotor characterMotor;
    private float baseDuration;

    private void Start()
    {
        this.characterBody = base.GetComponent<CharacterBody>();
        this.characterMotor = base.GetComponent<CharacterMotor>();
        this.teamComponent = base.GetComponent<TeamComponent>();
        this.hungering = false;
    }
    private void Update()
    {
        bool flag = this.duration > 0f;
        if (flag)
        {
            this.duration -= Time.deltaTime;
        }
        bool flag2 = this.duration < 0f;
        if (flag2)
        {
            this.Full();
        }
    }
    public void Hunger()
    {
        bool flag = !this.hungering;
        if (flag)
        {
            this.hungering = true;
            this.characterBody.baseMoveSpeed = this.characterBody.baseMoveSpeed + 10;
            this.characterBody.baseAttackSpeed = this.characterBody.baseAttackSpeed * 2;
            this.characterBody.baseArmor = this.characterBody.baseArmor + 10;
            //Util.PlaySound("Play_MULT_shift_start", base.gameObject);

        }
        this.characterBody.AddTimedBuff(BuffIndex.HiddenInvincibility, 1.25f);
        this.duration = this.maxDuration;
    }
    public void Full()
    {
        bool flag = this.hungering;
        if (flag)
        {
            this.hungering = false;
            this.rechargingOnHit = false;
            this.characterBody.baseMoveSpeed = this.characterBody.baseMoveSpeed - 10;
            this.characterBody.baseAttackSpeed = this.characterBody.baseAttackSpeed / 2;
            this.characterBody.baseArmor = this.characterBody.baseArmor - 10;
        }
    }
}