namespace LCF.Core
{
    public class ObjectLifeTimeSummary : IObjectLifeTimeSummary
    {
        public ObjectLifeTimeSummary(IObjectBornInformation bornInformation, IObjectDeathInformation deathInformation)
        {
            ObjectBornInformation = bornInformation;
            ObjectDeathInformation = deathInformation;
        }

        public IObjectBornInformation ObjectBornInformation { get; }
        public IObjectDeathInformation ObjectDeathInformation { get; }

        public override string ToString() => JsonHelper.SerializeObject(this);
    }
}
