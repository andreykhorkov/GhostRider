using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            // Всегда возвращаем true, то есть игнорируем ошибки сертификата
            return true;
        }
    }
}