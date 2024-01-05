using bankingApplication_API.Controllers;

namespace bankingApplication_API
{
    public class VerificationTokenGenerator
    {
        public static int GenerateVerificationToken()
        {
            Random random = new Random();
            int verificationToken;

            do
            {
                verificationToken = random.Next();
                if (!NaturalPersonController._naturalPersonInterface.VerificationTokenExists(verificationToken))
                    return verificationToken; //none of existing verification tokens is equals to the newly created one
            } while (true);
        }
    }
}
