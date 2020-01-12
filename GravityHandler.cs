using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHandler : MonoBehaviour {


    /// <summary>
    ///  Use this class only for gravity Management
    /// </summary>
    /// 
    [Header("playeroptions")]
    public float playerHeight;
    [Header("Movment Options")]
    public float movementSpeed;
    public bool smooth;
    public float smoothSpeed;
    [Header("Gravity")]
    public float gravity = 2.5f;
    [Header("Physics")]
    public LayerMask discludePlayer;
    [Header("References")]
    public SphereCollider sphereCol;
    [Header("JumpOptions")]
    public float jumpForce;
    public float jumpSpeed;
    public float jumpDecreases;
    private Vector3 velocity;
    private Vector3 move;
    private Vector3 vel;
    private bool grounded;   // Gravity
    private float currentGravity = 0;
    private Vector3 lifPoint = new Vector3(0, 1.2f, 0);
    private RaycastHit groundHit;
    private float jumpHeight = 0;
    private bool inputJump = false;
    bool triggerJump;
    private Vector3 groundCheckPoint = new Vector3(0, -0.87f, 0);


    
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FinalMove()
    {
        Vector3 vel = new Vector3(velocity.x, velocity.y, velocity.z) * movementSpeed;
        vel = transform.TransformDirection(vel);
        transform.position += vel * Time.deltaTime;
        velocity = Vector3.zero;
    }

    #region Gravity Handler
    public void Gravity()
    {
        if (grounded == false)
        {
            velocity.y -= gravity ;
        }
    }


    public void GroundChecking()
    {
        /// Create a ray and cast a sphere dowwards to check if there is any floor
        Ray ray = new Ray(transform.TransformPoint(lifPoint), Vector3.down);
        RaycastHit tempHit = new RaycastHit();
        if (Physics.SphereCast(ray, 0.17f, out tempHit, 20, discludePlayer))
        {
            GroundConfirm(tempHit);
        }
        else
        {
            grounded = false;
        }
    }

    public void GroundConfirm(RaycastHit tempHit)
    {
        Collider[] col = new Collider[3];
        int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(groundCheckPoint), 0.57f, col, discludePlayer);
        grounded = false;
        for (int i = 0; i < num; i++)
        {
            if (col[i].transform == tempHit.transform)
            {
                groundHit = tempHit;
                grounded = true;

                if (inputJump == false)
                {
                    if (!smooth)
                    {
                        transform.position = new Vector3(transform.position.x,
                            (groundHit.point.y + playerHeight / 2), transform.position.z);
                    }
                    else
                    {
                        transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x,
                            (groundHit.point.y + playerHeight / 2),
                            transform.position.z), smoothSpeed * Time.deltaTime);
                    }
                }
                break;
            }
        }

        if (num <= 1 && tempHit.distance <= 3.1f && inputJump == false)
        {
            if (col[0] != null)
            {
                Ray ray = new Ray(transform.TransformPoint(lifPoint), Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 3.1f, discludePlayer))
                {
                    if (hit.transform != col[0].transform)
                    {
                        grounded = false;
                        return;
                    }
                }

            }
        }  
    }


    public void CollisionCheck()
    {
        Collider[] overlaps = new Collider[4];
        int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(sphereCol.center), sphereCol.radius,
            overlaps, discludePlayer, QueryTriggerInteraction.UseGlobal);

        for (int i = 0; i < num; i++)
        {
            Transform t = overlaps[i].transform;
            Vector3 dir;
            float dist;

            if (Physics.ComputePenetration(sphereCol, transform.position, transform.rotation,
                overlaps[i], t.position, t.rotation, out dir, out dist))
            {
                Vector3 penetrationVector = dir * dist;

                Vector3 velocityProjected = Vector3.Project(velocity, -dir);
                transform.position = transform.position + penetrationVector;
                vel -= velocityProjected;
            }
        }
    }

    public void Jump()
    {
        bool canJump = false;
        canJump = !Physics.Raycast(new Ray(transform.position, Vector3.up), playerHeight, discludePlayer);
        if (grounded && jumpHeight > 0.2f || jumpHeight <= 0.2f && grounded)
        {
            jumpHeight = 0;
            inputJump = false;
        }

        if (grounded && canJump)
        {
            if (triggerJump)
            {
                inputJump = false;
                transform.position += Vector3.up * 0.6f * 2;                             ///Vector3.up 
                jumpHeight += jumpForce;
                
            }
            triggerJump = false;
        }
        else
        {
            if (!grounded)
            {
                jumpHeight -= (jumpHeight * jumpDecreases * Time.deltaTime);
            }
        }

        velocity.y += jumpHeight;
    }

    public void TriggerJump()
    {
        triggerJump = true;
    }
    #endregion
}
