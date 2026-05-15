using CSaP.CourseProject.Service;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public class VerifyCommand : ICommand
    {
        private readonly ParsedCommand _parsed;
        private readonly MessageVerifier _verifier;
        private readonly CommandDescription _description = new CommandDescription(
            "verify",
            new List<CommandHelpEntry>()
            {
                    new("verify <messageID>", "Verify message signature"),
                    new("verify <messageID> --extended", "Show detailed verification process")
            }
        );


        public CommandDescription Description => _description;


        public VerifyCommand(ParsedCommand parsed, MessageVerifier verifier)
        {
            _parsed = parsed;
            _verifier = verifier;
        }


        public void Execute()
        {
            if (_parsed.Arguments.Count == 0) throw new FormatException("Message ID required");

            string messageID = _parsed.Arguments[0];

            VerificationResult result = _verifier.Verify(messageID);

            bool isExtended = _parsed.Flags.Contains("--extended");

            if (isExtended)
            {
                PrintExtended(result);
            }
            else
            {
                PrintSimple(result);
            }
        }

        private void PrintSimple(VerificationResult result)
        {
            Console.WriteLine(result.IsValid ? "VALID\n" : "INVALID\n");
        }

        private void PrintExtended(VerificationResult result)
        {
            Console.WriteLine( 
                $"""
                Message ID: {result.MessageID}
                Sender: {result.SenderID}
                Timestamp: {result.Timestamp}
                
                SHA3(message): 
                {Convert.ToHexString(result.MessageHash)}

                Signature:
                {Convert.ToHexString(result.Signature)}
                
                Verification: {(result.IsValid ? "VALID" : "INVALID")}
                
                """
            );
        }
    }
}