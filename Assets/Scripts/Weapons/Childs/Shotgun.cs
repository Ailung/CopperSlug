using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour,IWeapon
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackSpeed;
    private CharacterController player;
    private bool isAttacking = false;
    public int AttackDamage => attackDamage;

    public int AttackSpeed => attackSpeed;

    public CharacterController Player => player;
    public bool IsAttacking => isAttacking;

    private void Awake()
    {
        player = this.gameObject.GetComponentInParent<CharacterController>();
    }
    public void Attack()
    {
        Debug.Log("Shtgun attack");
    }

    IEnumerator waitToEnd()
    {
        yield return new WaitForSeconds(attackSpeed);
        gameObject.SetActive(false);
        isAttacking = false;
    }
}
