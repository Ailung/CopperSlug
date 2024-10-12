using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour, IState
{

    private GameObject m_gameObject;
    private CharacterController characterController;

    public Jumping(GameObject gameObject)
    {
        m_gameObject = gameObject;
        m_gameObject.TryGetComponent<CharacterController>(out characterController);
    }
    public void Enter()
    {
        characterController.Rb.AddForce(new Vector2(0,1*characterController.JumpIntentisy),ForceMode2D.Impulse);
    }

    public void Exit()
    {
        
    }

    public void UpdateState()
    {
        
    }
}
