using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW4
{
    class Edge
    {
        private string vertexId;
        public string VertexId
        {
            get { return vertexId; }
            set { vertexId = value; }
        }
        private int weight;
        public int Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        private Edge next;
        internal Edge Next
        {
            get { return next; }
            set { next = value; }
        }

        public Edge(string vertexId, int weight)
        {
            this.vertexId = vertexId;
            this.weight = weight;
            this.next = null;
        }

    }
}
