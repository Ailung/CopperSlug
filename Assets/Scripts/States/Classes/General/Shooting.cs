using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour, IState
{
    private GameObject m_gameObject;
    private CharacterController characterController;
    private CloseEnemy enemyController;

    public Shooting(GameObject gameObject)
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

    }
}
