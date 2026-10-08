using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Models
{
    public class MBook
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public MBook() { }
        public MBook(int Id, string Title) {
            this.Id = Id;
            this.Title = Title;
        }

    }
}
