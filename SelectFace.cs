using UnityEngine;
using System.Collections.Generic;

public class SelectFace : MonoBehaviour
{
    CubeState cubeState;
    ReadCube readCube;
    int layerMask = 1 << 6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            readCube.ReadState();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //shoot out ray from mouse input
            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };

                foreach (List<GameObject> cubeSide in cubeSides)
                {
                    if (cubeSide.Contains(face))
                    {
                        cubeState.PickUp(cubeSide); //pieces of selected face are picked up
                    }
                }
            }
        }
    }
}
