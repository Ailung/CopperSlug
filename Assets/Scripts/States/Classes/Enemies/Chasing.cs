using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour, IState
{
    private GameObject parentGameObject;
    private CharacterController characterController;
    private CloseEnemy enemyController;

    public Chasing(GameObject gameObject)
    {
        parentGameObject = gameObject;
        parentGameObject.TryGetComponent<CharacterController>(out characterController);
        parentGameObject.TryGetComponent<CloseEnemy>(out enemyController);
    }
    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void UpdateState()
    {
        if (enemyController.PlayerDistance > enemyController.ChaseDistance)
        {
            enemyController.StateMachine.TransitionTo(enemyController.StateMachine.idleState);
        }
        if (enemyController.PlayerDistance <= enemyController.StopDistance)
        {
            enemyController.StateMachine.TransitionTo(enemyController.StateMachine.shootingState);
        }
    }
}
