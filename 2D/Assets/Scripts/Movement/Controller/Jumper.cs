using UnityEngine;

public class Jumper
{
	private readonly Rigidbody2D _body2d;
	private readonly JumpData _jumpData;

	public Jumper(Rigidbody2D body2d, JumpData jumpData)
	{
		_body2d = body2d;
		_jumpData = jumpData;
	}
	public void Jump()
	{
		
		_body2d.velocity = new Vector2(_body2d.velocity.x, _jumpData.JumpForce);
	}
}
