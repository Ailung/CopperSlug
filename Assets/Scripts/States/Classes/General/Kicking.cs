using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kicking : MonoBehaviour, IState
{
    private GameObject m_gameObject;
    private CharacterController characterController;
    private CloseEnemy enemyController;

    public Kicking(GameObject gameObject)
    {
        m_gameObject = gameObject;
        m_gameObject.TryGetComponent<CharacterController>(out characterController);
        m_gameObject.TryGetComponent<CloseEnemy>(out enemyController);
    }
    public void Enter()
    {
        Debug.Log("Entro en Kicking");
    }

    public void Exit()
    {
        Debug.Log("Salio en Punching");
    }

    public void UpdateState()
    {

    }
}
