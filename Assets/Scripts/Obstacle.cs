using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int damage = 3;
        if (collision.gameObject==Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage(damage);
        }
    }
}
