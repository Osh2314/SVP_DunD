using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Round
{
    public GameObject[] spownEnemyPrefabs;
};
public class EnemySpowner : MonoBehaviour
{
    [Header("적의 스폰 위치 오브젝트")]
    public GameObject spownPlace;
    [Header("적의 스폰시간 간격 ")]
    public float spownIntervalTime = 2.5f;
    
    
    [Header("1차원인덱스:원하는 라운드 2차원인덱스:원하는 적들")]
    public Round[] roundEnemyInfo;

    private void Awake()
    {
    }

    /// <summary>
    /// 인자로 받은 라운드값에 따라 적을 스폰하는 역할의 함수이다.
    /// </summary>
    /// <param name="wantRound"></param>
    /// <returns></returns>
    public IEnumerator SpownEnemyWithRound(int wantRound)
    {
        Debug.Log("EnemySpowner : 스폰 코루틴 시작!!");
        //스폰시간 간격을 두고 원하는 라운드의 Enemy프리팹을 하나씩 인스턴스화한다
        for (int i = 0; i < roundEnemyInfo[wantRound].spownEnemyPrefabs.Length; ++i)
        {
            
            Instantiate(roundEnemyInfo[wantRound].spownEnemyPrefabs[i], spownPlace.transform.position, Quaternion.identity);
            Debug.Log("스폰!!"+(i+1).ToString());
            yield return new WaitForSeconds(spownIntervalTime);
        }
        yield break;
    }
}
