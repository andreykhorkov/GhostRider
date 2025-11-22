using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class Test : MonoBehaviour
    {
        public class BypassCertificate : CertificateHandler
        {
            protected override bool ValidateCertificate(byte[] certificateData)
            {
                // Всегда возвращаем true, то есть игнорируем ошибки сертификата
                return true;
            }
        }

        private const string m_StravaPath = "https://www.strava.com";
        private const string m_OuthAuthorize = "oauth/authorize";
        private const string m_ClientId = "185672";
        private const string redirectUri = "https://andreykhorkov.github.io/GhostRider/";
        private const string m_ClientSecret = "25e0a95343482d3c10552d1b28bef71aee6a9bed";

        private string m_accessToken;


        private void Awake()
        {
            Application.deepLinkActivated += OnDeepLinkActivated;
        }

        private void OnDestroy()
        {
            Application.deepLinkActivated -= OnDeepLinkActivated;
        }

        private void OnDeepLinkActivated(string obj)
        {
            Debug.Log($"Deeplink: {obj}");
            var match = Regex.Match(obj, @"[?&]code=([^&]+)");

            if (match.Success)
            {
                string code = match.Groups[1].Value;
                Debug.Log($"Code: {code}");
                _ = Init(code);
            }

            //https://www.strava.com/api/v3/athlete/activities

        }

        private async Awaitable<string> ExchangeCodeForToken(string code)
        {
            string url = "https://www.strava.com/oauth/token";
            WWWForm form = new WWWForm();
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

                string responseText = webRequest.downloadHandler.text;
                Debug.Log("Token response: " + responseText);

                // Парсим access_token из JSON
                var match = Regex.Match(responseText, "\"access_token\":\"([^\"]+)\"");
                if (match.Success)
                    return match.Groups[1].Value;
                return null;
            }
        }


        private async Awaitable Init(string code)
        {
            m_accessToken = await ExchangeCodeForToken(code);

            var info = await GetRequestAsync("https://www.strava.com/api/v3/athlete/activities",
                new Tuple<string, string>("Authorization", $"Bearer {m_accessToken}"));
            Debug.Log(info);
        }

        private void Start()
        {
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                OnDeepLinkActivated(Application.absoluteURL);
            }
            else
            {
                Authorize(m_ClientId);
            }
        }

        private void Authorize(string clientId)
        {
            var url = $"{m_StravaPath}/{m_OuthAuthorize}?client_id={clientId}&response_type=code&redirect_uri={Uri.EscapeDataString(redirectUri)}&approval_prompt=auto&scope=read_all,profile:read_all,activity:read_all";
            Application.OpenURL(url);
        }

        public static async Awaitable<string> GetRequestAsync(string url, params Tuple<string, string>[] headers)
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