using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer_Arrow : MonoBehaviour
{
    public float speed;

    private Vector3 lookDir;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
