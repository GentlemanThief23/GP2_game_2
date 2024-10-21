using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camControl : MonoBehaviour
{

    Vector3 myLook;
    float lookspeed = 1000f;
    public Camera myCam;
    public float camLock = 90f;

    float onStartTimer;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        myLook = transform.localEulerAngles;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onStartTimer += Time.deltaTime;
        myLook += DeltaLook() * lookspeed * Time.deltaTime;
        myLook.y = Mathf.Clamp(myLook.y, -camLock, camLock);
        transform.rotation = Quaternion.Euler(0f, myLook.x, 0f);
        myCam.transform.rotation = Quaternion.Euler(-myLook.y, myLook.x, 0f);
    }

    //here we are going to calculate the difference in mouse position (on screen) relative to the previous frame
    Vector3 DeltaLook()
    {
        Vector3 dlook;
        float rotY = Input.GetAxisRaw("Mouse Y");
        float rotX = Input.GetAxisRaw("Mouse X");
        dlook = new Vector3(rotX, rotY, 0);

        if(dlook != Vector3.zero) { Debug.Log("delta look:" + dlook); }

        if(onStartTimer < 1f)
        {
            dlook = Vector3.ClampMagnitude(dlook, onStartTimer);
        }

        return dlook;

    }
}
