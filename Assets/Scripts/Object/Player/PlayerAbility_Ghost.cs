using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility_Ghost : MonoBehaviour
{
    public GameObject sword;
    public GameObject stonePrefab;
    [Header("발사체를 스폰할 곳의 X좌표 - PlayerX좌표")]
    public float stonePrefab_SpownX;
    [Header("발사체를 스폰할 곳의 Y좌표 - PlayerY좌표")]
    public float stonePrefab_SpownY;

    public float attackCoolTime = 0.1f;

    private bool canAttack=true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;
        Vector3 mouseConvertedpoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        Vector2 lookDir = mouseConvertedpoint - sword.transform.position;
 
        if (Input.GetKeyDown(KeyCode.Mouse0)&&canAttack==true)
        {
            StartCoroutine(AttackCoolTimeTimerStart());

            Vector2 targetFollowDir;
            GameObject createdObject;
            Rigidbody2D createdRigid;

           // Debug.Log(mousePos + " || " + point);
            if (transform.position.x - mouseConvertedpoint.x >= 0)
                createdObject = Instantiate(stonePrefab, new Vector3(transform.position.x - stonePrefab_SpownX, transform.position.y + stonePrefab_SpownY, 0), Quaternion.FromToRotation(Vector3.right, lookDir));
            else
                createdObject = Instantiate(stonePrefab, new Vector3(transform.position.x + stonePrefab_SpownX, transform.position.y + stonePrefab_SpownY, 0), Quaternion.FromToRotation(Vector3.right, lookDir));

            createdRigid = createdObject.GetComponent<Rigidbody2D>();
            targetFollowDir = mouseConvertedpoint - createdObject.transform.position;
            targetFollowDir.Normalize();

            createdRigid.AddForce(targetFollowDir * 1000);
        }

    }

    /// <summary>
    /// canAttack변수를 false상태로 만든 후, 쿨타임을 적용한다.
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackCoolTimeTimerStart() {
        float duration = attackCoolTime;
        canAttack = false;
        while (duration > 0) {
            duration -= Time.deltaTime;
            yield return null;
        }
        canAttack = true;
        yield break;
    }
}