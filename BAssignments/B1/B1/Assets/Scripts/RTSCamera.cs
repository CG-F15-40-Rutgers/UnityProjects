using UnityEngine;
using System.Collections;

public class RTSCamera : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x;
        float y;
        int scrollDistance = 100;
        float cameraSpeed = 70;


        if (Input.GetKey(KeyCode.C))
        {
            x = Input.GetAxis("Mouse X");
            
            transform.Rotate(Vector3.up, x * cameraSpeed * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.X))
        {
			Vector3 move = new Vector3(Input.GetAxis("Horizontal"),200*Input.GetAxis("Mouse ScrollWheel"),Input.GetAxis("Vertical"));
			transform.position += move * 5 * Time.deltaTime;
            x = Input.mousePosition.x;
            y = Input.mousePosition.y;

           /* if (x < scrollDistance)
            {
                transform.Translate(-1, 0, 1);
            }
            if (x >= Screen.width - scrollDistance)
            {
                transform.Translate(1, 0, -1);
            }

            if (y < scrollDistance)
            {
                transform.Translate(-1, 0, -1);
            }

            if (y >= Screen.height - scrollDistance)
            {
                transform.Translate(1, 0, 1);
            }*/
        }
        
     

        Camera Eye = GetComponentInChildren<Camera>();
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Eye.orthographicSize > 4)
        {
            Eye.orthographicSize -= 4;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Eye.orthographicSize > 4)
        {
            Eye.orthographicSize += 4;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Eye.orthographicSize = 50;
        }
    }
}
