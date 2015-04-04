using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalorieStack.Models
{
    public class Stack
    {
        public string Id { get; set; }

        public List<Day> Days { get; set; }
    }
}