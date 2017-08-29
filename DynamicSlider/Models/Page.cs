using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicSlider.Models
{
    public class Page
    {
        public int Id { get; set; }
        public String ImgPath { get; set; }
        public String Heading { get; set; }
        public String Text { get; set; }
        public int Catagory { get; set; }
    }
}