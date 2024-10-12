using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackSpeed;
    private CharacterController player;
    private EnemyController enemy;
    private bool isAttacking = false;
    public int AttackDamage => attackDamage;

    public int AttackSpeed => attackSpeed;

    public CharacterController Player => player;
    public bool IsAttacking => isAttacking;

    private void Awake()
    {
        player = this.gameObject.GetComponentInParent<CharacterController>();
        enemy = this.gameObject.GetComponentInParent<EnemyController>();
    }
    public void Attack()
    {
        Debug.Log("Shotgun attack");
    }

    IEnumerator waitToEnd()
    {
        yield return new WaitForSeconds(attackSpeed);
        gameObject.SetActive(false);
        isAttacking = false;
    }
    
}
