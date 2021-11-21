using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10.0F;
    public int damage;
    public LayerMask whatIsSolid;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 1.4F);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, whatIsSolid);
        if(hitInfo.collider != null)
        {
            if(hitInfo.collider.CompareTag("Entity"))
            {
                hitInfo.collider.GetComponent<Entity>().GetDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
