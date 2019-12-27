using RoR2;
using System.Collections.Generic;
using UnityEngine;

public class PrepperController : MonoBehaviour
{
    public bool rechargingOnHit = false;
    public bool hungering = false;
    public float maxDuration = 3f;
    private CharacterBody characterBody;
    private float duration;
    private float baseDuration;

    private void Start()
    {
        this.characterBody = base.GetComponent<CharacterBody>();
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