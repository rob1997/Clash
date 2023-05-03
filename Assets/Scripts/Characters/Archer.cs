using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character<RangedCharacterData>
{
    [SerializeField] private Transform _muzzle;
    
    protected override void Attack()
    {
        base.Attack();
        
        if (!CanAttack)
        {
            return;
        }
        
        AnimateAttack();
        
        ResetAttackInterval();
        
        ThrowProjectile();
    }
    
    private void ThrowProjectile()
    {
        Projectile projectile = Instantiate(Data.Projectile, _muzzle.transform.position, Quaternion.identity);
        
        projectile.Initialize(TargetDamagable, Data.ProjectileSpeed, Data.ProjectileProximity, Data.Damage);
    }
}
