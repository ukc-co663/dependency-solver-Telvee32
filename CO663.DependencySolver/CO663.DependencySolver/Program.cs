using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;

namespace CO663.DependencySolver
{
    class Program
    {
        static void Main(string[] args)
        {
            string repositoryPath;
            string initialPath;
            string constraintsPath;
            if (args.Any())
            {
                // running in docker sandbox
                repositoryPath = args[0];
                initialPath = args[1];
                constraintsPath = args[2];
            }
            else
            {
                // running in Visual Studio, setting command line arguments is not worth the effort
                var basePath = @"D:/Users/Kyle/Documents/co663-depsolver-tests/example-0/";
                repositoryPath = basePath + "repository.json";
                initialPath = basePath + "initial.json";
                constraintsPath = basePath + "constraints.json";
            }

            Repository repo = new Repository(File.ReadAllText(repositoryPath));
            List<string> initial = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(initialPath));
            List<string> constraints = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(constraintsPath));

            // TODO - do something with this stuff
        }
    }
}
