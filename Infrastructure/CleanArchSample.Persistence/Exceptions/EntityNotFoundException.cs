
using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Persistence.Exceptions
{
    internal sealed class EntityNotFoundException() : BaseRepositoryException($"İşlem yapılacak kayıt bulunamadı!", ErrorType.NotFound)
    {
    }
}
