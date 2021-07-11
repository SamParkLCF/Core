namespace LCF.Core
{
    public interface ICallerInformation
    {
        string Name { get;  }
        string FilePath { get;  }
        int LineNumber { get;  }
    }
}
