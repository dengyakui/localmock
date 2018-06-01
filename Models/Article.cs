using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace localmock.Models
{
    public class Article
    {

        public int importance { get; set; }
        public string id { get; set; }
        public string author { get; set; }
        public string source_name { get; set; }
        public string category_item
        {
            get;
            set;
        }
        public bool comment_disabled { get; set; }
        public string content { get; set; }
        public string content_short { get; set; }
        public string display_time { get; set; }
        public string image_uri { get; set; }
        public string platforms { get; set; }
        public string source_uri { get; set; }
        public string status { get; set; }
        public string tags { get; set; }
        public string title { get; set; }

    }
}
