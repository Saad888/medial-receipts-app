using System.Threading.Tasks;

namespace MedicalReceiptsApp.Google
{
    public interface IGoogleAccessTokenReader
    {
        Task<GoogleToken> GetAccessToken();
    }
}
