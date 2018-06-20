using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entities
{
    public class OutputMessage
    {
        public string Operation { get; set; }
        public string Message { get; set; }
        public Type ErrorType { get; set; }
        public bool Success { get; set; }
        public string Comment { get; set; }
        public object Object { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Exception Ex { get; set; }
        public OutputMessage(string Message,bool Success,Type ErrorType,string Operation,HttpStatusCode StatusCode)
        {
            this.Operation = Operation;
            this.Message = Message;
            this.Success = Success;
            this.ErrorType = ErrorType;
            this.StatusCode = StatusCode;
        }
        public OutputMessage(string Message, bool Success, Type ErrorType, string Operation, HttpStatusCode StatusCode,string Comment)
        {
            this.Operation = Operation;
            this.Message = Message;
            this.Success = Success;
            this.ErrorType = ErrorType;
            this.StatusCode = StatusCode;
            this.Comment = Comment;
        }
        public OutputMessage(string Message, bool Success, Type ErrorType, string Operation, HttpStatusCode StatusCode,object Object)
        {
            this.Operation = Operation;
            this.Message = Message;
            this.Success = Success;
            this.ErrorType = ErrorType;
            this.StatusCode = StatusCode;
            this.Object = Object;
        }
        public OutputMessage(string Message, bool Success, Type ErrorType, string Operation, HttpStatusCode StatusCode, Exception Ex)
        {
            this.Operation = Operation;
            this.Message = Message;
            this.Success = Success;
            this.ErrorType = ErrorType;
            this.StatusCode = StatusCode;
            this.Ex = Ex;
            Task.Run(()=> Application.Helper.LogException(this.Ex, this.Operation));
            
        }
        public OutputMessage()
        {

        }

    }
    public enum Type
    {
        NoError,
        ForeignKeyViolation,
        Others,
        RequiredFields,
        InsufficientPrivilege    ,
        UnAuthenticated,
        UnAuthorized ,
        NotFound
               

    }

}
