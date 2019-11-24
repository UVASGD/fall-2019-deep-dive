using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

public class Boulder : MonoBehaviour
{
    float impact, impact_threshold = 3f, damage = 2, rockforce = 200f, waypoint_checkpoint = 5f;
    Rigidbody2D rb;

    bool triggered;

    public GameObject rumble_fx;
    public GameObject impact_fx;

    Damager damager;

    Animator anim;

    List<Transform> waypoints;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        damager = GetComponent<Damager>();
        Transform wp = transform.Find("Waypoints");
        wp.parent = transform.parent;
        waypoints = new List<Transform>(wp.GetComponentsInChildren<Transform>());
        waypoints.Remove(wp);
    }

    // Update is called once per frame
    void Update()
    {
        impact = rb.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        if (waypoints.Count == 0)
            return;
        if (Vector2.Distance(transform.position, waypoints[0].position) < waypoint_checkpoint)
        {
            Transform t = waypoints[0];
            waypoints.RemoveAt(0);
            if (t) Destroy(t.gameObject);
        }
        else if (Mathf.Abs(rb.angularVelocity) < rockforce)
        {
            rb.AddTorque((transform.position.x - waypoints[0].position.x) * rockforce);
        }
    }

    public void Damage(Damager damager, Damageable damageable)
    {
        damageable.TakeDamage(damager);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Impulse: " + collision.contacts[0].normalImpulse);
        print("Impact: " + impact);
        if (impact > impact_threshold && collision.contacts[0].normalImpulse > impact_threshold)
        {
            if (impact_fx) Instantiate(impact_fx, collision.contacts[0].point, Quaternion.identity).transform.up = collision.contacts[0].normal;
            if (collision.collider.CompareTag("Player"))
            {
                print("Hit that stinky player");
                Damage(damager, collision.collider.GetComponent<Damageable>());
            }
        }
    }

    public void Trigger()
    {
        if (!triggered)
        {
            print("RUMBLE");
            triggered = true;
            anim.SetTrigger("Rumble");
        }
    }

    public void Rumble()
    {
        if (rumble_fx) Instantiate(rumble_fx, transform.position, Quaternion.identity).transform.up = transform.up;
    }

    public void Dislodge()
    {
        anim.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
