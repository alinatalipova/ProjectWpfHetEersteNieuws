using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediabank_GUI.Models
{
    //KLASSE DIE ARTIKEL VOORSTELT 
    public class Article
    {
        public string Title { get; set; }
        public string ID { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
        public bool AuthToPublish { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}

