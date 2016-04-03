using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    private LinkedList<GameObject> accountedObj;
    private LinkedList<GameObject> ObjectContainer;

    public int maxScale = 4;
    public float timeBetweenGeneration = 1f; // change it from unity editor
    public int maxObjectsAtScene = 20; 
    private int zStart = 0;
    private int lastZIndex = -9;
    private int GameScore;
    private float timeSinceLastGeneration;
    private Text txtRef;
    private Color LastColorPressed = Color.black; // change to smth more fitable
    private Color[] colors;
    private bool CollapseCheckNeeded = false; // invoke CheckForCollapse or not 

    // Use this for initialization
    void Start () {
        accountedObj = new LinkedList<GameObject>();
        ObjectContainer = new LinkedList<GameObject>();
        ObjectContainer = GameObject.Find("Controller").GetComponent<QuadGenerator>().getContainer;
        colors = GameObject.Find("PlatesController").GetComponent<PlatesWatcher>().getColors;

        txtRef = GameObject.Find("Timer").GetComponent<Text>();
        GameScore = 0;
        timeSinceLastGeneration = 0;
    }
	
	// Update is called once per frame
	void Update () { 
        if(CollapseCheckNeeded) checkForCollapse();
        timeSinceLastGeneration += Time.deltaTime;
        checkObjOut();
        if (timeSinceLastGeneration > timeBetweenGeneration)
        {
            generateNewObj();
        }
    }
    void generateNewObj()
    {
        timeSinceLastGeneration = 0;
        if (LastColorPressed != Color.black)
        {
            ObjectContainer.Last.Value.GetComponent<Renderer>().material.color = LastColorPressed;
            LastColorPressed = Color.black;
        }
        else
        {
            int i = Random.Range(0, colors.Length);
            ObjectContainer.Last.Value.GetComponent<Renderer>().material.color = colors[i];

        }
        foreach (GameObject Spreading in ObjectContainer)
        {
            Spreading.GetComponent<QuadSpread>().moveByZ();                                  //z++, if z < 10 z-- (for all) 
        }
        if (zStart > lastZIndex)
        {
            zStart--;
        }
        gameObject.GetComponent<QuadGenerator>().generate(zStart);
        CollapseCheckNeeded = true;
        CheckContainerForDestroy();
    } // generates new obj through  QuadGenerator.cs
    public void changeLastColor(Color c) // color of last pressed plate
    {
        LastColorPressed = c;
    }

    void CheckContainerForDestroy() // checks if container is out of bounds
    {
        if (ObjectContainer.Count > maxObjectsAtScene)
        {
            Destroy(ObjectContainer.First.Value);
            ObjectContainer.RemoveFirst();
        }
    }
    void checkObjOut() // checks if objects is out of scene
    {   
        if((ObjectContainer.First.Value.transform.localScale.x > maxScale) )
        {
            ObjectContainer.First.Value.GetComponent<Renderer>().material.color = Color.black;
        }
        if (ObjectContainer.First.Next != null)
        {
            if (ObjectContainer.First.Next.Value.transform.localScale.x > maxScale)
            {
                Destroy(ObjectContainer.First.Value);
                ObjectContainer.RemoveFirst();
            }
        }
    }
    void checkForCollapse()// is it really important to invoke this every frame?
    {
        CollapseCheckNeeded = false;

        GameObject currObject = ObjectContainer.Last.Value;
        LinkedList<GameObject> ListToCollapse = new LinkedList<GameObject>();
        while ((currObject != null ) && (currObject != ObjectContainer.First.Value))
        {
            int objectCollapsed = 1;
            bool collapse = false;
            ListToCollapse.AddLast(currObject);
            while (!collapse)
            {
                if (ObjectContainer.Find(currObject).Previous.Value != null)
                {
                    GameObject prev = ObjectContainer.Find(currObject).Previous.Value;
                    if (currObject.GetComponent<Transform>().rotation == prev.GetComponent<Transform>().rotation &&
                        currObject.GetComponent<Renderer>().material.color == prev.GetComponent<Renderer>().material.color)
                    {
                        ListToCollapse.AddLast(prev);
                        currObject = prev;
                        objectCollapsed++;
                    }
                    else
                    {
                        if(ListToCollapse.Count > 1)
                        CollapseObjects(ListToCollapse, objectCollapsed);
                        currObject = prev;
                        collapse = false;
                        ListToCollapse.Clear();
                    }
                }
            }
        }
    }
    void CollapseObjects(LinkedList<GameObject> collapse, int count)// removes collapseobjects from ObjectContainer and Scene
    {
        LinkedListNode<GameObject> curObject = ObjectContainer.Find(collapse.Last.Value);
        foreach (GameObject o in collapse)
        {
            ObjectContainer.Remove(o);
            Destroy(o);
        }
        do
        {
            curObject.Value.GetComponent<Transform>().position -= new Vector3(0, 0, count);
            curObject.Value.GetComponent<Transform>().localScale -= new Vector3(0.5f, 0.5f, 0) * count;
            curObject = curObject.Next;
        } while (curObject != null);
        
    }
    void changeScore(int sc)
    {
        GameScore = sc;
        txtRef.text = "Score: " + GameScore;
    }

    public bool CheckNeeded
    {
        set{
            CollapseCheckNeeded = value;
        }
    }
}
