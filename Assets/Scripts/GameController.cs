using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    private LinkedList<GameObject> accounteObj;
    private LinkedList<GameObject> ObjectContainer;
    private GameObject lastObjSeen;

    public float timeBetweenGeneration = 0.6f;
    public int maxObjectsAtScene = 20; 
    private int zStart = 0;
    private int lastZIndex = -9;
    private int GameScore;
    private float timeSinceLastGeneration;
    private Text txtRef;

    
    // Use this for initialization
    void Start () {
        accounteObj = new LinkedList<GameObject>();
        ObjectContainer = new LinkedList<GameObject>();
        ObjectContainer = GameObject.Find("Controller").GetComponent<QuadGenerator>().getContainer;
        Debug.Log(ObjectContainer);
        txtRef = GameObject.Find("Timer").GetComponent<Text>();
        GameScore = 0;
        timeSinceLastGeneration = 0;
        lastObjSeen = null;
    }
	
	// Update is called once per frame
	void Update () {
        timeSinceLastGeneration += Time.deltaTime;
        //checkObjOut();
    }
    public void Spread(Color c)
    {
        if (timeSinceLastGeneration > timeBetweenGeneration)
        {
            timeSinceLastGeneration = 0;
            ObjectContainer.Last.Value.GetComponent<Renderer>().material.color = c;
            foreach (GameObject Spreading in ObjectContainer)
            {
                Spreading.GetComponent<QuadSpread>().Spread();
            }
            if (zStart > lastZIndex) zStart--;
            gameObject.GetComponent<QuadGenerator>().generate(zStart);
           
            CheckContainerForDestroy();
        }
    }
    void CheckContainerForDestroy()
    {
        if (ObjectContainer.Count > maxObjectsAtScene)
        {
            Destroy(ObjectContainer.First.Value);
            ObjectContainer.RemoveFirst();
        }
    }
    void checkObjOut()
    {   
        if((ObjectContainer.First.Value.transform.localScale.x < 10) )
        {
            lastObjSeen = null;
        }
        else
        {
            lastObjSeen = ObjectContainer.First.Value;
          
            lastObjSeen.GetComponent<Renderer>().material.color = Color.black;
        }
    }
    void changeScore(int sc)
    {
        GameScore = sc;
        txtRef.text = "Score: " + GameScore;
    }
}
