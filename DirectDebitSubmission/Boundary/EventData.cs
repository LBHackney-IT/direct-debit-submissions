using System.Diagnostics.CodeAnalysis;

namespace DirectDebitSubmission.Boundary
{
    [ExcludeFromCodeCoverage]
    public class EventData
    {
        public object OldData { get; set; }

        public object NewData { get; set; }
    }
}
