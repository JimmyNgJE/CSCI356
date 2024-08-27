using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private float jumpSpeed = 5f;

    [SerializeField]
    private float jumpButtonGracePeriod = 0.2f;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private Transform cameraTransform;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    private bool isJumping;

    public GameObject settingsPopup; // Reference to the settings popup GameObject

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        characterController.stepOffset = 0.3f; // Adjust as needed
    }

    void Update()
    {
        // Check if settings menu is active
        if (settingsPopup.activeSelf)
        {
            return; // Skip processing if settings menu is open
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection;

        // Handle backward movement independently of the camera direction
        if (verticalInput < 0) // Moving backwards
        {
            movementDirection = -transform.forward;
        }
        else
        {
            movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            // Convert movement direction to world space relative to camera
            movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        }

        movementDirection.Normalize();

        // Calculate movement magnitude for animations
        float inputMagnitude = Mathf.Clamp01(new Vector3(horizontalInput, 0, verticalInput).magnitude);

        // Update animator parameter
        animator.SetFloat("InputMagnitude", inputMagnitude, 0.01f, Time.deltaTime);

        // Apply gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // Jump logic
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;

            if (Input.GetButtonDown("Jump"))
            {
                jumpButtonPressedTime = Time.time;
            }

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                isJumping = true;
                animator.SetBool("IsJumping", true);
            }
        }

        if (isJumping && characterController.isGrounded)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }

        // Apply movement
        Vector3 velocity = movementDirection * inputMagnitude * moveSpeed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        // Rotate character towards movement direction
        if (verticalInput >= 0 && movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Ensure character doesn't rotate when moving backwards
        if (verticalInput < 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-transform.forward), rotationSpeed * Time.deltaTime);
        }
    }

    private void OnAnimatorMove()
    {
        if (characterController.isGrounded)
        {
            Vector3 velocity = animator.deltaPosition;
            velocity.y = ySpeed * Time.deltaTime;
            characterController.Move(velocity);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
