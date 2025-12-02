using System;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IActivityLoader
    {
        Awaitable<string> GetActivityData(string url, params Tuple<string, string>[] headers);
    }
}