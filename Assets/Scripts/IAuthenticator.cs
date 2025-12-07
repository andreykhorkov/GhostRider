using UnityEngine;

namespace DefaultNamespace
{
    public interface IAuthenticator
    {
        void Authorize(string clientId);
        Awaitable<string> ExchangeCodeForToken(string code);
        string RetrieveExchangeCode();
        string AbsoluteUrl { get; }
    }
}