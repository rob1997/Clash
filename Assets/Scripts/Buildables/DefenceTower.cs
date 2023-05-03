using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DefenceTower : Buildable<DefenceTowerData>
{
    [SerializeField] private Transform _muzzle;
    
    private BattleManager _battleManager;

    private Projectile[] _projectiles = {};

    private float _fireInterval;
    
    protected override void Start()
    {
        base.Start();

        GameManager.Instance.GetManager(out _battleManager);

        _fireInterval = Data.FireInterval;
    }

    protected override void OnReadyUpdate()
    {
        base.OnReadyUpdate();

        if (_fireInterval < Data.FireInterval)
        {
            _fireInterval += Time.deltaTime;
            
            return;
        }

        _fireInterval = 0;
        
        Collider[] results = new Collider[Data.Burst];

        int hits = Physics.OverlapSphereNonAlloc(transform.position, Data.Range, results, _battleManager.CharacterLayer);

        if (hits > 0)
        {
            foreach (var characterCollider in results.Where(c => c != null))
            {
                if (characterCollider.TryGetComponent(out Damagable damagable))
                {
                    ThrowProjectile(damagable);
                }
            }
        }
    }

    private void ThrowProjectile(Damagable damagable)
    {
        Projectile projectile = Instantiate(Data.Projectile, _muzzle.transform.position, Quaternion.identity);
        
        projectile.Initialize(damagable, Data.ProjectileSpeed, Data.Proximity, Data.Damage);
    }
}
