namespace ConwaysGameOfLife
{
	public interface IGameUi
	{
		void Update(IReadonlyField field);
		void Update(int x, int y, int age);
	}
}