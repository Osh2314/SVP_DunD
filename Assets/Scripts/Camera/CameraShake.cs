using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    
    public IEnumerator CameraVibration(float shakeTime, float shakeAmount, float shakeSpeed){
        Vector3 origPosition = transform.localPosition;

        while (shakeTime>0){
            Vector3 randomPoint = origPosition + Random.insideUnitSphere * shakeAmount;

            transform.localPosition = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10), new Vector3(randomPoint.x, randomPoint.y, -10), shakeSpeed * Time.deltaTime);

            yield return null;
	        
	        shakeTime-=Time.deltaTime;
	    }
	yield break;
    }
}
