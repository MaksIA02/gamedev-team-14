using Assets.Scripts.StatsSystem;
using UnityEngine;

public class HeroKnight : MonoBehaviour
{

	[SerializeField] private DirectionalMovementData _directionalMovementData;
	[SerializeField] private JumpData _jumpData;


	[SerializeField] private DirectionalCameraPlayer _cameras;
	[SerializeField] private Direction _direction;


	private Animator _animator;
	private Rigidbody2D _body2d;
	private Sensor_HeroKnight _groundSensor;
	private bool _grounded = false;
	private int _currentAttack = 0;
	private float _timeSinceAttack = 0.0f;
	private float _delayToIdle = 0.0f;

	private DirectionalMover _directionalMover;
	private Jumper _jumper;
	readonly IStatValueGiver statValueGiver;


	
	void Start()
	{
		_animator = GetComponent<Animator>();
		_body2d = GetComponent<Rigidbody2D>();
		_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
		_directionalMover = new DirectionalMover(_body2d, _animator, _cameras, _directionalMovementData, statValueGiver); // 
		_jumper = new Jumper(_body2d, _jumpData);
	}

	public void Initialize (IStatValueGiver statValueGiver)
	{
		_directionalMover = new DirectionalMover(_body2d, _animator, _cameras, _directionalMovementData, statValueGiver); //
	}

	
	void Update()
	{
		PlayerAnimations();
	}

	private void PlayerAnimations()
	{
		//Set AirSpeed in animator
		_animator.SetFloat("AirSpeedY", _body2d.velocity.y);

		//Increase timer that controls attack combo
		_timeSinceAttack += Time.deltaTime;

		//Check if character just landed on the ground
		if (!_grounded && _groundSensor.State())
		{
			_grounded = true;
			_animator.SetBool("Grounded", _grounded);
		}

		//Check if character just started falling
		if (_grounded && !_groundSensor.State())
		{
			_grounded = false;
			_animator.SetBool("Grounded", _grounded);
		}
		//Idle
		else
		{
			// Prevents flickering transitions to idle
			_delayToIdle -= Time.deltaTime;
			if (_delayToIdle < 0)
				_animator.SetInteger("AnimState", 0);
		}
	}
	public void PlayerMovement(float inputX)
	{
		_directionalMover.PlayerMovement(inputX);
		if (Mathf.Abs(inputX) > Mathf.Epsilon)
		{
			// Reset timer
			_delayToIdle = 0.05f;
			_animator.SetInteger("AnimState", 1);
		}
	}

	public void Jump()
	{
		if (_grounded)
		{
			
			_animator.SetTrigger("Jump");
			_grounded = false;
			_animator.SetBool("Grounded", _grounded);
			_jumper.Jump();
			_groundSensor.Disable(0.2f);
		}
	}

	public void Attack()
	{
		//Attack
		if (_timeSinceAttack > 0.25f)
		{
			_currentAttack++;

			// Loop back to one after third attack
			if (_currentAttack > 3)
				_currentAttack = 1;

			// Reset Attack combo if time since last attack is too large
			if (_timeSinceAttack > 1.0f)
				_currentAttack = 1;

			// Call one of three attack animations "Attack1", "Attack2", "Attack3"
			_animator.SetTrigger("Attack" + _currentAttack);

			// Reset timer
			_timeSinceAttack = 0.0f;
		}
	}

	
}
