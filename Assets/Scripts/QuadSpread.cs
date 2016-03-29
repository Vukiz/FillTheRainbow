using UnityEngine;
using System.Collections;

public class QuadSpread : MonoBehaviour
{
    bool perfectRotation = false;

    private Vector3 scaleVector = new Vector3(0.5f, 0.5f, 0);
    void Update()
    {
        gameObject.transform.localScale += scaleVector * Time.deltaTime;
    }
    public void Spread()
    {
        transform.position += new Vector3(0, 0, 1);
        //StartCoroutine("Scale", 0.5f);
        // StartCoroutine("Turn", 0.5f);
    }
 
    IEnumerator Turn(float time)
    {
        float elapsedTime = 0.0f;
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 45));
        while (elapsedTime < time)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime/ time);
            elapsedTime += Time.deltaTime; 
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
    IEnumerator Scale(float time)
    {
        float elapsedTime = 0.0f;
        Vector3 startingScale = transform.localScale;
        while (elapsedTime < time)
        {
            transform.localScale = Vector3.Lerp(startingScale, startingScale + new Vector3(0.5f, 0.5f, 0), elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Turn", 0.5f);
        }
    }
}
