using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BitmapFilters
{
    public static class ExtBitmap
    {
        private static Bitmap GetArgbCopy(Image sourceImage)
        {
            Bitmap bmpNew = new Bitmap(sourceImage.Width, sourceImage.Height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(bmpNew))
            {
                graphics.DrawImage(sourceImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), GraphicsUnit.Pixel);
                graphics.Flush();
            }

            return bmpNew;
        }
        public static Bitmap CopyToSquareCanvas(this Bitmap sourceBitmap, int canvasWidthLenght)
        {
            float ratio = 1.0f;
            int maxSide = sourceBitmap.Width > sourceBitmap.Height ?
                          sourceBitmap.Width : sourceBitmap.Height;

            ratio = (float)maxSide / (float)canvasWidthLenght;

            Bitmap bitmapResult = (sourceBitmap.Width > sourceBitmap.Height ?
                                    new Bitmap(canvasWidthLenght, (int)(sourceBitmap.Height / ratio))
                                    : new Bitmap((int)(sourceBitmap.Width / ratio), canvasWidthLenght));

            using (Graphics graphicsResult = Graphics.FromImage(bitmapResult))
            {
                graphicsResult.CompositingQuality = CompositingQuality.HighQuality;
                graphicsResult.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsResult.PixelOffsetMode = PixelOffsetMode.HighQuality;

                graphicsResult.DrawImage(sourceBitmap,
                                        new Rectangle(0, 0,
                                            bitmapResult.Width, bitmapResult.Height),
                                        new Rectangle(0, 0,
                                            sourceBitmap.Width, sourceBitmap.Height),
                                            GraphicsUnit.Pixel);
                graphicsResult.Flush();
            }

            return bitmapResult;
        }
        /*
         * Преобразует изображение в grayscale (полутон, оттенки серого)
         */
        public static Bitmap CopyAsGrayscale(this Image sourceImage)
        {
            Bitmap bmpNew = GetArgbCopy(sourceImage);
            BitmapData bmpData = bmpNew.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            IntPtr ptr = bmpData.Scan0;

            byte[] byteBuffer = new byte[bmpData.Stride * bmpNew.Height];

            System.Runtime.InteropServices.Marshal.Copy(ptr, byteBuffer, 0, byteBuffer.Length);

            float rgb = 0;

            for (int k = 0; k < byteBuffer.Length; k += 4)
            {
                rgb = byteBuffer[k] * 0.071f; //Blue
                rgb += byteBuffer[k + 1] * 0.707f; //Green
                rgb += byteBuffer[k + 2] * 0.222f; //Red

                byteBuffer[k] = (byte)rgb;
                byteBuffer[k + 1] = byteBuffer[k];
                byteBuffer[k + 2] = byteBuffer[k];

                byteBuffer[k + 3] = 255;
            }

            Marshal.Copy(byteBuffer, 0, ptr, byteBuffer.Length);

            bmpNew.UnlockBits(bmpData);

            bmpData = null;
            byteBuffer = null;

            return bmpNew;
        }

        private static Bitmap ConvolutionFilter(Bitmap sourceBitmap,
                                             double[,] filterMatrix,
                                                  double factor = 1,
                                                       int bias = 0,
                                             bool grayscale = false)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            if (grayscale == true)
            {
                float rgb = 0;

                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }

            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;

            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;

                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {

                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            blue += (double)(pixelBuffer[calcOffset]) *
                                    filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   filterMatrix[filterY + filterOffset,
                                                      filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    if (blue > 255)
                    { blue = 255; }
                    else if (blue < 0)
                    { blue = 0; }

                    if (green > 255)
                    { green = 255; }
                    else if (green < 0)
                    { green = 0; }

                    if (red > 255)
                    { red = 255; }
                    else if (red < 0)
                    { red = 0; }

                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                 PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public static Bitmap ConvolutionFilter(this Bitmap sourceBitmap,
                                                double[,] xFilterMatrix,
                                                double[,] yFilterMatrix,
                                                      double factor = 1,
                                                           int bias = 0)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                  PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            double blueX = 0.0;
            double greenX = 0.0;
            double redX = 0.0;

            double blueY = 0.0;
            double greenY = 0.0;
            double redY = 0.0;

            double blueTotal = 0.0;
            double greenTotal = 0.0;
            double redTotal = 0.0;

            int filterOffset = 1;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blueX = greenX = redX = 0;
                    blueY = greenY = redY = 0;

                    blueTotal = greenTotal = redTotal = 0.0;

                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            blueX += (double)(pixelBuffer[calcOffset]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            greenX += (double)(pixelBuffer[calcOffset + 1]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            redX += (double)(pixelBuffer[calcOffset + 2]) *
                                      xFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            blueY += (double)(pixelBuffer[calcOffset]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            greenY += (double)(pixelBuffer[calcOffset + 1]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];

                            redY += (double)(pixelBuffer[calcOffset + 2]) *
                                      yFilterMatrix[filterY + filterOffset,
                                              filterX + filterOffset];
                        }
                    }

                    blueTotal = Math.Sqrt((blueX * blueX) + (blueY * blueY));
                    greenTotal = Math.Sqrt((greenX * greenX) + (greenY * greenY));
                    redTotal = Math.Sqrt((redX * redX) + (redY * redY));

                    if (blueTotal > 255)
                    { blueTotal = 255; }
                    else if (blueTotal < 0)
                    { blueTotal = 0; }

                    if (greenTotal > 255)
                    { greenTotal = 255; }
                    else if (greenTotal < 0)
                    { greenTotal = 0; }

                    if (redTotal > 255)
                    { redTotal = 255; }
                    else if (redTotal < 0)
                    { redTotal = 0; }

                    resultBuffer[byteOffset] = (byte)(blueTotal);
                    resultBuffer[byteOffset + 1] = (byte)(greenTotal);
                    resultBuffer[byteOffset + 2] = (byte)(redTotal);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                  PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public static Bitmap Laplacian3x3Filter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Laplacian3x3, 1.0, 0);
            return resultBitmap;
        }

        public static Bitmap Laplacian5x5Filter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Laplacian5x5, 1.0, 0);
            return resultBitmap;
        }

        public static Bitmap LaplacianOfGaussianFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.LaplacianOfGaussian, 1.0, 0);
            return resultBitmap;
        }

        public static Bitmap Laplacian3x3OfGaussian3x3Filter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Gaussian3x3, 1.0 / 16.0, 0);
            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap, Matrix.Laplacian3x3, 1.0, 0);
            return resultBitmap;
        }

        public static Bitmap Laplacian3x3OfGaussian5x5Filter1(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Gaussian5x5Type1, 1.0 / 159.0, 0);
            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap, Matrix.Laplacian3x3, 1.0, 0);
            return resultBitmap;
        }

        public static Bitmap Laplacian3x3OfGaussian5x5Filter2(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Gaussian5x5Type2, 1.0 / 256.0, 0);
            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap, Matrix.Laplacian3x3, 1.0, 0);
            return resultBitmap;
        }

        public static Bitmap Laplacian5x5OfGaussian3x3Filter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Gaussian3x3, 1.0 / 16.0, 0);
            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap, Matrix.Laplacian5x5, 1.0, 0);
            return resultBitmap;
        }

        public static Bitmap Laplacian5x5OfGaussian5x5Filter1(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Gaussian5x5Type1, 1.0 / 159.0, 0);
            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap, Matrix.Laplacian5x5, 1.0, 0);
            return resultBitmap;
        }

        public static Bitmap Laplacian5x5OfGaussian5x5Filter2(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Gaussian5x5Type2, 1.0 / 256.0, 0);
            resultBitmap = ExtBitmap.ConvolutionFilter(resultBitmap, Matrix.Laplacian5x5, 1.0, 0);
            return resultBitmap;
        }
        public static Bitmap GradientModule(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.GradientModule3x3, 1.0, 0);
            return resultBitmap;
        }
        public static Bitmap LaplacianSecondOrderPlaneMatching(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Laplacian3x3SecondOrderPlaneMatching, 1.0 / 3.0, 0);
            return resultBitmap;
        }
        public static Bitmap PlaneMatchingIn2x2Window(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.PlaneMatchingIn2x2WindowHorizontal, Matrix.PlaneMatchingIn2x2WindowVertical, 1.0 / 2.0, 0);
            return resultBitmap;
        }
        public static Bitmap RobertsCrossFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Roberts3x3Horizontal, Matrix.Roberts3x3Vertical, 1.0, 0);
            return resultBitmap;
        }
        public static Bitmap Sharpness1(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Laplacian3x3SharpnessType1, 1.0, 0);
            return resultBitmap;
        }
        public static Bitmap Sharpness2(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Laplacian3x3SharpnessType2, 1.0, 0);
            return resultBitmap;
        }
        public static Bitmap Sharpness3(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Laplacian3x3SharpnessType3, 1.0, 0);
            return resultBitmap;
        }
        public static Bitmap Sharpness4(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Laplacian3x3SharpnessType4, 1.0, 0);
            return resultBitmap;
        }
        public static Bitmap Approximation(this Bitmap sourceBitmap, double[,] filterMatrix, double factor = 1)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, filterMatrix, factor, 0);
            return resultBitmap;
        }

        public static Bitmap Sobel3x3Filter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Sobel3x3Horizontal, Matrix.Sobel3x3Vertical, 1.0, 0);
            return resultBitmap;
        }

        public static Bitmap PrewittFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Prewitt3x3Horizontal, Matrix.Prewitt3x3Vertical, 1.0, 0);
            return resultBitmap;
        }

        public static Bitmap KirschFilter(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = ExtBitmap.ConvolutionFilter(sourceBitmap, Matrix.Kirsch3x3Horizontal, Matrix.Kirsch3x3Vertical, 1.0, 0);
            return resultBitmap;
        }
        /*
         * Преобразование вида ∇I = |I(i,j) - I(i+1,j)| + |I(i,j) - I(i,j+1)|
         */
        public static Bitmap EdgeDetectionFilter1(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap);
            Color color1, color2; 
            int newColor = 0;

            for (int i = 1; i < resultBitmap.Width - 1; i++)
            {
                for (int j = 1; j < resultBitmap.Height - 1; j++)
                {
                    color1 = sourceBitmap.GetPixel(i, j);
                    color2 = sourceBitmap.GetPixel(i + 1, j);

                    newColor = Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i, j + 1);

                    newColor += Math.Abs(color1.R - color2.R);

                    if (newColor > 255) 
                    {
                        newColor = 255; 
                    }
                    resultBitmap.SetPixel(i, j, Color.FromArgb(newColor, newColor, newColor));
                }
            }
            return resultBitmap;
        }
        /*
         * Преобразование вида ∇I = max(|I(i,j) - I(i+1,j)| , |I(i,j) - I(i,j+1)|)
         */
        public static Bitmap EdgeDetectionFilter2(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap);
            Color color1, color2;
            int newColor = 0;
            int temp1 = 0;
            int temp2 = 0;

            for (int i = 1; i < resultBitmap.Width - 1; i++)
            {
                for (int j = 1; j < resultBitmap.Height - 1; j++)
                {
                    color1 = sourceBitmap.GetPixel(i, j);
                    color2 = sourceBitmap.GetPixel(i + 1, j);

                    temp1 = Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i, j + 1);

                    temp2 = Math.Abs(color1.R - color2.R);

                    newColor = Math.Max(temp1, temp2);

                    if (newColor > 255)
                    {
                        newColor = 255;
                    }
                    resultBitmap.SetPixel(i, j, Color.FromArgb(newColor, newColor, newColor));
                }
            }
            return resultBitmap;
        }
        /*
         * Преобразование вида ∇I = |I(i,j) - I(i+1,j+1)| + |I(i+1,j) - I(i,j+1)|
         */
        public static Bitmap EdgeDetectionFilter3(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap);
            Color color1, color2;
            int newColor = 0;

            for (int i = 1; i < resultBitmap.Width - 1; i++)
            {
                for (int j = 1; j < resultBitmap.Height - 1; j++)
                {
                    color1 = sourceBitmap.GetPixel(i, j);
                    color2 = sourceBitmap.GetPixel(i + 1, j + 1);

                    newColor = Math.Abs(color1.R - color2.R);

                    color1 = sourceBitmap.GetPixel(i + 1, j);
                    color2 = sourceBitmap.GetPixel(i, j + 1);

                    newColor += Math.Abs(color1.R - color2.R);

                    if (newColor > 255)
                    {
                        newColor = 255;
                    }
                    resultBitmap.SetPixel(i, j, Color.FromArgb(newColor, newColor, newColor));
                }
            }
            return resultBitmap;
        }
        /*
         * Преобразование вида ∇I = Σ|I(i,j) - I(i+k,j+L)| в окне 3x3
         */
        public static Bitmap EdgeDetectionFilter4(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap);
            Color color1, color2;
            int newColor = 0;

            for (int i = 1; i < resultBitmap.Width - 1; i++)
            {
                for (int j = 1; j < resultBitmap.Height - 1; j++)
                {
                    color1 = sourceBitmap.GetPixel(i, j);
                    color2 = sourceBitmap.GetPixel(i - 1, j - 1);
                    newColor = Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i, j - 1); 
                    newColor += Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i + 1, j - 1);
                    newColor += Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i + 1, j);
                    newColor += Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i + 1, j + 1);
                    newColor += Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i, j + 1);
                    newColor += Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i - 1, j + 1);
                    newColor += Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i - 1, j);
                    newColor += Math.Abs(color1.R - color2.R);

                    if (newColor > 255)
                    {
                        newColor = 255;
                    }
                    resultBitmap.SetPixel(i, j, Color.FromArgb(newColor, newColor, newColor));
                }
            }
            return resultBitmap;
        }
        /*
         * Преобразование вида ∇I = max |I(i,j) - I(i+k,j+L)| в окне 3x3
         */
        public static Bitmap EdgeDetectionFilter5(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap);
            Color color1, color2;
            int newColor = 0;
            int[] temp = new int[8];

            for (int i = 1; i < resultBitmap.Width - 1; i++)
            {
                for (int j = 1; j < resultBitmap.Height - 1; j++)
                {
                    color1 = sourceBitmap.GetPixel(i, j);
                    color2 = sourceBitmap.GetPixel(i - 1, j - 1);
                    temp[0] = Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i, j - 1);
                    temp[1] = Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i + 1, j - 1);
                    temp[2] = Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i + 1, j);
                    temp[3] = Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i + 1, j + 1);
                    temp[4] = Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i, j + 1);
                    temp[5] = Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i - 1, j + 1);
                    temp[6] = Math.Abs(color1.R - color2.R);

                    color2 = sourceBitmap.GetPixel(i - 1, j);
                    temp[7] = Math.Abs(color1.R - color2.R);

                    newColor = temp.Max<int>();

                    if (newColor > 255)
                    {
                        newColor = 255;
                    }
                    resultBitmap.SetPixel(i, j, Color.FromArgb(newColor, newColor, newColor));
                }
            }
            return resultBitmap;
        }
        /*
        * Функция, выполняющая проверку, является ли точка локальным максимумом в окне 3x3
        */
        public static Bitmap LocalMaximum3x3(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap);
            int[] temp = new int[9];

            for (int i = 1; i < resultBitmap.Width - 1; i++)
            {
                for (int j = 1; j < resultBitmap.Height - 1; j++)
                {
                    temp[0] = sourceBitmap.GetPixel(i, j).R;
                    temp[1] = sourceBitmap.GetPixel(i - 1, j - 1).R;
                    temp[2] = sourceBitmap.GetPixel(i, j - 1).R;
                    temp[3] = sourceBitmap.GetPixel(i + 1, j - 1).R;
                    temp[4] = sourceBitmap.GetPixel(i + 1, j).R;
                    temp[5] = sourceBitmap.GetPixel(i + 1, j + 1).R;
                    temp[6] = sourceBitmap.GetPixel(i, j + 1).R;
                    temp[7] = sourceBitmap.GetPixel(i - 1, j + 1).R;
                    temp[8] = sourceBitmap.GetPixel(i - 1, j).R;
                    if (temp[0] == temp.Max<int>())
                    {
                        resultBitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        resultBitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            return resultBitmap;
        }
        /*
        * Функция, выполняющая проверку, является ли точка локальным максимумом по оси X
        */
        public static Bitmap LocalMaximumX(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap);
            int[] temp = new int[3];

            for (int i = 1; i < resultBitmap.Width - 1; i++)
            {
                for (int j = 1; j < resultBitmap.Height - 1; j++)
                {
                    temp[0] = sourceBitmap.GetPixel(i, j).R;
                    temp[1] = sourceBitmap.GetPixel(i - 1, j).R;
                    temp[2] = sourceBitmap.GetPixel(i + 1, j).R;
                    if (temp[0] == temp.Max<int>())
                    {
                        resultBitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        resultBitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            return resultBitmap;
        }
        /*
        * Функция, выполняющая проверку, является ли точка локальным максимумом по оси Y
        */
        public static Bitmap LocalMaximumY(this Bitmap sourceBitmap)
        {
            Bitmap resultBitmap = new Bitmap(sourceBitmap);
            int[] temp = new int[3];

            for (int i = 1; i < resultBitmap.Width - 1; i++)
            {
                for (int j = 1; j < resultBitmap.Height - 1; j++)
                {
                    temp[0] = sourceBitmap.GetPixel(i, j).R;
                    temp[1] = sourceBitmap.GetPixel(i, j - 1).R;
                    temp[2] = sourceBitmap.GetPixel(i, j + 1).R;
                    if (temp[0] == temp.Max<int>())
                    {
                        resultBitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        resultBitmap.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }
            return resultBitmap;
        }
    }
}