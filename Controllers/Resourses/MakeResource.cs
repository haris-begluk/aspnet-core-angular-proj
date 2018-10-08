using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AspNetCoreAngularApp.Controllers.Resourses
{
    public class MakeResource : KeyValuePairResource
    {
        public ICollection<KeyValuePairResource> Models { get; set; } 
        public MakeResource()
        {
            Models = new Collection<KeyValuePairResource>();
        }
    }
}