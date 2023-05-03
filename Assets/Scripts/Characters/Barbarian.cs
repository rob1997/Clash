using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Character<MeleeCharacterData>
{
    private Coroutine _slashCoroutine;
    
    protected override void Attack()
    {
        base.Attack();
        
        if (!CanAttack)
        {
            return;
        }
        
        AnimateAttack();
        
        ResetAttackInterval();

        if (_slashCoroutine != null)
        {
            StopCoroutine(_slashCoroutine);
            
            if (TargetDamagable != null) TargetDamagable.TakeDamage(Data.Damage);
        }
        
        _slashCoroutine = StartCoroutine(Slash());
    }

    private IEnumerator Slash()
    {
        yield return new WaitForSeconds(Data.AttackInterval / 2f);
        
        if (TargetDamagable != null) TargetDamagable.TakeDamage(Data.Damage);
    }
}
