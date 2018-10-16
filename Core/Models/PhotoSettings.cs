using System.IO;
using System.Linq;

namespace aspnet_core_angular_proj.Core.Models
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; } 
          public bool isSuported(string fileName){
            return AcceptedFileTypes.Any(s => s == Path.GetExtension(fileName).ToLower());
           
        }
    }
}