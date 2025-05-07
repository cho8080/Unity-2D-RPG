using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    public float speed;
    public float jumpPower = 200f;
    public bool isFlap;
    public bool isDead;
    public Vector2 startPos;
    public Animator animator;
   [HideInInspector] public Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        startPos = transform.position;  
    }
    private void Update()
    {
        if ((isDead)) { return; }
        Flap();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if ((isDead)){return;}

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = speed;
        if (isFlap)
        {
            velocity.y += jumpPower;
            isFlap = false;
        }
       

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void Flap()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            isFlap = true;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
            return;

        animator.SetInteger("IsDie", 1);
        isDead = true;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.EndGame();
    }
}
