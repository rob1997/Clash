using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidBody;
    
    private Damagable _damagable;

    private float _speed;
    
    private float _proximity;
    
    private float _damage;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void Initialize(Damagable damagable, float speed, float proximity, float damage)
    {
        _damagable = damagable;

        _speed = speed;

        _proximity = proximity;

        _damage = damage;
    }

    private void Update()
    {
        if (_damagable != null) CheckProximity();

        else Destroy(gameObject);
    }

    private void CheckProximity()
    {
        Vector3 position = transform.position;

        float distance = Vector3.Distance(position, _damagable.Target.position);

        if (distance <= _proximity)
        {
            _damagable.TakeDamage(_damage);
            
            Destroy(gameObject);
        }
    }
    
    private void FixedUpdate()
    {
        if (_damagable != null) FollowTarget();

        else Destroy(gameObject);
    }

    private void FollowTarget()
    {
        Vector3 position = transform.position;
        
        Vector3 direction = _damagable.Target.position - position;

        _rigidBody.velocity = _speed * direction.normalized;
        
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
