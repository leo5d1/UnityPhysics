using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControllerAI2D : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField] SpriteRenderer spriteRenderer;

	[SerializeField] float speed;
	[SerializeField] float jumpHeight;
	[SerializeField] float doubleJumpHeight;
	[SerializeField, Range(1, 5)] float fallRateMultiplier;
	[SerializeField, Range(1, 5)] float lowJumpRateMultiplier;
	[Header("Ground")]
	[SerializeField] Transform groundTransform;
	[SerializeField] LayerMask groundLayerMask;
	[SerializeField] float groundRadius;
	[Header("AI")]
	[SerializeField] Transform[] waypoints;
	[SerializeField] float rayDistance = 1;

	Rigidbody2D rb;
	Vector2 velocity = Vector2.zero;
	bool faceRight = true;
	float groundAngle = 0;
	Transform targetWaypoint = null;

	enum State
	{
		IDLE,
		PATROL,
		CHASE,
		ATTACK
	}

	State state = State.IDLE;
	float stateTimer = 0;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		Vector2 direction = Vector2.zero;

		//update ai
		switch (state)
		{
			
			case State.IDLE:
				if (CanSeePlayer())
				{
					state = State.CHASE;
				}

				stateTimer += Time.deltaTime;
				if (stateTimer >= 0.5f)
				{
					SetNewWaypointTarget();
					state = State.PATROL;
				}
				break;

			case State.PATROL:
				if (CanSeePlayer())
				{
					state = State.CHASE;
				}

				speed = 2;
				direction.x = Mathf.Sign(targetWaypoint.position.x - transform.position.x);
				
				float dx = Mathf.Abs(targetWaypoint.position.x - transform.position.x);

				if (dx <= 0.25f)
				{
					state = State.IDLE;
					stateTimer = 0;
					//direction.x = 0;
				}

				break;
			
			case State.CHASE:

				break;

			case State.ATTACK:
				break;
			
			default:
				break;
		}

		// check if the character is on the ground
		bool onGround = Physics2D.OverlapCircle(groundTransform.position, 0.2f, groundLayerMask) != null;
		//bool onGround = UpdateGroundCheck();
		
		velocity.x = direction.x * speed;

		// set velocity
		if (onGround)
		{
			if (velocity.y < 0) velocity.y = 0;
			if (Input.GetButtonDown("Jump"))
			{
				velocity.y += Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
				StartCoroutine(DoubleJump());
				animator.SetTrigger("Jump");
			}
		}
		velocity.y += Physics.gravity.y * Time.deltaTime;

		// adjust gravity for jump
		float gravityMultiplier = 1;
		if (!onGround && velocity.y < 0) gravityMultiplier = fallRateMultiplier;
		if (!onGround && velocity.y > 0 && !Input.GetButtonDown("Jump")) gravityMultiplier = lowJumpRateMultiplier;

		velocity.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

		// move character
		rb.velocity = velocity;

		// flip character to face direction of movement (velocity)
		if (velocity.x > 0 && !faceRight) Flip();
		if (velocity.x < 0 && faceRight) Flip();

		// update animator
		animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
	}

	IEnumerator DoubleJump()
	{
		// wait a little after the jump to allow a double jump
		yield return new WaitForSeconds(0.01f);
		// allow a double jump while moving up
		while (velocity.y > 0)
		{
			// if "jump" pressed add jump velocity
			if (Input.GetButtonDown("Jump"))
			{
				velocity.y += Mathf.Sqrt(doubleJumpHeight * -2 * Physics.gravity.y);
				break;
			}
			yield return null;
		}
	}

	private void Flip()
	{
		faceRight = !faceRight;
		spriteRenderer.flipX = !faceRight;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(groundTransform.position, groundRadius);
	}

	private void SetNewWaypointTarget()
	{
		Transform waypoint = targetWaypoint;

		while (waypoint == targetWaypoint)
		{
			waypoint = waypoints[Random.Range(0, waypoints.Length)];
		}
		targetWaypoint = waypoint;
	}

	private bool CanSeePlayer()
	{
		RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, ((faceRight) ? Vector2.right : Vector2.left) * rayDistance);
		Debug.DrawRay(transform.position, ((faceRight) ? Vector2.right : Vector2.left) * rayDistance);

		return raycastHit.collider != null && raycastHit.collider.gameObject.CompareTag("Player");
	}
}