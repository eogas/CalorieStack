using System;
using System.Collections.Generic;

namespace CalorieStack.Models
{
    public class Stack
    {
        public string Id { get; set; }

        public List<Day> Days { get; set; }

        public bool IsReminderDismissed { get; set; }

        // Just throw this here for now.  If we need this functionality elsewhere,
        // break out into some sort of util class
        private const string ID_CHARS = "abcdefghijklmnopqrstuvwxyz1234567890";
        private static Random RAND = new Random();

        public static string GenerateId()
        {
            var id = String.Empty;
            var length = 8;

            while (length > 0)
            {
                id += ID_CHARS[RAND.Next(ID_CHARS.Length)];
                length--;
            }

            return id;
        }
    }
}