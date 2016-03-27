using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject SpreadObj;
	private LinkedList<GameObject> ObjectContainer;
    private int zStart = 0;
    private int lastZIndex = -9;
    private Text txtRef;
    private float gameTime;
    // Use this for initialization
	void Start () {
        ObjectContainer = new LinkedList<GameObject>();
        ObjectContainer.AddLast(GameObject.Instantiate(SpreadObj));
        txtRef = GameObject.Find("Timer").GetComponent<Text>();
        gameTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        gameTime += Time.deltaTime;
        txtRef.text = "Time: " + gameTime;
    }
    public void Spread()
    {
        foreach (GameObject Spreading in ObjectContainer)
        {
            Spreading.GetComponent<QuadSpread>().Spread();
        }
        if (zStart > lastZIndex) zStart--;
        ObjectContainer.AddLast(GameObject.Instantiate(SpreadObj));
        ObjectContainer.Last.Value.GetComponent<Transform>().position = new Vector3(0, 0, zStart);

        CheckContainerForDestroy();
    }
    void CheckContainerForDestroy()
    {
        if (ObjectContainer.Count > 10)
        {
            Destroy(ObjectContainer.First.Value);
            ObjectContainer.RemoveFirst();
        }
    }
}
