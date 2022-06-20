using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CRifle : MonoBehaviour
{
    public float walkSpeed = 2.5f;
    public float jumpHeight = 5f;

    public Transform groundCheckTransform;
    public float groundCheckRadius = 0.2f;

    public Transform targetTransform;
    public LayerMask mouseAimMask;
    public LayerMask groundMask;

    public GameObject bulletPrefab; // BULLET MUUTOKSET
    public Transform muzzleTransform; //BULLET MUUTOKSET

    private float inputMovement;
    private Animator animator;
    private Rigidbody rbody;
    private bool isGrounded;
    private bool crouch; // CROUCH MUUTOKSET
    private Camera mainCamera;

    [SerializeField]
    private Collider standingCollider;      // Collider muutokset kun crouch

    private int FacingSign
    {
        get
        {
            Vector3 perp = Vector3.Cross(transform.forward, Vector3.forward);
            float dir = Vector3.Dot(perp, transform.up);
            return dir > 0f ? -1 : dir < 0f ? 1 : 0;
        }
    }

    public Rig weaponRig;      // TEST MOUSE AIM
    public Rig spineRig; // RIG TEST
    private float targetWeight; // TEST MOUSE AIM    

    // Start is called before the first frame update
    void Start()
    {        
        animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        inputMovement = Input.GetAxis("Horizontal");

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
        {
            targetTransform.position = hit.point;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rbody.velocity = new Vector3(rbody.velocity.x, 0, 0);
            rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -1 * Physics.gravity.y), ForceMode.Impulse); // Test Impulse oli VelocityChange
        }

        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)        // CROUCH MUUTOKSET
        {
            crouch = true;
            standingCollider.enabled = false;       // Collider muutokset kun crouch
            animator.Play("Armature|Crouch_gun");
            walkSpeed = 0f;
        }
        else
        {
            crouch = false;
            standingCollider.enabled = true;        // Collider muutokset kun crouch
            walkSpeed = 4f;
        }        

        if (Input.GetButton("Fire2"))
        {
            weaponRig.weight = targetWeight;
            spineRig.weight = targetWeight;     //RIG TEST
            targetWeight = 1f;
            Debug.Log("Shoot aim happens");

            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("SHOOT BULLET");
                Fire();
            }
        }        
        else
        {
            weaponRig.weight = targetWeight;
            spineRig.weight = targetWeight;     // RIG TEST
            targetWeight = 0f;
            Debug.Log("Shoot aim pois");
        }
    }
    private void Fire()
    {
        var go = Instantiate(bulletPrefab);
        go.transform.position = muzzleTransform.position;
        var bullet = go.GetComponent<Bullet>();
        bullet.Fire(go.transform.position, muzzleTransform.eulerAngles, gameObject.layer);
    }

    private void FixedUpdate()
    {
        // Movement
        rbody.velocity = new Vector3(inputMovement * walkSpeed, rbody.velocity.y, 0);
        animator.SetFloat("speed", (FacingSign * rbody.velocity.x) / walkSpeed);

        // Facing Rotation
        rbody.MoveRotation(Quaternion.Euler(new Vector3(0, 90 * Mathf.Sign(targetTransform.position.x - transform.position.x), 0)));

        // Ground Check
        isGrounded = Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundMask, QueryTriggerInteraction.Ignore);
        animator.SetBool("isGrounded", isGrounded);

        animator.SetBool("crouch", crouch);       // CROUCH MUUTOKSET
    }
}
