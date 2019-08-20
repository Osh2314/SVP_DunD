using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackObject: MonoBehaviour
{
    public int damage = 5;
    [Header("보통 투사체라면 True로, 근접무기라면 false로 한다")]
    public bool canAttack = false;
    [Header("카메라 밖으로 나가면 오브젝트를 삭제할 것인가?")]
    public bool willInvisibleDestroy = false;
    [Header("적 타격에 성공하면 오브젝트를 삭제할 것인가?")]
    public bool willSuccessAttackDestroy = false;
    [Header("벽이나 땅, 플랫폼에 닿으면 삭제할 것인가?")]
    public bool willTouchedWallDestroy = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && canAttack==true)
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.Hp -= damage;
            if(willSuccessAttackDestroy==true)
                Destroy(gameObject);
        }
        if(collision.gameObject.tag=="Platform" && willTouchedWallDestroy == true) {
            Destroy(gameObject);
        }
    }
    void OnBecameInvisible()
    {
        if(willInvisibleDestroy==true)
        Destroy(gameObject);
    }
}
