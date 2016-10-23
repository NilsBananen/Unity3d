using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private float rotationSpeed = 450;
    private float speed = 5;
    private float jumpSpeed = 0;
    private float distToGround = 0;

    private Vector3 _velocity = new Vector3();

    private Quaternion targetRotation;

    private Rigidbody rgbd;
    private Camera cam;
    private Animator anim;

    public bool Grounded;

    private bool preJumping = false;

    // Use this for initialization
    void Start()
    {
        Physics.gravity = new Vector3(0,-100,0);

        rgbd = GetComponent<Rigidbody>();
        cam = Camera.main;
        anim = gameObject.GetComponent<Animator>();

        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMouse();
        //ControlWASD();
    }

    void FixedUpate()
    {
    }

    private void ControlMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, (cam.transform.position.y - transform.position.y)));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

        float xVelocity = 0, yVelocity = 0, zVelocity = 0;
        
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {            
            anim.SetBool("PreJump", true);
            anim.CrossFade("PreJumping", 0);
        }

        if (Input.GetKeyUp(KeyCode.Space) && IsGrounded)
        {
            rgbd.AddForce(Vector3.up * 3000f);
            anim.SetBool("PreJump", false);
            anim.CrossFade("Idle", 0);
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                zVelocity += 800f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                xVelocity -= 800f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                zVelocity -= 800f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                xVelocity += 800f;
            }

            if (xVelocity != 0 && zVelocity != 0)
            {
                xVelocity *= 0.7f;
                zVelocity *= 0.7f;
            }

            rgbd.velocity = new Vector3(xVelocity * Time.deltaTime, yVelocity, zVelocity * Time.deltaTime);
        }

        anim.SetFloat("Speed", Mathf.Abs(xVelocity) + Mathf.Abs(zVelocity));
        anim.SetBool("Grounded", IsGrounded);        
    }

    private void ControlWASD()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        }

        Vector3 motion = input;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        //motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        rgbd.AddForce(motion * Time.deltaTime);
    }

    private bool IsGrounded
    {
        get
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.01f);
        }
    }

}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  