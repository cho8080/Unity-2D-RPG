using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] protected float attackRange = 1.3f;
    [SerializeField] protected int attackDamage = 10;

    protected Rigidbody2D _rigidbody;
    protected Animator animator;
    public GameObject target;
    [Range(1, 10)] public float moveSpeed;


    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        animator = this.gameObject.transform.GetChild(0).GetComponent<Animator>();

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Attack()
    {
        BaseController baseController = target.GetComponent<BaseController>();
        if (currentHealth == 0 || baseController.currentHealth == 0) { return; }

        target.GetComponent<BaseController>().Damage(attackDamage);
    }
    protected virtual void Damage(int damage)
    {

        if (currentHealth == 0) { return; }
        currentHealth -= damage;
        animator.SetBool("IsDamage", true);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    protected virtual void Die()
    {

        Destroy(this.gameObject);
    }
    // 시각화용: 공격 범위 표시
    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
