using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ImageQuantization
{
   /* public struct RGBPixel
    {
        public byte red, green, blue;
    }**/
    class Encryption 
    {
        string initial_seed;
        int tap_position ,add_tap;
        byte[] initial_seed_arr  ;
        byte [] inc_value_byte ;
        static int [] inc_value_int ;
        int arr_real_size, arr_add_size , x, rgb;
        int hight, width;
       
        public Encryption()
        {
            initial_seed = "";
            tap_position = add_tap = 0;
            arr_real_size = arr_add_size = 0;
            hight = width =rgb= 0;
            x = 23;
            inc_value_int = new int [3];
            inc_value_byte = new byte[3];
        }
        public Encryption(string str , int t)
        {
            initial_seed = str;
            tap_position = t ;
            add_tap = t + 24;
            arr_real_size = str.Length;
            arr_add_size = arr_real_size + 24;
            hight = width = rgb = 0;
            x = 23;
            inc_value_int = new int[3];
            inc_value_byte = new byte[3];
        }

        public void convert_str_to_arr ()
        {
            
            initial_seed_arr = new byte[arr_add_size];

            for (int i=0;i<initial_seed.Length;i++)
            {
                if (initial_seed[i] == '0')
                    initial_seed_arr[arr_add_size -1] = 0;

                else if (initial_seed[i] == '1')
                    initial_seed_arr[arr_add_size-1] = 1;

                else
                    MessageBox.Show(" please Enter only binary password ");
                arr_add_size--;
            }
            arr_add_size = arr_real_size + 24;
            
            
        }
        public void shift()
        {
          //  inc_value_int = 0;
            
            for (int i = 0; i < 8; i++)
            {
                if (initial_seed_arr[arr_add_size - 1] == initial_seed_arr[add_tap ])
                {
                    initial_seed_arr[x] = 0;
                }
                else
                {
                    initial_seed_arr[x] = 1;
                }
                
                arr_add_size--;
                add_tap--;
                x--;
            }
            

        }
        public void extract ()
        {
            inc_value_int[0] = inc_value_int[1] = inc_value_int[2] = 0;

            for (int i=0;i<3;i++)
            {
                shift();

                int j = arr_add_size -1 ;
               //  j = j - (arr_real_size - 8) -7;
                j = j - (arr_real_size - 1);
                int a = 1;
                for (int z = 0; z <8; z++)
                {
                    inc_value_int[rgb] += initial_seed_arr[j] * a;
                    a = a * 2;
                    j++;

                }

               // inc_value_byte[rgb] = (byte)inc_value_int[rgb];
                rgb++;

            }

        }
        public void update ()
        {
            for (int i=0;i<arr_real_size;i++)
            {
                initial_seed_arr[i + 24] = initial_seed_arr[i];
            }
            arr_add_size = arr_real_size + 24;
            add_tap = tap_position + 24;
            x = 23;
            rgb = 0;
        }
        public void enc (RGBPixel[,]  img_matrix ,int width , int hight)
        {
            for (int i=0 ; i < hight ; i++)
            {
                for (int j=0; j<width; j++)
                {
                    extract();
                    img_matrix[i, j].red =(byte)( (int) img_matrix[i, j].red ^ inc_value_int[0]);
                    img_matrix[i, j].green = (byte)((int)img_matrix[i, j].green ^ inc_value_int[1]);
                    img_matrix[i, j].blue = (byte)((int)img_matrix[i, j].blue ^ inc_value_int[2]);

                    update();
                    
                }
            }
           
            //return img_matrix;
        }
        
        ~Encryption()
        {
            
        }

    }
}
