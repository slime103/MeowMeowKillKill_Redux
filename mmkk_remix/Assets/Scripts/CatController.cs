using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    public static CatController me;

    [Header("Important Components")]
    CharacterController cc;
    Camera pc;
    BoxCollider attackBC;
    AudioSource aS;

    [Header("Camera Movement")]
    public Vector2 mouseAxis;
    public float mouseSensitivity;
    public float vertCam;
    public Vector3 camOgPos;
    public float lowPos;

    [Header("World Movement")]
    public Vector3 moveAxis;
    public Vector3 velocity;
    public float speed;

    [Header("GroundChecking")]
    public bool onGround;
    public bool onCeiling;

    [Header("Gravity")]
    public float gravity;
    float savedVelocityY;

    [Header("Jump")]
    public float jumpAmount;
    public bool JumpInput;
    public bool doJump; //The frame the player will jump on
    public float jumpDuration;
    public float jumpVel;
    public float jumpVelMin;
    public float jumpVelMax;
    public float jumpDelay;
    public Slider slider;
    public Image slideImage;
    public Color slideStartColor;
    public Color slideEndColor;

    [Header("Claws")]
    bool attack;
    public GameObject claw1;
    public GameObject claw2;
    public float spinSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        me = this;
        cc = GetComponent<CharacterController>();
        pc = GetComponentInChildren<Camera>();
        attackBC = GetComponentInChildren<BoxCollider>();
        aS = GetComponent<AudioSource>();
        camOgPos = pc.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        MouseKeyboardInputs();
        MouseMovements();
        SetSlider();
    }

    void MouseKeyboardInputs()
    {
        mouseAxis = new Vector2(Input.GetAxis("Mouse X") * mouseSensitivity, Input.GetAxis("Mouse Y") * mouseSensitivity);
        moveAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput = true;
        }

        if (Input.GetKeyUp(KeyCode.Space) && onGround)
        {
            doJump = true;
            JumpInput = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attack = true;
            aS.Play();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            attack = false;
            aS.Stop();
        }
    }

    void MouseMovements()
    {
        transform.Rotate(Vector3.up * mouseAxis.x);

        vertCam -= mouseAxis.y;
        vertCam = Mathf.Clamp(vertCam, -90, 90);

        pc.transform.localRotation = Quaternion.Euler(vertCam, 0, 0);
    }

    void SetSlider()
    {
        slider.value = jumpAmount;
        if (slider.value >= .94f)
        {
            Flash(true);
        } else if (slider.value <= .1f)
        {
            Flash(false);
        }
        slideImage.color = Color.Lerp(slideStartColor, slideEndColor, jumpAmount);
    }

    void Flash(bool b)
    {
        slideImage.gameObject.GetComponent<HudFlash>().Flash(b);
    }

    void FixedUpdate()
    {
        if (JumpInput)
        {
            JumpFunc();
        }
        MouseMovements();
        VerticalVelocitySave();
        HorizontalVelocityCalc();
        VerticalVelocityCalc();

        if (!attack || attack & !onGround)
        {
            Velocity();
        }

        if (attack && onGround)
        {
            Attack();
            attackBC.enabled = true;
        }
        else
        {
            attackBC.enabled = false;
        }
        
        
        if(moveAxis != Vector3.zero)
        {
            CatWalk();
        }
        CamPos();
    }

    void JumpFunc()
    {
        if (onGround)
        {
            jumpAmount = Mathf.Lerp(jumpAmount, 1, 0.01f);
            jumpVel = jumpVelMin + (jumpVelMax * jumpAmount);
        }
        else
        {
            jumpAmount = 0;
            jumpVel = 0;
        }
    }

    void VerticalVelocitySave()
    {
        savedVelocityY = velocity.y;
    }

    void HorizontalVelocityCalc()
    {
        if (onGround)
        {
            velocity = ((moveAxis.x * transform.right) + (moveAxis.z * transform.forward)) * speed;
        }
        else
        {
            Vector3 newVel = Vector3.zero;
            newVel += ((moveAxis.x * transform.right) + (moveAxis.z * transform.forward)) * speed;
            velocity = Vector3.Lerp(velocity, newVel, 0.025f);
        }
    }
    void VerticalVelocityCalc()
    {
        //start jump
        if (doJump && onGround)
        {
            velocity.y = jumpVel;
            VerticalVelocitySave();
            doJump = false;
            jumpAmount = 0;
            jumpDuration = 0;
            onGround = false;
        }

        //Reached end of jump
        if (onCeiling)
        {
            //jumpDuration = jumpLength;
        }

        if (jumpDuration < jumpDelay)
        {
            velocity.y = jumpVel;
        }

        //start falling
        if (!onGround)
        {
            velocity.y = savedVelocityY - gravity;
            jumpDuration += Time.fixedDeltaTime;
        }
        //If on ground then there is no vertical velocity
        else if (!JumpInput && jumpDuration > jumpDelay)
        {
            velocity.y = 0;
        }
    }

    void Velocity()
    {
        cc.Move(velocity);
    }

    void Attack()
    {
        claw2.transform.localRotation = Quaternion.Euler(claw1.transform.localEulerAngles.x, claw1.transform.localEulerAngles.y, (Mathf.Cos(GameManager.me.timer * 0.2f)) * 30);
        claw1.transform.localRotation = Quaternion.Euler(claw1.transform.localEulerAngles.x, claw1.transform.localEulerAngles.y, (Mathf.Sin(GameManager.me.timer * 0.2f)) * 30);
    }

    void CatWalk()
    {
        claw1.transform.rotation = Quaternion.Euler(claw1.transform.eulerAngles.x, claw1.transform.eulerAngles.y, -90 + (Mathf.Sin(GameManager.me.timer * 0.1f)) * 30);
        claw2.transform.rotation = Quaternion.Euler(claw1.transform.eulerAngles.x, claw1.transform.eulerAngles.y, -90 + (Mathf.Cos(GameManager.me.timer * 0.1f)) * 30);
    }

    void CamPos()
    {
        pc.transform.localPosition = camOgPos + (Vector3.down * lowPos * jumpAmount);
    }

}
