using System;
using System.Collections.Generic;
using System.Text;

namespace _8LMBackend.Service.DTO
{
    public class dtoPage
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeID { get; set; }
        public string JSON { get; set; }
        public string HTML { get; set; }
        public string PreviewUrl { get; set; }

        public List<dtoPageTag> tags { get; set; }
    }

    public class WebClick
    {
        public string date { get; set; }
        public int clicks { get; set; }
    }

    public class WebClickByHour
    {
        public string date { get; set; }
        public int[] clicks { get; set; }
    }

    public class EPageStat
    {
        public string date { get; set; }
        public int views { get; set; }
        public int clicks { get; set; }
    }

    public class EPageStatByHour
    {
        public string date { get; set; }
        public int[] views { get; set; }
        public int[] clicks { get; set; }
    }
}
