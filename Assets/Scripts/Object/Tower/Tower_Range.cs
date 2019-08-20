using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Range : MonoBehaviour
{
    GameObject Tower;

    Tower tower;
    // Start is called before the first frame update
    void Start()
    {
        tower = GetComponentInParent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 colPos = collision.gameObject.transform.position;
            tower.GetEnemyPosition(colPos);
            tower.StartCoroutine(tower.State_Attack());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            tower.StartCoroutine(tower.State_Idle());
        }
    }
}
