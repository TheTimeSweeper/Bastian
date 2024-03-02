using EntityStates;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using UnityEngine;

namespace Bastian.SkillDefs
{
    public class HasBlastDamageBuildupSkillDef : HasAuthorityComponentSkillDef<BlastDamageBuildupController> { }

    public interface IHasAuthoritySkillDefComponent<T>
    {
        T componentFromSkillDef { get; set; }
    }

    public abstract class HasAuthorityComponentSkillDef<T> : SkillDef where T : MonoBehaviour
    {
        public override BaseSkillInstanceData OnAssigned([NotNull] GenericSkill skillSlot)
        {
            return new InstanceData
            {
                componentFromSkillDef = skillSlot.GetComponent<T>()
            };
        }

        public class InstanceData : BaseSkillInstanceData 
        {
            public T componentFromSkillDef;
        }

        public override EntityState InstantiateNextState([NotNull] GenericSkill skillSlot)
        {
            EntityState entityState = base.InstantiateNextState(skillSlot);

            InstanceData instanceData = (InstanceData)skillSlot.skillInstanceData;

            IHasAuthoritySkillDefComponent<T> stateWithComponent;
            if ((stateWithComponent = entityState as IHasAuthoritySkillDefComponent<T>) != null)
            {
                stateWithComponent.componentFromSkillDef = instanceData.componentFromSkillDef;

            }
            return entityState;
        }
    }
}