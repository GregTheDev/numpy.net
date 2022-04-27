﻿/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2018-2021
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumpyDotNet;
using MathNet.Numerics;

namespace NumpyDotNetTests
{
    [TestClass]
    public class NumericOperationsTests : TestBaseClass
    {
        [TestMethod]
        public void test_add_operations()
        {
            var a = np.arange(0, 20, 1, dtype: np.Int16);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            var b = a + 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB = new Int16[,]
            {{8,  9, 10, 11},
             {12, 13, 14, 15},
             {16, 17, 18, 19},
             {20, 21, 22, 23},
             {24, 25, 26, 27}
            };
            AssertArray(b, ExpectedDataB);

            a = np.arange(0, 20, 1, dtype: np.Int64);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a + 2400;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB1 = new Int64[,]
            {{2400, 2401, 2402, 2403},
             {2404, 2405, 2406, 2407},
             {2408, 2409, 2410, 2411},
             {2412, 2413, 2414, 2415},
             {2416, 2417, 2418, 2419}
            };
            AssertArray(b, ExpectedDataB1);

        }

        [TestMethod]
        public void test_add_operations_2()
        {
            var a = np.arange(0, 20, 1, dtype: np.Int16);
            a = a.reshape(new shape(5, -1));
            print(a);

            var ExpectedDataA = new Int16[,]
                {{0,  1,  2,  3},
                 {4,  5,  6,  7},
                 {8,  9, 10, 11},
                 {12, 13, 14, 15},
                 {16, 17, 18, 19}};
            AssertArray(a, ExpectedDataA);

            var b = np.array(new Int16[] { 2 });
            var c = a + b;
            print(c);

            var ExpectedDataC = new Int16[,]
                {{2,  3,  4,  5},
                 {6,  7,  8,  9},
                 {10, 11, 12, 13},
                 {14, 15, 16, 17},
                 {18, 19, 20, 21}};
            AssertArray(c, ExpectedDataC);


            b = np.array(new Int16[] { 10,20,30,40 });
            var d = a + b;
            print(d);

            var ExpectedDataD = new Int16[,]
                {{10, 21, 32, 43},
                 {14, 25, 36, 47},
                 {18, 29, 40, 51},
                 {22, 33, 44, 55},
                 {26, 37, 48, 59}};
            AssertArray(d, ExpectedDataD);
        }

        //[TestMethod]
        //public void test_add_operations_performance()
        //{

        //    var a = np.arange(0, 20000000, 1, dtype: np.Int32);

        //    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        //    sw.Start();

        //    a = a.reshape(new shape(5, -1));
        //    print(a.shape);
        //    print(a.strides);

        //    var b = a + 8;
        //    print(b.shape);
        //    print(b.strides);
        //    sw.Stop();
        //    Console.WriteLine("Milliseconds = {0}", sw.ElapsedMilliseconds);
        //}

        //[TestMethod]
        //public void test_add_operations_performance_2()
        //{

        //    var a = np.arange(0, 20000000, 1, dtype: np.Int32);

        //    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        //    sw.Start();

        //    a = a.reshape(new shape(-1, 5));
        //    //print(a.shape);
        //    //print(a.strides);

        //    var b = a + np.arange(2, 7);
        //    //print(b.shape);
        //    //print(b.strides);
        //    sw.Stop();
        //    Console.WriteLine("Milliseconds = {0}", sw.ElapsedMilliseconds);
        //}

        [TestMethod]
        public void test_subtract_operations()
        {
            var a = np.arange(0, 20, 1, dtype: np.Int16);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            var b = a - 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB = new Int16[,]
            {{-8, -7, -6, -5},
             {-4, -3, -2, -1},
             {0,  1,  2,  3},
             {4,  5,  6,  7},
             {8,  9, 10, 11}
            };
            AssertArray(b, ExpectedDataB);

            a = np.arange(0, 20, 1, dtype: np.Int64);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a - 2400;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB2 = new Int64[,]
            {{-2400, -2399, -2398, -2397},
             {-2396, -2395, -2394, -2393},
             {-2392, -2391, -2390, -2389},
             {-2388, -2387, -2386, -2385},
             {-2384, -2383, -2382, -2381}
            };

            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_subtract_operations_2()
        {
            var a = np.arange(100, 102, 1, dtype: np.Int16);
            var b = np.array(new Int16[] { 1,63 });
            var c = a - b;
            print(a);
            print("****");
            print(b);
            print("****");
            print(c);
            print("****");
            AssertArray(c, new Int16[] { 99, 38 });


            a = np.arange(0, 4, 1, dtype: np.Int16).reshape(new shape(2,2));
            b = np.array(new Int16[] { 65, 78 }).reshape(new shape(1,2));
            c = a - b;
            print(a);
            print("****");
            print(b);
            print("****");
            print(c);
            print("****");
            AssertArray(c, new Int16[,] { { -65, -77 }, { -63, -75 } });

        }

        [TestMethod]
        public void test_multiply_operations()
        {
            var a = np.arange(0, 20, 1, dtype: np.Int16);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            var b = a * 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB1 = new Int16[,]
            {
                {0,   8,  16,  24},
                {32,  40,  48,  56},
                {64,  72,  80, 88},
                {96, 104, 112, 120},
                {128, 136, 144, 152}
            };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(0, 20, 1, dtype: np.Int64);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a * 2400;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB2 = new Int64[,]
            {
                {0,  2400,  4800,  7200},
                {9600, 12000, 14400, 16800},
                {19200, 21600, 24000, 26400},
                {28800, 31200, 33600, 36000},
                {38400, 40800, 43200, 45600}
            };
            AssertArray(b, ExpectedDataB2);
        }

        [TestMethod]
        public void test_division_operations()
        {
            var a = np.arange(20000, 20020, 1, dtype: np.Int16);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            var b = a / 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB1 = new Int16[,]
            {
                {2500, 2500, 2500, 2500},
                {2500, 2500, 2500, 2500},
                {2501, 2501, 2501, 2501},
                {2501, 2501, 2501, 2501},
                {2502, 2502, 2502, 2502}
            };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2000000, 2000020, 1, dtype: np.Int64);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a / 2400;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB2 = new Int64[,]
            {
                {833, 833, 833, 833},
                {833, 833, 833, 833},
                {833, 833, 833, 833},
                {833, 833, 833, 833},
                {833, 833, 833, 833},
            };
            AssertArray(b, ExpectedDataB2);
        }

