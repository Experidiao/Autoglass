using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Application.Shared
{
    public class ErrorNotification
    {
        public string Message { get; set; }
        public string Field { get; set; }
    }
    public class NotificationResult
    {
        public bool IsSucess { get; set; }
        public string Mensage { get; set; }
        public ICollection<ErrorNotification> Erros { get; set; }

        public static NotificationResult RequestError(string message, ValidationResult validationResult)
        {
            return new NotificationResult
            {
                IsSucess = false,
                Mensage = message,
             //   Erros = validationResult.ErrorMessage.Select(x => new ErrorNotification { Field = x.PropertyName, Message = x.ErrorMessage }).ToList(),
            };
        }
        public static NotificationResult RequestError<T>(string message, ValidationResult validationResult)
        {
            return new NotificationResult<T>
            {
                IsSucess = false,
                Mensage = message,
            //    Erros = validationResult.ErrorMessage.Select(x => new ErrorNotification { Field = x.PropertyName, Message = x.ErrorMessage }).ToList(),
            };
        }
        public static NotificationResult Fail(string message) =>  new NotificationResult { IsSucess = false, Mensage =message};
        public static NotificationResult Fail<T>(string message) => new NotificationResult { IsSucess = false, Mensage = message };
        public static NotificationResult Ok(string message) => new NotificationResult { IsSucess = true, Mensage = message };
        public static NotificationResult Ok<T>(string message) => new NotificationResult { IsSucess = false, Mensage = message };
    }

    public class NotificationResult<T> : NotificationResult
    {
        public T Data { get; set; }
    }
}
