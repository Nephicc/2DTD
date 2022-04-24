using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{

    [SerializeField]
    private float cameraSpeed = 0f;

    private float xMax;
    private float yMin;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0),-10);
    }

    public void SetLimit(Vector3 maxTile)
    {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - wp.x;
        Debug.Log("x: (" + maxTile.x + " - " + wp.x + ") = " + (maxTile.x - wp.x) + "\ty: (" + maxTile.y + " - " + wp.y + ") = " + (maxTile.y - wp.y));
        yMin = maxTile.y - wp.y;
    }


    public void MoveCamera(string s)
    {
        switch (s)
        {
            case "w":
                transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime / 2);
                break;
            case "a":
                transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime / 2);
                break;
            case "s":
                transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime / 2);
                break;
            case "d":
                transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime / 2);
                break;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0), -10);
    }

}
