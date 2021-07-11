namespace LCF.Core
{
    public class TelemetryParameters : ITelemetryParameters
    {
        public override string ToString() => JsonHelper.SerializeObject(this);
    }
    public class TelemetryParameters<TValue> : TelemetryParameters, ITelemetryParameters<TValue>
    {
        public TelemetryParameters(string name, TValue value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; protected set; }
        public TValue Value { get; protected set; }
    }
}
