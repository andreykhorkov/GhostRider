using System;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IHTTPManager
    {
        Awaitable<string> GetRequestAsync(string url, params Tuple<string, string>[] headers);
    }
}