using System.Threading;
using System.Threading.Tasks;

namespace TCGPocketDex.SDK.Images;

public interface IImagesClient
{
    Task<string[]> GetCardImageNamesAsync(CancellationToken ct = default);
    string GetCardImageUrl(string name);
}