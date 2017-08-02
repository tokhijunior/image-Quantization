using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
namespace ImageQuantization
{
    class DistinctColors
    {
        public static Dictionary<RGBPixel, double> getcolor(RGBPixel[,] image)
        {
            Dictionary<RGBPixel, double> colors = new Dictionary<RGBPixel, double>();
            int Height = image.GetLength(0);
            int Width = image.GetLength(1);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (colors.ContainsKey(image[i, j]))
                        continue;
                    else
                        colors.Add(image[i, j], 0);
                }
            }
           // Console.WriteLine(colors.Count());

            return colors;
        }
        public static long counttt(Dictionary<RGBPixel, double> m)
        {
            return m.Count();
        }
        public static RGBPixel [] convert_to_array (Dictionary<RGBPixel, double> colors)
        {
            RGBPixel [] array=new RGBPixel [colors.Count];
            for (int index = 0; index < colors.Count; index++)
            {
               
                    var item = colors.ElementAt(index);
                    RGBPixel itemKey = item.Key;
                    array[index] = itemKey;            
            }
            return array;
        }
        //public double [,]  graph (RGBPixel[] arr)
        //{

        //    double[,] initialgraph = new double[arr.Length ,arr.Length-1];
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        for (int j = 0; j < arr.Length; j++)
        //        {
        //            if (i != j)
        //            {

        //                initialgraph[j, i] = Math.Sqrt(Math.Pow((arr[i].red - arr[j].red), 2) +
        //                                               Math.Pow((arr[i].green - arr[j].green), 2) +
        //                                               Math.Pow((arr[i].blue - arr[j].blue), 2));
        //            }
        //        }
        //    }
        //    return initialgraph;
        //}
    }
}
