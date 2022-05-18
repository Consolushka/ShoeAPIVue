using System;

namespace Shop.Data.Models
{
    public class Log: Base
    {
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; } //XML
        public string LogEvent { get; set; }
    }
}