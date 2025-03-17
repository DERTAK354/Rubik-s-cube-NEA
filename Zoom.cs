using UnityEngine;

public class Zoom : MonoBehaviour
{
    public Camera cam;
    public float zoomSpeed = 5f; // magnitude of magnification per click of button
    public float minFov = 20f;
    public float maxFov = 60f; //establishing in and max magnification

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomIn()
    {
        cam.fieldOfView -= zoomSpeed; //fov decreases 
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFov, maxFov); //keep within boundaries
    }

    public void ZoomOut()
    {
        cam.fieldOfView += zoomSpeed;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFov, maxFov);
    }
}
