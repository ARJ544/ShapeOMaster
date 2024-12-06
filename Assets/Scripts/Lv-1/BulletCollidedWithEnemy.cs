using UnityEngine;

public class BulletCollidedWithEnemy : MonoBehaviour
{
    public GameObject bullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy.Instance.EnemyAnimator.SetTrigger("Damage");
            Destroy(bullet);
            Enemy.Instance.DamageCollectedByEnemy(1);
        }
    }
}
