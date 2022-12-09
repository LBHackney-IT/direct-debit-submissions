using System.Diagnostics.CodeAnalysis;

namespace DirectDebitSubmission.Boundary
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
