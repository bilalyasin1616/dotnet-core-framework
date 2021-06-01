using System;
using System.Collections.Generic;

namespace Framework.Exceptions
{
    public class CustomException : Exception
    {
        public List<string> Errors { get; set; }
        public List<string> Warnings { get; set; }
        public CustomException(string msg, List<string> errors = null, List<string> warnings = null) : base(msg)
        {
            Errors = errors;
            Warnings = warnings;
        }
    }
}
