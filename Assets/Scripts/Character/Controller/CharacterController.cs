using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private int speed = 3;
    [SerializeField] private int jumpIntensity = 3;
    [SerializeField] private float invencibleTime = 3;

    private bool isFacingRight = true;
    private bool isFacingUp = false;
    private bool isInvencible = false;
    private float timerInvencible = 0;
    private StateMachine playerStateMachine;
    private Rigidbody2D rb;
    private MeshRenderer mr;
    private IWeapon weapon;
    private GameObject defaultWeapon;
    private GameObject currentWeapon;
    [SerializeField] private GameObject startWeapon;
    [SerializeField] private GameObject weaponPos;
    [SerializeField] private GameObject weaponPos2;

    private float horizontal;
    private float vertical;

    public StateMachine StateMachine => playerStateMachine;
    public Rigidbody2D Rb => rb;
    public int JumpIntentisy => jumpIntensity;
    public IWeapon Weapon => weapon;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mr = GetComponent<MeshRenderer>();
        playerStateMachine = new StateMachine(this.gameObject);
        playerStateMachine.Initialize(playerStateMachine.idleState);
        defaultWeapon = Instantiate(startWeapon);
        currentWeapon = defaultWeapon;

        //this.weapon = defaultWeapon.GetComponent<Pistol>();
        this.weapon = ((IWeapon)defaultWeapon.GetComponent(typeof(IWeapon)));

        currentWeapon.transform.SetParent(this.transform);
        currentWeapon.transform.position = weaponPos.transform.position;

        GameManager.Instance.SetCharacter(this);
        
    }

    void Update()
    {
        playerStateMachine.UpdateState();

        if (Input.GetKey(KeyCode.J))
        {
            weapon.Attack();

            //((IWeapon)currentWeapon.GetComponent(typeof(IWeapon))).Attack();
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (!isFacingUp) 
            {
                currentWeapon.transform.position = weaponPos2.transform.position;
                if (isFacingRight)
                {
                    currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(currentWeapon.transform.rotation.eulerAngles.x, currentWeapon.transform.rotation.eulerAngles.y, currentWeapon.transform.rotation.eulerAngles.z + 90));
                }
                else
                {
                    currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(currentWeapon.transform.rotation.eulerAngles.x, currentWeapon.transform.rotation.eulerAngles.y, currentWeapon.transform.rotation.eulerAngles.z - 90));
                }
            }
            isFacingUp = true;
        } else if(isFacingUp && !Input.GetKey(KeyCode.W))
        {
            currentWeapon.transform.position = weaponPos.transform.position;
            if (isFacingRight)
            {
                currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(currentWeapon.transform.rotation.eulerAngles.x, currentWeapon.transform.rotation.eulerAngles.y, currentWeapon.transform.rotation.eulerAngles.z - 90));
            } else
            {
                currentWeapon.transform.rotation = Quaternion.Euler(new Vector3(currentWeapon.transform.rotation.eulerAngles.x, currentWeapon.transform.rotation.eulerAngles.y, currentWeapon.transform.rotation.eulerAngles.z + 90));
            }
            isFacingUp=false;
        }

        if (weapon.BulletQuantity <= 0)
        {
            ResetWeapon();
        }

        if (isInvencible && timerInvencible < invencibleTime) 
        {
            timerInvencible += Time.deltaTime;
        }

        if (timerInvencible >= invencibleTime) 
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            isInvencible = false;
            timerInvencible = 0;
        }
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

        if (collision.gameObject.CompareTag("EnemyBullets"))
        {
            if (!isInvencible)
            {
                GetComponent<HealthManager>().getDamage(collision.gameObject.GetComponent<Bullet>().damage);
                collision.gameObject.GetComponent<Bullet>().Deactivate();
                isInvencible = true;
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            StateMachine.TransitionTo(StateMachine.idleState);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!isInvencible)
            {
                GetComponent<HealthManager>().getDamage(collision.gameObject.GetComponent<Enemy>().ContactDamage);
                isInvencible = true;
                GetComponent<SpriteRenderer>().color = Color.red;
            }
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

    public void SetWeapon(GameObject weaponPrefab)
    {
        GameObject newWeapon = Instantiate(weaponPrefab);
        defaultWeapon.SetActive(false);
        currentWeapon = newWeapon;

        this.weapon = ((IWeapon)newWeapon.GetComponent(typeof(IWeapon)));
        currentWeapon.transform.SetParent(this.transform);
        if (!isFacingRight) 
        {
            currentWeapon.transform.localScale = new Vector3(currentWeapon.transform.localScale.x * -1, currentWeapon.transform.localScale.y, currentWeapon.transform.localScale.z) ;
        }
        currentWeapon.transform.position = weaponPos.transform.position;
    }

    public void ResetWeapon()
    {
        Destroy(currentWeapon.gameObject);
        currentWeapon = defaultWeapon;

        //this.weapon = defaultWeapon.GetComponent<Pistol>();
        this.weapon = ((IWeapon)defaultWeapon.GetComponent(typeof(IWeapon)));

        defaultWeapon.gameObject.SetActive(true);
    }
}
