using Godot;

public class Respawnable : RigidBody2D
{
	private float _startX;
	private float _startY;

	public float RespawnY = 1080;

	public override void _Ready()
	{
		_startX = Position.x;
		_startY = Position.y;
		var respawnLine = GetTree().Root.GetNodeOrNull<Position2D>("Level/RespawnLine");
		if (respawnLine != null)
			RespawnY = respawnLine.Position.y;
	}

	public override void _IntegrateForces(Physics2DDirectBodyState state)
	{
		var transform = state.Transform;
		if (Position.y >= RespawnY)
		{
			transform.origin = new Vector2(_startX, _startY);
		}

		state.Transform = transform;
	}
}
