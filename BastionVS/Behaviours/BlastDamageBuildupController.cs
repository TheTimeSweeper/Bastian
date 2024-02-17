using R2API.Networking.Interfaces;
using RoR2;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Bastian
{
    public class BlastDamageBuildupController : MonoBehaviour/*, IOnDamageDealtServerReceiver*/
    {
        public float charge;
        
        private CharacterBody characterBody;
        private NetworkIdentity bodyNetIdentity;
        private GenericSkill skill => characterBody.skillLocator.special;

        private float timer;

        void Start()
        {
            characterBody = GetComponent<CharacterBody>();
            bodyNetIdentity = GetComponent<NetworkIdentity>();
        }

        void FixedUpdate()
        {
            if (!Util.HasEffectiveAuthority(bodyNetIdentity))
                return;

            timer -= Time.fixedDeltaTime;
            if(timer < 0)
            {
                timer = 1;
                charge -= Configs.M4_Charge_Decay.Value;
                OnFillCharge();
            }
        }

        //public void OnDamageDealtServer(DamageReport damageReport)
        //{
        //    if (!damageReport.damageInfo.rejected && R2API.DamageAPI.HasModdedDamageType(damageReport.damageInfo, Prefabs.buildupDamage))
        //    {
        //        if (characterBody.damage != 0)
        //        {
        //            float damageCoefficient = damageReport.damageDealt / characterBody.damage;
        //            FillCharge(damageCoefficient);
        //        }
        //    }
        //}

        public void FillChargeAuthority(float damageCoefficient)
        {
            if (!skill.CanExecute())
                return;

            timer = Configs.M4_Charge_Decay_Delay.Value;
            charge += damageCoefficient * Configs.M4_Charge_Multiplier.Value;

            OnFillCharge();
        }

        private void OnFillCharge()
        {
            charge = Mathf.Clamp(charge, 0, Configs.M4_Max_Damage.Value - Configs.M4_Min_Damage.Value);

            if (NetworkServer.active)
            {
                SyncCheckBuffs(charge);
            }
            else
            {
                new Networking.SyncFillCharge(charge, gameObject).Send(R2API.Networking.NetworkDestination.Server);
            }
        }

        public void SyncCheckBuffs(float charge)
        {
            if (!NetworkServer.active)
                return;

            int count = characterBody.GetBuffCount(Prefabs.damageCount);
            while(count < (int)charge)
            {
                characterBody.AddBuff(Prefabs.damageCount);
                count++;
            }
            while(count > (int)charge)
            {
                characterBody.RemoveBuff(Prefabs.damageCount);
                count--;
            }
        }

        public void ResetChargeAuthority()
        {
            if (NetworkServer.active)
            {
                SyncResetCharge();
            }
            else
            {
                new Networking.SyncResetCharge(gameObject).Send(R2API.Networking.NetworkDestination.Server);
            }
        }

        public void SyncResetCharge()
        {
            charge = 0;
            if (NetworkServer.active)
            {
                int count = characterBody.GetBuffCount(Prefabs.damageCount);
                while (count > 0)
                {
                    characterBody.RemoveBuff(Prefabs.damageCount);
                    count--;
                }
            }
        }
    }
}