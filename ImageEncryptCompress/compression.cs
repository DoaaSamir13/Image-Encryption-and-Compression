using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;


namespace ImageQuantization
{
    public class node
    {
        int value;
        node left, right;
        public node()
        {
            left = right = null;
        }
        public node(int val)
        {
            value = val;
            left = right = null;
        }
        public void set(int value)
        {
            this.value = value;

        }
        public void set_pointers (node first , node second)
        {
            this.left = second;
            this.right = first;
        }
        public int get ()
        {
            return value;
        }
    }
   public class compression
    {
        int [,] red = new int[2,256];
        int[,] green = new int[2, 256];
        int[,] blue = new int[2, 256];
        
        
        public compression ()
        {

        }
        public void count_color (RGBPixel [,]img_matrix , int width , int hight)
        {
            for (int i=0;i<=255;i++)
            {
                red[0, i] = i;
                green[0, i] = i;
                blue[0, i] = i;
            }
            for (int i=0; i<hight; i++)
            {
                for (int j=0; j< width; j++)
                {
                    
                    red[1,img_matrix[i, j].red]++;
                    green[1,img_matrix[i, j].green]++;
                    blue[1,img_matrix[i, j].blue]++;
                }
            }

           
        }
        public void final_tree ()
        { 
            node red_root, green_root, blue_root;
            red_root = sort(red);
            green_root = sort(green);
            blue_root = sort(blue);
        }
        public node sort (int [,] color_arr)
        {
            node root;
            PriorityQueue<int, node> RGB = new PriorityQueue<int, node>();
            for (int i=0; i<=255;i++)
            {
                if (color_arr[1, i] > 0)
                {
                    node color_value = new node(color_arr[0, i]);
                    RGB.Enqueue( color_arr[1, i], color_value);
                }
            }
            
          root = huffman(RGB);
            return root;

        }
        public node huffman (PriorityQueue<int , node> RGB)
        {
            KeyValuePair<int, node> first_child, second_child , root;
            node parent_pixels = new node();
            int pixels;

            while (RGB.Count > 1)
            {
                
                first_child = RGB.Dequeue();
                second_child = RGB.Dequeue();
                parent_pixels = new node();
                pixels = first_child.Key + second_child.Key;
                parent_pixels.set_pointers(first_child.Value, second_child.Value);
                parent_pixels.set(pixels);


                RGB.Enqueue(pixels , parent_pixels );
            }
            root = RGB.Dequeue();
           node root_node = root.Value;


            return root_node;
        }

        public void insert(int sum)
        {

        }

        /*
            KeyValuePair<int, int> red_pix_val1, red_pix_val2;
            int pixel_sum, value_sum;
            red_pix_val1= hred.Dequeue();
            red_pix_val2 = hred.Dequeue();
            pixel_sum = red_pix_val1.Key + red_pix_val2.Key;
            value_sum = red_pix_val1.Value + red_pix_val2.Value;


            KeyValuePair<int, int> ptr1 = red_pix_val1;
            
            KeyValuePair<int, int> red_pix_val1, red_pix_val2;
            int pixel_sum, value_sum;
            Dictionary<int, List<int>> huffman_tree = new Dictionary<int, List<int>>();
            List<int> list = new List<int>();
            while (hred.Count > 0)
            {
                
                red_pix_val1 = hred.Dequeue();
                red_pix_val2 = hred.Dequeue();
                pixel_sum = red_pix_val1.Key + red_pix_val2.Key;
                value_sum = red_pix_val1.Value + red_pix_val2.Value;
          
                list.Add(red_pix_val1.Key);
                list.Add(red_pix_val2.Key);
                huffman_tree.Add(pixel_sum, list);

                hred.Enqueue(pixel_sum, value_sum);

            }
            foreach ( int q in huffman_tree.Keys )
            {

            }
            */

    }
}
