using Managers;
using UnityEngine;
using Upgrades;

namespace Units
{
    public class BoomerUnit : AbstractUnit
    {
        
        
        protected override void Awake() {
            base.Awake();
        }

        public override void ComputeRotationFromChild() {
            
        }

        protected override void InitialiseUnitParameters() {
            
        }
        
        protected override GameObject target { get; set; }
        public override Animation Anim { get; }
        public override AbstractUpgradeContainer abstractUpgradeContainer { get; protected set; }
        public override bool placed { get; protected set; }
        protected override int price { get; set; }
        protected override int sell { get; }
        protected override UIManager uiManager { get; set; }
        public override IUpgrade currentUpgrade { get; set; }
        
        public override float baseAttackSpeed => 1f;
    }
}
