using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Movement : MonoBehaviour
{

    Rigidbody rb;
    MvInputKey Key;
    float speed;
    Vector3 oldPosition;



    [SerializeField] float mainThrust = 400000f; //1000
    [SerializeField] float backgroundThrust = 10000;
    [SerializeField] float rotationThrust = 100f;


    enum MvInputKey {
        Key_Neutral = 0,
        Key_Left = 1,
        Key_Right = 2,
    };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {

        Key = MvInputKey.Key_Neutral;

        if (Input.GetKey(KeyCode.A)) {
            Key = Key | MvInputKey.Key_Left;

        }
    
        if (Input.GetKey(KeyCode.D)) {
            Key = Key | MvInputKey.Key_Right;

        }



    }


    // Update is called once per frame
    void FixedUpdate()
    {


        ProcessRotation();
        //rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        speed = Vector3.Distance(oldPosition, transform.position) * 100f;
        oldPosition = transform.position;
        Debug.Log("Speed: " + speed.ToString("F2"));



    }



    void ProcessRotation()
    {
        //textLabel.text = "";

        if ((Key & MvInputKey.Key_Left)!=0) {
            RotatePlayer(rotationThrust);
        }
        if ((Key & MvInputKey.Key_Right)!=0) {
            RotatePlayer(-rotationThrust);
        }
        if (Key != MvInputKey.Key_Neutral) {

            if ((Key & MvInputKey.Key_Left)!=0 && (Key & MvInputKey.Key_Right)!=0) {
                rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            }

            else {
                rb.AddRelativeForce(Vector3.up * (mainThrust / 2) * Time.deltaTime);

            }

        }
    }

    private void RotatePlayer(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}
