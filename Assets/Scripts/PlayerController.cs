using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : BaseController
{
    public bool inGame;
    public Vector2 input;

    SpriteRenderer spriteRenderer;
    public bool isEnemyDie;
    private void Start()
    { 
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Update()
    {
        if (!isEnemyDie && UnityEngine.Input.GetMouseButtonDown(0))
        {
            Attack();

        }
    }
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + input * moveSpeed * Time.fixedDeltaTime);
    }

    public void OnMove(InputValue inputValue)
    {
        input = inputValue.Get<Vector2>();
        if (input != Vector2.zero)
        {
            input = input.normalized;
            animator.SetBool("IsMoving", true);
            Vector2 lookDir = (input - (Vector2)transform.position);
            spriteRenderer.flipX = (lookDir.x < 0);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
           
    }
   
    public void OnLook(InputValue inputValue)
    {
        Vector2 mousePos = inputValue.Get<Vector2>();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 lookDir = (worldPos - (Vector2)transform.position);

       spriteRenderer.flipX = (lookDir.x < 0);
    }
    protected override void Attack()
    {
         base.Attack();
    }
    protected override void Damage(int damage)
    {
        base.Damage(damage);
    }
    protected override void Die()
    {

        GameManager.Instance.LoseGame();
    }
}
