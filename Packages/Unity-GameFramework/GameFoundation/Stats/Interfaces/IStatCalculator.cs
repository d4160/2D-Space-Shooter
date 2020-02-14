namespace d4160.Systems.Flow
{
    public interface IStatCalculator
    {
        float CalculateStat(int difficultyLevel = 1);
    }

    public interface IMultipleStatCalculator
    {
        float[] CalculateStat(int difficultyLevel = 1);

        float CalculateStat(int index, int difficultyLevel = 1);
    }

    public interface IStatCalculator<T>
    {
        T CalculateStat(int difficultyLevel = 1);
    }
}