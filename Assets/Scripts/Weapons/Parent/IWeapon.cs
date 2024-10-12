using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public int AttackDamage { get; }
    public int AttackSpeed { get; }
    public CharacterController Player { get; }
    public bool IsAttacking { get; }
    public void Attack();
}
