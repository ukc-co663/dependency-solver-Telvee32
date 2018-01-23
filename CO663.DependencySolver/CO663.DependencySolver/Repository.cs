using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CO663.DependencySolver
{
    public class Repository
    {
        public List<Package> Packages { get; set; }

        public Repository(string json)
        {
            // parse JSON and construct package list
            Packages = JsonConvert.DeserializeObject<List<Package>>(json);
        }
    }
}
