using UnityEngine;

public class Worms : Entity
{
    [SerializeField] private int lives = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage(1);
            lives--;
            Debug.Log("У червя " + lives);
            
        }
        if (lives < 1)
            Die();
    }
}
