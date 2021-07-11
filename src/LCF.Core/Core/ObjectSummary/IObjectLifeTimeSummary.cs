namespace LCF.Core
{
    /// <summary>
    /// Provides summary information about the object.
    /// </summary>
    public interface IObjectLifeTimeSummary
    {
        IObjectBornInformation ObjectBornInformation { get; }
        IObjectDeathInformation ObjectDeathInformation { get; }
    }
}
