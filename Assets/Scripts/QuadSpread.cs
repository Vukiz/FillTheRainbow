using UnityEngine;
using System.Collections;

public class QuadSpread : MonoBehaviour
{
    private GameController curController;
    bool objActive = true;
    private Vector3 scaleVector = new Vector3(0.5f, 0.5f, 0);
    void Update()
    {
        if (gameObject.GetComponent<Renderer>().material.color == Color.black) objActive = false;
        gameObject.transform.localScale += scaleVector * Time.deltaTime;
    }
    public void moveByZ()
    {
        transform.position += new Vector3(0, 0, 1);
    }
 
    void Start()
    {
        curController = GameObject.Find("Controller").GetComponent<GameController>();
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && objActive)
        {
            curController.CheckNeeded = true;
            StartCoroutine("Turn", 0.5f);
        }
    }
    IEnumerator Turn(float time)
    {
        float elapsedTime = 0.0f;
        
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 45));
        while (elapsedTime < time)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
