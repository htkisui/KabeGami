using KabeGami.Domain.Images;

namespace KabeGami.Application.Common.Interfaces.Persistence;
public interface IImageRepository
{
    Task AddAsync(Image image);
}
