using System;
using CoreData.Models.ResponseModel;

namespace CoreData.Exceptions
{
    public class ConflitoException<T> : Exception
    {
        public ConflitoException()
        {
        }

        public ConflitoException(ResponseModel<T> response)
            : base(response.Mensagem)
        {
        }

        public ConflitoException(ResponseModel<T> response, Exception innerException)
            : base(response.Mensagem, innerException)
        {
        }
    }
}
