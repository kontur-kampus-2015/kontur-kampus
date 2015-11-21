namespace ConwaysGameOfLife
{
	public class Point
	{
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public readonly int X, Y;

		public Point Add(Point p)
		{
			return new Point(p.X + X, p.Y + Y);
		}

		protected bool Equals(Point other)
		{
			return X == other.X && Y == other.Y;
		}

		public override string ToString()
		{
			return string.Format("X: {0}, Y: {1}", X, Y);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Point) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (X*397) ^ Y;
			}
		}
	}
}