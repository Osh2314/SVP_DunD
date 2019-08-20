using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_BigThornFloor : MonoBehaviour
{
    public GameObject Thron1;
    public GameObject Thron2;
    public GameObject Thron3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Thorn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Thorn()
    {
        while (true)
        {
            if (GameManager.Instance.gameState == GameManager.GameState.ROUNDPLAYING)
            {
                Thron1.gameObject.SetActive(false);
                Thron2.gameObject.SetActive(false);
                Thron3.gameObject.SetActive(false);
                yield return new WaitForSeconds(3f);
                Thron1.gameObject.SetActive(true);
                Thron2.gameObject.SetActive(true);
                Thron3.gameObject.SetActive(true);
                yield return new WaitForSeconds(3f);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
