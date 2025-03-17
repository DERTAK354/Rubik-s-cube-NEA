using UnityEngine;
using System.Collections.Generic;

public class CubeState : MonoBehaviour
{
    public List<GameObject> front = new List<GameObject>(); //new lists created to store state of cube
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(List<GameObject> cubeSide) // all pieces of a given side of the cube are made children of
    {                                             // the centre piece, except for centre piece itself
        //Debug.Log("PickUp() called on " + cubeSide[4].name);
        foreach (GameObject face in cubeSide)
        {
            if (face != cubeSide[4])
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
                //Debug.Log(face.name + " parented to " + cubeSide[4].transform.parent.name);
            }
        }
        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
    }
}
