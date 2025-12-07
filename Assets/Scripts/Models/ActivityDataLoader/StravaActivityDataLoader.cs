using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StravaActivityDataLoader : IActivityLoader
    {
        private readonly IHTTPManager m_HTTPManager;

        public StravaActivityDataLoader(IHTTPManager httpManager)
        {
            m_HTTPManager = httpManager;
        }

        async Awaitable<string> IActivityLoader.GetActivityData(string url, params Tuple<string, string>[] headers)
        {
            return await m_HTTPManager.GetRequestAsync(url, headers);
        }
    }
}