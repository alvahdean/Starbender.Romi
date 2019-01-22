namespace Starbender.Romi.Contracts
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IDeviceDriverFile
    {
        string ContentDisposition { get; }

        string ContentType { get; }

        string FileName { get; }

        IHeaderDictionary Headers { get; }

        long Length { get; }

        string Name { get; }

        void CopyTo(Stream target);

        Task CopyToAsync(Stream target, CancellationToken cancellationToken);

        Stream OpenReadStream();
    }
}