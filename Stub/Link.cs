using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HDManager.Stub
{
    /// <summary>
    /// Representation of a link for checked list box
    /// </summary>
    public class Link
    {
        public string Filename { get; set; }
        public string Linkname { get; set; }

        public Link(string filename, string linkname)
        {
            Filename = filename;
            Linkname = linkname;
        }

        public override string ToString()
        {
            return Filename;
        }
    }
}
