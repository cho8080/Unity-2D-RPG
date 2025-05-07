using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyController;

public class EnemyController : BaseController
{
    // Start is called before the first frame update
    public enum EnemyState
    {
        Idle,
        Move,
        Attack
    }
    public EnemyState enemyState = EnemyState.Idle;

    public UIManager uIManager;
    protected float timer;
    protected int waitingTime;
    private void Start()
    {
        timer = 0.0f;
        waitingTime = 1;
    }
    // Update is called once per frame
    void Update()
    {
        FollowTartget();
    }
   
    protected override void Attack()
    {
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            base.Attack();
            uIManager.ChangeHp();
            timer = 0;
        }
    }
    protected override void Damage(int damage)
    {
        if (currentHealth == 0) { return; }
        currentHealth -= damage;
        animator.SetBool("IsDamage", true);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            PlayerController playerController = target.GetComponent<PlayerController>();
            playerController.isEnemyDie = true;
            if (currentHealth == 0) { GameManager.Instance.WinGame(); }
            Die();
        }

        
    }
    void FollowTartget()
    {
        BaseController baseController = target.GetComponent<BaseController>();
      
        if (baseController.currentHealth == 0)
        {
            enemyState = EnemyState.Idle;
            return;
        }
       
        float dis = Vector2.Distance(target.transform.position, transform.transform.position);
        enemyState = EnemyState.Move;
        Vector2 direction = (target.transform.position - transform.position).normalized;
        Vector2 velocity = direction * moveSpeed;

        _rigidbody.velocity = velocity;
     
        // 도착 판정 (정지)
       if (dis < attackRange)
        {
            _rigidbody.velocity = Vector2.zero;
            enemyState = EnemyState.Attack;
            Attack();
        }       
    }
}
