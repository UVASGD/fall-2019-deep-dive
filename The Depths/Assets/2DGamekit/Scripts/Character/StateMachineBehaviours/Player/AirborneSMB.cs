using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit2D
{
    public class AirborneSMB : SceneLinkedSMB<PlayerCharacter>
    {
        public override void OnSLStateNoTransitionUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            m_MonoBehaviour.UpdateFacing();
            m_MonoBehaviour.UpdateJump();
            if (m_MonoBehaviour.CheckForJumpInput())
            {
                m_MonoBehaviour.DoubleJump();
            }

			if (m_MonoBehaviour.CheckForDashInput()) {
				m_MonoBehaviour.Dash(true);
			} else {
				m_MonoBehaviour.GroundedHorizontalMovement(true);
			}
			m_MonoBehaviour.AirborneVerticalMovement();
            m_MonoBehaviour.CheckForGrounded();
            m_MonoBehaviour.CheckForHoldingGun();
            if(m_MonoBehaviour.CheckForMeleeAttackInput())
                m_MonoBehaviour.MeleeAttack ();
            m_MonoBehaviour.CheckAndFireGun ();
            m_MonoBehaviour.CheckForCrouching ();
			m_MonoBehaviour.CheckForDashInput();
		}
    }
}