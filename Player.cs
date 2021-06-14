using Godot;

namespace TheGame
{
	public class Player : KinematicBody2D
	{
		[Export] public int Speed { get; set; } = 200;
		[Export] public int JumpSpeed { get; set; } = -300;
		[Export] public int Gravity { get; set; } = 980;
		[Export] public float NormalFriction { get; set; } = 0.1f;
		[Export] public float Acceleration { get; set; } = 0.25f;
		[Export] public int Push { get; set; } = 10;
		[Export] public Texture NormalTexture { get; set; }
		[Export] public Texture HalfNormalTexture { get; set; }
		[Export] public Texture NotNormalTexture { get; set; }

		private float _friction;

		[Export]
		public float Health
		{
			get => _health;
			set => _health = Mathf.Min(value, MaxHealth);
		}

		[Export] public float MaxHealth { get; set; } = 100;

		public float RespawnY = 1080;
		[Export] public PackedScene SpawnParticlesScene;

		public Vector2 SpawnPoint { get; set; }

		private Vector2 _velocity = new Vector2(0, 0);
		private float _health = 100;

		public int MeltingAreasCount { get; set; } = 0;

		public bool IsMelting => MeltingAreasCount > 0;

		[Export] public float MeltingCoefficient = -10f;
		[Export] public float RegenerationCoefficient = 0.1f;

	  

		public override void _Ready()
		{
			SpawnPoint = Position;
		}

		public void Respawn()
		{
			Health = MaxHealth;
			Teleport(SpawnPoint);
		}

		public void ChangeSpawnPoint(Vector2 point)
		{
			SpawnPoint = point;
			Respawn();
		}

		public void Teleport(Vector2 point)
		{
			Position = point;


			var spawnParticles = (Particles2D) SpawnParticlesScene?.Instance();
			if (spawnParticles != null)
			{
				var root = GetTree().Root;
				GD.Print();
				root.AddChild(spawnParticles);
				spawnParticles.Position = Position;
				spawnParticles.Emitting = true;
			}
		}

		public override void _PhysicsProcess(float delta)
		{
			GetLeftRightInput();
			_velocity.y += Gravity * delta;
			_velocity = MoveAndSlide(_velocity, Vector2.Up,
				false, 4, Mathf.Pi / 4, false);
			if (Input.IsActionJustPressed("up") && IsOnFloor())
			{
				GD.Print("Jump");
				_velocity.y = JumpSpeed;
			}

			for (var i = 0; i < GetSlideCount(); ++i)
			{
				var collision = GetSlideCollision(i);

				if (collision.Collider is RigidBody2D body)
					body.ApplyCentralImpulse(-collision.Normal * Push);
			}
		}

		public override void _Process(float delta)
		{
			if (Position.y >= RespawnY)
				Respawn();
			GetNode<Particles2D>("MeltingParticles").Emitting = IsMelting;
			Health += IsMelting ? MeltingCoefficient * delta : delta * RegenerationCoefficient;
			_friction = IsMelting ? 0.0f : NormalFriction;
			if (Health <= 0)
				Respawn();
			if (Input.IsActionJustPressed("cheat_change_spawn_point"))
			{
				ChangeSpawnPoint(GetGlobalMousePosition());
				GD.Print("Change spawn point");
			}

			var sprite = GetNode<Sprite>("Sprite");
		   
			if (Health <= 0.33 * MaxHealth)
			{
				sprite.Texture = NotNormalTexture;
			}
			else
			{
				if (Health <= 0.66 * MaxHealth)
				{
					sprite.Texture = HalfNormalTexture;
				}
				else
					sprite.Texture = NormalTexture;
			}
		}

		private void GetLeftRightInput()
		{
			var dir = 0;

			if (Input.IsActionPressed("right")) dir += 1;

			if (Input.IsActionPressed("left")) dir -= 1;

			if (dir != 0)
				_velocity.x = Mathf.Lerp(_velocity.x, dir * Speed, Acceleration);
			else
				_velocity.x = Mathf.Lerp(_velocity.x, 0, _friction);
		}
	}
}
