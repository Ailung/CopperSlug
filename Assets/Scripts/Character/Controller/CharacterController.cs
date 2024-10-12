using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private int speed = 3;
    [SerializeField] private int jumpIntensity = 3;

    private bool isFacingRight = true;
    private StateMachine playerStateMachine;
    private Rigidbody2D rb;
    private MeshRenderer mr;
    private IWeapon weapon;

    private float horizontal;
    private float vertical;

    public StateMachine StateMachine => playerStateMachine;
    public Rigidbody2D Rb => rb;
    public int JumpIntentisy => jumpIntensity;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mr = GetComponent<MeshRenderer>();
        playerStateMachine = new StateMachine(this.gameObject);
        playerStateMachine.Initialize(playerStateMachine.idleState);
        
    }

    void Update()
    {
        playerStateMachine.UpdateState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Drop"))
        {

            if (collision.TryGetComponent<Drops>(out Drops drop))
            {
                drop.PickUp();
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            StateMachine.TransitionTo(StateMachine.idleState);
        }
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");

        if (inputHorizontal < 0 && isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x *-1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            isFacingRight = false;
        } else if (inputHorizontal > 0 && !isFacingRight)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            isFacingRight = true;
        }

        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);

        Debug.Log(StateMachine.CurrentState.GetType().Name);
    }
}
