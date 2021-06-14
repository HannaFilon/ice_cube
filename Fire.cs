using Godot;

namespace TheGame
{
	public class Fire : Area2D
	{
		public void OnBodyEntered(object body)
		{
			if (body is Player p)
			{
				p.MeltingAreasCount += 1;
			}
		}

		public void OnBodyExited(object body)
		{
			if (body is Player p)
			{
				p.MeltingAreasCount -= 1;
			}
		}
	}
}
