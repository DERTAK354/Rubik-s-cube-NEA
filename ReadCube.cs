using UnityEngine;
using System.Collections.Generic;

public class ReadCube : MonoBehaviour
{
    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;

    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();

    private int layerMask = 1 << 6; // for faces only
    CubeState cubeState;
    CubeMap cubeMap;
    public GameObject emptyGO;

    void Start()
    {
        SetRayTransforms();

        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        cubeState.front.Clear(); // Clear old hits
        cubeState.up.Clear();
        cubeState.left.Clear();
        cubeState.right.Clear();
        cubeState.down.Clear();
        cubeState.back.Clear();


        if (cubeState == null)
        {
            Debug.LogError("CubeState script not found! Make sure it is attached to a GameObject in the scene.");
        }
    }

    void Update()
    {

    }

    public void ReadState()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        cubeState.up = ReadFace(upRays, tUp);
        cubeState.down = ReadFace(downRays, tDown);
        cubeState.left = ReadFace(leftRays, tLeft);
        cubeState.right = ReadFace(rightRays, tRight);
        cubeState.front = ReadFace(frontRays, tFront);
        cubeState.back = ReadFace(backRays, tBack);

        if (cubeMap == null)
        {
        Debug.LogError("CubeMap not found! Make sure it is attached to a GameObject in the scene.");
        return;
        }
        //DebugCubeState();

        cubeMap.Set();
    }

    /*void DebugCubeState()
    {
        Debug.Log("Cube State after ReadState():");
        Debug.Log("Front: " + string.Join(", ", cubeState.front.ConvertAll(c => c.name)));
        Debug.Log("Back: " + string.Join(", ", cubeState.back.ConvertAll(c => c.name)));
        Debug.Log("Left: " + string.Join(", ", cubeState.left.ConvertAll(c => c.name)));
        Debug.Log("Right: " + string.Join(", ", cubeState.right.ConvertAll(c => c.name)));
        Debug.Log("Up: " + string.Join(", ", cubeState.up.ConvertAll(c => c.name)));
        Debug.Log("Down: " + string.Join(", ", cubeState.down.ConvertAll(c => c.name)));
    }   */

    /*void DebugCubeState()
    {
        Debug.Log("Cube State after ReadState():");
        Debug.Log("Front: " + string.Join(", ", cubeState.front.ConvertAll(c => c.name)));
        Debug.Log("Back: " + string.Join(", ", cubeState.back.ConvertAll(c => c.name)));
        Debug.Log("Left: " + string.Join(", ", cubeState.left.ConvertAll(c => c.name)));
        Debug.Log("Right: " + string.Join(", ", cubeState.right.ConvertAll(c => c.name)));
        Debug.Log("Up: " + string.Join(", ", cubeState.up.ConvertAll(c => c.name)));
        Debug.Log("Down: " + string.Join(", ", cubeState.down.ConvertAll(c => c.name)));
    }  */ 
   

    void SetRayTransforms()
    {
        upRays = BuildRays(tUp, new Vector3(90, 0, 0)); // rays are built, so they point at cube
        downRays = BuildRays(tDown, new Vector3(270, 90, 0)); //
        leftRays = BuildRays(tLeft, new Vector3(0, -90, -90)); //changing z temp
        rightRays = BuildRays(tRight, new Vector3(0, 90, -90));//changing y temp
        frontRays = BuildRays(tFront, new Vector3(0, 180, -90));
        backRays = BuildRays(tBack, new Vector3(0, 0, -90)); //
        
    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction) //rays are built, ROW BY ROW, not column by column 
    {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        for (int x = -1; x < 2; x++) //iterating through rows
        {
            for (int y = -1; y < 2; y++) //iterating through columns
            {
                Vector3 startPos = new Vector3( rayTransform.localPosition.x + x, //offset by one to districute rays evenly...
                                                rayTransform.localPosition.y + y,//...same here
                                                rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform); //emptygo is origin of raycasts
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform) //rays shot out at each piece for each face
    {
        List<GameObject> facesHit = new List<GameObject>();

        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position; //changed from Front 
            RaycastHit hit;

            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                //Debug.Log("Hit: " + hit.collider.gameObject.name);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green); // changed from front
                //Debug.Log("No hit detected.");
            }

            //Debug.Log("facesHit count before assignment: " + facesHit.Count); // Debug

            if (rayTransform == tUp) cubeState.up.AddRange(facesHit); //lists storing state of cube are updated accordingly
            else if (rayTransform == tDown) cubeState.down.AddRange(facesHit);
            else if (rayTransform == tRight) cubeState.right.AddRange(facesHit); //left and right swapped
            else if (rayTransform == tLeft) cubeState.left.AddRange(facesHit); // 
            else if (rayTransform == tFront) cubeState.front.AddRange(facesHit);
            else if (rayTransform == tBack) cubeState.back.AddRange(facesHit);

            //Debug.Log("cubeState.front count after assignment: " + cubeState.front.Count); // Debug
        }
        

        return facesHit;
    }

    
}
