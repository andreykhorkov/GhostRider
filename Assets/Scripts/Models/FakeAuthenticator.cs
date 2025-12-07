using DefaultNamespace;
using UnityEngine;

namespace Models
{
    public class FakeAuthenticator : IAuthenticator
    {
        void IAuthenticator.Authorize(string clientId)
        {
        }

        async Awaitable<string> IAuthenticator.ExchangeCodeForToken(string code)
        {
            return "fakeToken";
        }

        string IAuthenticator.RetrieveExchangeCode()
        {
            return "fakeExchangeCode";
        }

        public string AbsoluteUrl => "fakeAbsoluteUrl";
    }
}