using bankingApplication_API.Controllers;

namespace bankingApplication_API
{
    public class UniqueNumberGenerator
    {
        public static int GenerateVerificationToken(Type controllerType)
        {
            Random random = new Random();
            int verificationToken;
            do
            {
                verificationToken = random.Next();

                if (controllerType == typeof(NaturalPersonController))
                {
                    if (!NaturalPersonController._naturalPersonInterface.VerificationTokenExists(verificationToken))
                        return verificationToken; //none of existing verification tokens is equals to the newly created one
                }
                else if (controllerType == typeof(JuridicalPersonController))
                {
                    if (!JuridicalPersonController._juridicalPersonInterface.VerificationTokenExists(verificationToken))
                        return verificationToken; //none of existing verification tokens is equals to the newly created one
                }
            } while (true);
        }

        public static int GenerateCustomerNumber(Type controllerType)
        {
            Random random = new Random();
            int customerNumber;

            do
            {
                customerNumber = random.Next();

                if (controllerType == typeof(NaturalPersonController))
                {
                    if (!NaturalPersonController._naturalPersonInterface.customerNumberExists(customerNumber))
                        return customerNumber; //none of existing customer numbers is equals to the newly created one
                }
                else if (controllerType == typeof(JuridicalPersonController))
                {
                    if (!JuridicalPersonController._juridicalPersonInterface.customerNumberExists(customerNumber))
                        return customerNumber; //none of existing customer numbers is equals to the newly created one
                }

            } while (true);
        }
    }
}
