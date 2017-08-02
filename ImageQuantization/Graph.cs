using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Graph
    {
        public  struct edge
        {
            public int source, destination;
            public  double weight;
        }

        /// <summary>
        /// getting the distnict color from the image
        /// </summary>
        /// <param name="img">array 2d of image</param>
        /// <returns>list of distnict color</returns>
        public static List<RGBPixel> get_color(RGBPixel[,] img)   //--->O(N^2)
        {
            bool[, ,] iscolor = new bool[256, 256, 256];        //-->t(1) 
            List<RGBPixel> color = new List<RGBPixel>();        //-->t(1) 
            int Height = img.GetLength(0);                      //-->O(1)
            int Width = img.GetLength(1);                       //-->O(1)
            for (int i = 0; i < Height; i++)                    //-->t(N)
            {
                for (int j = 0; j < Width; j++)                 //-->t(N)
                {
                    if (iscolor[img[i, j].red, img[i, j].green, img[i, j].blue] == false)      //-->t(1)
                    {
                        color.Add(img[i, j]);                                                  //-->t(1)
                        iscolor[img[i, j].red, img[i, j].green, img[i, j].blue] = true;        //-->t(1)
                    }
                }
            }
            return color;           
        }

        /// <summary>
        /// getting minimum spanning tree of distnict color 
        /// </summary>
        /// <param name="colors">list of distnict color</param>
        /// <returns>list of MST</returns>
       public static double sum = 0;
        public static List<edge> MST(List<RGBPixel> color)      //-->O(E*D)
        {
              sum = 0;                                        
            List<edge> Mst_edge = new List<edge>();                 //-->t(1)
            int curnt_node = 0;                                     
            int n = color.Count;                                    //-->O(1)
            bool[] visited = new bool[n];                           //-->t(1)
            double[] Weight = new double[n];                        //-->t(1)
            double cost;
            int[] source = new int[n];                              //-->t(1)
            edge obj = new edge();                                  //-->t(1)
            for (int i = 0; i < n; i++)                             //-->O(D)
            {
                Weight[i] = int.MaxValue;                           //Reset all element by max value
            }
            for (int k = 0; k < n - 1; k++)                         //-->O(E)
            {
                visited[curnt_node] = true;                         
                double min_dist = int.MaxValue;
                int min_indx=0;                                        
                         
                for (int  i = 0; i < n; i++)                        //-->O(D)
                {
                    if (!visited[i])       //O(1)
                    {
                        int r = color[curnt_node].red - color[i].red;
                        int g = color[curnt_node].green - color[i].green;
                        int b = color[curnt_node].blue - color[i].blue;
                        cost = (r * r) + (g * g) + (b * b);            //-->t(1)
                        cost=Math.Sqrt(cost);
                        if (cost < Weight[i])                        //-->O(1)
                        {                       //Check that i have minmum weight
                            Weight[i] = cost;               //-->t(1)
                            source[i] = curnt_node;         //-->t(1)
                        }
                        if (Weight[i] < min_dist)                    //-->O(1)
                        {                       //Check that i have minmum weight for this node    
                            min_dist = Weight[i];                   //-->t(1)
                            min_indx = i;                           //-->t(1)
                        }
                    }

                }
                obj.source = source[min_indx];       //-->t(1)
                obj.destination = min_indx;          //-->t(1)
                obj.weight = min_dist;               //-->t(1)
                Mst_edge.Add(obj);                   //-->O(1)
               // min_dist = Math.Sqrt(min_dist);      //-->O(1)
                sum += min_dist;                    //-->t(2)
                curnt_node = min_indx;              //-->t(1)
            }
           

           // System.Windows.Forms.MessageBox.Show(sum.ToString());
            return Mst_edge;
        }
        /// <summary>
        /// Breadth First Search 
        /// </summary>
        /// <param name="s">start Node</param>
        /// <param name="visited">visited array</param>
        /// <param name="new_graph">new_graph</param>
        /// <returns>list of connected graph</returns>
        public static List<int> BFS(int s, bool[] visited, List<List<int>> new_graph)  //O(V+E)
        {
            visited[s] = true;
            Queue<int> Q = new Queue<int>();
            List<int> component = new List<int>();
            Q.Enqueue(s);
            component.Add(s);
            while (Q.Count != 0)        //O(Q.Count)
            {
                int v = Q.Dequeue();        //-->O(1)
                for (int i = 0; i < new_graph[v].Count; i++)  //O( new_graph[v].Count)
                {
                    int u = new_graph[v][i];
                    if (!visited[u])                    //-->O(1)
                    {
                        visited[u] = true;
                        component.Add(u);
                        Q.Enqueue(u);               //-->O(1)
                    }
                }

            }
            return component;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="K">NUM_OF_Cluster</param>
        /// <param name="colors">list of distnict color</param>
        /// <param name="final">list of edges</param>
        /// <returns>list of clusters</returns>
        public static List<List<RGBPixel>> Cluster(int K, List<RGBPixel> colors, List<edge> final)  //O(D(E+V))
        {
            //cut the largest edge with maximum weight by make it equal (-1)   O(K*E)
            for (int i = 0; i < K - 1; i++)                 //-->O(K)  -- K = Number of cluster
            {
                edge obj = new edge();
                double temp = 0;
                int indx = 0;
                for (int j = 0; j < final.Count(); j++)    //-->O(E)
                {
                    if (temp < final[j].weight)            //-->O(1)
                    {
                        temp = final[j].weight;
                        indx = j;
                    }
                }
                obj.source = final[indx].source;
                obj.destination = final[indx].destination;
                obj.weight = -1;
                final[indx]=obj;
            }
            //Make new graph every source with it's destination   
            List<List<int>> new_graph = new List<List<int>>();
            for (int i = 0; i < colors.Count; i++)             //O(D)
            {
                new_graph.Add(new List<int>());
            }
            for (int i = 0; i < final.Count; i++)             //O(E)
            {
                if (final[i].weight != -1)
                {
                    new_graph[final[i].source].Add(final[i].destination);
                    new_graph[final[i].destination].Add(final[i].source);
                }
            }
            // divide connected graph into clusters 
            bool[] visited = new bool[colors.Count];                        //t(1)
            List<List<RGBPixel>> K_cluster = new List<List<RGBPixel>>();   //t(1)
            for (int i = 0; i < K; i++)  //-->O(K)
            {
                new_graph.Add(new List<int>());
                K_cluster.Add(new List<RGBPixel>());
            }
            int k = 0;

            for (int i = 0; i < colors.Count; i++) //O(D(E+V))
            {

                List<int> ilist = new List<int>();
                if (!visited[i])   // new cluster
                {

                    int c = 0;
                    int r = 0, g = 0, b = 0;
                    ilist = BFS(i, visited, new_graph);    //-->O(E+V)
                    for (int l = 0; l < ilist.Count; l++)  //-->O(ilist.Count)
                    {
                        K_cluster[k].Add(colors[ilist[l]]);
                        r += colors[ilist[l]].red;
                        g += colors[ilist[l]].green;
                        b += colors[ilist[l]].blue;
                        c++;
                    }
                    r /= c;
                    g /= c;
                    b /= c;
                    K_cluster[k].Add(new RGBPixel(r, g, b));
                    k++;
                }
            }
            return K_cluster;
        }
        /// <summary>
        /// get every color with new color that will painting the old color 
        /// </summary>
        /// <param name="K_cluster">K_cluster</param>
        /// <returns>list of each color and new color</returns>
        public static RGBPixel[, ,] finalcolor(List<List<RGBPixel>> K_cluster)   //O(N^2)
        {
           RGBPixel[, ,] final = new RGBPixel[256, 256, 256];

           for (int k = 0; k < K_cluster.Count; k++)    //O(K_cluster.Count)
            {
                RGBPixel newcolor =new RGBPixel();
                newcolor = K_cluster[k][K_cluster[k].Count-1] ;
                for (int i = K_cluster[k].Count - 2; i >= 0; i--)   //O(size of K_cluster)
                {
                    int r=K_cluster[k][i].red;
                    int g=K_cluster[k][i].green;
                    int b=K_cluster[k][i].blue;
                    final[r, g, b] = newcolor;
                }
            }
            return final;
        }
        /// <summary>
        /// replace each color with the new color in final list
        /// </summary>
        /// <param name="img">2D array of input image</param>
        /// <param name="final">3D array of input image</param>
        public static void  painting(RGBPixel[,] img,RGBPixel[, ,] final) //O(N^2)
        {
            int Height = img.GetLength(0);
            int Width = img.GetLength(1);
            for (int i = 0; i < Height; i++)  //O(N)
            {
                for (int j = 0; j < Width; j++) //O(N)
                {
                    img[i, j] = final[img[i, j].red, img[i, j].green, img[i, j].blue];
                }
            }
        }

        public static Tuple<double,double> get_mean_stander(List<edge> final,bool [] visited) //O(N)
        {
                    int cnt = 0;
                    double mean = 0,stand=0;
                    for (int l = 0; l < final.Count; l++)  //-->O(N)
                    {
                        if (!visited[l])
                        {
                            mean += final[l].weight;
                            cnt++;
                        }
                    }
                    mean /= cnt;
                    for (int l = 0; l < final.Count; l++)  //-->O(N)
                    {
                        if(!visited[l])
                        stand += (final[l].weight - mean) * (final[l].weight - mean);
                    }
                    stand /= cnt;
                    stand = Math.Sqrt(stand);               //O(1)
            Tuple<double,double> s=new Tuple<double,double> (stand,mean);
            return s;
        }
        public static int Num_cluster (List<edge> final)
        {
            int k = 1;
            bool []visited = new bool[final.Count];
            
            Tuple<double, double> M_S = get_mean_stander(final,visited);
            while(true)
            {
                double temp = -1;
                int temp_indx =-1;
                for(int i=0;i<final.Count;i++)
                {
                    if(!visited[i])
                    {
                         if(temp<Math.Abs(final[i].weight-M_S.Item2))
                         {
                             temp =Math.Abs( final[i].weight-M_S.Item2);
                             temp_indx = i;
                         }
                    }
                }
                if (temp_indx == -1) break;
                visited[temp_indx] = true;
                Tuple<double, double> M_S1 = get_mean_stander(final,visited);
                k++;
                if(Math.Abs(M_S1.Item1-M_S.Item1)<0.0001)
                {
                    break;
                }
                M_S = M_S1;
            }
            return k;
        }
    }
}