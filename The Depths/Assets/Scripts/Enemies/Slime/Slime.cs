using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;



public class Slime : MonoBehaviour
{
    Animator anim, idle_anim;
    Rigidbody2D rb;
    Collider2D body_collider;

    GameObject locked;
    float decision_time = 3f, jump_force = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        idle_anim = transform.Find("SlimeBody").GetComponent<Animator>();
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D c in colliders)
        {
            if (!c.isTrigger) 
            {
                body_collider = c;
                break;
            }
        }
        rb = GetComponent<Rigidbody2D>();
        print("SLIME AWAKE");
    }

    // Update is called once per frame
    void Start()
    {
        print("SLIME START");
        Decide();
    }

    private void Update()
    {
        idle_anim.enabled = anim.GetCurrentAnimatorStateInfo(0).IsName("Slime_Idle");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            locked = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            locked = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
            anim.SetBool("Grounded", true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
            anim.SetBool("Grounded", false);
    }

    public void Hit(Damager damager, Damageable damageable)
    {
        anim.SetTrigger("Hit");
        Vector2 target = ((Vector2)(transform.position - damager.transform.position).normalized + (Vector2.up * 2f)) / 2f;
        rb.AddForce(target * Random.Range(jump_force - 5f, jump_force), ForceMode2D.Impulse);
        Decide();
    }

    public void Die(Damager damager, Damageable damageable)
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        body_collider.enabled = false;
        GetComponent<Damager>().enabled = false;
        StopAllCoroutines();
        anim.SetBool("Dead", true);
        Destroy(transform.parent.gameObject, 3f);
    }

    public void Jump()
    {
        if (!locked || !idle_anim.enabled)
            return;
        anim.SetBool("Grounded", false);
        anim.SetTrigger("Jump");
        Vector2 target = ((Vector2)(locked.transform.position - transform.position).normalized + (Vector2.up * 2f)) / 2f;
        rb.AddForce(target * Random.Range(jump_force-5f, jump_force), ForceMode2D.Impulse);
    }

    void Decide()
    {
        StopAllCoroutines();
        StartCoroutine(JumpLoop());
    }

    IEnumerator JumpLoop()
    {
        yield return new WaitForSeconds(Random.Range(0.5f,decision_time));
        Jump();
        Decide();
    }
}
