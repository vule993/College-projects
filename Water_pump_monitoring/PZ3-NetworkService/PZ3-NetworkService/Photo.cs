using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ3_NetworkService
{
    public class Photo
    {
        public String imageUri { get; set; }

        public Photo()
        {
            imageUri = "";
        }

        public Photo(String uri)
        {
            imageUri = uri;
        }
    }
}
