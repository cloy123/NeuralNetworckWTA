﻿using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NeuralNetworck
{
    class NeiroGraphUtils
    {
        public static void ClearImage(InkCanvas InkCanvas)
        {
            InkCanvas.Strokes.Clear();
        }

        public static int[,] GetArrayFromBitmap(Bitmap image, bool message = false)
        {
            int[,] res = new int[image.Width, image.Height];
            for (int n = 0; n < res.GetLength(0); n++)
            {
                for (int m = 0; m < res.GetLength(1); m++)
                {
                    int color = (image.GetPixel(n, m).R + image.GetPixel(n, m).G + image.GetPixel(n, m).B) / 3;
                    if(color > 0)
                    {
                        res[n, m] = 1;
                    }
                    else
                    {
                        res[n, m] = 0;
                    }
                }
            }
            return res;
        }

        public static Bitmap GetBitmapFromArr(int[,] array)
        {
            Bitmap bitmap = new Bitmap(array.GetLength(0), array.GetLength(1));
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    if (array[x, y] == 0)
                    {
                        bitmap.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        bitmap.SetPixel(x, y, Color.Black);
                    }
                }
            }
            return bitmap;
        }

        public static int[,] CutImageToArray(FrameworkElement Canvas, int width, int height)
        {
            int x1 = 0;
            int y1 = 0;
            int x2 = width;
            int y2 = height;
            
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)Canvas.Width, (int)Canvas.Height, 96d, 96d, System.Windows.Media.PixelFormats.Default);
            
            bitmap.Render(Canvas);
            
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(stream);
            Bitmap b = new Bitmap(stream);

            for (int y = 0; y < b.Height && y1 == 0; y++)
            {
                for (int x = 0; x < b.Width && y1 == 0; x++)
                {
                    if (b.GetPixel(x, y).ToArgb() != -1)
                    {
                        y1 = y;
                    }
                }
            }
            for (int y = b.Height - 1; y >= 0 && y2 == height; y--)
            {
                for (int x = 0; x < b.Width && y2 == height; x++)
                {
                    if (b.GetPixel(x, y).ToArgb() != -1)
                    {
                        y2 = y;
                    }
                }
            }
            for (int x = 0; x < b.Width && x1 == 0; x++)
            {
                for (int y = 0; y < b.Height && x1 == 0; y++)
                {
                    if (b.GetPixel(x, y).ToArgb() != -1)
                    {
                        x1 = x;
                    }
                }
            }
            for (int x = b.Width - 1; x >= 0 && x2 == width; x--)
            {
                for (int y = 0; y < b.Height && x2 == width; y++)
                {
                    if (b.GetPixel(x, y).ToArgb() != -1)
                    {
                        x2 = x;
                    }
                }
            }

            if (x1 == 0 && y1 == 0 && x2 == width && y2 == height)
            {
                return null;
            }

            int size;
            if(x2 - x1 > y2 - y1)
            {
                size = x2 - x1 + 1;
            }
            else
            {
                size = y2 - y1 + 1;
            }

            int dx;
            if (y2 - y1 > x2 - x1)
            {
                dx = ((y2 - y1) - (x2 - x1)) / 2;
            }
            else
            {
                dx = 0;
            }

            int dy;
            if (y2 - y1 < x2 - x1)
            {
                dy = ((x2 - x1) - (y2 - y1)) / 2;
            }
            else
            {
                dy = 0;
            }

            int[,] res = new int[size, size];
            for (int x = 0; x < res.GetLength(0); x++)
            {
                for (int y = 0; y < res.GetLength(1); y++)
                {
                    int pX = x + x1 - dx;
                    int pY = y + y1 - dy;
                    if (pX < 0 || pX >= width || pY < 0 || pY >= height)
                    {
                        res[x, y] = 0;
                    }
                    else
                    {
                        res[x, y] = b.GetPixel(x + x1 - dx, y + y1 - dy).ToArgb() == -1 ? 0 : 1;
                    }
                }
            }
            return res;
        }

        public static int[,] LeadArray(int[,] source, int[,] res)
        {
            for (int n = 0; n < res.GetLength(0); n++)
            {
                for (int m = 0; m < res.GetLength(1); m++) res[n, m] = 0;
            }

            double pX = (double)res.GetLength(0) / (double)source.GetLength(0);
            double pY = (double)res.GetLength(1) / (double)source.GetLength(1);

            for (int n = 0; n < source.GetLength(0); n++)
            {
                for (int m = 0; m < source.GetLength(1); m++)
                {
                    int posX = (int)(n * pX);
                    int posY = (int)(m * pY);
                    if (res[posX, posY] == 0) res[posX, posY] = source[n, m];
                }
            }
            return res;
        }
    }
}
