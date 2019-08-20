using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Ball : MonoBehaviour
{
    public GameObject Ball;

    public float ballspeed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShotBall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShotBall()
    {
        while (true)
        {
            if (GameManager.Instance.gameState == GameManager.GameState.ROUNDPLAYING)
            {
                GameObject createdObject;
                Rigidbody2D createdRigid;
                createdObject = Instantiate(Ball, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                createdRigid = createdObject.GetComponent<Rigidbody2D>();
                createdRigid.AddForce(new Vector2(0, ballspeed * -1));
                yield return new WaitForSeconds(3f);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