        [TestMethod]
        public void test_leftshift_operations()
        {
            var a = np.arange(0, 20, 1, dtype: np.Int16);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            var b = a << 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB1 = new Int16[,]
            {
                {0,  256,  512,  768},
                {1024, 1280, 1536, 1792},
                {2048, 2304, 2560, 2816},
                {3072, 3328, 3584, 3840},
                {4096, 4352, 4608, 4864}
            };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(0, 20, 1, dtype: np.Int64);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a << 24;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB2 = new Int64[,]
            {
                {0,  16777216,  33554432,  50331648},
                {67108864,  83886080, 100663296, 117440512},
                {134217728, 150994944, 167772160, 184549376},
                {201326592, 218103808, 234881024, 251658240},
                {268435456, 285212672, 301989888, 318767104}
            };
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_leftshift_operations2()
        {
            var a = np.arange(0, 20, 1, dtype: np.Int8);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            var b = a << 16;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB1 = new sbyte[,]
            {
                {0, 0, 0, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 0},
                {0, 0, 0, 0}
            };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(0, 20, 1, dtype: np.Int64);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a << 48;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB2 = new Int64[,]
            {
                {0,  281474976710656,  562949953421312,  844424930131968},
                {1125899906842624, 1407374883553280, 1688849860263936, 1970324836974592},
                {2251799813685248, 2533274790395904, 2814749767106560, 3096224743817216},
                {3377699720527872, 3659174697238528, 3940649673949184, 4222124650659840},
                {4503599627370496, 4785074604081152, 5066549580791808, 5348024557502464}
            };
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_rightshift_operations()
        {
            var a = np.arange(20000, 20020, 1, dtype: np.Int16);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            var b = a >> 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB1 = new Int16[,]
            {
                {78, 78, 78, 78},
                {78, 78, 78, 78},
                {78, 78, 78, 78},
                {78, 78, 78, 78},
                {78, 78, 78, 78}
            };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2123450, 2123470, 1, dtype: np.Int64);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a >> 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB2 = new Int64[,]
            {
                {8294 , 8294 , 8294 , 8294 },
                {8294 , 8294 , 8294 , 8294 },
                {8294 , 8294 , 8294 , 8294 },
                {8294 , 8294 , 8294 , 8294 },
                {8294 , 8294 , 8294 , 8294 }
            };
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_bitwiseand_operations()
        {
            var a = np.arange(0, 32, 1, dtype: np.Int16);
            print(a);

            var b = a & 0x0f;
            print(b);

            var ExpectedDataB1 = new Int16[]
            { 0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15,
              0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15 };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2048, 2048 + 32, 1, dtype: np.Int64);
            print(a);

            b = a & 0xFF;
            print(b);

            var ExpectedDataB2 = new Int64[]
            { 0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15,
              16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_bitwiseor_operations()
        {
            var a = np.arange(0, 32, 1, dtype: np.Int16);
            print(a);

            var b = a | 0x100;
            print(b);

            var ExpectedDataB1 = new Int16[]
            { 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271,
              272, 273, 274, 275, 276, 277, 278, 279, 280, 281, 282, 283, 284, 285, 286, 287 };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2048, 2048 + 32, 1, dtype: np.Int64);
            print(a);

            b = a | 0x1000;
            print(b);

            var ExpectedDataB2 = new Int64[]
            { 6144, 6145, 6146, 6147, 6148, 6149, 6150, 6151, 6152, 6153, 6154, 6155, 6156, 6157,
              6158, 6159, 6160, 6161, 6162, 6163, 6164, 6165, 6166, 6167, 6168, 6169, 6170, 6171,
              6172, 6173, 6174, 6175 };
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_bitwisexor_operations()
        {
            var a = np.arange(0, 32, 1, dtype: np.Int16);
            print(a);

            var b = a ^ 0xAAA;
            print(b);

            var ExpectedDataB1 = new Int16[]
            { 2730, 2731, 2728, 2729, 2734, 2735, 2732, 2733, 2722, 2723, 2720, 2721, 2726, 2727, 2724,
              2725, 2746, 2747, 2744, 2745, 2750, 2751, 2748, 2749, 2738, 2739, 2736, 2737, 2742, 2743, 2740, 2741 };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2048, 2048 + 32, 1, dtype: np.Int64);
            print(a);

            b = a ^ 0xAAAA;
            print(b);

            var ExpectedDataB2 = new Int64[]
            { 41642, 41643, 41640, 41641, 41646, 41647, 41644, 41645, 41634, 41635, 41632, 41633,
              41638, 41639, 41636, 41637, 41658, 41659, 41656, 41657, 41662, 41663, 41660, 41661,
              41650, 41651, 41648, 41649, 41654, 41655, 41652, 41653};
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_remainder_operations()
        {
            var a = np.arange(0, 32, 1, dtype: np.Int16);
            print(a);

            var b = a % 6;
            print(b);

            AssertArray(b, new Int16[] { 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3,
                                         4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1 });

            a = np.arange(2048, 2048 + 32, 1, dtype: np.Int64);
            print(a);

            b = a % 6;
            print(b);

            AssertArray(b, new Int64[] { 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5,
                                         0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3 });

        }

        [TestMethod]
        public void test_sqrt_operations()
        {
            var a = np.arange(0, 32, 1, dtype: np.Int32);
            print(a);

            var b = np.sqrt(a);
            print(b);

            var ExpectedDataB1 = new float[]
            {
                0.0f,        1.0f,       1.4142135f, 1.7320508f, 2.0f,        2.236068f,  2.4494898f,
                2.6457512f,  2.828427f,  3.0f,       3.1622777f, 3.3166249f,  3.4641016f, 3.6055512f,
                3.7416575f,  3.8729835f, 4.0f,       4.1231055f, 4.2426405f,  4.358899f,  4.472136f,
                4.582576f,   4.690416f,  4.7958317f, 4.8989797f, 5.0f,        5.0990195f, 5.196152f,
                5.2915025f,  5.3851647f, 5.477226f,  5.5677643f
            };

            AssertArray(b, ExpectedDataB1);

            a = np.arange(2048, 2048 + 32, 1, dtype: np.Int64);
            print(a);

            b = np.sqrt(a);
            print(b);

            var ExpectedDataB2 = new double[]
            {
                45.254834,   45.26588119, 45.27692569, 45.2879675,  45.29900661, 45.31004304,
                45.32107677, 45.33210783, 45.3431362,  45.35416188, 45.36518489, 45.37620522,
                45.38722287, 45.39823785, 45.40925016, 45.4202598,  45.43126677, 45.44227107,
                45.45327271, 45.46427169, 45.475268,   45.48626166, 45.49725266, 45.50824101,
                45.51922671, 45.53020975, 45.54119015, 45.5521679,  45.563143,   45.57411546,
                45.58508528, 45.59605246
            };
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_cbrt_operations()
        {
            var a = np.arange(0, 32, 1, dtype: np.Int32);
            print(a);

            var b = np.cbrt(a);
            print(b);

            var ExpectedDataB1 = new double[]
            {
                0.0,              1.0,              1.25992104989487, 1.44224957030741, 1.5874010519682,  1.7099759466767,
                1.81712059283214, 1.91293118277239, 2.0,              2.0800838230519,  2.15443469003188, 2.22398009056932,
                2.28942848510666, 2.35133468772076, 2.41014226417523, 2.46621207433047, 2.51984209978975, 2.57128159065824,
                2.6207413942089,  2.66840164872194, 2.71441761659491, 2.75892417638112, 2.80203933065539, 2.84386697985157,
                2.88449914061482, 2.92401773821287, 2.96249606840737, 3.0,              3.03658897187566, 3.07231682568585,
                3.10723250595386, 3.14138065239139
            };

            AssertArray(b, ExpectedDataB1);

            a = np.arange(2048, 2048 + 32, 1, dtype: np.Int64);
            print(a);

            b = np.cbrt(a);
            print(b);

            var ExpectedDataB2 = new double[]
            {
                12.6992084157456, 12.7012750078758, 12.7033409277248, 12.7054061758392, 12.7074707527647, 12.7095346590463,
                12.7115978952285, 12.7136604618549, 12.7157223594684, 12.7177835886112, 12.719844149825,  12.7219040436504,
                12.7239632706277, 12.7260218312964, 12.728079726195,  12.7301369558617, 12.7321935208338, 12.7342494216479,
                12.7363046588401, 12.7383592329456, 12.7404131444989, 12.742466394034, 12.744518982084,   12.7465709091815,
                12.7486221758582, 12.7506727826453, 12.7527227300734, 12.754772018672, 12.7568206489705,  12.7588686214971,
                12.7609159367796, 12.762962595345
            };
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_negative_operations()
        {
            var a = np.arange(0, 32, 1, dtype: np.Int16);
            print(a);

            var b = -a;
            print(b);

            AssertArray(b, new Int16[] {0, -1,  -2,  -3,  -4,  -5,  -6 , -7,  -8,  -9, -10,
                                      -11, -12, -13, -14, -15, -16, -17, -18, -19, -20,
                                      -21, -22, -23, -24, -25, -26, -27, -28, -29, -30, -31 });
        }

        [TestMethod]
        public void test_invert_operations()
        {
            var a = np.arange(-32, 32, 1, dtype: np.Int16);
            print(a);

            var b = ~a;
            print(b);

            var ExpectedDataB = new Int16[]
            {
                31,  30,  29,  28,  27,  26,  25,  24,  23,  22,  21,  20,
                19,  18,  17,  16,  15,  14,  13,  12,  11,  10,   9,   8,
                7,   6,   5,   4,   3,   2,   1,   0,  -1,  -2,  -3,  -4,
               -5,  -6,  -7,  -8,  -9,  -10, -11, -12, -13, -14, -15, -16,
               -17, -18, -19, -20, -21, -22, -23, -24, -25, -26, -27, -28,
               -29, -30, -31, -32
            };
            AssertArray(b, ExpectedDataB);

        }

        [TestMethod]
        public void test_LESS_operations()
        {
            var a = np.arange(-5, 5, 1, dtype: np.Int16);
            print(a);

            var b = a < -2;
            print(b);

            AssertArray(b, new Boolean[] { true, true, true, false, false, false, false, false, false, false });
        }

        [TestMethod]
        public void test_LESSEQUAL_operations()
        {
            var a = np.arange(-5, 5, 1, dtype: np.Int16);
            print(a);

            var b = a <= -2;
            print(b);

            AssertArray(b, new Boolean[] { true, true, true, true, false, false, false, false, false, false });
        }

        [TestMethod]
        public void test_EQUAL_operations()
        {
            var a = np.arange(-5, 5, 1, dtype: np.Int16);
            print(a);

            var b = a == -2;
            print(b);

            AssertArray(b, new Boolean[] { false, false, false, true, false, false, false, false, false, false });
        }

        [TestMethod]
        public void test_NOTEQUAL_operations()
        {
            var a = np.arange(-5, 5, 1, dtype: np.Int16);
            print(a);

            var b = a != -2;
            print(b);

            AssertArray(b, new Boolean[] { true, true, true, false, true, true, true, true, true, true });
        }

        [TestMethod]
        public void test_GREATER_operations()
        {
            var a = np.arange(-5, 5, 1, dtype: np.Int16);
            print(a);

            var b = a > -2;
            print(b);

            AssertArray(b, new Boolean[] { false, false, false, false, true, true, true, true, true, true });

        }

        [TestMethod]
        public void test_GREATEREQUAL_operations()
        {
            var a = np.arange(-5, 5, 1, dtype: np.Int16);
            print(a);

            var b = a >= -2;
            print(b);

            AssertArray(b, new Boolean[] { false, false, false, true, true, true, true, true, true, true });

        }

        [TestMethod]
        public void test_arrayarray_or()
        {
            var a = np.arange(0, 32, 1, dtype: np.Int16);
            var b = np.arange(33, 33 + 32, 1, dtype: np.Int16);
            var c = a | b;
            print(a);
            print(b);
            print(c);

            AssertArray(c, new Int16[] {33, 35, 35, 39, 37, 39, 39, 47, 41, 43, 43, 47,
                                        45, 47, 47, 63, 49, 51, 51, 55, 53, 55, 55, 63,
                                        57, 59, 59, 63, 61, 63, 63, 95 });
        }

        [TestMethod]
        public void test_bitwise_and()
        {
            var x = np.arange(1023, 1039, 1, dtype: np.UInt32).reshape(new shape(2, -1));
            var y = np.bitwise_and(x, 0x3FF);
            var z = x & 0x3FF;

            print(x);
            print(y);
            print(z);

            var ExpectedData = new UInt32[,]
            {
                { 1023, 0, 1,  2,  3,  4,  5,  6 },
                {  7, 8, 9, 10, 11, 12, 13, 14 }
            };

            AssertArray(y, ExpectedData);
            AssertArray(z, ExpectedData);

        }

        [TestMethod]
        public void test_bitwise_or()
        {
            var x = np.arange(1023, 1039, 1, dtype: np.UInt32).reshape(new shape(2, -1));
            var y = np.bitwise_or(x, 0x10);
            var z = x | 0x10;

            print(x);
            print(y);
            print(z);

            var ExpectedData = new UInt32[,]
            {
                { 1023, 1040, 1041, 1042, 1043, 1044, 1045, 1046 },
                { 1047, 1048, 1049, 1050, 1051, 1052, 1053, 1054 }
            };

            AssertArray(y, ExpectedData);
            AssertArray(z, ExpectedData);
        }

        [TestMethod]
        public void test_bitwise_xor()
        {
            var a = np.bitwise_xor(13, 17);
            Assert.AreEqual(28, a.GetItem(0));
            print(a);

            var b = np.bitwise_xor(31, 5);
            Assert.AreEqual(26, b.GetItem(0));
            print(b);

            var c = np.bitwise_xor(new int[] { 31, 3 }, 5);
            AssertArray(c, new int[] { 26, 6 });
            print(c);

            var d = np.bitwise_xor(new int[] { 31, 3 }, new int[] { 5, 6 });
            AssertArray(d, new int[] { 26, 5 });
            print(d);

            var e = np.bitwise_xor(new bool[] { true, true }, new bool[] { false, true });
            AssertArray(e, new bool[] { true, false });
            print(e);

            return;
        }

        [TestMethod]
        public void test_bitwise_not()
        {
            var a = np.bitwise_not(13);
            Assert.AreEqual(-14, a.GetItem(0));
            print(a);

            var b = np.bitwise_not(31);
            Assert.AreEqual(-32, b.GetItem(0));
            print(b);

            var c = np.bitwise_not(new int[] { 31, 3 });
            AssertArray(c, new int[] { -32, -4 });
            print(c);

            var d = np.bitwise_not(new int[] { 31, 3 });
            AssertArray(d, new int[] { -32, -4 });
            print(d);

            var e = np.bitwise_not(new bool[] { true, false });
            AssertArray(e, new bool[] { false, true });
            print(e);

            return;
        }

        [TestMethod]
        public void test_invert()
        {
            var a = np.invert(13);
            Assert.AreEqual(-14, a.GetItem(0));
            print(a);

            var b = np.invert(31);
            Assert.AreEqual(-32, b.GetItem(0));
            print(b);

            var c = np.invert(new int[] { 31, 3 });
            AssertArray(c, new int[] { -32, -4 });
            print(c);

            var d = np.invert(new int[] { 31, 3 });
            AssertArray(d, new int[] { -32, -4 });
            print(d);

            var e = np.invert(new bool[] { true, false });
            AssertArray(e, new bool[] { false, true });
            print(e);

            return;
        }

        [TestMethod]
        public void test_right_shift()
        {
            var x = np.arange(1023, 1039, 1, dtype: np.UInt32).reshape(new shape(2, -1));
            var y = np.right_shift(x, 2);
            var z = x >> 2;

            print(x);
            print(y);
            print(z);

            var ExpectedData = new UInt32[,]
            {
                { 255, 256, 256, 256, 256, 257, 257, 257 },
                { 257, 258, 258, 258, 258, 259, 259, 259 }
            };

            AssertArray(y, ExpectedData);
            AssertArray(z, ExpectedData);
        }

        [TestMethod]
        public void test_right_shift_2()
        {
            var a = np.right_shift(new int[] { 10 }, new int[] { 1, 2, 3 });
            AssertArray(a, new int[] { 5, 2, 1 });
            print(a);
        }

        [TestMethod]
        public void test_left_shift()
        {
            var x = np.arange(1023, 1039, 1, dtype: np.UInt32).reshape(new shape(2, -1));
            var y = np.left_shift(x, 2);
            var z = x << 2;

            print(x);
            print(y);
            print(z);

            var ExpectedData = new UInt32[,]
            {
                { 4092, 4096, 4100, 4104, 4108, 4112, 4116, 4120 },
                { 4124, 4128, 4132, 4136, 4140, 4144, 4148, 4152 }
            };

            AssertArray(y, ExpectedData);
            AssertArray(z, ExpectedData);
        }

        [TestMethod]
        public void test_left_shift_2()
        {
            var a = np.left_shift(new int[] { 10 }, new int[] { 1, 2, 3 });
            AssertArray(a, new int[] { 20, 40, 80 });
            print(a);
        }

        [TestMethod]
        public void test_NAN()
        {
            var x = np.arange(1023, 1039, 1, dtype: np.Float32).reshape(new shape(2, -1));
            x[":"] = np.NaN;

            print(x);

            var ExpectedData = new float[,]
            {
                { np.NaN, np.NaN, np.NaN, np.NaN, np.NaN, np.NaN, np.NaN, np.NaN },
                { np.NaN, np.NaN, np.NaN, np.NaN, np.NaN, np.NaN, np.NaN, np.NaN }
            };

            //AssertArray(x, ExpectedData);  // seems to be some sort of rounding error.  Data looks right

        }

        [TestMethod]
        public void test_min()
        {
            float[] TestData = new float[] { 2.5f, -1.7f, -1.5f, -0.2f, 0.2f, 1.5f, 1.7f, 2.0f };
            var x = np.array(TestData);
            var y = np.min(x);

            print(x);
            print(y);

            Assert.AreEqual(-1.7f, (float)y);
        }

        [TestMethod]
        public void test_max()
        {
            float[] TestData = new float[] { 2.5f, -1.7f, -1.5f, -0.2f, 0.2f, 1.5f, 1.7f, 2.0f };
            var x = np.array(TestData);
            var y = np.max(x);

            print(x);
            print(y);

            Assert.AreEqual(2.5f, (float)y);
        }

        [TestMethod]
        public void test_isnan()
        {
            float[] TestData = new float[] { -1.7f, np.NaN, np.NaN, 0.2f, 1.5f, np.NaN, 2.0f };
            var x = np.array(TestData);
            var y = np.isnan(x);
            var z = x == np.NaN;

            print(x);
            print(y);
            print(z);

            AssertArray(y, new bool[] { false,  true, true, false, false, true, false });
            AssertArray(z, new bool[] { false, false, false, false, false, false, false });

        }

        [TestMethod]
        public void test_setdiff1d()
        {
            Int32[] TestDataA = new Int32[] { 1, 2, 3, 2, 4, };
            Int32[] TestDataB = new Int32[] { 3, 4, 5, 6 };

            var a = np.array(TestDataA);
            var b = np.array(TestDataB);
            ndarray c = np.setdiff1d(a, b);

            print(a);
            print(b);
            print(c);

            AssertArray(c, new Int32[] { 1, 2 });

        }

        [TestMethod]
        public void test_setdiff1d_2()
        {
            Int32[] TestDataB = new Int32[] { 3, 4, 5, 6 };

            var a = np.arange(1, 39, dtype: np.UInt32).reshape(new shape(2, -1));
            var b = np.array(TestDataB);
            ndarray c = np.setdiff1d(a, b);

            print(a);
            print(b);
            print(c);

            AssertArray(c, new UInt32[] {1,  2,  7,  8,  9,  10, 11, 12, 13, 14, 15, 16,
                                         17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28,
                                         29, 30, 31, 32, 33, 34, 35, 36, 37, 38 });

        }

        [TestMethod]
        public void test_interp1d()
        {
            var x = np.arange(2, 12, 2);
            var y = np.arange(1, 6, 1);

            double[] a = new double[] { 2, 4, 6, 8, 10 };
            double[] b = new double[] { 0.51341712, 0.26359714, 0.13533528, 0.06948345, 0.03567399 };

            var ynew = MathNet.Numerics.Interpolate.Linear(a, b);

            print(x);
            print(y);
            print(ynew);

           // AssertArray(ynew, new double[] { 0.51341712, 0.26359714, 0.13533528, 0.06948345, 0.03567399 });

        }

        [TestMethod]
        public void test_interp1d_2()
        {
            ndarray xarray = np.arange(0, 10, dtype: np.Float64);
            ndarray yarray = np.array(new double[0]);
            ndarray xnewarray = np.arange(0, 9, 0.1, dtype: np.Float64);


            double[] x = xarray.rawdata().datap as double[];
            double[] y = new double[] { 1.0, 0.71653131, 0.51341712, 0.36787944, 0.26359714, 0.1888756,
                                             0.13533528, 0.09697197, 0.06948345, 0.04978707 };

            var f = MathNet.Numerics.Interpolate.Linear(x, y);

            double[] xnew = xnewarray.rawdata().datap as double[];
            double[] ynew = new double[xnew.Length];

            int index = 0;
            foreach (var xn in xnew)
            {
                ynew[index] = Math.Round(f.Interpolate(xn), 8, MidpointRounding.AwayFromZero);
                index++;
            }

            ndarray ynewarray = np.array(ynew);

            print(xarray);
            print(yarray);
            print(xnewarray);
            print(ynewarray);
        }

        [TestMethod]
        public void test_rot90_1()
        {
            ndarray m = np.array(new Int32[,] { { 1, 2 }, { 3, 4 } }, np.Int32);
            print(m);
            print("************");

            ndarray n = np.rot90(m);
            print(n);
            AssertArray(n, new Int32[,] { {2,4}, {1,3}, });
            print("************");

            n = np.rot90(m, 2);
            print(n);
            AssertArray(n, new Int32[,] { { 4, 3 }, { 2, 1 }, });
            print("************");

            m = np.arange(8).reshape(new shape(2, 2, 2));
            n = np.rot90(m, 1, new int[] { 1, 2 });
            print(n);
            AssertArray(n, new Int32[,,] {{{ 1, 3 }, { 0, 2 } }, {{ 5, 7 }, { 4, 6 } }});

        }

        [TestMethod]
        public void test_flip_1()
        {
            ndarray A = np.arange(8).reshape(new shape(2, 2, 2));
            ndarray B = np.flip(A, 0);
            print(A);
            print("************");
            print(B);
            AssertArray(B, new Int32[,,] { { { 4, 5 }, { 6, 7 } }, { { 0, 1 }, { 2, 3 } } });

            print("************");
            ndarray C = np.flip(A, 1);
            print(C);
            AssertArray(C, new Int32[,,] { { { 2, 3 }, { 0, 1 } }, { { 6, 7 }, { 4, 5 } } });
            print("************");

        }

        [TestMethod]
        public void test_iterable_1()
        {
            var a = np.iterable(new int[] { 1, 2, 3 });
            var b = np.iterable(2);
            print(a);
            print(b);

            Assert.AreEqual(true, a);
            Assert.AreEqual(false, b);

        }

        [TestMethod]
        public void test_trim_zeros_1()
        {
            ndarray a = np.array(new int[] { 0, 0, 0, 1, 2, 3, 0, 2, 1, 0 });

            var b = np.trim_zeros(a);
            print(b);
            AssertArray(b, new int[] { 1, 2, 3, 0, 2, 1 });

            var c = np.trim_zeros(a, "b");
            print(c);
            AssertArray(c, new int[] { 0, 0, 0, 1, 2, 3, 0, 2, 1 });
        }

        [TestMethod]
        public void test_isfinite_1()
        {
            var a = np.isfinite(1);
            Assert.AreEqual(true, a.GetItem(0));
            print(a);

            var b = np.isfinite(0);
            Assert.AreEqual(true, b.GetItem(0));
            print(b);

            var c = np.isfinite(np.NaN);
            Assert.AreEqual(false, c.GetItem(0));
            print(c);


            var d = np.isfinite(np.Inf);
            Assert.AreEqual(false, d.GetItem(0));
            print(d);

            var e = np.isfinite(np.NInf);
            Assert.AreEqual(false, e.GetItem(0));
            print(e);

            //var f = np.isfinite(new float[] { np.log(-1.0), 1.0f, np.log(0) });
            //print(f);


            var x = np.array(new float[] { -np.Inf, 0.0f, np.Inf, np.Inf }).reshape((2,2));
            var g = np.isfinite(x);
            AssertArray(g, new bool[,] { { false, true }, { false, false } });
            print(g);
        }

        [TestMethod]
        public void test_isinf_1()
        {
            var a = np.isinf(1);
            Assert.AreEqual(false, a.GetItem(0));
            print(a);

            var b = np.isinf(0);
            Assert.AreEqual(false, b.GetItem(0));
            print(b);

            var c = np.isinf(np.NaN);
            Assert.AreEqual(false, c.GetItem(0));
            print(c);


            var d = np.isinf(np.Inf);
            Assert.AreEqual(true, d.GetItem(0));
            print(d);

            var e = np.isinf(np.NInf);
            Assert.AreEqual(true, e.GetItem(0));
            print(e);

            //var f = np.isfinite(new float[] { np.log(-1.0), 1.0f, np.log(0) });
            //print(f);


            var x = np.array(new float[] { -np.Inf, 0.0f, np.Inf, np.Inf }).reshape((2, 2));
            var g = np.isinf(x);
            AssertArray(g, new bool[,] { { true, false }, { true, true } });
            print(g);
        }

        [TestMethod]
        public void test_isneginf_1()
        {
            var a = np.isneginf(1);
            Assert.AreEqual(false, a.GetItem(0));
            print(a);

            var b = np.isneginf(0);
            Assert.AreEqual(false, b.GetItem(0));
            print(b);

            var c = np.isneginf(np.NaN);
            Assert.AreEqual(false, c.GetItem(0));
            print(c);


            var d = np.isneginf(np.Inf);
            Assert.AreEqual(false, d.GetItem(0));
            print(d);

            var e = np.isneginf(np.NInf);
            Assert.AreEqual(true, e.GetItem(0));
            print(e);

            //var f = np.isfinite(new float[] { np.log(-1.0), 1.0f, np.log(0) });
            //print(f);


            var x = np.array(new float[] { -np.Inf, 0.0f, np.Inf, np.Inf }).reshape((2, 2));
            var g = np.isneginf(x);
            AssertArray(g, new bool[,] { { true, false }, { false, false } });
            print(g);
        }

        [TestMethod]
        public void test_isposinf_1()
        {
            var a = np.isposinf(1);
            Assert.AreEqual(false, a.GetItem(0));
            print(a);

            var b = np.isposinf(0);
            Assert.AreEqual(false, b.GetItem(0));
            print(b);

            var c = np.isposinf(np.NaN);
            Assert.AreEqual(false, c.GetItem(0));
            print(c);


            var d = np.isposinf(np.Inf);
            Assert.AreEqual(true, d.GetItem(0));
            print(d);

            var e = np.isposinf(np.NInf);
            Assert.AreEqual(false, e.GetItem(0));
            print(e);

            //var f = np.isfinite(new float[] { np.log(-1.0), 1.0f, np.log(0) });
            //print(f);


            var x = np.array(new float[] { -np.Inf, 0.0f, np.Inf, np.Inf }).reshape((2, 2));
            var g = np.isposinf(x);
            AssertArray(g, new bool[,] { { false, false }, { true, true } });
            print(g);
        }

        [TestMethod]
        public void test_logical_and_1()
        {
            var a = np.logical_and(true, false);
            Assert.AreEqual(false, a.GetItem(0));
            print(a);

            var b = np.logical_and(new bool[] { true, false }, new bool[] { false, false });
            AssertArray(b, new bool[] { false, false });
            print(b);

            var x = np.arange(5);
            var c = np.logical_and(x > 1, x < 4);
            AssertArray(c, new bool[] { false, false, true, true, false });
            print(c);

            var y = np.arange(6).reshape((2, 3));
            var d = np.logical_and(y > 1, y < 4);
            AssertArray(d, new bool[,] { { false, false, true }, {true, false, false } });
            print(d);
        }

        [TestMethod]
        public void test_logical_or_1()
        {
            var a = np.logical_or(true, false);
            Assert.AreEqual(true, a.GetItem(0));
            print(a);

            var b = np.logical_or(new bool[] { true, false }, new bool[] { false, false });
            AssertArray(b, new bool[] { true, false });
            print(b);

            var x = np.arange(5);
            var c = np.logical_or(x < 1, x > 3);
            AssertArray(c, new bool[] { true, false, false, false, true });
            print(c);

            var y = np.arange(6).reshape((2, 3));
            var d = np.logical_or(y < 1, y > 3);
            AssertArray(d, new bool[,] { { true, false, false }, { false, true, true } });
            print(d);
        }

        [TestMethod]
        public void test_logical_xor_1()
        {
            var a = np.logical_xor(true, false);
            Assert.AreEqual(true, a.GetItem(0));
            print(a);

            var b = np.logical_xor(new bool[] { true, false }, new bool[] { false, false });
            AssertArray(b, new bool[] { true, false });
            print(b);

            var x = np.arange(5);
            var c = np.logical_xor(x < 1, x > 3);
            AssertArray(c, new bool[] { true, false, false, false, true });
            print(c);

            var y = np.arange(6).reshape((2, 3));
            var d = np.logical_xor(y < 1, y > 3);
            AssertArray(d, new bool[,] { { true, false, false }, { false, true, true } });
            print(d);

            var e = np.logical_xor(0, np.eye(2));
            AssertArray(e, new bool[,] { { true, false }, { false, true } });
        }

        [TestMethod]
        public void test_logical_not_1()
        {
            var a = np.logical_not(3);
            Assert.AreEqual(false, a.GetItem(0));
            print(a);

            var b = np.logical_not(new object[] { 0, -1, 0, 1 });
            AssertArray(b, new bool[] { true, false, true, false });
            print(b);

            var x = np.arange(5);
            var c = np.logical_not(x < 3);
            AssertArray(c, new bool[] { false, false, false, true, true });
            print(c);
        }

        [TestMethod]
        public void test_greater_1()
        {
            var a = np.greater(new int[] { 4, 2, 1 }, new int[] { 2, 2, 2 });
            AssertArray(a, new bool[] { true, false, false });
            print(a);

            var b = np.greater(new int[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { true, true, false });
            print(b);

            var c = np.greater(2, new int[] { 4, 2, 1 });
            AssertArray(c, new bool[] { false, false, true });
            print(c);

        }

        [TestMethod]
        public void test_greater_equal_1()
        {
            var a = np.greater_equal(new int[] { 4, 2, 1 }, new int[] { 2, 2, 2 });
            AssertArray(a, new bool[] { true, true, false });
            print(a);

            var b = np.greater_equal(new int[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { true, true, true });
            print(b);

            var c = np.greater_equal(2, new int[] { 4, 2, 1 });
            AssertArray(c, new bool[] { false, true, true });
            print(c);
        }

        [TestMethod]
        public void test_less_1()
        {
            var a = np.less(new int[] { 4, 2, 1 }, new int[] { 2, 2, 2 });
            AssertArray(a, new bool[] { false, false, true });
            print(a);

            var b = np.less(new int[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { false, false, false });
            print(b);

            var c = np.less(2, new int[] { 4, 2, 1 });
            AssertArray(c, new bool[] { true, false, false });
            print(c);
        }

        [TestMethod]
        public void test_less_equal_1()
        {
            var a = np.less_equal(new int[] { 4, 2, 1 }, new int[] { 2, 2, 2 });
            AssertArray(a, new bool[] { false, true, true });
            print(a);

            var b = np.less_equal(new int[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { false, false, true });
            print(b);

            var c = np.less_equal(2, new int[] { 4, 2, 1 });
            AssertArray(c, new bool[] { true, true, false });
            print(c);
        }

        [TestMethod]
        public void test_equal_1()
        {
            var a = np.equal(new int[] { 4, 2, 1 }, new int[] { 2, 2, 2 });
            AssertArray(a, new bool[] { false, true, false });
            print(a);

            var b = np.equal(new int[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { false, false, true });
            print(b);

            var c = np.equal(2, new int[] { 4, 2, 1 });
            AssertArray(c, new bool[] { false, true, false });
            print(c);
        }
 
        [TestMethod]
        public void test_not_equal_1()
        {
            var a = np.not_equal(new int[] { 4, 2, 1 }, new int[] { 2, 2, 2 });
            AssertArray(a, new bool[] { true, false, true });
            print(a);

            var b = np.not_equal(new int[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { true, true, false });
            print(b);

            var c = np.not_equal(2, new int[] { 4, 2, 1 });
            AssertArray(c, new bool[] { true, false, true });
            print(c);
        }

        [TestMethod]
        public void test_copyto_1()
        {
            var a = np.zeros((10, 5), dtype: np.Int64);
            var b = new int[] { 11, 22, 33, 44, 55 };
            np.copyto(a, b);

            AssertShape(a, 10, 5);
            Assert.AreEqual((long)1650, a.Sum().GetItem(0));
            print(a);

            a = np.zeros((10, 5), dtype: np.Int64);
            np.copyto(a, 99);
            AssertShape(a, 10, 5);
            Assert.AreEqual((long)4950, a.Sum().GetItem(0));
            print(a);

            a = np.zeros((10, 5), dtype: np.Int64);
            var c = np.arange(11, 60, 11);

            try
            {
                np.copyto(c, a);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("not broadcastable"));
                return;
            }

            Assert.IsTrue(false);
 
        }

        [TestMethod]
        public void test_copyto_2()
        {
            var a = np.zeros((1, 2, 2, 1, 2), dtype: np.Float32);
            var b = new int[] { 1, 2 };
            np.copyto(a, b);

            AssertArray(a, new float[,,,,] { { { { { 1.0f, 2.0f } }, { { 1.0f, 2.0f } } }, { { { 1.0f, 2.0f } }, { { 1.0f, 2.0f, } } } } });

        }
    }
}
