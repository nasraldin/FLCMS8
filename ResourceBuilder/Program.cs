using System;
using Resources.Concrete;

namespace ResourceBuilder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new Resources.Utility.ResourceBuilder();
            var filePath =
                builder.Create(
                    new DbResourceProvider(
                        @"Data Source=.;Initial Catalog=FalMedia-20161225083617;Integrated Security=True"),
                    summaryCulture: "en-us");
            Console.WriteLine("Created file {0}", filePath);
        }
    }
}