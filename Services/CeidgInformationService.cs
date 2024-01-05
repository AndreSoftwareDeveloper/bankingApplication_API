using System.Diagnostics;

class CeigdInformationService
{
    public static async Task<string> CallCeidgApi()
    {
        string apiUrl = "https://dane.biznes.gov.pl/api/ceidg/v2/firma?nip=5250008028";
        string accessToken = "Bearer eyJraWQiOiJjZWlkZyIsImFsZyI6IkhTNTEyIn0.eyJnaXZlbl9uYW1lIjoiQW5kcnplaiIsInBlc2VsIjoiMDEyNjIzMDA3OTQiLCJpYXQiOjE3MDE0MTk4MDYsImF1ZCI6IjA0M2Y1MmEyLTAwNDMtNGY5MS1hOTFhLTNmMzYyODU0N2E1NSJ9.KiEo4XCXb0A2geO8ZrKj0KUis3nSjMB3we4MENwa6YK5xxmG-q8LYv2nxbO4s6ZbSwn-c4XY_gDd5QVGOWsY3Iu1ACSlb-49wCTt8X2wX0FehK_HY2w_qYEBX4QLErFq0sQCT2EBplzx6mIr4gyRkqJ8g4v9ToR0XQPtAJaXNsdNv5Rqk6tf_gk7RH-8O_xCfb2TgVvWiXEKkAtnSk4pxGWUNMfqyRjXvl6rvD5cVvHdU3_rA1AInRiyZPfsZM7g7m-cWKzwFrPVwd5R5G6M4ZGcZI3K_FeZISvgrAGF1AxpuMMwTmV7LV3U6w6l7rYH7fNz0qIz0sxqH0gfCEYKw8IorA";
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
        string curlCommand = "curl \"https://dane.biznes.gov.pl/api/ceidg/v2/firma?nip=5250008028\" -H \"Authorization: Bearer eyJraWQiOiJjZWlkZyIsImFsZyI6IkhTNTEyIn0.eyJnaXZlbl9uYW1lIjoiQW5kcnplaiIsInBlc2VsIjoiMDEyNjIzMDA3OTQiLCJpYXQiOjE3MDE0MTk4MDYsImZhbWlseV9uYW1lIjoiU3pyZWRlciIsImNsaWVudF9pZCI6IlVTRVItMDEyNjIzMDA3OTQtQU5EUlpFSi1TWlJFREVSIn0.Z3WEZvXoRKHbt5HJd1awi7WGO-iJ4BGAv1SqGCU-beTkTwKR1ZApp267PpfZgiOzFS299uuqZ-6j3qp2uyHo9g\" --silent --verbose";
               
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
