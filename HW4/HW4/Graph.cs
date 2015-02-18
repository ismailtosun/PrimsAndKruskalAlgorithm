using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW4
{
    class Graph
    {
        public Vertex head;
        public Vertex findVertex(string id)
        {
            Vertex iterator = head;
            while (iterator!=null)
            {
                if (iterator.Id.CompareTo(id)==0)
                {
                    return iterator;
                }
                iterator = iterator.Next;
            }
            return null;
        }
        public int vertexCount()
        {
            Vertex iterator = head;
            int count = 0;
            while (iterator!=null)
            {
                count++;
                iterator = iterator.Next;
            }
            return count;
        }
        public void insertVertex(string id)
        {
            if (head==null)
            {
                head = new Vertex(id);
            }
            else
            {
                Vertex iterator = head;
                while (iterator.Next != null)
                {
                    iterator = iterator.Next;
                }
                iterator.Next = new Vertex(id);
            }
        }
        public void insertEdge(string start, string vertexId, int weight)
        {
            Vertex startVertex = findVertex(start);
            Edge iterator = startVertex.EdgeLink;
            if (iterator==null)
            {
                startVertex.EdgeLink = new Edge(vertexId,weight);
            }
            else
            {
                while (iterator.Next!=null)
                {
                    iterator = iterator.Next;
                }
                iterator.Next = new Edge(vertexId, weight);
            }
        }
        public void display()
        {
            Vertex iterator = head;
            Edge iteratorEdge;
            while (iterator!=null)
            {
                Console.Write(iterator.Id+"\t");
                iteratorEdge = iterator.EdgeLink;
                while (iteratorEdge!=null)
                {
                    Console.Write(iteratorEdge.VertexId+" "+iteratorEdge.Weight+"   ");
                    iteratorEdge = iteratorEdge.Next;
                }
                iterator = iterator.Next;
                Console.WriteLine();
            }
        }

        public Vertex startEdge(Edge e1)
        {
            Vertex iterator = head;
            Edge edgeIterator;
            while (iterator!=null)
            {
                edgeIterator = iterator.EdgeLink;
                while (edgeIterator!=null)
                {
                    if (edgeIterator==e1)
                    {
                        return iterator;
                    }
                    edgeIterator = edgeIterator.Next;
                }
                iterator = iterator.Next;
            }
            return null;
        }

        public void deleteEdge(Edge e1)
        {
            Vertex iterator = head;
            Edge edgeIterator;
            Edge prevEdge = iterator.EdgeLink;
            while (iterator != null)
            {
                edgeIterator = iterator.EdgeLink;
                while (edgeIterator != null)
                {
                    if (edgeIterator == e1)
                    {
                        if (edgeIterator == iterator.EdgeLink)
                            iterator.EdgeLink = edgeIterator.Next;
                        else
                            prevEdge.Next = edgeIterator.Next;
                    }
                    prevEdge = edgeIterator;
                    edgeIterator = edgeIterator.Next;
                }
                iterator = iterator.Next;
            }
        }

        

    }
}
