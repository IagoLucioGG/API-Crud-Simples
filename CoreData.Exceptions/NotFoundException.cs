using System;
using CoreData.Models.ResponseModel;

namespace CoreData.Exceptions
{
    public class NotFoundException<T> : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(ResponseModel<T> response)
            : base(response.Mensagem)
        {
        }

        public NotFoundException(ResponseModel<T> response, Exception innerException)
            : base(response.Mensagem, innerException)
        {
        }
    }
}
