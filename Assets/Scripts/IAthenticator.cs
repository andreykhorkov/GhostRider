using UnityEngine;

namespace DefaultNamespace
{
    public interface IAthenticator
    {
        void Authorize(string clientId);
        Awaitable<string> ExchangeCodeForToken(string code);
    }
}