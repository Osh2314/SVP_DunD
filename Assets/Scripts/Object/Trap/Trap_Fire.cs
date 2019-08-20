using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Fire : MonoBehaviour
{
    public GameObject Fire;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Burn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Burn()
    {
        while(true)
        {
            if(GameManager.Instance.gameState == GameManager.GameState.ROUNDPLAYING)
            {
                Fire.SetActive(false);
                yield return new WaitForSeconds(4f);
                Fire.SetActive(true);
                yield return new WaitForSeconds(2f);

            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
