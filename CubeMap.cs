using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class CubeMap : MonoBehaviour
{
    CubeState cubeState;

    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set()
    {
        cubeState = FindObjectOfType<CubeState>();

        /*Debug.Log("Front: " + cubeState.front.Count);
        Debug.Log("Back: " + cubeState.back.Count);
        Debug.Log("Left: " + cubeState.left.Count);
        Debug.Log("Right: " + cubeState.right.Count);
        Debug.Log("Up: " + cubeState.up.Count);
        Debug.Log("Down: " + cubeState.down.Count);*/

        //cubeState.front = RotateFace(cubeState.front, -90);
        //cubeState.back = RotateFace(cubeState.back, -90);
        //cubeState.left = RotateFace(cubeState.left, -90);
        //cubeState.right = RotateFace(cubeState.right, -90);

        UpdateMap(cubeState.front, front); //cube map is updaetd
        UpdateMap(cubeState.back, back);
        UpdateMap(cubeState.up, up);
        UpdateMap(cubeState.down, down);
        UpdateMap(cubeState.left, left); //left and right have been swapped, 
        UpdateMap(cubeState.right, right); //to fix colour order of cube, this has been swapped back now
    }

    void UpdateMap(List<GameObject> face, Transform side)
    {
        List<Transform> sortedPanels = new List<Transform>(side.GetComponentsInChildren<Transform>());
        sortedPanels.Remove(side); // remove parent 
        sortedPanels.Sort((a, b) => string.Compare(a.name, b.name)); // sort alphabetically

        /*Debug.Log($"Updating {side.name} face with sorted panel order:");
        for (int i = 0; i < sortedPanels.Count; i++)
        {
            Debug.Log($"Panel {i}: {sortedPanels[i].name}");
        }*/

        int[] correctOrder = { 2, 5, 8, 1, 4, 7, 0, 3, 6 }; // rotate 90 degrees 

        for (int i = 0; i < sortedPanels.Count; i++)
        {
            char faceLetter = face[i].name[0];
            Color newColor = Color.white;

            if (faceLetter == 'F') 
                ColorUtility.TryParseHtmlString("#009B48", out newColor); //cube panels are assigned a colour
            else if (faceLetter == 'B') 
                ColorUtility.TryParseHtmlString("#0046AD", out newColor);
            else if (faceLetter == 'U') 
                ColorUtility.TryParseHtmlString("#FFD500", out newColor);
            else if (faceLetter == 'D') 
                ColorUtility.TryParseHtmlString("#FFFFFF", out newColor);
            else if (faceLetter == 'R') 
                ColorUtility.TryParseHtmlString("#B71234", out newColor);
            else if (faceLetter == 'L') 
                ColorUtility.TryParseHtmlString("#FF5800", out newColor);

            if (side == up) // fix up face
            {
                sortedPanels[correctOrder[i]].GetComponent<Image>().color = newColor;
            }
            else
            {
                sortedPanels[i].GetComponent<Image>().color = newColor;
            }
        }
    }


    /*List<GameObject> RotateFace(List<GameObject> face, int degrees)
    {
        List<GameObject> rotatedFace = new List<GameObject>(face);
        if (degrees == -90)
        {
            rotatedFace[0] = face[2];
            rotatedFace[1] = face[5];
            rotatedFace[2] = face[8];
            rotatedFace[3] = face[1];
            rotatedFace[4] = face[4];
            rotatedFace[5] = face[7];
            rotatedFace[6] = face[0];
            rotatedFace[7] = face[3];
            rotatedFace[8] = face[6];
        }
        return rotatedFace;
    }*/
}
