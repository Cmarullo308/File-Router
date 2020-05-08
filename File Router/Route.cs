using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Router
{
    public class Route : IComparable<Route>
    {
        public string name;
        public string sourceFolder;
        public string destinationFolder;

        public Route(string name, string sourceFolder, string destinationFolder)
        {
            this.name = name;
            this.sourceFolder = sourceFolder;
            this.destinationFolder = destinationFolder;
        }

        public int CompareTo(Route other)
        {
            return this.name.CompareTo(other.name);
        }
    }
}
