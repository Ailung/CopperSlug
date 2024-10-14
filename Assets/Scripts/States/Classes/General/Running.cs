using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : MonoBehaviour, IState
{
    private GameObject m_gameObject;
    private CharacterController characterController;
    private CloseEnemy enemyController;

    public Running(GameObject gameObject)
    {
        m_gameObject = gameObject;
        m_gameObject.TryGetComponent<CharacterController>(out characterController);
        m_gameObject.TryGetComponent<CloseEnemy>(out enemyController);
    }
    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void UpdateState()
    {
        if (Mathf.Abs(characterController.Rb.velocity.x) < 0.01f && Mathf.Abs(characterController.Rb.velocity.y) < 0.01f)
        {
            characterController.StateMachine.TransitionTo(characterController.StateMachine.idleState);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            characterController.StateMachine.TransitionTo(characterController.StateMachine.shootingState);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            characterController.StateMachine.TransitionTo(characterController.StateMachine.kickingState);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            characterController.StateMachine.TransitionTo(characterController.StateMachine.jumpingState);
        }
    }
}
