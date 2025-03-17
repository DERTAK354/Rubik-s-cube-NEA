using UnityEngine;
using System.Collections.Generic;

public class RotateMainCube : MonoBehaviour
{
    Vector2 first_press_pos;
    Vector2 second_press_pos;
    Vector3 current_swipe;

    public GameObject target;

    float speed = 200f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        HandleKeyboardInput();
        //automatically move to target position
        if (transform.rotation != target.transform.rotation)
        {
            var step = speed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
        }
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // get 2D position of first mouse click
            first_press_pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //print(first_press_pos);
        }
        if (Input.GetMouseButtonUp(1))
        {
            // get 2D position of second mouse click
            second_press_pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            // create vector from first to second positions
            current_swipe = new Vector2(second_press_pos.x - first_press_pos.x, second_press_pos.y - first_press_pos.y);
            // normalise 2D vector
            current_swipe.Normalize();

            if (LeftSwipe(current_swipe)) //carry out rotation itself
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(current_swipe))
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (UpLeftSwipe(current_swipe))
            {
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (UpRightSwipe(current_swipe))
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }
            else if (DownLeftSwipe(current_swipe))
            {
                target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (DownRightSwipe(current_swipe))
            {
                target.transform.Rotate(0, 0, 90, Space.World);
            }
        }
    }

    void HandleKeyboardInput() //rotate using WASD and arroe keys, UpRight and DownRight ignored
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            target.transform.Rotate(0, 90, 0, Space.World);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            target.transform.Rotate(0, -90, 0, Space.World);
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            target.transform.Rotate(-90, 0, 0, Space.World);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            target.transform.Rotate(90, 0, 0, Space.World);
    }

    bool LeftSwipe(Vector2 swipe) //detemine type of swipe by using mouse movement of user
    {
        return current_swipe.x < 0 && current_swipe.y > -0.5f && current_swipe.y < 0.5f;
    }

    bool RightSwipe(Vector2 swipe)
    {
        return current_swipe.x > 0 && current_swipe.y > -0.5f && current_swipe.y < 0.5f;
    }

    bool UpLeftSwipe(Vector2 swipe)
    {
        return current_swipe.y > 0 && current_swipe.x < 0f;
    }

    bool UpRightSwipe(Vector2 swipe)
    {
        return current_swipe.y > 0 && current_swipe.x > 0f;
    }

    bool DownLeftSwipe(Vector2 swipe)
    {
        return current_swipe.y < 0 && current_swipe.x < 0f;
    }

    bool DownRightSwipe(Vector2 swipe)
    {
        return current_swipe.y < 0 && current_swipe.x > 0f;
    }
}
