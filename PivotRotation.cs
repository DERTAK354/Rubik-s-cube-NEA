using UnityEngine;
using System.Collections.Generic;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Vector3 mouseRef;
    private bool dragging = false;
    private bool autoRotating = false; // new variable needed for snapping into place
    private float sensitivity = 0.4f;
    private float speed = 300f;
    private Vector3 rotation;

    private Quaternion targetQuaternion; //for target angle to snap into place

    private ReadCube readCube;
    private CubeState cubeState;
    public AudioSource audioSource; // sound component
    public AudioClip rotateSound; // sound effect, not the same thing

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
        audioSource = GetComponent<AudioSource>(); // get AudioSource component
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource missing");
        }
            SpinSide(activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                RotateToRightAngle();
            }
        }
        if (autoRotating)
        {
            AutoRotate();
        }
    }

    private void SpinSide(List<GameObject> side) //sides of cube are turned
    {
        rotation = Vector3.zero;
        Vector3 mouseOffset = (Input.mousePosition - mouseRef);

        if (side == cubeState.front)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.back)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if (side == cubeState.up)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.down)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        if (side == cubeState.left)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }
        if (side == cubeState.right)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;
        }
        transform.Rotate(rotation, Space.Self);
        mouseRef = Input.mousePosition;

        /*if (!audioSource.isPlaying && rotateSound != null)
        {
            audioSource.PlayOneShot(rotateSound);
        }*/
    }

    public void Rotate(List<GameObject> side)
    {
        Debug.Log("Rotate() called on side: " + side[4].name);
        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }

    public void RotateToRightAngle() //for snapping into place, rounded to nearest 90 degrees
    {
        Vector3 vec = transform.localEulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotating = true;

        // play sound only once when the cube snaps into place
        if (audioSource != null && rotateSound != null)
        {
            audioSource.PlayOneShot(rotateSound);
        }
    }

    private void AutoRotate()
    {
        dragging = false;
        var step = speed * Time.deltaTime; //snapping into place is not instant
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <=1 ) //if angle offset is 1 degree or less
        {
            transform.localRotation = targetQuaternion;
            readCube.ReadState();

            autoRotating = false;
            dragging = false;

            // play sound effect when snapping to final position
            if (rotateSound != null)
            {
                audioSource.PlayOneShot(rotateSound);
            }
        }
    }
}
