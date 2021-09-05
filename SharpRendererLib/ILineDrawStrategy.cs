namespace SharpRendererLib
{
    public interface ILineDrawStrategy : IInitializable
    {
        int DetermineY(int x, Line line);
    }

    public interface IInitializable
    {
        void Initialize();
    }
}