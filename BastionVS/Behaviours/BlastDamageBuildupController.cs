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
        private GenericSkill skill => characterBody.skillLocator.special;
        void Start()
        {
            characterBody = GetComponent<CharacterBody>();
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

        public void FillCharge(float damageCoefficient)
        {
            if (!skill.CanExecute())
                return;

            charge += damageCoefficient * Configs.M4_Charge_Multiplier.Value;
            charge = Mathf.Clamp(charge, 0, Configs.M4_Max_Damage.Value - Configs.M4_Min_Damage.Value);

            Debug.LogWarning($"Adding charge {damageCoefficient * Configs.M4_Charge_Multiplier.Value} - {charge}");

            if (NetworkServer.active)
            {
                CheckBuffs();
            }
        }

        private void CheckBuffs()
        {
            int count = characterBody.GetBuffCount(Prefabs.damageCount);
            while(count < charge)
            {
                characterBody.AddBuff(Prefabs.damageCount);
                count++;
            }
        }

        internal void ResetCharge()
        {
            charge = 0;

            int count = characterBody.GetBuffCount(Prefabs.damageCount);
            while (count > 0)
            {
                characterBody.RemoveBuff(Prefabs.damageCount);
                count--;
            }
        }
    }
}