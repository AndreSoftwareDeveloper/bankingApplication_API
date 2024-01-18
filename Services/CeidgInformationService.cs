using System.Diagnostics;

class CeigdInformationService
{
    public static async Task<string> CallCeidgApi()
    {
        string apiUrl = "https://dane.biznes.gov.pl/api/ceidg/v2/firma?nip=5250008028";
        string accessToken = "eyJraWQiOiJjZWlkZyIsImFsZyI6IkhTNTEyIn0.eyJnaXZlbl9uYW1lIjoiQW5kcnplaiIsInBlc2VsIjoiMDEyNjIzMDA3OTQiLCJpYXQiOjE3MDE0MTk4MDYsImZhbWlseV9uYW1lIjoiU3pyZWRlciIsImNsaWVudF9pZCI6IlVTRVItMDEyNjIzMDA3OTQtQU5EUlpFSi1TWlJFREVSIn0.Z3WEZvXoRKHbt5HJd1awi7WGO-iJ4BGAv1SqGCU-beTkTwKR1ZApp267PpfZgiOzFS299uuqZ-6j3qp2uyHo9g";
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

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            string response = output + "\n" + error;

            process.WaitForExit();
            return response;
        }
    }
}
