using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum MovementStates
    {
        Initial, OnGround, OnAir, Jumping, DoubleJumping, Dash, Attack, Dance, Death
    }

    public MovementStates playerState { get; set; }

    [Header("General Settings")]
    [SerializeField] private float speedMove = 4f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -15f;

    CharacterController charControl;
    Vector3 moveDirection;
    Vector3 moveDir;
    float smoothRotationVelocity;

    private CharacterController controller;


    HealthController healthController;
    public GameObject gameOverPanel;
    
    Vector3 verticalVelocity;

    [SerializeField] private float dashDuration = 0.6f;
    [SerializeField] private float dashForce = 8f;

    [Header("Animation Settings")]
    [SerializeField] private float acceleration = 2.5f;
    [SerializeField] private float deceleration = 5f;
    float playerSpeed;

    Animator anim;

    int numberOfClicks = 0;

    [SerializeField] private SphereCollider attackCollider;

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100;
    float currentStamina;

    [SerializeField] private float staminaReg = 5f;

    [SerializeField] private Image StaminaBar;

    void Start()
    {
        playerState = MovementStates.Initial;

        ChangeState(playerState);
        charControl = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;

        anim = GetComponentInChildren<Animator>();

        controller = GetComponent<CharacterController>();

        currentStamina = maxStamina;

        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        StateUpdate();

        if (currentStamina < maxStamina)
        {
            RegenerateStamina();
        }

        float finalLife = (float)currentStamina / maxStamina;

        StaminaBar.fillAmount = Mathf.MoveTowards(StaminaBar.fillAmount, finalLife, 1f * Time.deltaTime);

        if (anim.GetBool("dead") == true) 
        {
            transform.rotation = Quaternion.identity;
            controller.enabled= false;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
#endif
    }

    private void ChangeState(MovementStates newState)
    {
        //outcoming triggering condition
        switch (playerState)
        {
            case MovementStates.Initial:
                //
                break;
            case MovementStates.OnGround:
                break;
            case MovementStates.OnAir:
                break;
            case MovementStates.Jumping:
                break;
            //case MovementStates.DoubleJumping:
            //    anim.SetBool("doubleJumping", false);
            //    break;
            case MovementStates.Dash:
                anim.SetBool("dashing", false);
                anim.SetInteger("attack", 0);
                numberOfClicks = 0;
                break;
            case MovementStates.Attack:
                break;
            case MovementStates.Dance:
                anim.SetBool("dance", false);
                break;
            case MovementStates.Death:
                anim.SetBool("dead", false);
                break;
        }

        //incoming triggering condition
        switch (newState)
        {
            case MovementStates.Initial:
                break;
            case MovementStates.OnGround:
                //
                break;
            case MovementStates.OnAir:
                break;
            case MovementStates.Jumping:
                verticalVelocity.y = jumpForce;
                break;
            //case MovementStates.DoubleJumping:
            //    verticalVelocity.y = jumpForce;
            //    anim.SetBool("doubleJumping", true);
            //    break;
            case MovementStates.Dash:
                Invoke("DashLater", 0.1f);
                anim.SetBool("dashing", true);
                attackCollider.enabled = false;
                break;
            case MovementStates.Attack:
                OnClickAttack();
                playerSpeed = 0;
                anim.SetFloat("Speed", playerSpeed);
                break;
            case MovementStates.Dance:
                anim.SetBool("dance", true);
                break;
            case MovementStates.Death:
                anim.SetBool("dead", true);
                break;
        }

        playerState = newState;
    }
    private void StateUpdate()
    {
        switch (playerState)
        {
            case MovementStates.Initial:
                PlayerMovement();
                if (charControl.isGrounded)
                    ChangeState(MovementStates.OnGround);
                break;
            case MovementStates.OnGround:
                PlayerMovement();
                if (currentStamina > 0)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        currentStamina -= 10;
                        ChangeState(MovementStates.Jumping);
                    }
                    if (Input.GetButtonDown("Fire3"))
                    {
                        currentStamina -= 10;
                        ChangeState(MovementStates.Dash);
                    }
                    if (Input.GetButtonDown("Fire1"))
                    {
                        ChangeState(MovementStates.Attack);
                    }
                }
                if (Input.GetKey(KeyCode.B))
                    ChangeState(MovementStates.Dance);
                break;
            case MovementStates.OnAir:
                PlayerMovement();
                if (charControl.isGrounded)
                    ChangeState(MovementStates.OnGround);
                if (currentStamina > 0)
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        currentStamina -= 10;
                        //ChangeState(MovementStates.DoubleJumping);
                    }
                }
                break;
            case MovementStates.Jumping:
                PlayerMovement();
                if (charControl.isGrounded)
                    ChangeState(MovementStates.OnGround);
                break;
                //if (currentStamina > 0)
                //{
                //    if (Input.GetButtonDown("Jump"))
                //    {
                //        currentStamina -= 10;
                //        //ChangeState(MovementStates.DoubleJumping);
                //    }
                //}
                //break;
            //case MovementStates.DoubleJumping:
            //    PlayerMovement();
            //    if (charControl.isGrounded)
            //        ChangeState(MovementStates.OnGround);
            //    break;
            case MovementStates.Dash:
                break;
            case MovementStates.Attack:
                if (currentStamina > 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        OnClickAttack();
                    }

                    if (Input.GetButtonDown("Fire3"))
                    {
                        currentStamina -= 10;
                        ChangeState(MovementStates.Dash);
                    }
                }
                break;
            case MovementStates.Dance:
                if (Input.GetKey(KeyCode.W))
                    ChangeState(MovementStates.OnGround);
                break;
            case MovementStates.Death:
                Dead();
                break;
        }
    }

    private void PlayerMovement()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (moveDirection.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothRotationVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            charControl.Move(moveDir.normalized * speedMove * Time.deltaTime);

            if (playerSpeed < 1)
                playerSpeed += acceleration * Time.deltaTime;
            else
                playerSpeed = 1;
        }
        else
        {
            if (playerSpeed > 0)
                playerSpeed -= deceleration * Time.deltaTime;
            else
                playerSpeed = 0;
        }
        ConstantGravity();

        anim.SetFloat("Speed", playerSpeed);

        anim.SetBool("isGrounded", charControl.isGrounded);
    }

    private void ConstantGravity()
    {
        verticalVelocity.y += gravity * Time.deltaTime;
        charControl.Move(verticalVelocity * Time.deltaTime);

        if (charControl.isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -5f;
        }

    }

    public void Dead()
    {
        controller.enabled = false;
    }
    private void DashLater()
    {
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        Vector3 moveDash = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        if (moveDirection.magnitude < 0.1f)
            moveDash = transform.forward;
        else
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        while (Time.time < startTime + dashDuration)
        {
            charControl.Move(moveDash.normalized * dashForce * Time.deltaTime);
            ConstantGravity();
            yield return null;
        }
        ChangeState(MovementStates.OnGround);
    }

    private void OnClickAttack()
    {
        numberOfClicks++;
        if (numberOfClicks == 1)
            anim.SetInteger("attack", 1);

        numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);
    }

    public void CheckCombo()
    {
        if (playerState == MovementStates.Dash)
            return;

        if (numberOfClicks == 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("attack_1"))
        {
            currentStamina -= 10;   
            anim.SetInteger("attack", 0);
            numberOfClicks = 0;
            ChangeState(MovementStates.OnGround);
        }

        if (numberOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).IsName("attack_1"))
        {
            currentStamina -= 10;
            anim.SetInteger("attack", 2);
        }
        if (numberOfClicks == 2 && anim.GetCurrentAnimatorStateInfo(0).IsName("attack_2"))
        {
            currentStamina -= 10;
            anim.SetInteger("attack", 0);
            numberOfClicks = 0;
            ChangeState(MovementStates.OnGround);
        }
        if (numberOfClicks == 3 && anim.GetCurrentAnimatorStateInfo(0).IsName("attack_2"))
        {
            currentStamina -= 10;
            anim.SetInteger("attack", 3);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack_3"))
        {
            currentStamina -= 10;
            anim.SetInteger("attack", 0);
            numberOfClicks = 0;
            ChangeState(MovementStates.OnGround);
        }
    }
    private void RegenerateStamina()
    {
        currentStamina += staminaReg * Time.deltaTime;
    }

    public void SetAttackCollider(bool value)
    {
        if (playerState == MovementStates.Dash)
            return;

        attackCollider.enabled = value;
    }

    private void OnEnable()
    {
        AnimationEventController.onAnimationEvent += CheckCombo;
    }

    private void OnDisable()
    {
        AnimationEventController.onAnimationEvent -= CheckCombo;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<iTakeItem>() != null)
            other.GetComponent<iTakeItem>().TakeItem();
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
