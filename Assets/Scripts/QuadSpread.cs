using UnityEngine;
using System.Collections;

public class QuadSpread : MonoBehaviour
{

	
    public void Spread()
    {
        transform.position += new Vector3(0, 0, 1);
        StartCoroutine("ScaleAndTurn", 1.5f);
    }
 
    IEnumerator ScaleAndTurn(float time)
    {
        float elapsedTime = 0.0f;
        Vector3 startingScale = transform.localScale;
        Color nColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        Color sColor = GetComponent<Renderer>().material.color;
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 45));
        while (elapsedTime < time)
        {
            GetComponent<Renderer>().material.color = Color.Lerp(sColor, nColor, time / elapsedTime);
            transform.localScale = Vector3.Lerp(startingScale,startingScale + new Vector3(0.5f,0.5f,0),elapsedTime/time);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime/ time);
            elapsedTime += Time.deltaTime; 
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
