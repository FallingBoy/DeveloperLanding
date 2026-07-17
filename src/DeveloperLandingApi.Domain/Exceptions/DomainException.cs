using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Domain.Exceptions
{
    // Исключение, которое будет использоваться для обработки ошибок в доменной логике. Оно наследуется от базового класса Exception и может быть использовано для передачи сообщений об ошибках.
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {

        }
    }
}
