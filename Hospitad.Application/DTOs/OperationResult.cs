using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospitad.Application.DTOs
{
    public class OperationResult
    {
        #region Fields

        // Necessary
        public readonly bool Succeeded;
        public readonly int StatusCode;
        public readonly LogLevel LogLevel;
        public readonly bool NeedLogging;
        public readonly bool NeedCommit;

        // Optional
        public bool Failed => !Succeeded;        
        public readonly int? EntityId;
        public readonly object Value;
        public readonly string Message;

        #endregion

        #region Ctor

        public OperationResult(bool result)
        {
            Succeeded = result;
            StatusCode = GetStatusCodeByResult(result);
            LogLevel = GetLogLevelByStatusCode(StatusCode);
        }

        // This ctor is used in commit behavior
        public OperationResult(OperationResult operation, bool result)
        {
            Succeeded = result;
            StatusCode = GetStatusCodeByResult(result);
            LogLevel = GetLogLevelByStatusCode(StatusCode);

            Message = operation.Message;
            EntityId = operation.Value.GetType().GetProperty("Id")?.GetValue(operation.Value) as int?;
            Value = operation.Value;
            NeedLogging = operation.NeedLogging;
            NeedCommit = operation.NeedCommit;
        }

        /// This constructor is used for query classes
        public OperationResult(bool result, int statusCode, string message = null, object value = null)
        {
            Succeeded = result;
            StatusCode = statusCode;
            LogLevel = GetLogLevelByStatusCode(statusCode);

            Message = message;
            Value = value;
            NeedLogging = false;
            NeedCommit = false;
        }

        /// This constructor is used in command classes
        public OperationResult(bool result, int statusCode, string message = null, int? entityId = null, object value = null)
        {   
            EntityId = entityId;
            Succeeded = result;
            StatusCode = statusCode;
            LogLevel = GetLogLevelByStatusCode(statusCode);
            Message = message;
            Value = value;
            NeedLogging = true;
            NeedCommit = true;
        }

        #endregion

        #region Methods

        private int GetStatusCodeByResult(bool result)
        {
            if (result)
                return 200;
            else
                return 422;
        }

        private LogLevel GetLogLevelByStatusCode(int statusCode)
        {
            if (200 <= statusCode && statusCode <= 201)
                return LogLevel.Information;
            else if (400 <= statusCode && statusCode <= 422)
                return LogLevel.Error;

            return LogLevel.Trace;
        }

        #endregion
    }
}
