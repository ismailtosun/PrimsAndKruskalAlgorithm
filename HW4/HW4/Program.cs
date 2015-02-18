using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace HW4
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Graph g1 = new Graph();
            StreamReader readGraph = new StreamReader("D:\\graph.txt");
           
            List<string> satirlar = new List<string>();
            string str = readGraph.ReadLine();
            while (str!=null)//dosyadaki bütün satırlar okunur
            {
                satirlar.Add(str);
                str = readGraph.ReadLine();
            }
            //for (int i = 0; i < satirlar.Count; i++)          
            //{
            //    Console.WriteLine(satirlar[i]);
            //}

            string[] vertex = satirlar[0].Split(' ');//ilk satır da vertexleri bul ve graph a ekle
            for (int i = 0; i < vertex.Length; i++)
            {
                g1.insertVertex(vertex[i]);
            }
            satirlar.Remove(satirlar[0]);

            for (int i = 0; i < satirlar.Count; i++)//diğer satırlarda edgeler bulunur ve graph a eklenir
            {
                string[] edge = satirlar[i].Split(' ');
                g1.insertEdge(edge[0], edge[1], Int32.Parse(edge[2]));
            }


            

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            g1.display();//okunan dosyaya göre oluşturulan graph görüntülenir


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Program obj = new Program();
            Graph mstPrim = new Graph();//prim algoritması ile graph ın minimum spanning tree sini bulma ve görüntüleme
            obj.PrimsAlgorithm(g1, mstPrim);
            Console.WriteLine("Prim Algoritması ile Minimum Spanning Tree");
            mstPrim.display();


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Graph mstKruskal = new Graph();// kruskal algoritması ile
            obj.KrustalsAlgorithm(g1,mstKruskal);
            Console.WriteLine("Kruskal Algoritması ile Minimum Spanning Tree");
            mstKruskal.display();

            Console.ReadLine();
            readGraph.Close();
        }

        
        public void PrimsAlgorithm(Graph gr1,Graph mst)
        {
            List<Vertex> vt = new List<Vertex>();
            List<Edge> et = new List<Edge>();
            vt.Add(gr1.head);
            for (int i = 0; i < gr1.vertexCount()-1; i++)
            {
                Edge minEdge = findMinEdge(vt);
                if (minEdge!=null)
                {
                    Vertex vx = gr1.findVertex(minEdge.VertexId);
                    vt.Add(vx);
                    et.Add(minEdge);
                }

            }

            for (int i = 0; i < vt.Count; i++)
            {
                mst.insertVertex(vt[i].Id);
            }
            for (int i = 0; i < et.Count; i++)
            {
                string start = gr1.startEdge(et[i]).Id;
                mst.insertEdge(start, et[i].VertexId, et[i].Weight);
            }

        }

        public Edge findMinEdge(List<Vertex> vt)
        {
            Edge min;
            Edge iterator = vt[0].EdgeLink;
            List<string> vertices = new List<string>();
            for (int i = 0; i < vt.Count; i++)
            {
                vertices.Add(vt[i].Id);
            }
            for (int i = 0; i < vt.Count; i++)
            {
                iterator = vt[i].EdgeLink;
                while (iterator != null && vertices.Contains(iterator.VertexId))
                {
                    iterator = iterator.Next;
                }
            }
            
            if (iterator==null)
            {
                return null;
            }
            else
            {
                min = iterator;
            }

            for (int i = 0; i < vt.Count; i++)
            {
                iterator = vt[i].EdgeLink;
                while (iterator != null)
                {
                    if (!vertices.Contains(iterator.VertexId) && iterator.Weight < min.Weight)
                    {
                        min = iterator;
                    }
                    iterator = iterator.Next;
                }

            }
            return min;
        }

        public void quickSortEdgesList(List<Edge> edges,int start,int end)//recursive olarak edgeler sıralanır
        {
            if (start<end)
            {
                int s = qsPartition(edges,start,end);//döndürülen indis e göre list ikiye bölünerek tekrar karşılaştırılır
                quickSortEdgesList(edges,start,s-1);
                quickSortEdgesList(edges,s+1,end);
            }
            
        }
        public int qsPartition(List<Edge> list,int start,int end)//sıralama işlemi bu metotda yapıır
        {
            Edge p = list[start];
            int i = start;
            int j = end;
            while (i < j)
            {
                while (list[i].Weight <= p.Weight)//edgelerin weight lerinin büyüklüğüne göre indislerde artma yapılır
                    i += 1;
                while (list[j].Weight > p.Weight)
                    j -= 1;
                Edge temp = list[i];//swap(list[i],list[j])
                list[i] = list[j];
                list[j] = temp;
                
            }
            Edge temp1 = list[i];//swap(list[i],list[j])
            list[i] = list[j];
            list[j] = temp1;

            Edge temp2 = list[start];//swap(list[0],list[j])
            list[start] = list[j];
            list[j] = temp2;
            return j;//listeyi ikiye bölen indistir(i de döndürülebilir, işlem sonunda ikisi birbirine eşittir)
        }

        public bool isCyclic(Graph graph, Edge edge)//edgeler sıralı olarak eklenileceği için bir vertex e sadece bir edge gelmesi cyclic olmaması için gerek ve yeter şart olacaktır.
        {
            Vertex iterator = graph.head;
            Edge edgeIterator;
            while (iterator!=null)
            {
                edgeIterator = iterator.EdgeLink;
                while (edgeIterator!=null)
                {
                    if (edgeIterator.VertexId == edge.VertexId)
                    {
                        return true;
                    }
                    edgeIterator = edgeIterator.Next;
                }
                iterator = iterator.Next;
            }
            return false;
        }

        

        public void KrustalsAlgorithm(Graph g1, Graph mst)
        {

            List<Edge> edges = new List<Edge>();
            edges = returnEdgesList(g1);//graph taki bütün edgeleri list şeklinde tutalım
            quickSortEdgesList(edges, 0, edges.Count - 1);//Quick sort algoritması ile weight lerine göre sıralayalım

            Vertex iterator = g1.head;//graph taki bütün vertexleri min spannin tree ye eklemeliyiz öncelikli olarak
            while (iterator!=null)
            {
                mst.insertVertex(iterator.Id);
                iterator = iterator.Next;
            }

            //daha sonra hangi edge in ekleneceğine karar verilir
            int eCounter = 0;
            int k = 0;//işlem yapılmış edge sayısı
            while (eCounter<g1.vertexCount()-1)
            {
                Edge e1 = edges[k];
                if (!isCyclic(mst,e1))//sıralı listedeki k indisindeki eleman eklendiğinde cyclic olmuyorsa
                {
                    mst.insertEdge(g1.startEdge(edges[k]).Id,edges[k].VertexId,edges[k].Weight);
                    eCounter++;
                }
                k++;
            }
        }
        public List<Edge> returnEdgesList(Graph g1)//graph taki edgeleri liste olarak döndürür
        {
            Vertex vIterator = g1.head;
            Edge eIterator;
            List<Edge> edges = new List<Edge>();
            while (vIterator != null)
            {
                eIterator = vIterator.EdgeLink;
                while (eIterator != null)
                {
                    edges.Add(eIterator);
                    eIterator = eIterator.Next;
                }
                vIterator = vIterator.Next;
            }
            return edges;
        }
        
    }
}
