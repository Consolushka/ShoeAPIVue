using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeAPIVue.Models
{
    public class Shoe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public DateTime CreationTime { get; set; }
        public string PhotoFileName { get; set; }
    }
}
