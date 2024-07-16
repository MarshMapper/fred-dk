using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FredDevelopmentKit.Models
{
    // one of the tags returned from the category/tags and category/related_tags endpoint
    public class CategoryTagDto
    {
        public string Name { get; set; }
        public string GroupId { get; set; }
        public string Notes { get; set; }
        public string Created { get; set; }
        public int Popularity { get; set; }
        public int SeriesCount { get; set; }
    }
}
