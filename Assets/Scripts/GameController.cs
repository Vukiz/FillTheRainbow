using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

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
        CheckNeeded = true;
        CheckContainerForDestroy();
    } // generates new obj through  QuadGenerator.cs
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
    void checkForCollapse()
    {

        Debug.Log("Checking for collapse");
        CollapseCheckNeeded = false;

        int objectCollapsed = 0;
        GameObject currObject = ObjectContainer.Last.Value;
        LinkedList<GameObject> ListToCollapse = new LinkedList<GameObject>();
        objectCollapsed = 1;
        LinkedListNode<GameObject> prev = ObjectContainer.Find(currObject).Previous;
        while (prev != null)
        {
            if (currObject.GetComponent<QuadSpread>().IsPerfRotate== prev.Value.GetComponent<QuadSpread>().IsPerfRotate &&
                currObject.GetComponent<Renderer>().material.color == prev.Value.GetComponent<Renderer>().material.color)
            {
                objectCollapsed++;
                ListToCollapse.AddLast(currObject);
                //Debug.Log("Objects Collapsed! " + ListToCollapse.Count);
            }
            else
            {
                if (ListToCollapse.Count > 1)
                {
                    CollapseObjects(ListToCollapse, objectCollapsed);
                }
                objectCollapsed = 1;
                ListToCollapse.Clear();
                //Debug.Log("ListCleared now size is " + ListToCollapse.Count);
            }
            currObject = prev.Value;
            ListToCollapse.AddLast(currObject);
            prev = ObjectContainer.Find(currObject).Previous;
        }
        
    }
    void CollapseObjects(LinkedList<GameObject> collapse, int count)// removes collapseobjects from ObjectContainer and Scene
    {
        LinkedListNode<GameObject> curObject = ObjectContainer.First;
        LinkedListNode<GameObject> lastOjb = ObjectContainer.Find(collapse.Last.Value);
        do
        {
            curObject.Value.GetComponent<Transform>().position -= new Vector3(0, 0, count);
            // curObject.Value.GetComponent<Transform>().localScale -= new Vector3(0.5f, 0.5f, 0) * count;
            StartCoroutine(changeScaleToNullAndBack(curObject.Value.GetComponent<QuadSpread>(), count));
            curObject = curObject.Next;
            Debug.Log("Scale lowered");
        } while (curObject != lastOjb);
        foreach (GameObject o in collapse)
        {
            ObjectContainer.Remove(o);
            Destroy(o);
        }
        //TODO COROUTINE CHECKFORCOLLAPSE AFTER 0,5S IN CASE OF MULTIPLE COLLAPSES ( ZUMA) 
    }   
    IEnumerator changeScaleToNullAndBack(QuadSpread qs,int timeWaiting)
    {
        float timeNulled = 0.0f;
        qs.changeScale = Vector3.zero;
        while (timeNulled < 1.0f * timeWaiting)
        {
            timeNulled += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        qs.changeScale = new Vector3(0.5f, 0.5f, 0);
    }
    void changeScore(int sc)
    {
        GameScore = sc;
        txtRef.text = "Score: " + GameScore;
    }
    public Color changeLastColor // color of last pressed plate
    {
        set
        {
            LastColorPressed = value;
        }
    }
    public bool CheckNeeded
    {
        set{
            CollapseCheckNeeded = value;
        }
    }
}
