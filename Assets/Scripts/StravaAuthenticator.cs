using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class StravaAuthenticator : IAthenticator
    {
        private const string m_StravaPath = "https://www.strava.com";
        private const string m_OuthAuthorize = "oauth/authorize";
        private const string m_ClientId = "185672";
        private const string redirectUri = "https://andreykhorkov.github.io/GhostRider/";
        private const string m_ClientSecret = "25e0a95343482d3c10552d1b28bef71aee6a9bed";

        void IAthenticator.Authorize(string clientId)
        {
            var url =
                $"{m_StravaPath}/{m_OuthAuthorize}?client_id={m_ClientId}&response_type=code&redirect_uri={Uri.EscapeDataString(redirectUri)}&approval_prompt=auto&scope=read_all,profile:read_all,activity:read_all";
            Application.OpenURL(url);
        }

        async Awaitable<string> IAthenticator.ExchangeCodeForToken(string code)
        {
            const string url = "https://www.strava.com/oauth/token";
            var form = new WWWForm();
            form.AddField("client_id", m_ClientId);
            form.AddField("client_secret", m_ClientSecret);
            form.AddField("code", code);
            form.AddField("grant_type", "authorization_code");

            using (var webRequest = UnityWebRequest.Post(url, form))
            {
                webRequest.certificateHandler = new BypassCertificate();
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

                var responseText = webRequest.downloadHandler.text;

                // Парсим access_token из JSON
                var match = Regex.Match(responseText, "\"access_token\":\"([^\"]+)\"");
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }

                return null;
            }
        }
    }
}