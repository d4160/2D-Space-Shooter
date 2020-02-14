namespace d4160.Core
{
    public interface IFactory<T>
    {
        T Fabricate(int option = 0);
    }

    public interface IFactorySecond<T>
    {
        T FabricateSecond(int option = 0);
    }
}
