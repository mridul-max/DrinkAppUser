using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Model.CustomException
{
    public class EntryNotFoundException : ObjectResult
    {
        public EntryNotFoundException(string message) : base(message) { }
    }
}
