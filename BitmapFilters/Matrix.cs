using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace BitmapFilters
{
    public static class Matrix
    {
        public static string[,] ApproximationHorizontal
        {
            get
            {
                return new string[,]  
                { { "1",  "0",  "-1", }, 
                  { "c",  "0",  "-c", }, 
                  { "1",  "0",  "-1", }, };
            }
        }

        public static string[,] ApproximationVertical
        {
            get
            {
                return new string[,]  
                { { "-1", "-c", "-1", }, 
                  {  "0",  "0",  "0", }, 
                  {  "1",  "c",  "1", }, };
            }
        }

        public static double[,] GradientModule3x3
        {
            get
            {
                return new double[,]  
                { { 0,  0,  0, }, 
                  { 0, -1, -1, }, 
                  { 0,  1,  1, }, };
            }
        }

        public static double[,] Roberts3x3Horizontal
        {
            get
            {
                return new double[,]  
                { { 0,  0,  0, }, 
                  { 0,  1,  0, }, 
                  { 0,  0, -1, }, };
            }
        }

        public static double[,] Roberts3x3Vertical
        {
            get
            {
                return new double[,]  
                { { 0,  0,  0,  }, 
                  { 0,  0,  1,  }, 
                  { 0, -1,  0,  }, };
            }
        }
        public static double[,] PlaneMatchingIn2x2WindowHorizontal
        {
            get
            {
                return new double[,]  
                { {  0,  0,  0,  }, 
                  {  0, -1,  1,  }, 
                  {  0, -1,  1,  }, };
            }
        }
        public static double[,] PlaneMatchingIn2x2WindowVertical
        {
            get
            {
                return new double[,]  
                { {  0,  0,  0,  }, 
                  {  0, -1, -1,  }, 
                  {  0,  1,  1,  }, };
            }
        }
        public static double[,] Laplacian3x3SecondOrderPlaneMatching
        {
            get
            {
                return new double[,]  
                { {  2, -1,  2,  }, 
                  { -1, -4, -1,  }, 
                  {  2, -1,  2,  }, };
            }
        }
        public static double[,] Laplacian3x3SharpnessType1
        {
            get
            {
                return new double[,]  
                { {  0, -1,  0,  }, 
                  { -1,  5, -1,  }, 
                  {  0, -1,  0,  }, };
            }
        }
        public static double[,] Laplacian3x3SharpnessType2
        {
            get
            {
                return new double[,]  
                { { -1,  0, -1,  }, 
                  {  0,  5, -0,  }, 
                  { -1,  0, -1,  }, };
            }
        }
        public static double[,] Laplacian3x3SharpnessType3
        {
            get
            {
                return new double[,]  
                { { -1, -1, -1,  }, 
                  { -1,  9, -1,  }, 
                  { -1, -1, -1,  }, };
            }
        }
        public static double[,] Laplacian3x3SharpnessType4
        {
            get
            {
                return new double[,]  
                { {  1, -2,  1,  }, 
                  { -2,  5, -2,  }, 
                  {  1, -2,  1,  }, };
            }
        }

        public static double[,] Laplacian3x3
        {
            get
            {
                return new double[,]  
                { { -1, -1, -1,  }, 
                  { -1,  8, -1,  }, 
                  { -1, -1, -1,  }, };
            }
        }

        public static double[,] Laplacian5x5
        {
            get
            {
                return new double[,] 
                { { -1, -1, -1, -1, -1, }, 
                  { -1, -1, -1, -1, -1, }, 
                  { -1, -1, 24, -1, -1, }, 
                  { -1, -1, -1, -1, -1, }, 
                  { -1, -1, -1, -1, -1  }, };
            }
        }

        public static double[,] LaplacianOfGaussian
        {
            get
            {
                return new double[,]  
                { {  0,   0, -1,  0,  0 }, 
                  {  0,  -1, -2, -1,  0 }, 
                  { -1,  -2, 16, -2, -1 },
                  {  0,  -1, -2, -1,  0 },
                  {  0,   0, -1,  0,  0 }, };
            }
        }

        public static double[,] Gaussian3x3
        {
            get
            {
                return new double[,]  
                { { 1, 2, 1, }, 
                  { 2, 4, 2, }, 
                  { 1, 2, 1, }, };
            }
        }

        public static double[,] Gaussian5x5Type1
        {
            get
            {
                return new double[,]  
                { { 2, 04, 05, 04, 2 }, 
                  { 4, 09, 12, 09, 4 }, 
                  { 5, 12, 15, 12, 5 },
                  { 4, 09, 12, 09, 4 },
                  { 2, 04, 05, 04, 2 }, };
            }
        }

        public static double[,] Gaussian5x5Type2
        {
            get
            {
                return new double[,] 
                { {  1,   4,  6,  4,  1 }, 
                  {  4,  16, 24, 16,  4 }, 
                  {  6,  24, 36, 24,  6 },
                  {  4,  16, 24, 16,  4 },
                  {  1,   4,  6,  4,  1 }, };
            }
        }

        public static double[,] Sobel3x3Horizontal
        {
            get
            {
                return new double[,] 
                { { -1,  0,  1, }, 
                  { -2,  0,  2, }, 
                  { -1,  0,  1, }, };
            }
        }

        public static double[,] Sobel3x3Vertical
        {
            get
            {
                return new double[,] 
                { {  1,  2,  1, }, 
                  {  0,  0,  0, }, 
                  { -1, -2, -1, }, };
            }
        }

        public static double[,] Prewitt3x3Horizontal
        {
            get
            {
                return new double[,] 
                { { -1,  0,  1, }, 
                  { -1,  0,  1, }, 
                  { -1,  0,  1, }, };
            }
        }

        public static double[,] Prewitt3x3Vertical
        {
            get
            {
                return new double[,] 
                { {  1,  1,  1, }, 
                  {  0,  0,  0, }, 
                  { -1, -1, -1, }, };
            }
        }


        public static double[,] Kirsch3x3Horizontal
        {
            get
            {
                return new double[,] 
                { {  5,  5,  5, }, 
                  { -3,  0, -3, }, 
                  { -3, -3, -3, }, };
            }
        }

        public static double[,] Kirsch3x3Vertical
        {
            get
            {
                return new double[,] 
                { {  5, -3, -3, }, 
                  {  5,  0, -3, }, 
                  {  5, -3, -3, }, };
            }
        }
        public static double GetMatrixElementsSum(double[,] sourseMatrix)
        {
            double sum = 0;
            for (int i = 0; i < sourseMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < sourseMatrix.GetLength(1); j++)
                {
                    sum += sourseMatrix[i, j];
                }
            }
            return sum;
        }
    }
}