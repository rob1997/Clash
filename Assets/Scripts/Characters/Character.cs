using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character<T> : MonoBehaviour where T : CharacterData
{
    private static int _movingAnimatorHash = Animator.StringToHash("Moving");
    private static int _attackAnimatorHash = Animator.StringToHash("Attack");
    
    [field: SerializeField] public T Data { get; private set; }
    
    protected Damagable TargetDamagable { get; private set; }
    
    protected Animator Animator { get; set; }

    public bool CanAttack => _attackInterval >= Data.AttackInterval;
    
    private Rigidbody _rigidBody;

    private Collider _targetDamagableCollider;
    
    private NavMeshObstacle _navMeshObstacle;
    
    private BuildableManager _buildableManager;
    
    private float _attackInterval;
    
    private bool _attacking;
    
    private bool _moving;

    private Vector3 _velocity;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();

        Animator = GetComponent<Animator>();

        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    private void Start()
    {
        GameManager.Instance.GetManager(out _buildableManager);
    }

    private void Update()
    {
        TryGetTarget();
        
        MoveTowardsTarget();

        LookAtTarget();
        
        if (_attacking)
        {
            Attack();
        }

        _moving = !_attacking && _rigidBody.velocity != Vector3.zero;
        
        AnimateMotion();
        
        UpdateNavMeshObstacle();
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _velocity;
    }

    private void AnimateMotion()
    {
        Animator.SetBool(_movingAnimatorHash, _moving);
    }

    private void UpdateNavMeshObstacle()
    {
        _navMeshObstacle.enabled = !_moving;
    }

    private void LookAtTarget()
    {
        Vector3 lookDirection = _velocity;
        lookDirection.y = 0;
        
        if (_attacking && TargetDamagable != null)
        {
            lookDirection = TargetDamagable.transform.position - transform.position;
            lookDirection.y = 0;
        }

        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
    
    private void TryGetTarget()
    {
        if (TargetDamagable == null)
        {
            Vector3 position = transform.position;
            
            IBuildable closestBuildable = _buildableManager.AllBuildables.Where(b => !b.IsNull())
                .OrderBy(b => Vector3.Distance(b.Obj.transform.position, position)).FirstOrDefault();
            
            if (closestBuildable != null && !closestBuildable.IsNull())
            {
                TargetDamagable = closestBuildable.Obj.GetComponent<Damagable>();

                _targetDamagableCollider = TargetDamagable.GetComponent<Collider>();
            }
        }
    }

    private void MoveTowardsTarget()
    {
        if (TargetDamagable != null)
        {
            Vector3 targetPosition = TargetDamagable.Target.position;
            
            Vector3 position = transform.position;

            Vector3 closestPointOnBounds = _targetDamagableCollider.ClosestPointOnBounds(position);

            float proximity = Vector3.Distance(closestPointOnBounds.GetXz(), position.GetXz());
            
            if (proximity <= Data.AttackRange)
            {
                _velocity = Vector3.zero;
                
                //start attacking
                if (!_attacking)
                {
                    _attacking = true;
                    
                    ResetAttackInterval();
                }
            }

            else
            {
                NavMeshPath path = new NavMeshPath();

                if (!NavMesh.SamplePosition(targetPosition, out NavMeshHit targetNavMeshHit, float.PositiveInfinity, NavMesh.AllAreas))
                {
                    Debug.LogWarning($"can't fetch closest position on NavMesh {TargetDamagable.gameObject.name}");   
                }
                
                if (!NavMesh.SamplePosition(position, out NavMeshHit characterNavMeshHit, float.PositiveInfinity, NavMesh.AllAreas))
                {
                    Debug.LogWarning($"can't fetch closest position on NavMesh {gameObject.name}");   
                }
                
                if (!NavMesh.CalculatePath(characterNavMeshHit.position, targetNavMeshHit.position, NavMesh.AllAreas, path))
                {
                    Debug.LogWarning($"can't calculate NavMesh path status {path.status}");
                }

                if (path.status != NavMeshPathStatus.PathInvalid && path.corners.Length > 1)
                {
                    //draw path
                    for (int i = 0; i < path.corners.Length - 1; i++)
                    {
                        Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
                    }
                    
                    Vector3 direction = path.corners[1] - position;
                    
                    Debug.DrawLine(position, position + direction.normalized * 5f, Color.green);
                    
                    direction.y = 0;
                    
                    _velocity = direction.normalized * Data.MoveSpeed;
                }

                _attacking = false;
            }
        }

        else
        {
            _velocity = Vector3.zero;
        }
    }

    protected virtual void Attack()
    {
        if (TargetDamagable == null)
        {
            _attacking = false;

            ResetAttackInterval();
            
            return;
        }
        
        _attackInterval += Time.deltaTime;
    }

    protected void AnimateAttack()
    {
        Animator.SetTrigger(_attackAnimatorHash);
    }
    
    protected void ResetAttackInterval()
    {
        _attackInterval = 0;
    }

    public void FootL()
    {
        
    }
    
    public void FootR()
    {
        
    }
    
    public void Hit()
    {
        
    }
}
