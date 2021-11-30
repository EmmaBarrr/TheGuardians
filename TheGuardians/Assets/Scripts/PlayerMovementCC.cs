using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//Rodolfo León Gasca A01653185

public class PlayerMovementCC : MonoBehaviour
{
    public PlayerMovementCC inst;
    public LightingManager LM;
    bool isChanging = false;
    public bool isMagic = false;
    public CinemachineFreeLook cmfl;
    public GameObject cam, nelli, eandi;
    public bool isDead = false, jump, D;
    public float windforce, WindTime;
    float curWindTime;
    public int Hp;
    public float gravity = 14, verticalVelocity, moveSpeed = 5, jumpForce = 10, turnSmoothTime = 0.1f, maxFOV = 60, minFOV = 30;
    float turnSmoothVelocity, curSpeed;

    CharacterController controller;
    Camera m_cam;
    Vector3 mov;
    public Animator anim,anim2;
    lockEnemy lenem;

    public Transform pivolt, ownpivot;
    public float rotationSpeed;
    public GameObject PlayerModel;

    float FOVcm;

    bool canchange = true;

    void Start()
    {
        inst = this;
        curSpeed = moveSpeed;
        mov = Vector3.zero;
        FOVcm = 40;
        Hp = 3;
        controller = GetComponent<CharacterController>();
        m_cam = Camera.main;
        lenem = GetComponent<lockEnemy>();
    }
    private void Update()
    {
        eandi.SetActive(isMagic);
        nelli.SetActive(!isMagic);
        if (!D) { 
        anim.SetBool("Grounded", controller.isGrounded);
            anim2.SetBool("Grounded", controller.isGrounded);
            ownpivot.transform.position = this.transform.position;

        //Scroll Wheel Zoom
        /*
        if (Input.mouseScrollDelta.y != 0)
        {
            FOVcm += Input.mouseScrollDelta.y;

        }

        FOVcm = Mathf.Clamp(FOVcm, minFOV, maxFOV);
        */

        cmfl.m_Lens.FieldOfView = FOVcm;

            if (!isDead)
            {
                if (controller.isGrounded)
                {
                    verticalVelocity -= Time.deltaTime;
                    verticalVelocity = Mathf.Clamp(verticalVelocity, -1, 1000);
                    if (Input.GetKeyDown("space"))
                    {
                        verticalVelocity = jumpForce;
                        anim.SetBool("Grounded", controller.isGrounded);
                        anim2.SetBool("Grounded", controller.isGrounded);
                    }
                    if (Input.GetKeyDown("c"))
                    {
                        Debug.Log("Roll");
                        //anim.SetTrigger("Roll");
                        RollSpeed(3);

                    }
                    if (Input.GetKeyDown("q") && canchange)
                    {
                        canchange = false;
                        Debug.Log("Roll");
                        //anim.SetTrigger("Roll");
                        ChangeDimension();

                    }
                }
                else
                {
                    verticalVelocity -= gravity * Time.deltaTime;
                }

                mov = Vector3.zero;

                mov = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
                mov = mov.normalized;

                mov.y = verticalVelocity;

                cmfl.m_RecenterToTargetHeading.m_enabled = lenem.lo;

                if (lenem.lo == false)
                {
                    FOVcm = maxFOV;
                    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                    {
                        transform.rotation = Quaternion.Euler(0, pivolt.rotation.eulerAngles.y, 0);
                        Quaternion newRotation = Quaternion.LookRotation(new Vector3(mov.x, 0, mov.z));
                        PlayerModel.transform.rotation = Quaternion.Slerp(PlayerModel.transform.rotation, newRotation, rotationSpeed * Time.deltaTime);

                        anim.SetFloat("Speed 0", 0);
                        anim2.SetFloat("Speed 0", 0);
                    }
                    mov.x *= curSpeed;
                    mov.z *= curSpeed;
                    controller.Move(mov * Time.deltaTime);
                    anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
                    anim2.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")));
                }
                else if (lenem.lo)
                {
                    FOVcm = minFOV;
                    ownpivot.transform.LookAt(lenem.ReturnEnemy().transform);

                    transform.rotation = Quaternion.Euler(0, ownpivot.transform.rotation.eulerAngles.y, 0);
                    PlayerModel.transform.rotation = Quaternion.Euler(0, ownpivot.transform.rotation.eulerAngles.y, 0);

                    anim.SetFloat("Speed", Input.GetAxis("Vertical"));
                    anim.SetFloat("Speed 0", Input.GetAxis("Horizontal"));

                    anim2.SetFloat("Speed", Input.GetAxis("Vertical"));
                    anim2.SetFloat("Speed 0", Input.GetAxis("Horizontal"));

                    mov.x *= curSpeed * 0.7f;
                    mov.z *= curSpeed;
                    controller.Move(mov * Time.deltaTime);

                }
            }
        }

    }


    public void ChangeDimension()
    {
        isMagic = !isMagic;
        switch (isMagic)
        {
            case true:
                LM.SetTimeofDay(12.4f, false);
                break;
            case false:
                LM.SetTimeofDay(1.1f, false);
                break;
        }
        StartCoroutine(ChangeDayOrNight(isMagic));
    }

    IEnumerator ChangeDayOrNight(bool t)
    {
        Debug.Log(LM.getTimeOfDay());
        if (!t)
        {
            while (LM.getTimeOfDay() < 12)
            {
                LM.SetTimeofDay(2f,true);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else if (t)
        {
            while (LM.getTimeOfDay() > 1)
            {
                LM.SetTimeofDay(2f,true);
                yield return new WaitForSeconds(0.05f);
            }
        }
        canchange = true;
    }

    public void Dialogue(bool d)
    {
        D = d;
        cmfl.gameObject.SetActive(!d);
        cam.SetActive(d);
    }

    void ZeroSpeed()
    {
        curSpeed = 0;
    }
    void ResetSpeed()
    {
        curSpeed = moveSpeed;
    }
    void RollSpeed(float multiplier)
    {
        curSpeed = moveSpeed * multiplier;
    }
}
