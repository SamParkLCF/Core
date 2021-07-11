namespace LCF.Core
{
    public interface ITelemetryParameters
    {

    }
    public interface ITelemetryParameters<TValue> : ITelemetryParameters
    {
        string Name { get; }
        TValue Value { get; }
    }
}
