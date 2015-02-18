using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW4
{
    class Vertex
    {
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private Vertex next;
        internal Vertex Next
        {
            get { return next; }
            set { next = value; }
        }
        private Edge edgeLink;
        internal Edge EdgeLink
        {
            get { return edgeLink; }
            set { edgeLink = value; }
        }

        public Vertex(string id)
        {
            this.id = id;
            this.next = null;
            this.edgeLink = null;
        }
    }
}
