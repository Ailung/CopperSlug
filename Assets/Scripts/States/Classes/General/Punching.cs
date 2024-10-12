using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punching : MonoBehaviour, IState
{
    private GameObject m_gameObject;
    private CharacterController characterController;
    private EnemyController enemyController;

    public Punching(GameObject gameObject)
    {
        m_gameObject = gameObject;
        m_gameObject.TryGetComponent<CharacterController>(out characterController);
        m_gameObject.TryGetComponent<EnemyController>(out enemyController);
    }
    public void Enter()
    {

    }

    public void Exit()
    {
        Debug.Log("Salio en Punching");
    }

    public void UpdateState()
    {

    }
}
