using System.Diagnostics;
using bankingApplication_API.Services;

class CeigdInformationService
{
    public static async Task<string> CallCeidgApi(long nip)
    {
        string apiUrl = "https://dane.biznes.gov.pl/api/ceidg/v2/firma?nip=" + nip;
        EmailCredentials secrets = EmailMessageService.LoadSecrets();
        string accessToken = secrets.CEIDGToken;
        string result;

        try
        {
            result = await GetCeidgApiResponse(apiUrl, accessToken);
        }
        catch (Exception ex)
        {
            result = "An error occurred while downloading data from CEIDG. \nMore details: " + ex.Message;
        }

        return result;
    }


    static async Task<string> GetCeidgApiResponse(string apiUrl, string accessToken)
    {
        string curlCommand = "curl \"" + apiUrl + "\" -H \"Authorization: Bearer " + accessToken + "\" --silent --verbose";
               
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "curl",
            Arguments = curlCommand,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = startInfo })
        {
            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();
            string response = output + "\n" + error;
            await process.WaitForExitAsync();
            return response;
        }
    }
}
