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
        else if (collision.gameObject.tag == "Enemy2")
        {
            Enemy2.Instance.EnemyAnimator2.SetTrigger("Damage");
            Destroy(bullet);
            Enemy2.Instance.DamageCollectedByEnemy2(1);
        }
        else if (collision.gameObject.tag == "Enemy3")
        {
            Enemy3.Instance.EnemyAnimator3.SetTrigger("Damage");
            Destroy(bullet);
            Enemy3.Instance.DamageCollectedByEnemy3(1);
        }
        else if (collision.gameObject.tag == "Enemy4")
        {
            Enemy4.Instance.EnemyAnimator4.SetTrigger("Damage");
            Destroy(bullet);
            Enemy4.Instance.DamageCollectedByEnemy4(1);
        }
        else if (collision.gameObject.tag == "Enemy5")
        {
            Enemy5.Instance.EnemyAnimator5.SetTrigger("Damage");
            Destroy(bullet);
            Enemy5.Instance.DamageCollectedByEnemy5(1);
        }
    }
}
