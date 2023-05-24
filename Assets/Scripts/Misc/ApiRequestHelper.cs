using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class ApiRequestHelper
{
    public static async Task<string> RequestApiData(string apiURI)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiURI))
        {
            var asyncOperation = webRequest.SendWebRequest();

            while (!asyncOperation.isDone)
                await Task.Yield();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                case UnityWebRequest.Result.DataProcessingError:
                    {
                        Debug.LogError($"Something bad happened while trying to fetch data from api! Aborting... Error : {webRequest.error}");
                        return string.Empty;
                    }
                case UnityWebRequest.Result.Success:
                    {
                        return webRequest.downloadHandler.text;
                    }
                default:
                    {
                        Debug.LogError($"Unhandled web request result! Aborting... Result Type : {webRequest.result}");
                        return string.Empty;
                    }
            }
        }
    }
}
