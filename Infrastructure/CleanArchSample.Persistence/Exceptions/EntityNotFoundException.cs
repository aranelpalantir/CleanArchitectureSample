
using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Persistence.Exceptions
{
    internal class EntityNotFoundException() : BaseRepositoryException($"İşlem yapılacak kayıt bulunamadı!")
    {
    }
}
