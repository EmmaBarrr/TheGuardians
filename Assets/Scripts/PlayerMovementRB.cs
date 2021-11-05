using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//Rodolfo León Gasca A01653185

public class PlayerMovementRB : MonoBehaviour
{
    public GameObject lockcam;
    public CinemachineFreeLook cmfl;
    public bool isDead = false, onGround, jump;
    public float windforce, WindTime;
    float curWindTime;
    public int Hp;
    public float moveSpeed = 5, jumpForce = 10, turnSmoothTime = 0.1f, maxFOV = 60, minFOV = 30;
    float turnSmoothVelocity;

    Rigidbody rb;
    Camera m_cam;
    Vector3 mov;
    public Animator anim;
    Transform groundCheck;
    lockEnemy lenem;

    public Transform pivolt, ownpivot;
    public float rotationSpeed;
    public GameObject PlayerModel;

    float FOVcm;

    void Start()
    {
        FOVcm = 40;
        Hp = 3;
        groundCheck = gameObject.transform.Find("GroundCheck");
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
        m_cam = Camera.main;
        lenem = GetComponent<lockEnemy>();
    }
    private void Update()
    {
        ownpivot.transform.position = this.transform.position;
        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.mouseScrollDelta.y != 0)
        {
            FOVcm += Input.mouseScrollDelta.y;

        }

        FOVcm = Mathf.Clamp(FOVcm, minFOV, maxFOV);

        cmfl.m_Lens.FieldOfView = FOVcm;

        if (!isDead)
        {
            if (onGround && Input.GetKeyDown("space"))
            {
                jump = true;
            }
        }

        anim.SetBool("Grounded", onGround);
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            mov = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

            if (!onGround)
            {
                mov.y = 0;
            }
            
            if (onGround)
            {

                anim.SetFloat("Speed", mov.magnitude);
            }

            if (jump)
            {
                jump = false;
                rb.AddForce(Vector3.up * jumpForce);
                //PlaySong(jumpSound);
            }


            mov = mov.normalized;

           

            if (lenem.Islocked() == false)
            {
                lockcam.gameObject.SetActive(false);
                cmfl.gameObject.SetActive(true);

                anim.SetFloat("Speed", mov.magnitude);

                if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    transform.rotation = Quaternion.Euler(0, pivolt.rotation.eulerAngles.y, 0);
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(mov.x, 0, mov.z));
                    PlayerModel.transform.rotation = Quaternion.Slerp(PlayerModel.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
                  
                    anim.SetFloat("Speed 0", 0);
                }
                rb.velocity = new Vector3(mov.x * moveSpeed, rb.velocity.y, mov.z * moveSpeed);
            }
            else if (lenem.Islocked() == true)
            {
                //lockcam.gameObject.SetActive(true);
                //cmfl.gameObject.SetActive(false);

                lockcam.transform.LookAt(lenem.ReturnEnemy().transform);

                ownpivot.transform.LookAt(lenem.ReturnEnemy().transform);

                transform.rotation = Quaternion.Euler(0, ownpivot.transform.rotation.eulerAngles.y, 0);
                PlayerModel.transform.rotation = Quaternion.Euler(0, ownpivot.transform.rotation.eulerAngles.y, 0);

                anim.SetFloat("Speed", mov.magnitude);
                anim.SetFloat("Speed 0", Input.GetAxis("Horizontal"));
                rb.velocity = new Vector3(mov.x * moveSpeed * 2 / 3, rb.velocity.y, mov.z * moveSpeed*2/3);
                //Quaternion newRotation = Quaternion.LookRotation(new Vector3(mov.x, 0, mov.z));
                //PlayerModel.transform.rotation = Quaternion.Slerp(PlayerModel.transform.rotation, newRotation,1000);
            }
        }
    }
}