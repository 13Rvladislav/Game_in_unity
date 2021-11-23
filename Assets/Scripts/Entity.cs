using UnityEngine;

public class Entity : MonoBehaviour
{
    public int lives;
    public virtual void GetDamage(int damage)
    {
        lives -= damage;
    }
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
