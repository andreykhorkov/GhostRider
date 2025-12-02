using System;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class HttpManager : IHTTPManager
    {
        async Awaitable<string> IHTTPManager.GetRequestAsync(string url, params Tuple<string, string>[] headers)
        {
            using (var webRequest = UnityWebRequest.Get(new Uri(url)))
            {
                webRequest.certificateHandler = new BypassCertificate();

                foreach (var header in headers)
                {
                    webRequest.SetRequestHeader(header.Item1, header.Item2);
                }

                var operation = webRequest.SendWebRequest();

                while (!operation.isDone)
                {
                    await Awaitable.EndOfFrameAsync();
                }

                if (webRequest.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(webRequest.error);
                    return null;
                }

                return webRequest.downloadHandler.text;
            }
        }
    }
}