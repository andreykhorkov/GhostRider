using System;
using DefaultNamespace;
using UnityEngine;

namespace Models
{
    public class FakeStravaActivityDataLoader : IActivityLoader
    {
        async Awaitable<string> IActivityLoader.GetActivityData(string url, params Tuple<string, string>[] headers)
        {
            var json = Resources.Load<TextAsset>(url);
            return json.text;
        }
    }
}