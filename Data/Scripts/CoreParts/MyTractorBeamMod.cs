using Sandbox.ModAPI;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRageMath;

namespace MyTractorBeamMod
{
    public class TractorBeamTurretLogic : MyGameLogicComponent
    {
        private IMyLargeTurretBase turret;
        private IMyEntity targetEntity;
        private bool beamActive = false;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
            turret = Entity as IMyLargeTurretBase;
            if (turret == null) return;

            // Add user controls
            InitializeControls();
        }

        private void InitializeControls()
        {
            var toggleAction = MyAPIGateway.TerminalControls.CreateAction<IMyLargeTurretBase>("ToggleTractorBeam");
            toggleAction.Name = new StringBuilder("Activate Tractor Beam");
            toggleAction.Action = (block) => ToggleBeam();
            MyAPIGateway.TerminalControls.AddAction(toggleAction);
        }

        private void ToggleBeam()
        {
            beamActive = !beamActive;
            if (!beamActive) targetEntity = null; // Reset target when deactivated
        }

        public override void UpdateBeforeSimulation()
        {
            if (beamActive && turret.HasTarget)
            {
                ApplyTractorBeamEffect();
            }
        }

        private void ApplyTractorBeamEffect()
        {
            // Pull or push the target based on your design
            Vector3D forceDirection = turret.WorldMatrix.Forward;
            float forceMagnitude = 500f; // Adjust based on desired effect
            targetEntity.Physics.AddForce(MyPhysicsForceType.APPLY_WORLD_FORCE, forceDirection * forceMagnitude, null, null);
        }
    }
}
