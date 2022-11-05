using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Model.CustomException
{
    public class UserCreationConflictException : ObjectResult
    {
        public UserCreationConflictException(string message) : base(message) { }
    }
}
