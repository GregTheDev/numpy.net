﻿using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumpyDotNet;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NumpyLib;

namespace NumpyDotNetTests
{
    /// <summary>
    /// functions that test numpy support for objects
    /// </summary>
    [TestClass]
    public class ObjectOperationsTests : TestBaseClass
    {
        [TestMethod]
        public void xxx_Test_ObjectOperations_Placeholder()
        {
            string[] TestData = new string[] { "A", "B", "C", "D" };

            var a = np.array(TestData, dtype: np.Object);

            print(a);

            a = a.reshape((2, 2));
            print(a);

            a = a * 2;
            print(a);

        }

        private int SizeOfObject = IntPtr.Size;

        #region from ArrayCreationTests
        [TestMethod]
        public void test_asfarray_OBJECT()
        {
            var a = np.asfarray(new Object[] { 2, 3 });
            AssertArray(a, new double[] { 2, 3 });
            print(a);

            try
            {
                var b = np.asfarray(new Object[] { "2", "3" }, dtype: np.Object);
                AssertArray(a, new double[] { 2, 3 });
                print(a);
                Assert.Fail("This function should have thrown exception");
            }
            catch (Exception ex)
            {

            }


            return;
        }

        [TestMethod]
        public void test_copy_1_OBJECT()
        {
            var x = np.array(new Object[] { "1", "2", "3" });
            var y = x;

            var z = np.copy(x);

            // Note that, when we modify x, y changes, but not z:

            x[0] = "10";

            Assert.AreEqual((Object)"10", y[0]);

            Assert.AreEqual((Object)"1", z[0]);

            return;
        }

  
        [TestMethod]
        public void test_meshgrid_1_OBJECT()
        {
            
            var x = np.array(new object[] { "0", "50", "100" });
            var y = np.array(new object[] { "0", "100" });

            ndarray[] xv = np.meshgrid(new ndarray[] { x });
            AssertArray(xv[0], new object[] { "0", "50", "100" });
            print(xv[0]);

            print("************");

            ndarray[] xyv = np.meshgrid(new ndarray[] { x, y });
            AssertArray(xyv[0], new object[,] { { "0", "50", "100" }, { "0", "50", "100" } });
            AssertArray(xyv[1], new object[,] { { "0", "0", "0" }, { "100", "100", "100" } });

            print(xyv[0]);
            print(xyv[1]);

            print("************");

            xyv = np.meshgrid(new ndarray[] { x, y }, sparse: true);
            AssertArray(xyv[0], new object[,] { { "0", "50", "100" } });
            AssertArray(xyv[1], new object[,] { { "0" }, { "100" } });

            print(xyv[0]);
            print(xyv[1]);

            print("************");

            x = np.array(new object[] { "-5", "-4", "-3", "-2", "-1", "0", "1", "2", "3", "4" });
            y = np.array(new object[] { "-5", "-4", "-3", "-2", "-1", "0", "1", "2", "3", "4" });
            xyv = np.meshgrid(new ndarray[] { x, y }, sparse: true);

            AssertArray(xyv[0], new object[,] { { "-5", "-4", "-3", "-2", "-1", "0", "1", "2", "3", "4" } });
            AssertArray(xyv[1], new object[,] { { "-5" }, { "-4" }, { "-3" }, { "-2" }, { "-1" }, { "0" }, { "1" }, { "2" }, { "3" }, { "4" } });

            print(xyv[0]);
            print(xyv[1]);

            print("************");


        }

        [TestMethod]
        public void test_OneDimensionalArray_OBJECT()
        {
            object[] l = new object[] { 12, 13, 100, 36 };
            print("Original List:", l);
            var a = np.array(l);
            print("One-dimensional numpy array: ", a);
            print(a.shape);
            print(a.strides);

            AssertArray(a, l);
            AssertShape(a, 4);
            AssertStrides(a, SizeOfObject);
        }

        [TestMethod]
        public void test_reverse_array_OBJECT()
        {
            var x = np.array(new object[] { "-5A", "-4", "-3", "-2", "-1", "0", "1", "2", "3", "4H" });
            print("Original array:");
            print(x);
            print("Reverse array:");
            //x = (ndarray)x[new Slice(null, null, -1)];
            x = (ndarray)x["::-1"];
            print(x);

            AssertArray(x, new object[] { "4H", "3", "2", "1", "0", "-1", "-2", "-3", "-4", "-5A" });
            AssertShape(x, 10);
            AssertStrides(x, -SizeOfObject);

            var y = x + 100;
            print(y);

            var z = x.reshape((5, -1));
            print(z);
        }

        [TestMethod]
        public void test_checkerboard_1_OBJECT()
        {
            print("Checkerboard pattern:");
            var x = np.full((8, 8), "X", dtype: np.Object);
            x["1::2", "::2"] = "Y";
            x["::2", "1::2"] = "Y";
            print(x);

            var ExpectedData = new object[8, 8]
            {
                 { "X", "Y", "X", "Y", "X", "Y", "X", "Y" },
                 { "Y", "X", "Y", "X", "Y", "X", "Y", "X" },
                 { "X", "Y", "X", "Y", "X", "Y", "X", "Y" },
                 { "Y", "X", "Y", "X", "Y", "X", "Y", "X" },
                 { "X", "Y", "X", "Y", "X", "Y", "X", "Y" },
                 { "Y", "X", "Y", "X", "Y", "X", "Y", "X" },
                 { "X", "Y", "X", "Y", "X", "Y", "X", "Y" },
                 { "Y", "X", "Y", "X", "Y", "X", "Y", "X" },
            };

            AssertArray(x, ExpectedData);
            AssertShape(x, 8, 8);
            AssertStrides(x, SizeOfObject * 8, SizeOfObject);

        }


        [TestMethod]
        public void test_ArrayStats_1_OBJECT()
        {
            var x = np.array(new object[] { 1, 2, 3 }, dtype: np.Object);
            print("Size of the array: ", x.size);
            print("Length of one array element in bytes: ", x.ItemSize);
            print("Total bytes consumed by the elements of the array: ", x.nbytes);

            Assert.AreEqual(3, x.size);
            Assert.AreEqual(SizeOfObject, x.ItemSize);
            Assert.AreEqual(SizeOfObject * 3, x.nbytes);

        }

        [TestMethod]
        public void test_ndarray_flatten_OBJECT()
        {
            var x = np.array(new object[] { "A", "B", "C", "D"}, dtype: np.Object).reshape(new shape(2, 2));
            var y = x.flatten();
            print(x);
            print(y);
            AssertArray(y, new object[] { "A", "B", "C", "D" });

            y = x.flatten(order: NPY_ORDER.NPY_FORTRANORDER);
            print(y);
            AssertArray(y, new object[] { "A",  "C", "B", "D" });

            y = x.flatten(order: NPY_ORDER.NPY_KORDER);
            print(y);
            AssertArray(y, new object[] {"A", "B", "C", "D" });

        }

        [TestMethod]
        public void test_ndarray_byteswap_OBJECT()
        {
            var x = np.array(new object[] { "A", "B", "C", "D" }, dtype: np.Object);
            print(x);
            var y = x.byteswap(true);
            print(y);

            // Objects can't be swapped.  Data should be unchanged
            AssertArray(y, x.AsObjectArray());

            y = x.byteswap(false);
            print(y);

            // Objects can't be swapped.  Data should be unchanged
            AssertArray(y, x.AsObjectArray());

        }

        [TestMethod]
        public void test_ndarray_view_OBJECT()
        {
            var x = np.arange(256 + 32, 256 + 64, dtype: np.Int32);
            x = np.array(x.AsObjectArray());
            print(x);
            print(x.shape);
            print(x.Dtype);

            AssertArray(x, new Object[] { 288, 289, 290, 291, 292, 293, 294, 295, 296, 297, 298,
                                         299, 300, 301, 302, 303, 304, 305, 306, 307, 308, 309,
                                         310, 311, 312, 313, 314, 315, 316, 317, 318, 319});

            // BigInteger can't be mapped by something besides another BigInteger
            var y = x.view(np.UInt64);
            Assert.AreEqual((UInt64)0, (UInt64)y.Sum().GetItem(0));

            y = x.view(np.Object);
            AssertArray(y, y.AsObjectArray());

            y[5] = 1000;

            AssertArray(x, new Object[] { 288, 289, 290, 291, 292, 1000, 294, 295, 296, 297, 298,
                                         299, 300, 301, 302, 303, 304, 305, 306, 307, 308, 309,
                                         310, 311, 312, 313, 314, 315, 316, 317, 318, 319});

        }
               
        [TestMethod]
        public void test_ndarray_delete1_OBJECT()
        {
            var x = np.arange(0, 32, dtype: np.Int32).reshape(new shape(8, 4));
            x = np.array(x.AsObjectArray()).reshape(new shape(8, 4));

            print("X");
            print(x);
            print(x.shape);

            var ExpectedDataX = new Object[8, 4]
            {
                    { 0, 1, 2, 3},
                    { 4, 5, 6, 7},
                    { 8, 9, 10, 11 },
                    { 12, 13, 14, 15},
                    { 16, 17, 18, 19},
                    { 20, 21, 22, 23},
                    { 24, 25, 26, 27},
                    { 28, 29, 30, 31},
            };

            AssertArray(x, ExpectedDataX);
            AssertShape(x, 8, 4);

            var y = np.delete(x, new Slice(null), 0).reshape(new shape(8, 3));
            y[1] = 99;
            print("Y");
            print(y);
            print(y.shape);

            var ExpectedDataY = new Object[8, 3]
            {
                    { 1, 2, 3},
                    { 99, 99, 99},
                    { 9, 10, 11 },
                    { 13, 14, 15},
                    { 17, 18, 19},
                    { 21, 22, 23},
                    { 25, 26, 27},
                    { 29, 30, 31},
            };

            AssertArray(y, ExpectedDataY);
            AssertShape(y, 8, 3);

            print("X");
            print(x);


            AssertArray(x, ExpectedDataX);
            AssertShape(x, 8, 4);
        }

        [TestMethod]
        public void test_ndarray_delete2_OBJECT()
        {
            var x = np.arange(0, 32, dtype: np.Int32);
            x = np.array(x.AsObjectArray());

            print("X");
            print(x);
            print(x.shape);

            var ExpectedDataX = new object[] {0,  1,  2,  3,  4,  5,  6,  7,
                                             8,  9,  10, 11, 12, 13, 14, 15,
                                             16, 17, 18, 19, 20, 21, 22, 23,
                                             24, 25, 26, 27, 28, 29, 30, 31 };
            AssertArray(x, ExpectedDataX);
            AssertShape(x, 32);

            var y = np.delete(x, 1, 0);
            print("Y");
            print(y);
            print(y.shape);

            var ExpectedDataY = new object[] {0,  2,  3,  4,  5,  6,  7,
                                             8,  9,  10, 11, 12, 13, 14, 15,
                                             16, 17, 18, 19, 20, 21, 22, 23,
                                             24, 25, 26, 27, 28, 29, 30, 31 };
            AssertArray(y, ExpectedDataY);
            AssertShape(y, 31);

            print("X");
            print(x);

            AssertArray(x, ExpectedDataX);
        }

        [TestMethod]
        public void test_ndarray_delete3_OBJECT()
        {
            var x = np.arange(0, 32, dtype: np.Int32).reshape(new shape(8, 4));
            x = np.array(x.AsObjectArray()).reshape(new shape(8, 4));

            print("X");
            print(x);
            print(x.shape);

            var ExpectedDataX = new object[8, 4]
            {
                { 0, 1, 2, 3},
                { 4, 5, 6, 7},
                { 8, 9, 10, 11 },
                { 12, 13, 14, 15},
                { 16, 17, 18, 19},
                { 20, 21, 22, 23},
                { 24, 25, 26, 27},
                { 28, 29, 30, 31},
            };

            AssertArray(x, ExpectedDataX);
            AssertShape(x, 8, 4);

            var mask = np.ones_like(x, dtype: np.Bool);
            mask[new Slice(null), 0] = false;
            print(mask);

            var ExpectedDataMask = new bool[8, 4]
            {
                { false, true, true, true },
                { false, true, true, true },
                { false, true, true, true },
                { false, true, true, true },
                { false, true, true, true },
                { false, true, true, true },
                { false, true, true, true },
                { false, true, true, true },
            };

            AssertArray(mask, ExpectedDataMask);
            AssertShape(mask, 8, 4);

            var y = ((ndarray)(x[mask])).reshape(new shape(8, 3));

            print("Y");
            print(y);

            var ExpectedDataY = new object[8, 3]
            {
                { 1, 2, 3},
                { 5, 6, 7},
                { 9, 10, 11 },
                { 13, 14, 15},
                { 17, 18, 19},
                { 21, 22, 23},
                { 25, 26, 27},
                { 29, 30, 31},
            };

            AssertArray(y, ExpectedDataY);
            AssertShape(y, 8, 3);


            print("X");
            print(x);

            AssertArray(x, ExpectedDataX);
            AssertShape(x, 8, 4);
        }

        [TestMethod]
        public void test_ndarray_unique_1_OBJECT()
        {
            var x = np.array(new Object[] { 1, 2, 3, 1, 3, 4, 5, 4, 4 });

            print("X");
            print(x);

            var result = np.unique(x, return_counts: true, return_index: true, return_inverse: true);
            var uvalues = result.data;
            var indexes = result.indices;
            var inverse = result.inverse;
            var counts = result.counts;

            print("uvalues");
            print(uvalues);
            AssertArray(uvalues, new Object[] { 1, 2, 3, 4, 5 });

            print("indexes");
            print(indexes);
            AssertArray(indexes, new Int64[] { 0, 1, 2, 5, 6 });

            print("inverse");
            print(inverse);
            AssertArray(inverse, new Int64[] { 0, 1, 2, 0, 2, 3, 4, 3, 3 });

            print("counts");
            print(counts);
            AssertArray(counts, new Int64[] { 2, 1, 2, 3, 1 });
        }

        [TestMethod]
        public void test_ndarray_where_1_OBJECT()
        {
            var x = np.array(new Object[] { 1, 2, 3, 1, 3, 4, 5, 4, 4 }).reshape(new shape(3, 3));

            print("X");
            print(x);

            ndarray[] y = (ndarray[])np.where(x == 3);
            print("Y");
            print(y);


        }

        [TestMethod]
        public void test_ndarray_where_2_OBJECT()
        {
            var x = np.array(new object[] { 1, 2, 3, 1, 3, 4, 5, 4, 4 }).reshape(new shape(3, 3));

            print("X");
            print(x);

            ndarray[] y = (ndarray[])np.where(x == 3);
            print("Y");
            print(y);

            Assert.AreEqual(2, y.Length);
            AssertArray(y[0], new Int64[] { 0, 1 });
            AssertArray(y[1], new Int64[] { 2, 1 });

            var z = x.SliceMe(y) as ndarray;
            print("Z");
            print(z);
            AssertArray(z, new object[] { 3, 3 });
        }

        [TestMethod]
        public void test_ndarray_where_3_OBJECT()
        {
            var x = np.arange(0, 1000, dtype: np.Int32).reshape(new shape(-1, 10));
            x = np.array(x.AsObjectArray()).reshape(new shape(-1, 10));

            //print("X");
            //print(x);

            ndarray[] y = (ndarray[])np.where(x % 10 == 0);
            print("Y");
            print(y);

            var z = x[y] as ndarray;
            print("Z");
            print(z);

            var ExpectedDataZ = new object[]
            {
                0,  10,  20,  30,  40,  50,  60,  70,  80,
                90, 100, 110, 120, 130, 140, 150, 160, 170,
                180, 190, 200, 210, 220, 230, 240, 250, 260,
                270, 280, 290, 300, 310, 320, 330, 340, 350,
                360, 370, 380, 390, 400, 410, 420, 430, 440,
                450, 460, 470, 480, 490, 500, 510, 520, 530,
                540, 550, 560, 570, 580, 590, 600, 610, 620,
                630, 640, 650, 660, 670, 680, 690, 700, 710,
                720, 730, 740, 750, 760, 770, 780, 790, 800,
                810, 820, 830, 840, 850, 860, 870, 880, 890,
                900, 910, 920, 930, 940, 950, 960, 970, 980, 990
            };

            AssertArray(z, ExpectedDataZ);

        }

        [TestMethod]
        public void test_ndarray_where_4_OBJECT()
        {
            var x = np.arange(0, 3000000, dtype: np.Int32);
            x = np.array(x.AsObjectArray());

            var y = np.where(x % 7 == 0);
            print("Y");
            print(y);

            var z = x[y] as ndarray;
            print(z);
            //var m = np.mean(z);
            //print("M");
            //Assert.AreEqual((Object)1499998.5, m.GetItem(0));
            //print(m);

            return;
        }

        [TestMethod]
        public void test_ndarray_where_5_OBJECT()
        {
            var a = np.arange(10, dtype: np.Int32);
            a = np.array(a.AsObjectArray());

            var b = np.where(a < 5, a, 10 * a) as ndarray;
            AssertArray(b, new object[] { 0, 1, 2, 3, 4, 50, 60, 70, 80, 90 });
            print(b);

            a = np.array(new object[,] { { 0, 1, 2 }, { 0, 2, 4 }, { 0, 3, 6 } });
            b = np.where(a < 4, a, -1) as ndarray;  // -1 is broadcast
            AssertArray(b, new object[,] { { 0, 1, 2 }, { 0, 2, -1 }, { 0, 3, -1 } });
            print(b);

            var c = np.where(new bool[,] { { true, false }, { true, true } },
                                    new object[,] { { 1, 2 }, { 3, 4 } },
                                    new object[,] { { 9, 8 }, { 7, 6 } }) as ndarray;

            AssertArray(c, new object[,] { { 1, 8 }, { 3, 4 } });

            print(c);

            return;
        }

        [TestMethod]
        public void test_arange_slice_1_OBJECT()
        {
            var a = np.arange(0, 1024, dtype: np.Int32).reshape(new shape(2, 4, -1));
            a = np.array(a.AsObjectArray()).reshape(new shape(2, 4, -1));

            print("A");
            // print(a);
            print(a.shape);
            print(a.strides);

            AssertShape(a, 2, 4, 128);
            //AssertStrides(a, 8192, 2048, 16);

            var b = (ndarray)a[":", ":", 122];
            print("B");
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB = new object[2, 4]
            {
                { 122, 250, 378, 506},
                { 634, 762, 890, 1018 },
            };

            AssertArray(b, ExpectedDataB);
            AssertShape(b, 2, 4);
            //AssertStrides(b, 8192, 2048);

            var c = (ndarray)a.A(":", ":", new Int64[] { 122 });
            print("C");
            print(c);
            print(c.shape);
            print(c.strides);

            var ExpectedDataC = new object[2, 4, 1]
            {
                {
                    { 122 },
                    { 250 },
                    { 378 },
                    { 506 },
                },
                {
                    { 634 },
                    { 762 },
                    { 890 },
                    { 1018 },
                },

            };

            AssertArray(c, ExpectedDataC);
            AssertShape(c, 2, 4, 1);
            //AssertStrides(c, 64, 16, 128);

            var d = (ndarray)a.A(":", ":", new Int64[] { 122, 123 });
            print("D");
            print(d);
            print(d.shape);
            print(d.strides);

            var ExpectedDataD = new object[2, 4, 2]
            {
                {
                    { 122, 123 },
                    { 250, 251 },
                    { 378, 379 },
                    { 506, 507 },
                },
                {
                    { 634, 635 },
                    { 762, 763 },
                    { 890, 891 },
                    { 1018, 1019 },
                },

            };

            AssertArray(d, ExpectedDataD);
            AssertShape(d, 2, 4, 2);
            //AssertStrides(d, 64, 16, 128);

        }

        [TestMethod]
        public void test_arange_slice_2A_OBJECT()
        {
            var a = np.arange(0, 32, dtype: np.Int32).reshape(new shape(2, 4, -1));
            a = np.array(a.AsObjectArray()).reshape(new shape(2, 4, -1));

            print("A");
            // print(a);
            print(a.shape);
            print(a.strides);

            var b = (ndarray)a[":", ":", np.where(a > 20)];
            print("B");
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB = new object[,,,]
                {{{{1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1},
                   {1,  1,  1,  2,  2,  2,  2,  3,  3,  3,  3},
                   {1,  2,  3,  0,  1,  2,  3,  0,  1,  2,  3}},

                  {{5,  5,  5,  5,  5,  5,  5,  5,  5,  5,  5},
                   {5,  5,  5,  6,  6,  6,  6,  7,  7,  7,  7},
                   {5,  6,  7,  4,  5,  6,  7,  4,  5,  6,  7}},

                  {{9,  9,  9,  9,  9,  9,  9,  9,  9,  9,  9},
                   {9,  9,  9, 10, 10, 10, 10, 11, 11, 11, 11},
                   {9, 10, 11,  8,  9, 10, 11,  8,  9, 10, 11}},

                  {{13, 13 ,13, 13, 13, 13, 13, 13, 13, 13, 13},
                   {13, 13, 13, 14, 14, 14, 14, 15, 15, 15, 15},
                   {13, 14, 15, 12, 13, 14, 15, 12, 13, 14, 15}}},

                 {{{17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17},
                   {17, 17, 17, 18, 18, 18, 18, 19, 19, 19, 19},
                   {17, 18, 19, 16, 17, 18, 19, 16, 17, 18, 19}},

                  {{21, 21, 21, 21, 21, 21, 21, 21, 21, 21, 21},
                   {21, 21, 21, 22, 22, 22, 22, 23, 23, 23, 23},
                   {21, 22, 23, 20, 21, 22, 23, 20, 21, 22, 23}},

                  {{25, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25},
                   {25, 25, 25, 26, 26, 26, 26, 27, 27, 27, 27},
                   {25, 26, 27, 24, 25, 26, 27, 24, 25, 26, 27}},

                  {{29, 29, 29, 29, 29, 29, 29, 29, 29, 29, 29},
                   {29, 29, 29, 30, 30, 30, 30, 31, 31, 31, 31},
                   {29, 30, 31, 28, 29, 30, 31, 28, 29, 30, 31}}}};

            AssertArray(b, ExpectedDataB);
            //AssertStrides(b, 64, 16, 1408, 128);
        }

        [TestMethod]
        public void test_insert_1_OBJECT()
        {
            object[,] TestData = new object[,] { { 1, 1 }, { 2, 2 }, { 3, 3 } };
            ndarray a = np.array(TestData, dtype: np.Int32);
            a = np.array(a.AsObjectArray());

            ndarray b = np.insert(a, 1, 5);
            ndarray c = np.insert(a, 0, new object[] { 999, 100, 101 });

            print(a);
            print(a.shape);

            print("B");
            print(b);
            print(b.shape);
            print(b.strides);

            AssertArray(b, new int[] { 1, 5, 1, 2, 2, 3, 3 });
            AssertShape(b, 7);
            //AssertStrides(b, 16);

            print("C");
            print(c);
            print(c.shape);
            print(c.strides);

            AssertArray(c, new int[] { 999, 100, 101, 1, 1, 2, 2, 3, 3 });
            AssertShape(c, 9);
            //AssertStrides(c, 16);
        }

        [TestMethod]
        public void test_insert_2_OBJECT()
        {
            int[] TestData1 = new int[] { 1, 1, 2, 2, 3, 3 };
            int[] TestData2 = new int[] { 90, 91, 92, 92, 93, 93 };

            ndarray a = np.array(TestData1, dtype: np.Int32);
            a = np.array(a.AsObjectArray());

            ndarray b = np.array(TestData2, dtype: np.Int32);
            b = np.array(b.AsObjectArray());

            ndarray c = np.insert(a, new Slice(null), b);

            print(a);
            print(a.shape);

            print("B");
            print(b);
            print(b.shape);
            print(b.strides);

            AssertArray(b, new object[] { 90, 91, 92, 92, 93, 93 });
            AssertShape(b, 6);
            //AssertStrides(b, 4);

            print("C");
            print(c);
            print(c.shape);
            print(c.strides);

            AssertArray(c, new object[] { 90, 1, 91, 1, 92, 2, 92, 2, 93, 3, 93, 3 });
            AssertShape(c, 12);
            //AssertStrides(c, 4);

        }

        [TestMethod]
        public void test_append_1_OBJECT()
        {
            int[] TestData = new int[] { 1, 1, 2, 2, 3, 3 };
            ndarray a = np.array(TestData, dtype: np.Int32);
            a = np.array(a.AsObjectArray());

            ndarray b = np.append(a, (object)1);

            print(a);
            print(a.shape);

            print(b);
            print(b.shape);
            print(b.strides);

            AssertArray(b, new object[] { 1, 1, 2, 2, 3, 3, 1 });
            AssertShape(b, 7);
            //AssertStrides(b, 16);
        }

        [TestMethod]
        public void test_append_3_OBJECT()
        {
            object[] TestData1 = new object[] { "1", "1", "2", "2", "3", "3" };
            object[] TestData2 = new object[] { "4", "4", "5", "5", "6", "6" };
            ndarray a = np.array(TestData1, dtype: np.Object);
            ndarray b = np.array(TestData2, dtype: np.Object);


            ndarray c = np.append(a, b);

            print(a);
            print(a.shape);

            print(b);
            print(b.shape);

            print(c);
            print(c.shape);
            print(c.strides);

            AssertArray(c, new object[] { "1", "1", "2", "2", "3", "3", "4", "4", "5", "5", "6", "6" });
            AssertShape(c, 12);
            //AssertStrides(c, 16);
        }

        [TestMethod]
        public void test_append_4_OBJECT()
        {
            object[] TestData1 = new object[] { "1", "1", "2", "2", "3", "3" };
            object[] TestData2 = new object[] { "4", "4", "5", "5", "6", "6" };
            ndarray a = np.array(TestData1, dtype: np.Object).reshape((2, -1));
            ndarray b = np.array(TestData2, dtype: np.Object).reshape((2, -1));

            ndarray c = np.append(a, b, axis: 1);

            print(a);
            print(a.shape);
            print("");

            print(b);
            print(b.shape);
            print("");

            print(c);
            print(c.shape);
            print(c.strides);
            print("");

            var ExpectedDataC = new object[,]
            {
                { "1", "1", "2", "4", "4", "5" },
                { "2", "3", "3", "5", "6", "6" },
            };

            AssertArray(c, ExpectedDataC);
            AssertShape(c, 2, 6);
            //AssertStrides(c, 24, 4); 

        }

        [TestMethod]
        public void test_flat_2_OBJECT()
        {
            var x = np.arange(1, 7, dtype: np.Int32).reshape((2, 3));
            x = np.array(x.AsObjectArray()).reshape((2, 3));

            print(x);

            Assert.AreEqual((object)4, x.Flat[3]);
            print(x.Flat[3]);

            print(x.T);
            Assert.AreEqual((object)5, x.T.Flat[3]);
            print(x.T.Flat[3]);

            x.flat = 3;
            AssertArray(x, new object[,] { { 3, 3, 3 }, { 3, 3, 3 } });
            print(x);

            x.Flat[new int[] { 1, 4 }] = 1;
            AssertArray(x, new object[,] { { 3, 1, 3 }, { 3, 1, 3 } });
            print(x);
        }

        [TestMethod]
        public void test_intersect1d_1_OBJECT()
        {
            ndarray a = np.array(new Object[] { 1, 3, 4, 3 });
            ndarray b = np.array(new Object[] { 3, 1, 2, 1 });

            ndarray c = np.intersect1d(a, b);
            print(c);

            AssertArray(c, new object[] { 1, 3 });
            AssertShape(c, 2);
            //AssertStrides(c, 16);

        }

        [TestMethod]
        public void test_setxor1d_1_OBJECT()
        {
            ndarray a = np.array(new Object[] { 1, 2, 3, 2, 4 });
            ndarray b = np.array(new Object[] { 2, 3, 5, 7, 5 });

            ndarray c = np.setxor1d(a, b);
            print(c);

            AssertArray(c, new Object[] { 1, 4, 5, 7 });
            AssertShape(c, 4);
            //AssertStrides(c, 16);
        }

        [TestMethod]
        public void test_in1d_1_OBJECT()
        {
            ndarray test = np.array(new Object[] { 0, 1, 2, 5, 0 });
            ndarray states = np.array(new Object[] { 0, 2 });

            ndarray mask = np.in1d(test, states);
            print(mask);
            print(test[mask]);

            AssertArray(mask, new bool[] { true, false, true, false, true });
            AssertShape(mask, 5);
            //AssertStrides(mask, 1);

            ndarray a = test[mask] as ndarray;
            AssertArray(a, new Object[] { 0, 2, 0 });
            AssertShape(a, 3);
            //AssertStrides(a, 16);

            mask = np.in1d(test, states, invert: true);
            print(mask);
            print(test[mask]);

            AssertArray(mask, new bool[] { false, true, false, true, false });
            AssertShape(mask, 5);
            //AssertStrides(mask, 1);

            ndarray b = test[mask] as ndarray;
            AssertArray(b, new Object[] { 1, 5 });
            AssertShape(b, 2);
            //AssertStrides(b, 16);

        }

        [TestMethod]
        public void test_isin_1_OBJECT()
        {
            ndarray element = 2 * np.arange(4, dtype: np.Int32).reshape(new shape(2, 2));
            element = element.astype(np.Object);
            print(element);

            ndarray test_elements = np.array(new object[] { 1, 2, 4, 8 });
            ndarray mask = np.isin(element, test_elements);
            print(mask);
            print(element[mask]);

            ndarray a = element[mask] as ndarray;

            var ExpectedDataMask = new bool[2, 2]
            {
                { false, true },
                { true, false },
            };

            AssertArray(mask, ExpectedDataMask);
            AssertShape(mask, 2, 2);
            //AssertStrides(mask, 2, 1);

            AssertArray(a, new object[] { 2, 4 });
            AssertShape(a, 2);
            //AssertStrides(a, 16);

            print("***********");

            mask = np.isin(element, test_elements, invert: true);
            print(mask);
            print(element[mask]);

            a = element[mask] as ndarray;


            ExpectedDataMask = new bool[2, 2]
            {
                { true, false },
                { false, true },
            };


            AssertArray(mask, ExpectedDataMask);
            AssertShape(mask, 2, 2);
            //AssertStrides(mask, 2, 1);

            AssertArray(a, new object[] { 0, 6 });
            AssertShape(a, 2);
            //AssertStrides(a, 16);
        }

        [TestMethod]
        public void test_union1d_1_OBJECT()
        {
            ndarray a1 = np.array(new object[] { -1, 0, 1 });
            ndarray a2 = np.array(new object[] { -2, 0, 2 });

            ndarray a = np.union1d(a1, a2);
            print(a);

            AssertArray(a, new object[] { -2, -1, 0, 1, 2 });
            AssertShape(a, 5);
            //AssertStrides(a, 16);
        }

        [TestMethod]
        public void test_Ellipsis_indexing_1_OBJECT()
        {
            var a = np.array(new object[] { 10, 7, 4, 3, 2, 1 });

            var b = a.A("...", -1);
            Assert.AreEqual((object)1, b.GetItem(0));
            print(b);
            print("********");


            a = np.array(new object[,] { { 10, 7, 4 }, { 3, 2, 1 } });
            var c = a.A("...", -1);
            AssertArray(c, new object[] { 4, 1 });
            print(c);
            print("********");

            var TestData = new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            a = np.array(TestData, dtype: np.Object).reshape((1, 3, 2, -1, 1));
            var d = a["...", -1] as ndarray;
            AssertArray(d, new object[,,,] { { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } }, { { 9, 10 }, { 11, 12 } } } });
            print(d);
            print("********");

            var e = a[0, "...", -1] as ndarray;
            AssertArray(e, new object[,,] { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } }, { { 9, 10 }, { 11, 12 } } });
            print(e);
            print("********");

            var f = a[0, ":", ":", ":", -1] as ndarray;
            AssertArray(f, new object[,,] { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } }, { { 9, 10 }, { 11, 12 } } });
            print(f);
            print("********");

            var g = a.A(0, 1, "...", -1);
            AssertArray(g, new object[,] { { 5, 6 }, { 7, 8 } });
            print(g);
            print("********");

            var h = a.A(0, 2, 1, "...", -1);
            AssertArray(h, new object[] { 11, 12 });
            print(h);
            print("********");

            var i = a[":", 2, 1, 1, "..."] as ndarray;
            AssertArray(i, new object[,] { { 12 } });
            print(i);
        }

        [TestMethod]
        public void test_concatenate_1_OBJECT()
        {

            var a = np.array(new object[,] { { 1, 2 }, { 3, 4 } });
            var b = np.array(new object[,] { { 5, 6 } });
            var c = np.concatenate((a, b), axis: 0);
            AssertArray(c, new object[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            print(c);

            var d = np.concatenate((a, b.T), axis: 1);
            AssertArray(d, new object[,] { { 1, 2, 5 }, { 3, 4, 6 } });
            print(d);

            var e = np.concatenate((a, b), axis: null);
            AssertArray(e, new object[] { 1, 2, 3, 4, 5, 6 });
            print(e);

            var f = np.concatenate((np.eye(2, dtype: np.Object), np.ones((2, 2), dtype: np.Object)), axis: 0);
            //AssertArray(f, new object[,] { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 1, 1 }, });
            print(f);

            var g = np.concatenate((np.eye(2, dtype: np.Object), np.ones((2, 2), dtype: np.Object)), axis: 1);
            //AssertArray(g, new object[,] { { 1, 0, 1, 1 }, { 0, 1, 1, 1 } });
            print(g);
        }

        [TestMethod]
        public void test_concatenate_3_OBJECT()
        {

            var a = np.array(new object[,,,] { { { { 1, 2 }, { 3, 4 }, { 5, 6 } } } });
            var c = np.concatenate(a, axis: -1);
            AssertArray(c, new object[,,] { { { 1, 2 }, { 3, 4 }, { 5, 6 } } });
            print(c);

            var d = np.concatenate(a, axis: -2);
            AssertArray(d, new object[,,] { { { 1, 2 }, { 3, 4 }, { 5, 6 } } });
            print(d);

            c = np.concatenate((a, a, a), axis: -1);
            AssertArray(c, new object[,,,] { { { { 1, 2, 1, 2, 1, 2 }, { 3, 4, 3, 4, 3, 4 }, { 5, 6, 5, 6, 5, 6 } } } });
            print(c);

            d = np.concatenate((a, a, a), axis: -2);
            AssertArray(d, new object[,,,] { { { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 1, 2 }, { 3, 4 }, { 5, 6 }, { 1, 2 }, { 3, 4 }, { 5, 6 } } } });
            print(d);


        }

        #endregion

        #region from NumericalOperationsTests


        [TestMethod]
        public void test_rot90_1_OBJECT()
        {
            ndarray m = np.array(new Int32[,] { { 1, 2 }, { 3, 4 } }, np.Object);
            print(m);
            print("************");

            ndarray n = np.rot90(m);
            print(n);
            AssertArray(n, new Object[,] { { 2, 4 }, { 1, 3 }, });
            print("************");

            n = np.rot90(m, 2);
            print(n);
            AssertArray(n, new Object[,] { { 4, 3 }, { 2, 1 }, });
            print("************");

            m = np.arange(8, dtype: np.Int32).reshape(new shape(2, 2, 2)).astype(np.Object);
            n = np.rot90(m, 1, new int[] { 1, 2 });
            print(n);
            AssertArray(n, new Object[,,] { { { 1, 3 }, { 0, 2 } }, { { 5, 7 }, { 4, 6 } } });

        }

        [TestMethod]
        public void test_flip_1_OBJECT()
        {
            ndarray A = np.arange(8, dtype: np.Int32).reshape(new shape(2, 2, 2)).astype(np.Object);
            ndarray B = np.flip(A, 0);
            print(A);
            print("************");
            print(B);
            AssertArray(B, new Object[,,] { { { 4, 5 }, { 6, 7 } }, { { 0, 1 }, { 2, 3 } } });

            print("************");
            ndarray C = np.flip(A, 1);
            print(C);
            AssertArray(C, new Object[,,] { { { 2, 3 }, { 0, 1 } }, { { 6, 7 }, { 4, 5 } } });
            print("************");

        }

        [TestMethod]
        public void test_trim_zeros_1()
        {
            ndarray a = np.array(new Object[] { 0, 0, 0, 1, 2, 3, 0, 2, 1, 0 });

            var b = np.trim_zeros(a);
            print(b);
            AssertArray(b, new Object[] { 1, 2, 3, 0, 2, 1 });

            var c = np.trim_zeros(a, "b");
            print(c);
            AssertArray(c, new Object[] { 0, 0, 0, 1, 2, 3, 0, 2, 1 });
        }

  

        [TestMethod]
        public void test_copyto_1_OBJECT()
        {
            var a = np.zeros((2, 5), dtype: np.Object);
            var b = new int[] { 11, 22, 33, 44, 55 };
            np.copyto(a, b);

            AssertShape(a, 2, 5);
            AssertArray(a, new Object[,] { { 11, 22, 33, 44, 55 }, { 11, 22, 33, 44, 55 } });
            print(a);

            a = np.zeros((4, 5), dtype: np.Object);
            np.copyto(a, 99);
            AssertArray(a, new Object[,] { { 99, 99, 99, 99, 99 }, { 99, 99, 99, 99, 99 }, { 99, 99, 99, 99, 99 }, { 99, 99, 99, 99, 99 } });
            print(a);

            a = np.zeros((10, 5), dtype: np.Object);
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
        public void test_copyto_2_OBJECT()
        {
            var a = np.zeros((1, 2, 2, 1, 2), dtype: np.Object);
            var b = new int[] { 1, 2 };
            np.copyto(a, b);

            AssertArray(a, new Object[,,,,] { { { { { 1, 2 } }, { { 1, 2 } } }, { { { 1, 2 } }, { { 1, 2, } } } } });

        }

        #endregion

        #region from MathematicalFunctionsTests

        private string MathFunctionExceptionPrefix = "Arrays of type";

        [TestMethod]
        public void test_sin_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.sin(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }
  
        }

        [TestMethod]
        public void test_cos_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.cos(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        [TestMethod]
        public void test_tan_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.tan(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        [TestMethod]
        public void test_arcsin_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.arcsin(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        [TestMethod]
        public void test_arccos_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.arccos(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        [TestMethod]
        public void test_arctan_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.arctan(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        [TestMethod]
        public void test_hypot_1_OBJECT()
        {
            try
            {
                var b = np.hypot(np.ones((3, 3), dtype: np.Object) * 3, np.ones((3, 3), dtype: np.Object) * 4);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }
            
        }

        [TestMethod]
        public void test_arctan2_1_OBJECT()
        {
            try
            {
                var x = np.array(new Object[] { -1, +1, +1, -1 });
                var y = np.array(new Object[] { -1, -1, +1, +1 });
                var z = np.arctan2(y, x);
                print(z);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        #region Hyperbolic functions

        [TestMethod]
        public void test_sinh_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.sinh(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }
        }

        [TestMethod]
        public void test_cosh_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.cosh(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }


        }

        [TestMethod]
        public void test_tanh_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.tanh(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        [TestMethod]
        public void test_arcsinh_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.arcsinh(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        [TestMethod]
        public void test_arccosh_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.arccosh(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        [TestMethod]
        public void test_arctanh_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.arctanh(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        #endregion

        [TestMethod]
        public void test_degrees_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.degrees(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        [TestMethod]
        public void test_radians_1_OBJECT()
        {
            var a = np.arange(0, 10, dtype: np.Int32).astype(np.Object);
            a = a["::2"] as ndarray;

            try
            {
                var b = np.radians(a);
                Assert.Fail("This should have caused an exception");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(MathFunctionExceptionPrefix));
            }

        }

        [TestMethod]
        public void test_around_1_OBJECT()
        {
            ndarray a = np.around(np.array(new Object[] { (int)37, (double)164.2 }));
            print(a);
            AssertArray(a, new Object[] { (double)37, (double)164 });

            ndarray b = np.around(np.array(new Object[] { 37.22, 164.22 }), decimals: 1);
            print(b);
            AssertArray(b, new Object[] { (double)37.2, (double)164.2 });

            ndarray c = np.around(np.array(new Object[] { 5.1, 15.49, 25.499, 35.4999, 44.89 })); // rounds to nearest even value
            print(c);
            AssertArray(c, new Object[] { (double)5, (double)15, (double)25, (double)35, (double)45 });

            ndarray d = np.around(np.array(new Object[] { 1.23, 2.34, 3.45, 11.56 }), decimals: 1); // ndarray of ints is returned
            print(d);
            AssertArray(d, new Object[] { (double)1.2, (double)2.3, (double)3.4, (double)11.6 });


            try
            {
                ndarray e = np.around(np.array(new Object[] { "A", "B", "C", "D" }), decimals: -1);
                Assert.Fail("This should have thrown an exception");
            }
            catch (Exception ex)
            {

            }
   
        }

        [TestMethod]
        public void test_round_1_OBJECT()
        {
            double ref_step = 0;
            var a = np.linspace(-2, 10, ref ref_step, 12).reshape((2, 2, 3)).astype(np.Object);
            print(a);

            var ExpectedData1 = new object[,,] {{ { (double)-2, (double)-0.91, (double)0.18 }, { (double)1.27, (double)2.36, (double)3.45 } }, 
                                                { { (double)4.55, (double)5.64, (double)6.73 }, { (double)7.82, (double)8.91, (double)10 } } };
        
            print("********");
            var b = np.round_(a, 2);
            AssertArray(b, ExpectedData1);
            print(b);

            print("********");

            a[1,1,1] = "X";

            try
            {
                var c = np.round(a, 2);
                Assert.Fail("This should have thrown an exception");

            }
            catch (Exception ex)
            {

            }
            print(a);
  
        }

        [TestMethod]
        public void test_fix_1_OBJECT()
        {
            var a = np.fix((object)3.14);
            Assert.AreEqual((double)3, a.GetItem(0));
            print(a);

            var b = np.fix((object)3);
            Assert.AreEqual((Int32)3, b.GetItem(0));
            print(b);

            var c = np.fix(new object[] { 21, 29, -21, -29 });
            AssertArray(c, new Int32[] { 21, 29, -21, -29 });
            print(c);
        }

        [TestMethod]
        public void test_floor_1_OBJECT()
        {
            object[] TestData = new object[] { -17, -15, -02, 02, 15, 17, 20 };
            var x = np.array(TestData);
            var y = np.floor(x);

            print(x);
            print(y);

            AssertArray(y, new double[] { -17, -15, -02, 02, 15, 17, 20 });

        }

        [TestMethod]
        public void test_ceil_1_OBJECT()
        {
            object[] TestData = new object[] { -17, -15, -02, 02, 15, 17, 20 };
            var x = np.array(TestData);
            var y = np.ceil(x);

            print(x);
            print(y);

            AssertArray(y, new double[] { -17, -15, -02, 02, 15, 17, 20 });

        }

        [TestMethod]
        public void test_trunc_1_OBJECT()
        {
            var a = np.trunc((object)3.14);
            Assert.AreEqual((double)3.0, a.GetItem(0));
            print(a);

            var b = np.trunc((object)3);
            Assert.AreEqual((Int32)3, b.GetItem(0));
            print(b);

            var c = np.trunc(np.array(new object[] { 21, 29f, -21, -29 }));
            AssertArray(c, new double[] { 21, 29, -21, -29 });
            print(c);
        }

        [TestMethod]
        public void test_prod_2_OBJECT()
        {
            ndarray a = np.prod(np.array(new object[] { 1, 2 }));
            print(a);
            Assert.AreEqual(2, a.GetItem(0));
            print("*****");

            ndarray b = np.prod(np.array(new object[,] { { 1, 2 }, { 3, 4 } }));
            print(b);
            Assert.AreEqual((Int64)24, b.GetItem(0));
            print("*****");

            ndarray c = np.prod(np.array(new object[,] { { 1, 2 }, { 3, 4 } }), axis: 1);
            print(c);
            AssertArray(c, new Int64[] { 2, 12 });
            print("*****");

            ndarray d = np.array(new object[] { 1, 2, 3 }, dtype: np.Object);
            bool e = np.prod(d).Dtype.TypeNum == NPY_TYPES.NPY_OBJECT;
            print(e);
            Assert.AreEqual(true, e);
            print("*****");


            try
            {
                a = np.array(new object[,] { { 1, 2 }, { 3, 4 } }, dtype: np.Object);
                a[1, 1] = "X";

                b = np.prod(a);
                Assert.Fail("Should have throw exception");
            }
            catch
            {

            }
    


        }

        [TestMethod]
        public void test_sum_2_OBJECT()
        {
            Object[] TestData = new Object[] { 10, 15, 25, 45, 78, 90, 10, 15, 25, 45, 78, 90 };
            var x = np.array(TestData, dtype: np.Object).reshape(new shape(3, 2, -1));
            x = x * 3;

            var y = np.sum(x, axis: 0);
            print(y);
            AssertArray(y, new Object[,] { { 339, 450 }, { 339, 450 } });

            print("*****");

            y = np.sum(x, axis: 1);
            print(y);
            AssertArray(y, new Object[,] { { 105, 180 }, { 264, 315 }, { 309, 405 } });

            print("*****");

            y = np.sum(x, axis: 2);
            print(y);
            AssertArray(y, new Object[,] { { 75, 210 }, { 504, 75 }, { 210, 504 } });

            print("*****");

            try
            {
                x[0, 1, 1] = "X";
                y = np.sum(x, axis: 2);
                Assert.Fail("This should have thrown an exception");
            }
            catch (Exception ex)
            {

            }


        }

        [TestMethod]
        public void test_cumprod_2_OBJECT()
        {
            ndarray a = np.array(new Object[] { 1, 2, 3 });
            ndarray b = np.cumprod(a);          // intermediate results 1, 1*2
                                                // total product 1*2*3 = 6
            print(b);
            AssertArray(b, new Object[] { 1, 2, 6 });
            print("*****");

            a = np.array(new Object[,] { { 1, 2, 3 }, { 4, 5, 6 } }, dtype: np.Object);
            ndarray c = np.cumprod(a, dtype: np.Object); //specify type of output
            print(c);
            AssertArray(c, new Object[] { 1, 2, 6, 24, 120, 720 });
            print("*****");

            ndarray d = np.cumprod(a, axis: 0);
            print(d);
            AssertArray(d, new Object[,] { { 1, 2, 3 }, { 4, 10, 18 } });
            print("*****");

            ndarray e = np.cumprod(a, axis: 1);
            print(e);
            AssertArray(e, new Object[,] { { 1, 2, 6 }, { 4, 20, 120 } });
            print("*****");


            try
            {
                a[0] = "X";
                ndarray f = np.cumprod(a, axis: 1);
                Assert.Fail("Should have thrown an exception");
            }
            catch (Exception ex)
            {

            }

        }

        [TestMethod]
        public void test_cumsum_3_OBJECT()
        {
            ndarray a = np.array(new Object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }).reshape(new shape(2, 3, -1));
            print(a);
            print("*****");

            ndarray b = np.cumsum(a);
            print(b);
            AssertArray(b, new Object[] { 1, 3, 6, 10, 15, 21, 28,
                                         36, 45, 55, 66, 78 });
            print("*****");

            ndarray c = np.cumsum(a, dtype: np.Object);     // specifies type of output value(s)
            print(c);
            AssertArray(c, new Object[] { 1, 3, 6, 10, 15, 21, 28,
                                             36, 45, 55, 66, 78 });
            print("*****");

            ndarray d = np.cumsum(a, axis: 0);     // sum over rows for each of the 3 columns
            print(d);

            var ExpectedDataD = new Object[,,]
            {{{1,  2},
              {3,  4},
              {5,  6}},

             {{8, 10},
              {12, 14},
              {16, 18}}};

            AssertArray(d, ExpectedDataD);
            print("*****");



            ndarray e = np.cumsum(a, axis: 1);    // sum over columns for each of the 2 rows
            print(e);

            var ExpectedDataE = new Object[,,]
            {{{1,  2},
              {4,  6},
              {9,  12}},

             {{7, 8},
              {16, 18},
              {27, 30}}};

            AssertArray(e, ExpectedDataE);
            print("*****");

            try
            {
                a[1, 1, 1] = "X";
                ndarray f = np.cumsum(a, axis: 2);    // sum over columns for each of the 2 rows
                Assert.Fail("this should have thrown exception");
            }
            catch (Exception ex)
            {

            }
 

        }

        [TestMethod]
        public void test_diff_3_OBJECT()
        {
            Object[] TestData = new Object[] { 10, 15, 25, 45, 78, 90, 10, 15, 25, 45, 78, 90 };
            var x = np.array(TestData, dtype: np.Object).reshape(new shape(3, 2, -1));
            x = x * 3;
            var y = np.diff(x, axis: 2);

            print(x);
            print(y);

            var ExpectedData = new Object[,,]
                {
                 {{15},
                  {60}},

                 {{36},
                  {15}},

                 {{60},
                  {36}}
                };

            AssertArray(y, ExpectedData);

            try
            {
                x[0] = "X";
                y = np.diff(x, axis: 2);
                Assert.Fail("This should have thrown an exception");
            }
            catch (Exception ex)
            {

            }

        }

        [TestMethod]
        public void test_ediff1d_1_OBJECT()
        {
            ndarray x = np.array(new Object[] { 1, 2, 4, 7, 0 });
            ndarray y = np.ediff1d(x);
            print(y);
            AssertArray(y, new Object[] { 1, 2, 3, -7 });

            y = np.ediff1d(x, to_begin: np.array(new Object[] { -99 }), to_end: np.array(new Object[] { 88, 99 }));
            print(y);
            AssertArray(y, new Object[] { -99, 1, 2, 3, -7, 88, 99 });

            x = np.array(new Object[,] { { 1, 2, 4 }, { 1, 6, 24 } }, dtype: np.Object);
            y = np.ediff1d(x);
            print(y);
            AssertArray(y, new Object[] { 1, 2, -3, 5, 18 });

            try
            {
                x[0] = "X";
                y = np.ediff1d(x);
                Assert.Fail("This should have thrown an error");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_gradient_1_OBJECT()
        {
            // note: This function requires that all values be cast to doubles.
            // since objects get cast to 0 in this case, effectively this function does not work with objects
            var f = np.array(new object[] { 1, 2, 4, 7, 11, 16 }, dtype: np.Object);
            var a = np.gradient(f);
            AssertArray(a[0], new double[] { 0, 0, 0, 0, 0, 0});
            print(a[0]);
            print("***********");
        }

        [TestMethod]
        public void test_cross_2_OBJECT()
        {
            // Multiple vector cross-products. Note that the direction of the cross
            // product vector is defined by the `right-hand rule`.

            var x = np.array(new Object[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            var y = np.array(new Object[,] { { 4, 5, 6 }, { 1, 2, 3 } });
            var a = np.cross(x, y);
            AssertArray(a, new Object[,] { { -3, 6, -3 }, { 3, -6, 3 } });
            print(a);


            // The orientation of `c` can be changed using the `axisc` keyword.

            var b = np.cross(x, y, axisc: 0);
            AssertArray(b, new Object[,] { { -3, 3 }, { 6, -6 }, { -3, 3 } });
            print(b);

            // Change the vector definition of `x` and `y` using `axisa` and `axisb`.

            x = np.array(new Object[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
            y = np.array(new Object[,] { { 7, 8, 9 }, { 4, 5, 6 }, { 1, 2, 3 } });
            a = np.cross(x, y);
            AssertArray(a, new Object[,] { { -6, 12, -6 }, { 0, 0, 0 }, { 6, -12, 6 } });
            print(a);

            b = np.cross(x, y, axisa: 0, axisb: 0);
            AssertArray(b, new Object[,] { { -24, 48, -24 }, { -30, 60, -30 }, { -36, 72, -36 } });
            print(b);

            return;
        }

        [TestMethod]
        public void test_trapz_1_OBJECT()
        {
            var a = np.trapz(new Object[] { 1, 2, 3 });
            Assert.AreEqual((double)4.0, a.GetItem(0));
            print(a);

            var b = np.trapz(new Object[] { 1, 2, 3 }, x: new int[] { 4, 6, 8 });
            Assert.AreEqual((double)8.0, b.GetItem(0));
            print(b);

            var c = np.trapz(new Object[] { 1, 2, 3 }, dx: 2);
            Assert.AreEqual((double)8.0, c.GetItem(0));
            print(c);

            a = np.arange(6, dtype: np.Int32).reshape((2, 3)).astype(np.Object);
            b = np.trapz(a, axis: 0);
            AssertArray(b, new double[] { 1.5, 2.5, 3.5 });
            print(b);

            c = np.trapz(a, axis: 1);
            AssertArray(c, new double[] { 2.0, 8.0 });
            print(c);
        }

        [TestMethod]
        public void test_exp_1_OBJECT()
        {
     
            var x = np.array(new object[] { -17, -15, -02, 02, 15, 17, 20, -42 });

            try
            {
                var a = np.exp(x);
                Assert.Fail("This should throw an exception");
            }
            catch
            {

            }
     

        }

        [TestMethod]
        public void test_exp2_1_OBJECT()
        {
            var x = np.array(new object[] { -17, -15, -02, 02, 15, 17, 20, -42 });
            try
            {
                var a = np.exp2(x);
                Assert.Fail("This should throw an exception");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_i0_1_OBJECT()
        {
            var a = np.i0((Object)5);
            Assert.AreEqual(27.239871823604442, a.GetItem(0));
            print(a);

            a = np.i0(new Object[] { 5, 6 });
            AssertArray(a, new double[] { 27.239871823604442, 67.234406976478 });
            print(a);

            a = np.i0(new Object[,] { { 27.239871823604442, 67.234406976478 }, { 389.40628328, 427.56411572 } });
            AssertArray(a, new double[,] { { 51935526724.290375, 7.7171998335650329E+27 }, { 2.6475747102348978E+167, 9.4248115430920975E+183 } });
            print(a);

            return;

        }

        [TestMethod]
        public void test_sinc_1_OBJECT()
        {
            double retstep = 0;
            var x = np.linspace(-4, 4, ref retstep, 10, dtype: np.Int64).astype(np.Object);
            var a = np.sinc(x);
            AssertArray(a, new double[] { -3.89804309105148E-17, 3.89804309105148E-17, -3.89804309105148E-17,
                                           3.89804309105148E-17, 1.0, 1.0, 3.89804309105148E-17, -3.89804309105148E-17,
                                           3.89804309105148E-17, -3.89804309105148E-17 });
            print(a);

            print("********");

            var xx = np.outer(x, x);
            var b = np.sinc(xx);

            var ExpectedDataB = new double[,]

          { { -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, 1.0, 1.0,
              -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17 },
            { -3.89804309105148E-17, 3.89804309105148E-17, -3.89804309105148E-17, 3.89804309105148E-17, 1.0, 1.0,
               3.89804309105148E-17, -3.89804309105148E-17, 3.89804309105148E-17, -3.89804309105148E-17 },
            { -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, 1.0, 1.0,
              -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17 },
            { -3.89804309105148E-17, 3.89804309105148E-17, -3.89804309105148E-17, 3.89804309105148E-17, 1.0, 1.0,
               3.89804309105148E-17, -3.89804309105148E-17, 3.89804309105148E-17, -3.89804309105148E-17 },
            { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
            { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
            { -3.89804309105148E-17, 3.89804309105148E-17, -3.89804309105148E-17, 3.89804309105148E-17, 1.0, 1.0,
               3.89804309105148E-17, -3.89804309105148E-17, 3.89804309105148E-17, -3.89804309105148E-17 },
            { -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, 1.0, 1.0,
              -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17 },
            { -3.89804309105148E-17, 3.89804309105148E-17, -3.89804309105148E-17, 3.89804309105148E-17, 1.0, 1.0,
               3.89804309105148E-17, -3.89804309105148E-17, 3.89804309105148E-17, -3.89804309105148E-17 },
            { -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, 1.0, 1.0,
              -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17, -3.89804309105148E-17 } };

            AssertArray(b, ExpectedDataB);

            print(b);


        }

        [TestMethod]
        public void test_signbit_1_OBJECT()
        {
            var a = np.signbit((Object)(-12));
            Assert.AreEqual(true, a.GetItem(0));
            print(a);

            try
            {
                var b = np.signbit(np.array(new Object[] { 1, -23, 21 }));
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_copysign_1_OBJECT()
        {
            var a = np.copysign((Object)13, (Object)(-1));
            Assert.AreEqual((Object)(-13), a.GetItem(0));
            print(a);
    
            try
            {
                var d = np.copysign(np.array(new Object[] { -1, 0, 1 }, dtype: np.Object), -1.1);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_frexp_1_OBJECT()
        {
            var x = np.arange(9, dtype: np.Int32).astype(np.Object);

            try
            {
                var results = np.frexp(x);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

  
        }

        [TestMethod]
        public void test_ldexp_1_OBJECT()
        {
            try
            {
                var a = np.ldexp((Object)5, np.arange(4, dtype: np.Int32)).astype(np.Object);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_lcm_1_OBJECT()
        {
            try
            {
                var d = np.lcm(np.arange(6, dtype: np.Int32).astype(np.Object), new Object[] { 20 });
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {
            }
 
        }

        [TestMethod]
        public void test_gcd_1_OBJECT()
        {
            try
            {
                var d = np.gcd(np.arange(6, dtype: np.Int32).astype(np.Object), new Object[] { 20 });
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {
            }
  
        }

        [TestMethod]
        public void test_add_1_OBJECT()
        {
            var a = np.add(1, 4).astype(np.Object);
            Assert.AreEqual((Object)5, a.GetItem(0));
            print(a);

            var b = np.arange(9, dtype: np.Int32).reshape((3, 3)).astype(np.Object);
            var c = np.arange(3, dtype: np.Int32).astype(np.Object);
            var d = np.add(b, c);
            AssertArray(d, new Object[,] { { 0, 2, 4 }, { 3, 5, 7 }, { 6, 8, 10 } });
            print(d);

            try
            {
                b[0] = new StringBuilder();
                d = np.add(b, c);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }
  
        }

        [TestMethod]
        public void test_reciprocal_operations_OBJECT()
        {
            var a = np.arange(1, 32, 1, dtype: np.Float32).astype(np.Object);
            print(a);

            var b = np.reciprocal(a);
            print(b);

            var ExpectedDataB1 = new float[]
            {
                1.0f, 0.5f,        0.33333334f, 0.25f,       0.2f,        0.16666667f,
                0.14285715f,       0.125f,      0.11111111f, 0.1f,        0.09090909f, 0.08333334f,
                0.07692308f,       0.07142857f, 0.06666667f, 0.0625f,     0.05882353f, 0.05555556f,
                0.05263158f,       0.05f,       0.04761905f, 0.04545455f, 0.04347826f, 0.04166667f,
                0.04f,             0.03846154f, 0.03703704f, 0.03571429f, 0.03448276f, 0.03333334f,
                0.03225806f
            };

            AssertArray(b, ExpectedDataB1);


            a = np.arange(2048, 2048 + 32, 1, dtype: np.Float64).astype(np.Object);
            print(a);

            b = np.reciprocal(a);
            print(b);

            var ExpectedDataB2 = new double[]
             {
                0.00048828, 0.00048804, 0.0004878,  0.00048757, 0.00048733, 0.00048709,
                0.00048685, 0.00048662, 0.00048638, 0.00048614, 0.00048591, 0.00048567,
                0.00048544, 0.0004852,  0.00048497, 0.00048473, 0.0004845,  0.00048426,
                0.00048403, 0.00048379, 0.00048356, 0.00048333, 0.00048309, 0.00048286,
                0.00048263, 0.00048239, 0.00048216, 0.00048193, 0.0004817,  0.00048146,
                0.00048123, 0.000481
             };
            AssertArray(b, ExpectedDataB2);


            try
            {
                a[4] = "X";
                b = np.reciprocal(a);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_positive_1_OBJECT()
        {
            var d = np.positive(np.array(new Int32[] { -1, -0, 1 }).astype(np.Object));
            AssertArray(d, new object[] { -1, -0, 1 });
            print(d);

            var e = np.positive(np.array(new Int32[,] { { 1, 0, -1 }, { -2, 3, -4 } }).astype(np.Object));
            AssertArray(e, new object[,] { { 1, 0, -1 }, { -2, 3, -4 } });
            print(e);

            try
            {
                d = np.array(new Int32[] { -1, -0, 1 }).astype(np.Object);
                d[1] = "X";
                d = np.positive(d);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }
        }

        [TestMethod]
        public void test_negative_1_OBJECT()
        {
            var d = np.negative(np.array(new object[] { -1, -0, 1 }).astype(np.Object));
            AssertArray(d, new Int32[] { 1, 0, -1 });
            print(d);

            var e = np.negative(np.array(new Int32[,] { { 1, 0, -1 }, { -2, 3, -4 } }).astype(np.Object));
            AssertArray(e, new Int32[,] { { -1, 0, 1 }, { 2, -3, 4 } });
            print(e);

            try
            {
                d = np.array(new Int32[] { -1, -0, 1 }).astype(np.Object);
                d[1] = "X";
                d = np.negative(d);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_multiply_1_OBJECT()
        {
            var a = np.multiply((object)2, (object)4);
            Assert.AreEqual((object)8, a.GetItem(0));
            print(a);

            var b = np.arange(9).reshape((3, 3)).astype(np.Object);
            var c = np.arange(3).astype(np.Object);
            var d = np.multiply(b, c);
            AssertArray(d, new object[,] { { 0, 1, 4 }, { 0, 4, 10 }, { 0, 7, 16 } });
            print(d);

            try
            {
                c[1] = "X";
                d = np.multiply(b, c);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }
        }

        [TestMethod]
        public void test_divide_OBJECT()
        {
            var a = np.divide((object)7, (object)3);
            Assert.AreEqual((object)2, a.GetItem(0));
            print(a);

            var b = np.divide(np.array(new Int32[] { 1, 2, 3, 4 }).astype(np.Object), 2);
            AssertArray(b, new object[] { 0, 1, 1, 2 });
            print(b);

            var c = np.divide(np.array(new Int32[] { 2, 4, 6, 8 }).astype(np.Object), np.array(new object[] { 1, 2, 3, 4 }).astype(np.Object));
            AssertArray(c, new object[] { 2, 2, 2, 2 });
            print(c);


            try
            {
                c[1] = "X";
                var d = np.divide(b, c);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }
            return;


        }

        [TestMethod]
        public void test_power_operations_OBJECT()
        {
            var a = np.arange(0, 32, 1, dtype: np.Int32).astype(np.Object);
            print(a);

            var b = np.power(a, 3);
            print(b);

            var ExpectedDataB1 = new double[]
            {
                0, 1, 8, 27, 64, 125, 216, 343, 512, 729, 1000, 1331, 1728, 2197,
                2744, 3375, 4096, 4913, 5832, 6859, 8000, 9261, 10648, 12167, 13824,
                15625, 17576, 19683, 21952, 24389, 27000, 29791
            };

            AssertArray(b, ExpectedDataB1);

            a = np.arange(2048, 2048 + 32, 1, dtype: np.Int32).astype(np.Object);
            print(a);

            b = np.power(a, 4);
            print(b);

            var ExpectedDataB2 = new double[]
            {
                Math.Pow(2048, 4), 17626570956801, 17661006250000, 17695491973201,
                17730028175616, 17764614906481, 17799252215056, 17833940150625,
                17868678762496, 17903468100001, 17938308212496, 17973199149361,
                18008140960000, 18043133693841, 18078177400336, 18113272128961,
                18148417929216, 18183614850625, 18218862942736, 18254162255121,
                18289512837376, 18324914739121, 18360368010000, 18395872699681,
                18431428857856, 18467036534241, 18502695778576, 18538406640625,
                18574169170176, 18609983417041, 18645849431056, 18681767262081 };


            AssertArray(b, ExpectedDataB2);

            b = np.power(a, 0);
            print(b);
            var ExpectedDataB3 = new double[]
            {
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
            };
            AssertArray(b, ExpectedDataB3);

            try
            {
                a[1] = "X";
                var d = np.power(a, 1);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }
            return;


        }

        [TestMethod]
        public void test_subtract_1_OBJECT()
        {
            var a = np.subtract((object)1, (object)4);
            Assert.AreEqual((object)(-3), a.GetItem(0));
            print(a);

            var b = np.arange(9.0, dtype: np.Int32).reshape((3, 3)).astype(np.Object);
            var c = np.arange(3.0, dtype: np.Int32).astype(np.Object);
            var d = np.subtract(b, c);
            AssertArray(d, new object[,] { { 0, 0, 0 }, { 3, 3, 3 }, { 6, 6, 6 } });
            print(d);

            try
            {
                c[1] = "X";
                d = np.subtract(b, c);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }
            return;
        }

        [TestMethod]
        public void test_true_divide_OBJECT()
        {
            var a = np.true_divide((object)7, (object)3);
            Assert.AreEqual(2.3333333333333335, a.GetItem(0));
            print(a);

            var b = np.true_divide(np.array(new Int32[] { 1, 2, 3, 4 }).astype(np.Object), 2.5);
            AssertArray(b, new object[] { 0.4, 0.8, 1.2, 1.6 });
            print(b);

            var c = np.true_divide(np.array(new Int32[] { 1, 2, 3, 4 }).astype(np.Object), new double[] { 0.5, 2.5, 2.5, 3.5 });
            AssertArray(c, new double[] { 2.0, 0.8, 1.2, 1.14285714 });
            print(c);

            return;
        }

        [TestMethod]
        public void test_floor_divide_OBJECT()
        {
            var a = np.floor_divide((object)7, (object)3);
            Assert.AreEqual(2, a.GetItem(0));
            print(a);

            var b = np.floor_divide(np.array(new double[] { 1.0, 2.0, 3.0, 4.0 }).astype(np.Object), 2.5);
            AssertArray(b, new double[] { 0, 0, 1, 1 });
            print(b);

            var c = np.floor_divide(np.array(new double[] { 1.0, 2.0, 3.0, 4.0 }).astype(np.Object), 
                                    np.array(new double[] { 0.5, 2.5, 2.5, 3.5 }).astype(np.Object));
            AssertArray(c, new double[] { 2, 0, 1, 1 });
            print(c);

            return;

        }

        [TestMethod]
        public void test_float_power_OBJECT()
        {
            var x1 = new int[] { 0, 1, 2, 3, 4, 5 };

            try
            {
                var a = np.float_power(np.array(x1).astype(np.Object), 3);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }
     

            return;
        }

        [TestMethod]
        public void test_fmod_2_OBJECT()
        {
            var x = np.fmod(new object[] { -4, -7 }, new object[] { 2, 3 });
            AssertArray(x, new int[] { 0, -1 });
            print(x);

            var y = np.fmod(np.arange(7).astype(np.Object), -5);
            AssertArray(y, new object[] { 0, 1, 2, 3, 4, 0, 1 });
            print(y);

            try
            {
                x = np.arange(7).astype(np.Object);
                x[2] = "X";
                var a = np.fmod(x, 3);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

            return;
        }

        [TestMethod]
        public void test_mod_1_OBJECT()
        {
            var x = np.mod(new object[] { 4, 7 }, new object[] { 2, 3 });
            AssertArray(x, new int[] { 0, 1 });
            print(x);

            var y = np.mod(np.arange(7).astype(np.Object), 5);
            AssertArray(y, new object[] { 0, 1, 2, 3, 4, 0, 1 });
            print(y);

            try
            {
                x = np.arange(7).astype(np.Object);
                x[2] = "X";
                var a = np.mod(x, 3);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

            return;
        }

        [TestMethod]
        public void test_modf_1_OBJECT()
        {
            var x = np.modf(np.array(new double[] { 0, 3.5 }).astype(np.Object));
            AssertArray(x[0], new double[] { 0, 0.5 });
            AssertArray(x[1], new double[] { 0, 3.0 });
            print(x);

            var y = np.modf(np.arange(7.0).astype(np.Object));
            AssertArray(y[0], new float[] { 0, 0, 0, 0, 0, 0, 0 });
            AssertArray(y[1], new double[] { 0, 1, 2, 3, 4, 5, 6 });
            print(y);

            /////////////////////////////
            
            var x1 = np.arange(7.0).astype(np.Object);
            x1[2] = "X";

            try
            {
                var a = np.mod(x1, 3);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

            return;
        }

        [TestMethod]
        public void test_remainder_2_OBJECT()
        {
            var x = np.remainder(np.array(new int[] { -4, -7 }).astype(np.Object), np.array(new int[] { 2, 3 }).astype(np.Object));
            AssertArray(x, new int[] { 0, 2 });
            print(x);

            var y = np.remainder(np.arange(7).astype(np.Object), -5);
            AssertArray(y, new int[] { 0, -4, -3, -2, -1, 0, -4 });
            print(y);

            /////////////////////////////

            var x1 = np.arange(7).astype(np.Object);
            x1[2] = "X";

            try
            {
                var a = np.remainder(x1, 3);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

            return;
        }

        [TestMethod]
        public void test_divmod_1_OBJECT()
        {
            var a = np.divmod((object)7, (object)3);
            Assert.AreEqual(2, a[0].GetItem(0));
            Assert.AreEqual(1, a[1].GetItem(0));

            print(a);

            var b = np.divmod(new object[] { 1.0, 2.0, 3.0, 4.0 }, 2.5);
            AssertArray(b[0], new double[] { 0, 0, 1, 1 });
            AssertArray(b[1], new double[] { 1, 2, 0.5, 1.5 });
            print(b);

            var c = np.divmod(new object[] { 1.0, 2.0, 3.0, 4.0 }, new object[] { 0.5, 2.5, 2.5, 3.5 });
            AssertArray(c[0], new double[] { 2, 0, 1, 1 });
            AssertArray(c[1], new double[] { 0, 2, 0.5, 0.5 });
            print(c);

            /////////////////////////////

            var x1 = np.arange(7).astype(np.Object);
            x1[2] = "X";

            try
            {
                a = np.divmod(x1, 3);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {
            }

            return;

        }

        [TestMethod]
        public void test_convolve_1_OBJECT()
        {
            var a = np.convolve(np.array(new int[] { 1, 2, 3 }).astype(np.Object), np.array(new float[] { 0, 1, 0.5f }).astype(np.Object));
            AssertArray(a, new float[] { 0.0f, 1.0f, 2.5f, 4.0f, 1.5f });
            print(a);

            var b = np.convolve(new int[] { 1, 2, 3 }, new float[] { 0, 1, 0.5f }, mode: NPY_CONVOLE_MODE.NPY_CONVOLVE_SAME);
            AssertArray(b, new double[] { 1.0, 2.5, 4.0 });
            print(b);

            var c = np.convolve(new int[] { 1, 2, 3 }, new float[] { 0, 1, 0.5f }, mode: NPY_CONVOLE_MODE.NPY_CONVOLVE_VALID);
            AssertArray(c, new double[] { 2.5 });
            print(c);

            return;
        }

        [TestMethod]
        public void test_clip_2_OBJECT()
        {
            ndarray a = np.arange(16).reshape(new shape(4, 4)).astype(np.Object);
            print(a);
            print("*****");

            ndarray b = np.clip(a, 1, 8);
            print(b);
            print("*****");
            AssertArray(b, new double[,] { { 1, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 8, 8, 8 }, { 8, 8, 8, 8 } });

            ndarray c = np.clip(a, 3, 6, @out: a);
            print(c);
            AssertArray(c, new double[,] { { 3, 3, 3, 3 }, { 4, 5, 6, 6 }, { 6, 6, 6, 6 }, { 6, 6, 6, 6 } });
            print(a);
            AssertArray(a, new double[,] { { 3, 3, 3, 3 }, { 4, 5, 6, 6 }, { 6, 6, 6, 6 }, { 6, 6, 6, 6 } });
            print("*****");

            a = np.arange(16).reshape(new shape(4, 4)).astype(np.Object);
            print(a);
            b = np.clip(a, np.array(new Int32[] { 3, 4, 1, 1 }).astype(np.Object), 8);
            print(b);
            AssertArray(b, new double[,] { { 3, 4, 2, 3 }, { 4, 5, 6, 7 }, { 8, 8, 8, 8 }, { 8, 8, 8, 8 } });

            a["...", "..."] = "X";
            print(a);
            try
            {
                b = np.clip(a, np.array(new Int32[] { 3, 4, 1, 1 }).astype(np.Object), 8);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }


        }

        [TestMethod]
        public void test_square_operations_OBJECT()
        {
            var a = np.arange(0, 32, 1, dtype: np.Int32).astype(np.Object);
            print(a);

            var b = np.square(a);
            print(b);

            var ExpectedDataB1 = new Int32[]
            {
                0,   1,   4,   9,  16,  25,  36,  49,  64,  81, 100, 121, 144, 169, 196, 225, 256, 289,
                324, 361, 400, 441, 484, 529, 576, 625, 676, 729, 784, 841, 900, 961
            };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2048, 2048 + 32, 1, dtype: np.Int64).astype(np.Object);
            print(a);

            b = np.square(a);
            print(b);

            var ExpectedDataB2 = new Int64[]
            {
                4194304, 4198401, 4202500, 4206601, 4210704, 4214809, 4218916, 4223025, 4227136,
                4231249, 4235364, 4239481, 4243600, 4247721, 4251844, 4255969, 4260096, 4264225,
                4268356, 4272489, 4276624, 4280761, 4284900, 4289041, 4293184, 4297329, 4301476,
                4305625, 4309776, 4313929, 4318084, 4322241
            };
            AssertArray(b, ExpectedDataB2);

            //////////////////////

            a["..."] = "X";
            print(a);
            try
            {
                b = np.square(a);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_absolute_operations_OBJECT()
        {
            var a = np.arange(-32, 32, 1, dtype: np.Int16).astype(np.Object);
            print(a);

            var b = np.absolute(a);
            print(b);

            var ExpectedDataB = new Int16[]
            {
                32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18,
                17, 16, 15, 14, 13, 12, 11, 10,  9,  8,  7,  6,  5,  4,  3,
                2,  1,  0,  1,  2,  3,  4,  5,   6,  7,  8,  9, 10, 11, 12,
                13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27,
                28, 29, 30, 31
            };


            //////////////////////

            a["..."] = "X";
            print(a);
            try
            {
                b = np.absolute(a);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_fabs_1_OBJECT()
        {
            var a = np.arange(-32, 32, 1, dtype: np.Int16).astype(np.Object);
            print(a);

            var b = np.fabs(a);
            print(b);

            var ExpectedDataB = new Int16[]
            {
                32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18,
                17, 16, 15, 14, 13, 12, 11, 10,  9,  8,  7,  6,  5,  4,  3,
                2,  1,  0,  1,  2,  3,  4,  5,   6,  7,  8,  9, 10, 11, 12,
                13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27,
                28, 29, 30, 31
            };

            //////////////////////

            a["..."] = "X";
            print(a);
            try
            {
                b = np.fabs(a);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_sign_1_OBJECT()
        {
            var a = np.sign((Object)(-199));
            Assert.AreEqual((Int64)(-1), a.GetItem(0));
            print(a);

            try
            {
                var b = np.sign(np.array(new Object[] { 1, -23, 21 }).astype(np.Object));
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }
  
        }

        [TestMethod]
        public void test_heaviside_1_OBJECT()
        {
            var a = np.heaviside(np.array(new Int32[] { -15, 0, 20 }).astype(np.Object), 5);
            AssertArray(a, new double[] { 0, 5, 1 });
            print(a);

            var b = np.heaviside(np.array(new Int32[] { -15, 0, 20 }).astype(np.Object), 1);
            AssertArray(b, new double[] { 0, 1, 1 });
            print(b);

            var c = np.heaviside(np.array(new Int32[] { -1, 0, 2 }).astype(np.Object), 1);
            AssertArray(c, new double[] { 0, 1, 1 });
            print(c);

            //////////////////////

            a = np.array(new object[] { "A", "B", "C"});
            print(a);
            try
            {
                b = np.heaviside(a,1);
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }


        }

        [TestMethod]
        public void test_maximum_1_OBJECT()
        {
            var a = np.maximum(np.array(new Int32[] { 2, 3, 4 }).astype(np.Object), np.array(new Int32[] { 1, 5, 2 }).astype(np.Object));
            AssertArray(a, new double[] { 2, 5, 4 });
            print(a);

            var b = np.maximum(np.eye(2, dtype: np.Object), new Object[] { 5, 2 }); // broadcasting
            AssertArray(b, new double[,] { { 5, 2 }, { 5, 2 } });
            print(b);

            //////////////////////

            a = np.array(new object[] { "A", "B", "C" });
            print(a);
            try
            {
                b = np.maximum(a, np.array(new Int32[] { 1, 5, 2 }).astype(np.Object));
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }



        }


        [TestMethod]
        public void test_minimum_1_OBJECT()
        {
            var a = np.minimum(np.array(new Int32[] { 2, 3, 4 }).astype(np.Object), np.array(new Int32[] { 1, 5, 2 }).astype(np.Object));
            AssertArray(a, new double[] { 1, 3, 2 });
            print(a);

            var b = np.minimum(np.eye(2, dtype: np.Object), np.array(new Int32[] { 5, 2 }).astype(np.Object)); // broadcasting
            AssertArray(b, new double[,] { { 1, 0 }, { 0, 1 } });
            print(b);

            //////////////////////

            a = np.array(new object[] { "A", "B", "C" });
            print(a);
            try
            {
                b = np.minimum(a, np.array(new Int32[] { 1, 5, 2 }).astype(np.Object));
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }


        }

        [TestMethod]
        public void test_fmax_1_OBJECT()
        {
            var a = np.fmax(np.array(new int[] { 2, 3, 4 }).astype(np.Object), np.array(new int[] { 1, 5, 2 }).astype(np.Object));
            AssertArray(a, new double[] { 2, 5, 4 });
            print(a);

            var b = np.fmax(np.eye(2).astype(np.Object), new double[] { 0.5, 2 }); // broadcasting
            AssertArray(b, new double[,] { { 1, 2 }, { 0.5, 2.0 } });
            print(b);

            var c = np.fmax(np.array(new float[] { float.NaN, 0, float.NaN }).astype(np.Object), new float[] { 0, float.NaN, float.NaN });
            AssertArray(c, new double[] { double.NaN, double.NaN, double.NaN });
            print(c);

            var d = np.fmax(double.PositiveInfinity, 1);
            Assert.AreEqual(double.PositiveInfinity, d.GetItem(0));
            print(d);

            //////////////////////

            a = np.array(new object[] { "A", "B", "C" });
            print(a);
            try
            {
                b = np.fmax(a, np.array(new int[] { 1, 5, 2 }).astype(np.Object));
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }

        }

        [TestMethod]
        public void test_fmin_1_OBJECT()
        {
            var a = np.fmin(np.array(new int[] { 2, 3, 4 }).astype(np.Object), np.array(new int[] { 1, 5, 2 }).astype(np.Object));
            AssertArray(a, new double[] { 1, 3, 2 });
            print(a);

            var b = np.fmin(np.eye(2).astype(np.Object), new double[] { 0.5, 2 }); // broadcasting
            AssertArray(b, new double[,] { { 0.5, 0.0 }, { 0.0, 1.0 } });
            print(b);

            var c = np.fmin(np.array(new float[] { float.NaN, 0, float.NaN }).astype(np.Object), new float[] { 0, float.NaN, float.NaN });
            AssertArray(c, new double[] { double.NaN, double.NaN, double.NaN });
            print(c);

            //////////////////////

            a = np.array(new object[] { "A", "B", "C" });
            print(a);
            try
            {
                b = np.fmin(a, np.array(new int[] { 1, 5, 2 }).astype(np.Object));
                Assert.Fail("This should have thrown an exception");
            }
            catch
            {

            }
   

        }

 
        #endregion

        #region from FromNumericTests

        [TestMethod]
        public void test_take_1_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[] { 4, 3, 5, 7, 6, 8, 9, 12, 14, 16, 18, 20, 22, 24, 26, 28 });
            var indices = np.array(new Int32[] { 0, 1, 4 });
            ndarray b = np.take(a, indices);
            print("B");
            print(b);
            print(b.shape);
            print(b.strides);

            AssertArray(b, new BigInteger[] { 4, 3, 6 });
            AssertShape(b, 3);
            AssertStrides(b, SizeOfObject);


            a = np.array(new BigInteger[] { 4, 3, 5, 7, 6, 8, 9, 12, 14, 16, 18, 20, 22, 24, 26, 28 });
            indices = np.array(new Int32[,] { { 0, 1 }, { 2, 3 } });
            ndarray c = np.take(a, indices);
            print("C");
            print(c);
            print(c.shape);
            print(c.strides);

            var ExpectedDataC = new BigInteger[2, 2]
            {
                { 4, 3 },
                { 5, 7 },
            };
            AssertArray(c, ExpectedDataC);
            AssertShape(c, 2, 2);
            AssertStrides(c, SizeOfObject * 2, SizeOfObject);

            ndarray d = np.take(a.reshape(new shape(4, -1)), indices, axis: 0);
            print("D");
            print(d);
            print(d.shape);
            print(d.strides);

            var ExpectedDataD = new BigInteger[2, 2, 4]
            {
                {
                    { 4, 3, 5, 7 },
                    { 6, 8, 9, 12 },
                },
                {
                    { 14, 16, 18, 20 },
                    { 22, 24, 26, 28 },
                },

            };
            AssertArray(d, ExpectedDataD);
            AssertShape(d, 2, 2, 4);
            AssertStrides(d, SizeOfObject * 8, SizeOfObject * 4, SizeOfObject * 1);

            ndarray e = np.take(a.reshape(new shape(4, -1)), indices, axis: 1);
            print("E");
            print(e);
            print(e.shape);
            print(e.strides);

            var ExpectedDataE = new BigInteger[4, 2, 2]
            {
                {
                    { 4, 3 },
                    { 5, 7 },
                },
                {
                    { 6, 8 },
                    { 9, 12 },
                },
                {
                    { 14, 16 },
                    { 18, 20 },
                },
                {
                    { 22, 24 },
                    { 26, 28 },
                },

            };

            AssertArray(e, ExpectedDataE);
            AssertShape(e, 4, 2, 2);
            AssertStrides(e, SizeOfObject * 4, SizeOfObject * 2, SizeOfObject * 1);

        }

        [TestMethod]
        public void test_ravel_1_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            var b = np.ravel(a);
            AssertArray(b, new BigInteger[] { 1, 2, 3, 4, 5, 6 });
            print(b);

            var c = a.reshape(-1);
            AssertArray(c, new BigInteger[] { 1, 2, 3, 4, 5, 6 });
            print(c);

            var d = np.ravel(a, order: NPY_ORDER.NPY_FORTRANORDER);
            AssertArray(d, new BigInteger[] { 1, 4, 2, 5, 3, 6 });
            print(d);

            // When order is 'A', it will preserve the array's 'C' or 'F' ordering:
            var e = np.ravel(a.T);
            AssertArray(e, new BigInteger[] { 1, 4, 2, 5, 3, 6 });
            print(e);

            var f = np.ravel(a.T, order: NPY_ORDER.NPY_ANYORDER);
            AssertArray(f, new BigInteger[] { 1, 2, 3, 4, 5, 6 });
            print(f);
        }

        [TestMethod]
        public void test_choose_1_OBJECT_TODO()
        {
            ndarray choice1 = np.array(new BigInteger[] { 0, 1, 2, 3 });
            ndarray choice2 = np.array(new BigInteger[] { 10, 11, 12, 13 });
            ndarray choice3 = np.array(new BigInteger[] { 20, 21, 22, 23 });
            ndarray choice4 = np.array(new BigInteger[] { 30, 31, 32, 33 });

            ndarray[] choices = new ndarray[] { choice1, choice2, choice3, choice4 };

            ndarray a = np.choose(np.array(new Int32[] { 2, 3, 1, 0 }), choices);

            print(a);

            AssertArray(a, new BigInteger[] { 20, 31, 12, 3 });
        }

        [TestMethod]
        public void test_choose_2_OBJECT_TODO()
        {
            ndarray choice1 = np.array(new BigInteger[] { 0, 1, 2, 3 });
            ndarray choice2 = np.array(new BigInteger[] { 10, 11, 12, 13 });
            ndarray choice3 = np.array(new BigInteger[] { 20, 21, 22, 23 });
            ndarray choice4 = np.array(new BigInteger[] { 30, 31, 32, 33 });

            ndarray[] choices = new ndarray[] { choice1, choice2, choice3, choice4 };

            ndarray a = np.choose(np.array(new Int32[] { 2, 4, 1, 0 }), choices, mode: NPY_CLIPMODE.NPY_CLIP);
            print(a);
            AssertArray(a, new BigInteger[] { 20, 31, 12, 3 });

            a = np.choose(np.array(new Int32[] { 2, 4, 1, 0 }), choices, mode: NPY_CLIPMODE.NPY_WRAP);
            print(a);
            AssertArray(a, new BigInteger[] { 20, 1, 12, 3 });

            try
            {
                a = np.choose(np.array(new Int32[] { 2, 4, 1, 0 }), choices, mode: NPY_CLIPMODE.NPY_RAISE);
                print(a);
                AssertArray(a, new BigInteger[] { 20, 1, 12, 3 });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("invalid entry in choice array"))
                    return;
            }
            Assert.Fail("Should have caught exception from np.choose");


        }

        [TestMethod]
        public void test_select_1_OBJECT_TODO()
        {
            var x = np.arange(10, dtype: np.BigInt);
            var condlist = new ndarray[] { x < 3, x > 5 };
            var choicelist = new ndarray[] { x, np.array(np.power(x, 2), dtype: np.BigInt) };
            var y = np.select(condlist, choicelist);

            AssertArray(y, new BigInteger[] { 0, 1, 2, 0, 0, 0, 36, 49, 64, 81 });
            print(y);
        }

        [TestMethod]
        public void test_repeat_1_OBJECT_TODO()
        {
            ndarray x = np.array(new BigInteger[] { 1, 2, 3, 4 }).reshape(new shape(2, 2));
            var y = new Int32[] { 2 };

            ndarray z = np.repeat(x, y);
            print(z);
            print("");
            AssertArray(z, new BigInteger[] { 1, 1, 2, 2, 3, 3, 4, 4 });

            z = np.repeat((BigInteger)3, 4);
            print(z);
            print("");
            AssertArray(z, new BigInteger[] { 3, 3, 3, 3 });

            z = np.repeat(x, 3, axis: 0);
            print(z);
            print("");

            var ExpectedData1 = new BigInteger[6, 2]
            {
                { 1, 2 },
                { 1, 2 },
                { 1, 2 },
                { 3, 4 },
                { 3, 4 },
                { 3, 4 },
            };

            AssertArray(z, ExpectedData1);
            AssertShape(z, 6, 2);

            z = np.repeat(x, 3, axis: 1);
            print(z);
            print("");

            var ExpectedData2 = new BigInteger[2, 6]
            {
                { 1, 1, 1, 2, 2, 2 },
                { 3, 3, 3, 4, 4, 4 },
            };

            AssertArray(z, ExpectedData2);
            AssertShape(z, 2, 6);



            z = np.repeat(x, new Int32[] { 1, 2 }, axis: 0);
            print(z);

            var ExpectedData3 = new BigInteger[3, 2]
            {
                { 1, 2 },
                { 3, 4 },
                { 3, 4 },
            };

            AssertArray(z, ExpectedData3);
            AssertShape(z, 3, 2);
        }

        [TestMethod]
        public void test_put_1_OBJECT_TODO()
        {
            ndarray a = np.arange(5, dtype: np.BigInt);
            np.put(a, new int[] { 0, 2 }, new int[] { -44, -55 });
            print(a);
            AssertArray(a, new BigInteger[] { -44, 1, -55, 3, 4 });

            a = np.arange(5, dtype: np.BigInt);
            np.put(a, 22, -5, mode: NPY_CLIPMODE.NPY_CLIP);
            print(a);
            AssertArray(a, new BigInteger[] { 0, 1, 2, 3, -5 });

            a = np.arange(5, dtype: np.BigInt);
            np.put(a, 22, -5, mode: NPY_CLIPMODE.NPY_WRAP);
            print(a);
            AssertArray(a, new BigInteger[] { 0, 1, -5, 3, 4 });

            try
            {
                a = np.arange(5, dtype: np.BigInt);
                np.put(a, 22, -5, mode: NPY_CLIPMODE.NPY_RAISE);
                print(a);
            }
            catch (Exception ex)
            {
                return;
            }
            throw new Exception("this should have caught an exception");

        }

        [TestMethod]
        public void test_putmask_1_OBJECT_TODO()
        {
            var x = np.arange(6, dtype: np.BigInt).reshape((2, 3));
            np.putmask(x, x > 2, np.power(x, 2).astype(np.Int32));
            AssertArray(x, new BigInteger[,] { { 0, 1, 2, }, { 9, 16, 25 } });
            print(x);


            // If values is smaller than a it is repeated:

            x = np.arange(5, dtype: np.BigInt);
            np.putmask(x, x > 1, new Int32[] { -33, -44 });
            AssertArray(x, new BigInteger[] { 0, 1, -33, -44, -33 });
            print(x);

            return;
        }

        [TestMethod]
        public void test_swapaxes_1_OBJECT_TODO()
        {
            ndarray x = np.array(new BigInteger[,] { { 1, 2, 3 } });
            print(x);
            print("********");

            ndarray y = np.swapaxes(x, 0, 1);
            print(y);
            AssertArray(y, new BigInteger[3, 1] { { 1 }, { 2 }, { 3 } });
            print("********");

            x = np.array(new BigInteger[,,] { { { 0, 1 }, { 2, 3 } }, { { 4, 5 }, { 6, 7 } } });
            print(x);

            var ExpectedDataX = new BigInteger[2, 2, 2]
            {
                {
                    { 0,1 },
                    { 2,3 },
                },
                {
                    { 4,5 },
                    { 6,7 },
                },
            };
            AssertArray(x, ExpectedDataX);

            print("********");

            y = np.swapaxes(x, 0, 2);
            print(y);

            var ExpectedDataY = new BigInteger[2, 2, 2]
            {
                {
                    { 0,4 },
                    { 2,6 },
                },
                {
                    { 1,5 },
                    { 3,7 },
                },
            };
            AssertArray(y, ExpectedDataY);
        }

        [TestMethod]
        public void test_ndarray_T_1_OBJECT_TODO()
        {
            var x = np.arange(0, 32, dtype: np.BigInt).reshape(new shape(8, 4));
            print("X");
            print(x);
            print(x.shape);

            var y = x.T;

            print("Y");
            print(y);
            print(y.shape);

            var ExpectedDataY = new BigInteger[4, 8]
            {
                { 0, 4,  8, 12, 16, 20, 24, 28 },
                { 1, 5,  9, 13, 17, 21, 25, 29 },
                { 2, 6, 10, 14, 18, 22, 26, 30 },
                { 3, 7, 11, 15, 19, 23, 27, 31 },
            };

            AssertArray(y, ExpectedDataY);

        }

        [TestMethod]
        public void test_ndarray_transpose_1_OBJECT_TODO()
        {
            var x = np.arange(0, 64, dtype: np.BigInt).reshape(new shape(2, 4, -1, 4));
            print("X");
            print(x);
            print(x.shape);

            var y = np.transpose(x, new long[] { 1, 2, 3, 0 });

            print("Y");
            print(y);
            print(y.shape);

            var ExpectedDataY = new BigInteger[4, 2, 4, 2]
                {{{ {0, 32},
                    {1, 33},
                    {2, 34},
                    {3, 35}},
                   {{4, 36},
                    {5, 37},
                    {6, 38},
                    {7, 39}}},
                  {{{8, 40},
                    {9, 41},
                    {10, 42},
                    {11, 43}},
                   {{12, 44},
                    {13, 45},
                    {14, 46},
                    {15, 47}}},
                  {{{16, 48},
                    {17, 49},
                    {18, 50},
                    {19, 51}},
                   {{20, 52},
                    {21, 53},
                    {22, 54},
                    {23, 55}}},
                  {{{24, 56},
                    {25, 57},
                    {26, 58},
                    {27, 59}},
                   {{28, 60},
                    {29, 61},
                    {30, 62},
                    {31, 63}}}};

            AssertArray(y, ExpectedDataY);

        }

        [TestMethod]
        public void test_partition_3_OBJECT_TODO()
        {
            var a = np.arange(22, 10, -1, dtype: np.BigInt).reshape((3, 4, 1));
            var b = np.partition(a, 1, axis: 0);
            AssertArray(b, new BigInteger[,,] { { { 14 }, { 13 }, { 12 }, { 11 } }, { { 18 }, { 17 }, { 16 }, { 15 } }, { { 22 }, { 21 }, { 20 }, { 19 } } });
            print(b);

            var c = np.partition(a, 2, axis: 1);
            AssertArray(c, new BigInteger[,,] { { { 19 }, { 20 }, { 21 }, { 22 } }, { { 15 }, { 16 }, { 17 }, { 18 } }, { { 11 }, { 12 }, { 13 }, { 14 } } });
            print(c);

            var d = np.partition(a, 0, axis: 2);
            AssertArray(d, new BigInteger[,,] { { { 22 }, { 21 }, { 20 }, { 19 } }, { { 18 }, { 17 }, { 16 }, { 15 } }, { { 14 }, { 13 }, { 12 }, { 11 } } });
            print(d);

            try
            {
                var e = np.partition(a, 4, axis: 1);
                print(e);
            }
            catch (Exception ex)
            {
                return;
            }

            Assert.Fail("Should have caught the exception");

        }

        [TestMethod]
        public void test_argpartition_3_OBJECT_TODO()
        {
            var a = np.arange(22, 10, -1, np.BigInt).reshape((3, 4, 1));
            var b = np.argpartition(a, 1, axis: 0);
            AssertArray(b, new Int64[,,] { { { 2 }, { 2 }, { 2 }, { 2 } }, { { 1 }, { 1 }, { 1 }, { 1 } }, { { 0 }, { 0 }, { 0 }, { 0 } } });
            print(b);

            var c = np.argpartition(a, 2, axis: 1);
            AssertArray(c, new Int64[,,] { { { 3 }, { 2 }, { 1 }, { 0 } }, { { 3 }, { 2 }, { 1 }, { 0 } }, { { 3 }, { 2 }, { 1 }, { 0 } } });
            print(c);

            var d = np.argpartition(a, 0, axis: 2);
            AssertArray(d, new Int64[,,] { { { 0 }, { 0 }, { 0 }, { 0 } }, { { 0 }, { 0 }, { 0 }, { 0 } }, { { 0 }, { 0 }, { 0 }, { 0 } } });
            print(d);

            try
            {
                var e = np.partition(a, 4, axis: 1);
                print(e);
            }
            catch (Exception ex)
            {
                return;
            }

            Assert.Fail("Should have caught the exception");

        }

        [TestMethod]
        public void test_sort_2_OBJECT_TODO()
        {
            var InputData = new BigInteger[]
                {32, 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17,
                 16, 15, 14, 13, 12, 11, 10, 9,  8,  7,  6,  5,  4,  3,  2,  1};

            var a = np.array(InputData).reshape(new shape(8, 4));
            ndarray b = np.sort(a);                 // sort along the last axis
            print(b);

            var ExpectedDataB = new BigInteger[8, 4]
            {
             {29, 30, 31, 32},
             {25, 26, 27, 28},
             {21, 22, 23, 24},
             {17, 18, 19, 20},
             {13, 14, 15, 16},
             {9, 10, 11, 12},
             {5,  6,  7,  8},
             {1,  2,  3,  4},
            };

            AssertArray(b, ExpectedDataB);

            ndarray c = np.sort(a, axis: null);     // sort the flattened array
            print(c);
            print("********");

            var ExpectedDataC = new BigInteger[]
            {1,  2,  3,  4,  5,  6,  7,  8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32};

            AssertArray(c, ExpectedDataC);

            ndarray d = np.sort(a, axis: 0);        // sort along the first axis
            print(d);

            var ExpectedDataD = new BigInteger[8, 4]
            {
                {4,  3,  2,  1},
                {8,  7,  6,  5},
                {12, 11, 10, 9},
                {16, 15, 14, 13},
                {20, 19, 18, 17},
                {24, 23, 22, 21},
                {28, 27, 26, 25},
                {32, 31, 30, 29},
            };

            AssertArray(d, ExpectedDataD);
            print("********");

        }

        [TestMethod]
        public void test_msort_1_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[,] { { 1, 4 }, { 3, 1 } });
            ndarray b = np.msort(a);
            print(b);
            AssertArray(b, new BigInteger[,] { { 1, 1 }, { 3, 4 } });

            a = np.arange(32, 0, -1.0, dtype: np.BigInt);
            b = np.msort(a);

            var ExpectedDataB = new BigInteger[]
            {1,  2,  3,  4,  5,  6,  7,  8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32};
            AssertArray(b, ExpectedDataB);
            print(b);

        }

        [TestMethod]
        public void test_ndarray_argsort_2_OBJECT_TODO()
        {
            var ar = np.array(new BigInteger[] { 1, 2, 3, 1, 3, 4, 5, 4, 4, 1, 9, 6, 9, 11, 23, 9, 5, 0, 11, 12 }).reshape(new shape(5, 4));

            ndarray perm1 = np.argsort(ar, kind: NPY_SORTKIND.NPY_MERGESORT);
            ndarray perm2 = np.argsort(ar, kind: NPY_SORTKIND.NPY_QUICKSORT);
            ndarray perm3 = np.argsort(ar);

            print(perm1);

            var Perm1Expected = new Int64[,]
            {{0, 3, 1, 2},
             {0, 1, 3, 2},
             {1, 0, 3, 2},
             {0, 3, 1, 2},
             {1, 0, 2, 3}};
            AssertArray(perm1, Perm1Expected);

            print(perm2);
            var Perm2Expected = new Int64[,]
            {{0, 3, 1, 2},
             {0, 1, 3, 2},
             {1, 0, 3, 2},
             {0, 3, 1, 2},
             {1, 0, 2, 3}};
            AssertArray(perm2, Perm2Expected);


            print(perm3);
            var Perm3Expected = new Int64[,]
            {{0, 3, 1, 2},
             {0, 1, 3, 2},
             {1, 0, 3, 2},
             {0, 3, 1, 2},
             {1, 0, 2, 3}};
            AssertArray(perm3, Perm3Expected);
        }

        [TestMethod]
        public void test_argmin_1_OBJECT_TODO()
        {
            ndarray a = np.array(new BigInteger[] { 32, 33, 45, 98, 11, 02 }).reshape(new shape(2, 3));
            print(a);

            ndarray b = np.argmin(a);
            print(b);
            Assert.AreEqual(b.GetItem(0), (Int64)5);
            print("********");

            ndarray c = np.argmin(a, axis: 0);
            print(c);
            AssertArray(c, new Int64[] { 0, 1, 1 });
            print("********");

            ndarray d = np.argmin(a, axis: 1);
            print(d);
            AssertArray(d, new Int64[] { 0, 2 });
            print("********");

        }

        [TestMethod]
        public void test_argmax_1_OBJECT_TODO()
        {
            ndarray a = np.array(new BigInteger[] { 32, 33, 45, 98, 11, 02 }).reshape(new shape(2, 3));
            print(a);
            ndarray b = np.argmax(a);
            print(b);
            Assert.AreEqual(b.GetItem(0), (Int64)3);
            print("********");

            ndarray c = np.argmax(a, axis: 0);
            print(c);
            AssertArray(c, new Int64[] { 1, 0, 0 });
            print("********");

            ndarray d = np.argmax(a, axis: 1);
            print(d);
            AssertArray(d, new Int64[] { 2, 0 });
            print("********");

        }

        [TestMethod]
        public void test_searchsorted_1_OBJECT_TODO()
        {
            ndarray arr = np.array(new BigInteger[] { 1, 2, 3, 4, 5 });
            ndarray a = np.searchsorted(arr, 3);
            print(a);
            Assert.AreEqual(a.GetItem(0), (Int64)2);


            ndarray b = np.searchsorted(arr, 3, side: NPY_SEARCHSIDE.NPY_SEARCHRIGHT);
            print(b);
            Assert.AreEqual(b.GetItem(0), (Int64)3);


            ndarray c = np.searchsorted(arr, new Int32[] { -10, 10, 2, 3 });
            print(c);
            AssertArray(c, new Int64[] { 0, 5, 1, 2 });


            ndarray d = np.searchsorted(np.array(new BigInteger[] { 15, 14, 13, 12, 11 }), 13);
            print(d);
            Assert.AreEqual(d.GetItem(0), (Int64)0);
        }

        [TestMethod]
        public void test_resize_1_OBJECT_TODO()
        {
            ndarray a = np.array(new BigInteger[,] { { 0, 1 }, { 2, 3 } });
            print(a);

            ndarray b = np.resize(a, new shape(2, 3));
            print(b);

            var ExpectedDataB = new BigInteger[,]
            {
                { 0,1,2 },
                { 3,0,1 },
            };
            AssertArray(b, ExpectedDataB);


            ndarray c = np.resize(a, new shape(1, 4));
            print(c);
            var ExpectedDataC = new BigInteger[,]
            {
                { 0,1,2,3 },
            };
            AssertArray(c, ExpectedDataC);

            ndarray d = np.resize(a, new shape(2, 4));
            print(d);
            var ExpectedDataD = new BigInteger[,]
            {
                { 0,1,2,3 },
                { 0,1,2,3 },
            };
            AssertArray(d, ExpectedDataD);

        }

        [TestMethod]
        public void test_squeeze_1_OBJECT_TODO()
        {
            ndarray x = np.array(new BigInteger[,,] { { { 0 }, { 1 }, { 2 } } });
            print(x);
            AssertArray(x, new BigInteger[1, 3, 1] { { { 0 }, { 1 }, { 2 } } });

            ndarray a = np.squeeze(x);
            print(a);
            AssertArray(a, new BigInteger[] { 0, 1, 2 });

            ndarray b = np.squeeze(x, axis: 0);
            print(b);
            AssertArray(b, new BigInteger[3, 1] { { 0 }, { 1 }, { 2 } });

            bool CaughtException = false;

            try
            {
                ndarray c = np.squeeze(x, axis: 1);
                print(c);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("cannot select an axis to squeeze out which has size not equal to one"))
                    CaughtException = true;
            }
            Assert.IsTrue(CaughtException);

            ndarray d = np.squeeze(x, axis: 2);
            print(d);
            AssertArray(d, new BigInteger[,] { { 0, 1, 2 } });
        }

        [TestMethod]
        public void test_diagonal_1_OBJECT_TODO()
        {
            ndarray a = np.arange(4, dtype: np.BigInt).reshape(new shape(2, 2));
            print(a);
            print("*****");

            ndarray b = a.diagonal();
            print(b);
            AssertArray(b, new BigInteger[] { 0, 3 });
            print("*****");

            ndarray c = a.diagonal(1);
            print(c);
            AssertArray(c, new BigInteger[] { 1 });
            print("*****");

            a = np.arange(8, dtype: np.BigInt).reshape(new shape(2, 2, 2));
            print(a);
            print("*****");
            b = a.diagonal(0, // Main diagonals of two arrays created by skipping
                           0, // across the outer(left)-most axis last and
                           1); //the "middle" (row) axis first.

            print(b);
            AssertArray(b, new BigInteger[,] { { 0, 6 }, { 1, 7 } });
            print("*****");

            ndarray d = a.A(":", ":", 0);
            print(d);
            AssertArray(d, new BigInteger[,] { { 0, 2 }, { 4, 6 } });
            print("*****");

            ndarray e = a.A(":", ":", 1);
            print(e);
            AssertArray(e, new BigInteger[,] { { 1, 3 }, { 5, 7 } });
            print("*****");
        }

        [TestMethod]
        public void test_trace_1_OBJECT_TODO()
        {
            ndarray a = np.trace(np.eye(3, dtype: np.BigInt));
            print(a);
            Assert.AreEqual(a.GetItem(0), (BigInteger)3);
            print("*****");

            a = np.arange(8, dtype: np.BigInt).reshape(new shape(2, 2, 2));
            ndarray b = np.trace(a);
            print(b);
            AssertArray(b, new BigInteger[] { 6, 8 });
            print("*****");

            a = np.arange(24, dtype: np.BigInt).reshape(new shape(2, 2, 2, 3));
            var c = np.trace(a);
            print(c);
            AssertArray(c, new BigInteger[,] { { 18, 20, 22 }, { 24, 26, 28 } });

        }

        [TestMethod]
        public void test_nonzero_1_OBJECT_TODO()
        {
            ndarray x = np.array(new BigInteger[,] { { 1, 0, 0 }, { 0, 2, 0 }, { 1, 1, 0 } });
            print(x);
            print("*****");

            ndarray[] y = np.nonzero(x);
            print(y);
            AssertArray(y[0], new Int64[] { 0, 1, 2, 2 });
            AssertArray(y[1], new Int64[] { 0, 1, 0, 1 });
            print("*****");

            ndarray z = x.A(np.nonzero(x));
            print(z);
            AssertArray(z, new BigInteger[] { 1, 2, 1, 1 });
            print("*****");

            //ndarray q = np.transpose(np.nonzero(x));
            //print(q);

        }

        [TestMethod]
        public void test_compress_1_OBJECT_TODO()
        {
            ndarray a = np.array(new BigInteger[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            print(a);
            print("*****");

            ndarray b = np.compress(new int[] { 0, 1 }, a, axis: 0);
            print(b);
            AssertArray(b, new BigInteger[,] { { 3, 4 } });
            print("*****");

            ndarray c = np.compress(new bool[] { false, true, true }, a, axis: 0);
            print(c);
            AssertArray(c, new BigInteger[,] { { 3, 4 }, { 5, 6 } });
            print("*****");

            ndarray d = np.compress(new bool[] { false, true }, a, axis: 1);
            print(d);
            AssertArray(d, new BigInteger[,] { { 2 }, { 4 }, { 6 } });
            print("*****");

            ndarray e = np.compress(new bool[] { false, true }, a);
            AssertArray(e, new BigInteger[] { 2 });
            print(e);

        }

        [TestMethod]
        public void test_any_1_OBJECT_TODO()
        {
            BigInteger[] TestData = new BigInteger[] { 25, -17, -15, -02, 02, 15, 17, 20 };
            var x = np.array(TestData);
            var y = np.any(x);

            print(x);
            print(y);
            Assert.AreEqual(true, y.GetItem(0));

            TestData = new BigInteger[] { 0, 0, 0, 0 };
            x = np.array(TestData);
            y = np.any(x);

            print(x);
            print(y);
            Assert.AreEqual(false, y.GetItem(0));

        }

        [TestMethod]
        public void test_all_1_OBJECT_TODO()
        {
            BigInteger[] TestData = new BigInteger[] { 25, -17, -15, -02, 02, 15, 17, 20 };
            var x = np.array(TestData);
            var y = np.all(x);

            print(x);
            print(y);
            Assert.AreEqual(true, y.GetItem(0));

            TestData = new BigInteger[] { 1, 1, 0, 1 };
            x = np.array(TestData);
            y = np.all(x);

            print(x);
            print(y);
            Assert.AreEqual(false, y.GetItem(0));

        }

        [TestMethod]
        public void test_ndarray_mean_1_OBJECT_TODO()
        {
            var x = np.arange(0, 12, dtype: np.BigInt).reshape(new shape(3, -1));

            print("X");
            print(x);

            var y = (ndarray)np.mean(x, dtype: np.BigInt);
            Assert.AreEqual((BigInteger)5, y.GetItem(0));

            print("Y");
            print(y);

            y = (ndarray)np.mean(x, axis: 0, dtype: np.BigInt);
            AssertArray(y, new BigInteger[] { 4, 5, 6, 7 });

            print("Y");
            print(y);

            y = (ndarray)np.mean(x, axis: 1, dtype: np.BigInt);
            AssertArray(y, new BigInteger[] { 1, 5, 9 });

            print("Y");
            print(y);

        }

        [TestMethod]
        public void test_place_1_OBJECT_TODO()
        {
            var arr = np.arange(6, dtype: np.BigInt).reshape((2, 3));
            np.place(arr, arr > 2, new Int32[] { 44, 55 });
            AssertArray(arr, new BigInteger[,] { { 0, 1, 2 }, { 44, 55, 44 } });
            print(arr);

            arr = np.arange(16, dtype: np.BigInt).reshape((2, 4, 2));
            np.place(arr, arr > 12, new Int32[] { 33 });
            AssertArray(arr, new BigInteger[,,] { { { 0, 1 }, { 2, 3 }, { 4, 5 }, { 6, 7 } }, { { 8, 9 }, { 10, 11 }, { 12, 33 }, { 33, 33 } } });
            print(arr);

            arr = np.arange(6, dtype: np.BigInt).reshape((2, 3));
            np.place(arr, arr > 2, new Int32[] { 44, 55, 66, 77, 88, 99, 11, 22, 33 });
            AssertArray(arr, new BigInteger[,] { { 0, 1, 2 }, { 44, 55, 66 } });
            print(arr);

        }

        [TestMethod]
        public void test_extract_1_OBJECT_TODO()
        {
            var arr = np.arange(12, dtype: np.BigInt).reshape((3, 4));
            var condition = np.mod(arr, 3) == 0;
            print(condition);

            var b = np.extract(condition, arr);
            AssertArray(b, new BigInteger[] { 0, 3, 6, 9 });
            print(b);
        }

        [TestMethod]
        public void test_viewfromaxis_1_OBJECT_TODO()
        {
            BigInteger[] TestData = new BigInteger[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var a = np.zeros_like(TestData).reshape(new shape(3, 2, -1));
            //print(a);


            var b = np.ViewFromAxis(a, 0);
            b[":"] = 99;
            //print(a);
            AssertArray(a, new BigInteger[,,] { { { 99, 0 }, { 0, 0 } }, { { 99, 0 }, { 0, 0 } }, { { 99, 0 }, { 0, 0 } } });
            //print(a);
            AssertArray(np.sum(a, axis: 0), new BigInteger[,] { { 297, 0 }, { 0, 0 } });

            b = np.ViewFromAxis(a, 1);
            b[":"] = 11;
            AssertArray(a, new BigInteger[,,] { { { 11, 0 }, { 11, 0 } }, { { 99, 0 }, { 0, 0 } }, { { 99, 0 }, { 0, 0 } } });
            //print(a);
            AssertArray(np.sum(a, axis: 1), new BigInteger[,] { { 22, 0 }, { 99, 0 }, { 99, 0 } });

            b = np.ViewFromAxis(a, 2);
            b[":"] = 22;
            AssertArray(a, new BigInteger[,,] { { { 22, 22 }, { 11, 0 } }, { { 99, 0 }, { 0, 0 } }, { { 99, 0 }, { 0, 0 } } });
            //print(a);
            AssertArray(np.sum(a, axis: 2), new BigInteger[,] { { 44, 11 }, { 99, 0 }, { 99, 0 } });

            Assert.AreEqual((BigInteger)253, np.sum(a).GetItem(0));


        }

        [TestMethod]
        public void test_unwrap_1_OBJECT_TODO()
        {
            double retstep = 0;

            var phase = np.linspace(0, Math.PI, ref retstep, num: 5, dtype: np.BigInt);
            phase["3:"] = phase.A("3:") + Math.PI;
            print(phase);

            var x = np.unwrap(phase);
            //AssertArray(x, new decimal[] { 0, 00.785398163397448m, 01.5707963267949m, -00.78539816339746m, -00.00000000000001m });
            print(x);
        }


        #endregion

        #region from NumericTests

        [TestMethod]
        public void test_zeros_1_OBJECT_TODO()
        {
            var x = np.zeros(new shape(10), dtype: np.BigInt);
            print(x);
            print("Update sixth value to 11");
            x[6] = 11;
            print(x);
            print(x.shape);
            print(x.strides);

            AssertArray(x, new BigInteger[] { 0, 0, 0, 0, 0, 0, 11, 0, 0, 0 });
            AssertShape(x, 10);
            AssertStrides(x, SizeOfObject);
        }

        [TestMethod]
        public void test_zeros_like_2_OBJECT_TODO()
        {
            var a = new BigInteger[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var b = np.zeros_like(a);
            b[1, 2] = 99;

            AssertArray(b, new BigInteger[,] { { 0, 0, 0 }, { 0, 0, 99 } });

            return;
        }

        [TestMethod]
        public void test_ones_1_OBJECT_TODO()
        {
            var x = np.ones(new shape(10), dtype: np.BigInt);
            print(x);
            print("Update sixth value to 11");
            x[6] = 11;
            print(x);
            print(x.shape);
            print(x.strides);

            AssertArray(x, new BigInteger[] { 1, 1, 1, 1, 1, 1, 11, 1, 1, 1 });
            AssertShape(x, 10);
            AssertStrides(x, SizeOfObject);
        }

        [TestMethod]
        public void test_ones_like_3_OBJECT_TODO()
        {
            var a = new BigInteger[,,] { { { 1, 2, 3 }, { 4, 5, 6 } } };
            var b = np.ones_like(a);
            b[0, 0, 2] = 99;
            b[0, 1, 1] = 88;

            AssertArray(b, new BigInteger[,,] { { { 1, 1, 99 }, { 1, 88, 1 } } });

            return;
        }

        [TestMethod]
        public void test_empty_OBJECT_TODO()
        {
            var a = np.empty((2, 3));
            AssertShape(a, 2, 3);
            Assert.AreEqual(a.Dtype.TypeNum, NPY_TYPES.NPY_DOUBLE);

            var b = np.empty((2, 4), np.BigInt);
            AssertShape(b, 2, 4);
            Assert.AreEqual(b.Dtype.TypeNum, NPY_TYPES.NPY_BIGINT);
        }

        [TestMethod]
        public void test_empty_like_3_OBJECT_TODO()
        {
            var a = new BigInteger[,,] { { { 1, 2, 3 }, { 4, 5, 6 } } };
            var b = np.empty_like(a);
            b[0, 0, 2] = 99;
            b[0, 1, 1] = 88;

            AssertArray(b, new BigInteger[,,] { { { 0, 0, 99 }, { 0, 88, 0 } } });

            return;
        }

        [TestMethod]
        public void test_full_2_OBJECT_TODO()
        {
            var x = np.full((100), 99, dtype: np.BigInt).reshape(new shape(10, 10));
            print(x);
            print("Update sixth value to 11");
            x[6] = 55;
            print(x);
            print(x.shape);
            print(x.strides);

            //AssertArray(y, new float[] { 60, 61, 62, 63, 64, 65, 66, 67, 68, 69 });
            //AssertShape(y, 10);
            //AssertStrides(y, sizeof(float));

            //x[5, 5] = 12;
            //print(x);
            //print(x.shape);
            //print(x.strides);
        }

        [TestMethod]
        public void test_count_nonzero_1_OBJECT_TODO()
        {
            var a = np.count_nonzero_i(np.eye(4, dtype: np.BigInt));
            Assert.AreEqual(4, a);
            print(a);

            var b = np.count_nonzero_i(new BigInteger[,] { { 0, 1, 7, 0, 0 }, { 3, 0, 0, 2, 19 } });
            Assert.AreEqual(5, b);
            print(b);

            var c = np.count_nonzero(new BigInteger[,] { { 0, 1, 7, 0, 0 }, { 3, 0, 0, 2, 19 } }, axis: 0);
            AssertArray(c, new int[] { 1, 1, 1, 1, 1 });
            print(c);

            var d = np.count_nonzero(new BigInteger[,] { { 0, 1, 7, 0, 0 }, { 3, 0, 0, 2, 19 } }, axis: 1);
            AssertArray(d, new int[] { 2, 3 });
            print(d);

            return;
        }

        [TestMethod]
        public void test_asarray_1_OBJECT_TODO()
        {
            var a = new BigInteger[] { 1, 2 };
            var b = np.asarray(a);

            AssertArray(b, new BigInteger[] { 1, 2 });
            print(b);

            var c = np.array(new BigInteger[] { 1, 2 }, dtype: np.BigInt);
            var d = np.asarray(c, dtype: np.BigInt);

            c[0] = 3;
            AssertArray(d, new BigInteger[] { 3, 2 });
            print(d);

            var e = np.asarray(a, dtype: np.BigInt);
            AssertArray(e, new BigInteger[] { 1, 2 });

            print(e);

            return;
        }

        [TestMethod]
        public void test_ascontiguousarray_1_OBJECT_TODO()
        {
            var x = np.arange(6, dtype: np.BigInt).reshape((2, 3));
            var y = np.ascontiguousarray(x, dtype: np.BigInt);

            AssertArray(y, new BigInteger[,] { { 0, 1, 2 }, { 3, 4, 5 } });
            print(y);

            Assert.AreEqual(x.flags.c_contiguous, true);
            Assert.AreEqual(y.flags.c_contiguous, true);

            return;
        }

        [TestMethod]
        public void test_asfortranarray_1_OBJECT_TODO()
        {
            var x = np.arange(6, dtype: np.BigInt).reshape((2, 3));
            var y = np.asfortranarray(x, dtype: np.BigInt);

            AssertArray(y, new BigInteger[,] { { 0, 1, 2 }, { 3, 4, 5 } });
            print(y);

            Assert.AreEqual(x.flags.f_contiguous, false);
            Assert.AreEqual(y.flags.f_contiguous, true);

            return;
        }

        [TestMethod]
        public void test_isfortran_1_OBJECT_TODO()
        {

            var a = np.array(new BigInteger[,] { { 1, 2, 3 }, { 4, 5, 6 } }, order: NPY_ORDER.NPY_CORDER);
            var a1 = np.isfortran(a);
            Assert.AreEqual(false, a1);
            print(a1);

            var b = np.array(new BigInteger[,] { { 1, 2, 3 }, { 4, 5, 6 } }, order: NPY_ORDER.NPY_FORTRANORDER);
            var b1 = np.isfortran(b);
            Assert.AreEqual(true, b1);
            print(b1);

            var c = np.array(new BigInteger[,] { { 1, 2, 3 }, { 4, 5, 6 } }, order: NPY_ORDER.NPY_CORDER);
            var c1 = np.isfortran(c);
            Assert.AreEqual(false, c1);
            print(c1);

            var d = a.T;
            var d1 = np.isfortran(d);
            Assert.AreEqual(true, d1);
            print(d1);

            // C-ordered arrays evaluate as False even if they are also FORTRAN-ordered.

            var e1 = np.isfortran(np.array(new BigInteger[] { 1, 2 }, order: NPY_ORDER.NPY_FORTRANORDER));
            Assert.AreEqual(false, e1);
            print(e1);

            return;

        }

        [TestMethod]
        public void test_argwhere_1_OBJECT_TODO()
        {
            var x = np.arange(6, dtype: np.BigInt).reshape((2, 3));
            var y = np.argwhere(x > 1);

            var ExpectedY = new Int64[,] { { 0, 2 }, { 1, 0 }, { 1, 1 }, { 1, 2 } };
            AssertArray(y, ExpectedY);
            print(y);

            var a = np.arange(12).reshape((2, 3, 2));
            var b = np.argwhere(a > 1);

            var ExpectedB = new Int64[,]
                {{0, 1, 0}, {0, 1, 1}, {0, 2, 0}, {0, 2, 1}, {1, 0, 0},
                 {1, 0, 1}, {1, 1, 0}, {1, 1, 1}, {1, 2, 0}, {1, 2, 1}};

            AssertArray(b, ExpectedB);

            print(b);

            return;
        }

        [TestMethod]
        public void test_flatnonzero_1_OBJECT_TODO()
        {
            var x = np.arange(-2, 3, dtype: np.BigInt);

            var y = np.flatnonzero(x);
            AssertArray(y, new Int64[] { 0, 1, 3, 4 });
            print(y);

            // Use the indices of the non-zero elements as an index array to extract these elements:

            var z = x.ravel()[np.flatnonzero(x)] as ndarray;
            AssertArray(z, new BigInteger[] { -2, -1, 1, 2 });
            print(z);

            return;
        }

        [TestMethod]
        public void test_outer_1_OBJECT_TODO()
        {
            var a = np.arange(2, 10, dtype: np.BigInt).reshape((2, 4));
            var b = np.arange(12, 20, dtype: np.BigInt).reshape((2, 4));
            var c = np.outer(a, b);

            var ExpectedDataC = new BigInteger[,]
                {{24,  26,  28,  30,  32,  34,  36,  38},
                 {36,  39,  42,  45,  48,  51,  54,  57},
                 {48,  52,  56,  60,  64,  68,  72,  76},
                 {60,  65,  70,  75,  80,  85,  90,  95},
                 {72,  78,  84,  90,  96, 102, 108, 114},
                 {84,  91,  98, 105, 112, 119, 126, 133},
                 {96, 104, 112, 120, 128, 136, 144, 152},
                 {108, 117, 126, 135, 144, 153, 162, 171}};

            AssertArray(c, ExpectedDataC);

            print(c);

            //a = np.arange(2000, 10000, dtype: np.Decimal).reshape((-1, 4000));
            //b = np.arange(12000, 20000, dtype: np.Decimal).reshape((-1, 4000));

            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //sw.Start();
            //c = np.outer(a, b);
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);


            return;
        }

        [TestMethod]
        public void test_inner_1_OBJECT_TODO()
        {
            var a = np.arange(1, 5, dtype: np.BigInt).reshape((2, 2));
            var b = np.arange(11, 15, dtype: np.BigInt).reshape((2, 2));
            var c = np.inner(a, b);
            AssertArray(c, new BigInteger[,] { { 35, 41 }, { 81, 95 } });
            print(c);


            a = np.arange(2, 10, dtype: np.BigInt).reshape((2, 4));
            b = np.arange(12, 20, dtype: np.BigInt).reshape((2, 4));
            c = np.inner(a, b);
            print(c);
            AssertArray(c, new BigInteger[,] { { 194, 250 }, { 410, 530 } });
            print(c.shape);

            return;
        }

        [TestMethod]
        public void test_tensordot_2_OBJECT_TODO()
        {
            var a = np.arange(12.0, dtype: np.BigInt).reshape((3, 4));
            var b = np.arange(24.0, dtype: np.BigInt).reshape((4, 3, 2));
            var c = np.tensordot(a, b, axis: 1);
            AssertShape(c, 3, 3, 2);
            print(c.shape);
            AssertArray(c, new BigInteger[,,] { { { 84, 90 }, { 96, 102 }, { 108, 114 } }, { { 228, 250 }, { 272, 294 }, { 316, 338 } }, { { 372, 410 }, { 448, 486 }, { 524, 562 } } });


            c = np.tensordot(a, b, axis: 0);
            AssertShape(c, 3, 4, 4, 3, 2);
            print(c.shape);

            print(c);
        }

        [TestMethod]
        public void test_dot_1_OBJECT_TODO()
        {
            var a = new BigInteger[,] { { 1, 0 }, { 0, 1 } };
            var b = new BigInteger[,] { { 4, 1 }, { 2, 2 } };
            var c = np.dot(a, b);
            AssertArray(c, new BigInteger[,] { { 4, 1 }, { 2, 2 } });
            print(c);

            var d = np.dot((BigInteger)3, (BigInteger)4);
            Assert.AreEqual((BigInteger)12, d.GetItem(0));
            print(d);

            var e = np.arange(3 * 4 * 5 * 6, dtype: np.BigInt).reshape((3, 4, 5, 6));
            var f = np.arange(3 * 4 * 5 * 6, dtype: np.BigInt).A("::-1").reshape((5, 4, 6, 3));
            var g = np.dot(e, f);
            AssertShape(g.shape, 3, 4, 5, 5, 4, 3);
            Assert.AreEqual((BigInteger)695768400, g.Sum().GetItem(0));

            // TODO: NOTE: this crazy indexing is not currently working
            //g = g.A(2, 3, 2, 1, 2, 2);
            //Assert.AreEqual(499128, g.GetItem(0));
            //print(g);

        }

        [TestMethod]
        public void test_roll_forward_OBJECT_TODO()
        {
            var a = np.arange(10, dtype: np.BigInt);

            print("A");
            print(a);
            print(a.shape);
            print(a.strides);

            var b = np.roll(a, 2);
            print("B");
            print(b);
            print(b.shape);
            print(b.strides);
            AssertArray(b, new BigInteger[] { 8, 9, 0, 1, 2, 3, 4, 5, 6, 7 });
            AssertShape(b, 10);

            var c = np.roll(b, 2);
            print("C");
            print(c);
            print(c.shape);
            print(c.strides);
            AssertArray(c, new BigInteger[] { 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 });
            AssertShape(c, 10);

        }

        [TestMethod]
        public void test_roll_backward_OBJECT_TODO()
        {
            var a = np.arange(10, dtype: np.BigInt);

            print("A");
            print(a);
            print(a.shape);
            print(a.strides);

            var b = np.roll(a, -2);
            print("B");
            print(b);
            print(b.shape);
            print(b.strides);
            AssertArray(b, new BigInteger[] { 2, 3, 4, 5, 6, 7, 8, 9, 0, 1 });
            AssertShape(b, 10);

            var c = np.roll(b, -6);
            print("C");
            print(c);
            print(c.shape);
            print(c.strides);
            AssertArray(c, new BigInteger[] { 8, 9, 0, 1, 2, 3, 4, 5, 6, 7 });
            AssertShape(c, 10);
        }

        [TestMethod]
        public void test_ndarray_rollaxis_OBJECT_TODO()
        {
            var a = np.ones((3, 4, 5, 6), dtype: np.BigInt);
            var b = np.rollaxis(a, 3, 1).shape;
            AssertShape(b, 3, 6, 4, 5);
            print(b);

            var c = np.rollaxis(a, 2).shape;
            AssertShape(c, 5, 3, 4, 6);
            print(c);

            var d = np.rollaxis(a, 1, 4).shape;
            AssertShape(d, 3, 5, 6, 4);
            print(d);
        }

        [TestMethod]
        public void test_ndarray_moveaxis_OBJECT_TODO()
        {
            var x = np.zeros((3, 4, 5), np.BigInt);
            var b = np.moveaxis(x, 0, -1).shape;
            AssertShape(b, 4, 5, 3);
            print(b);

            var c = np.moveaxis(x, -1, 0).shape;
            AssertShape(c, 5, 3, 4);
            print(c);

            // These all achieve the same result:
            var d = np.transpose(x).shape;
            AssertShape(d, 5, 4, 3);
            print(d);

            var e = np.swapaxes(x, 0, -1).shape;
            AssertShape(e, 5, 4, 3);
            print(e);

            var f = np.moveaxis(x, new int[] { 0, 1 }, new int[] { -1, -2 }).shape;
            AssertShape(f, 5, 4, 3);
            print(f);

            var g = np.moveaxis(x, new int[] { 0, 1, 2 }, new int[] { -1, -2, -3 }).shape;
            AssertShape(g, 5, 4, 3);
            print(g);
        }

        [TestMethod]
        public void test_indices_1_OBJECT_TODO()
        {
            var grid = np.indices((2, 3), dtype: np.BigInt);
            AssertShape(grid, 2, 2, 3);
            print(grid.shape);
            AssertArray(grid[0] as ndarray, new BigInteger[,] { { 0, 0, 0 }, { 1, 1, 1 } });
            print(grid[0]);
            AssertArray(grid[1] as ndarray, new BigInteger[,] { { 0, 1, 2 }, { 0, 1, 2 } });
            print(grid[1]);

            var x = np.arange(20, dtype: np.BigInt).reshape((5, 4));

            var y = x[grid[0], grid[1]];
            AssertArray(y as ndarray, new BigInteger[,] { { 0, 1, 2 }, { 4, 5, 6 } });
            print(y);

            return;
        }

        [TestMethod]
        public void test_isscalar_1_OBJECT_TODO()
        {

            bool a = np.isscalar((BigInteger)3);
            Assert.AreEqual(true, a);
            print(a);

            bool b = np.isscalar(np.array((BigInteger)3));
            Assert.AreEqual(false, b);
            print(b);

            bool c = np.isscalar(new BigInteger[] { 3 });
            Assert.AreEqual(false, c);
            print(c);

            bool d = np.isscalar(false);
            Assert.AreEqual(true, d);
            print(d);

            bool e = np.isscalar("numpy");
            Assert.AreEqual(false, e);
            print(e);

            return;
        }

        [TestMethod]
        public void test_identity_1_OBJECT_TODO()
        {
            ndarray a = np.identity(2, dtype: np.BigInt);

            print(a);
            print(a.shape);
            print(a.strides);

            var ExpectedDataA = new BigInteger[2, 2]
            {
                { 1,0 },
                { 0,1 },
            };
            AssertArray(a, ExpectedDataA);
            AssertShape(a, 2, 2);
            AssertStrides(a, SizeOfObject * 2, SizeOfObject * 1);

            ndarray b = np.identity(5, dtype: np.BigInt);

            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB = new BigInteger[5, 5]
            {
                { 1, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 1 },
            };
            AssertArray(b, ExpectedDataB);
            AssertShape(b, 5, 5);
            AssertStrides(b, SizeOfObject * 5, SizeOfObject * 1);
        }


        [TestMethod]
        public void test_array_equal_1_OBJECT_TODO()
        {
            var a = np.array_equal(new BigInteger[] { 1, 2 }, new BigInteger[] { 1, 2 });
            Assert.AreEqual(true, a);
            print(a);

            var b = np.array_equal(np.array(new BigInteger[] { 1, 2 }), np.array(new BigInteger[] { 1, 2 }));
            Assert.AreEqual(true, b);
            print(b);

            var c = np.array_equal(new BigInteger[] { 1, 2 }, new BigInteger[] { 1, 2, 3 });
            Assert.AreEqual(false, c);
            print(c);

            var d = np.array_equal(new BigInteger[] { 1, 2 }, new BigInteger[] { 1, 4 });
            Assert.AreEqual(false, d);
            print(d);
        }

        [TestMethod]
        public void test_array_equiv_1_OBJECT_TODO()
        {
            var a = np.array_equiv(new BigInteger[] { 1, 2 }, new BigInteger[] { 1, 2 });
            Assert.AreEqual(true, a);
            print(a);

            var b = np.array_equiv(new BigInteger[] { 1, 2 }, new BigInteger[] { 1, 3 });
            Assert.AreEqual(false, b);
            print(b);

            var c = np.array_equiv(new BigInteger[] { 1, 2 }, new BigInteger[,] { { 1, 2 }, { 1, 2 } });
            Assert.AreEqual(true, c);
            print(c);

            var d = np.array_equiv(new BigInteger[] { 1, 2 }, new BigInteger[,] { { 1, 2, 1, 2 }, { 1, 2, 1, 2 } });
            Assert.AreEqual(false, d);
            print(d);

            var e = np.array_equiv(new BigInteger[] { 1, 2 }, new BigInteger[,] { { 1, 2 }, { 1, 3 } });
            Assert.AreEqual(false, e);
            print(e);
        }

        #endregion

        #region from NANFunctionsTests

        [TestMethod]
        public void test_nanprod_1_OBJECT_TODO()
        {

            var x = np.nanprod((BigInteger)1);
            Assert.AreEqual((BigInteger)1, x.GetItem(0));
            print(x);

            var y = np.nanprod(new BigInteger[] { 1 });
            Assert.AreEqual((BigInteger)1, y.GetItem(0));
            print(y);



            var a = np.array(new BigInteger[,] { { 1, 2 }, { 3, 4 } });
            var b = np.nanprod(a);
            Assert.AreEqual((BigInteger)24.0, b.GetItem(0));
            print(b);

            var c = np.nanprod(a, axis: 0);
            AssertArray(c, new BigInteger[] { 3, 8 });
            print(c);

            var d = np.nanprod(a, axis: 1);
            AssertArray(d, new BigInteger[] { 2, 12 });
            print(d);

            return;
        }

        #endregion

        #region from StatisticsTests

        [TestMethod]
        public void test_amin_2_OBJECT_TODO()
        {
            ndarray a = np.arange((BigInteger)30, (BigInteger)46).reshape(new shape(4, 4));
            print(a);
            print("*****");

            ndarray b = np.amin(a);          // Minimum of the flattened array
            print(b);
            Assert.AreEqual((BigInteger)30, b.GetItem(0));
            print("*****");

            ndarray c = np.amin(a, axis: 0);  // Minimum along the first axis
            print(c);
            AssertArray(c, new BigInteger[] { 30, 31, 32, 33 });
            print("*****");

            ndarray d = np.amin(a, axis: 1);   // Minimum along the second axis
            print(d);
            AssertArray(d, new BigInteger[] { 30, 34, 38, 42 });
            print("*****");

            // decimals don't support NAN
            //ndarray e = np.arange(5, dtype: np.Decimal);
            //e[2] = np.NaN;
            //ndarray f = np.amin(e);
            //print(f);
            //Assert.AreEqual(np.NaN, f.GetItem(0));
            //print("*****");

        }

        [TestMethod]
        public void test_amax_2_OBJECT_TODO()
        {
            ndarray a = np.arange((BigInteger)30, (BigInteger)46).reshape(new shape(4, 4));
            print(a);
            print("*****");

            ndarray b = np.amax(a);          // Maximum of the flattened array
            print(b);
            Assert.AreEqual((BigInteger)45, b.GetItem(0));
            print("*****");

            ndarray c = np.amax(a, axis: 0);  // Maxima along the first axis
            print(c);
            AssertArray(c, new BigInteger[] { 42, 43, 44, 45 });
            print("*****");

            ndarray d = np.amax(a, axis: 1);   // Maxima along the second axis
            print(d);
            AssertArray(d, new BigInteger[] { 33, 37, 41, 45 });
            print("*****");

            // decimals don't support NAN
            //ndarray e = np.arange(5, dtype: np.Float32);
            //e[2] = np.NaN;
            //ndarray f = np.amax(e);
            //print(f);
            //Assert.AreEqual(np.NaN, f.GetItem(0));
            //print("*****");

            //ndarray g = np.nanmax(b);
            //print(g);
        }

        [TestMethod]
        public void test_ptp_1_OBJECT_TODO()
        {
            ndarray a = np.arange(4, dtype: np.BigInt).reshape(new shape(2, 2));
            print(a);
            print("*****");

            ndarray b = np.ptp(a, axis: 0);
            print(b);
            AssertArray(b, new BigInteger[] { 2, 2 });
            print("*****");

            ndarray c = np.ptp(a, axis: 1);
            print(c);
            AssertArray(c, new BigInteger[] { 1, 1 });

            ndarray d = np.ptp(a);
            print(d);
            Assert.AreEqual((BigInteger)3, d.GetItem(0));
        }

        [TestMethod]
        public void test_percentile_2_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[,] { { 10, 7, 4 }, { 3, 2, 1 } });

            var b = np.percentile(a, new BigInteger[] { 50, 75 });
            AssertArray(b, new double[] { 3.5, 6.25 });
            print(b);

            var c = np.percentile(a, new BigInteger[] { 50, 75 }, axis: 0);
            AssertArray(c, new double[,] { { 6.5, 4.5, 2.5 }, { 8.25, 5.75, 3.25 } });
            print(c);

            var d = np.percentile(a, new BigInteger[] { 50, 75 }, axis: 1);
            AssertArray(d, new double[,] { { 7.0, 2.0 }, { 8.5, 2.5 } });
            print(d);

            var e = np.percentile(a, new BigInteger[] { 50, 75 }, axis: 1, keepdims: true);
            AssertArray(e, new double[,,] { { { 7.0 }, { 2.0 } }, { { 8.5 }, { 2.5 } } });
            print(e);

            // note: we dont support the out parameter
            //var m = np.percentile(a, 50, axis : 0);
            //var n = np.zeros_like(m);
            //var o = np.percentile(a, 50, axis : 0);
            //print(o);
            //print(n);
            // note: we don't support the overwrite_input flag
            //b = a.Copy();
            //c = np.percentile(b, 50, axis: 1, overwrite_input: true);
            //print(c);

            //Assert.IsFalse((bool)np.all(a.Equals(b)).GetItem(0));

            return;
        }

        [TestMethod]
        public void test_quantile_2_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[,] { { 10, 7, 4 }, { 3, 2, 1 } });

            var b = np.quantile(a, new double[] { 0.5, 0.75 });
            AssertArray(b, new double[] { 3.5, 6.25 });
            print(b);

            var c = np.quantile(a, new double[] { 0.5, 0.75 }, axis: 0);
            AssertArray(c, new double[,] { { 6.5, 4.5, 2.5 }, { 8.25, 5.75, 3.25 } });
            print(c);

            var d = np.quantile(a, new double[] { 0.5, 0.75 }, axis: 1);
            AssertArray(d, new double[,] { { 7.0, 2.0 }, { 8.5, 2.5 } });
            print(d);

            var e = np.quantile(a, new double[] { 0.5, 0.75 }, axis: 1, keepdims: true);
            AssertArray(e, new double[,,] { { { 7.0 }, { 2.0 } }, { { 8.5 }, { 2.5 } } });
            print(e);

            // note: we dont support the out parameter

            //var m = np.quantile(a, 0.5, axis: 0);
            //var n = np.zeros_like(m);
            //var o = np.quantile(a, 0.5, axis: 0);
            //print(o);
            //print(n);
            // note: we don't support the overwrite_input flag
            //b = a.Copy();
            //c = np.quantile(b, 0.5, axis: 1, overwrite_input: true);
            //print(c);

            //Assert.IsFalse((bool)np.all(a.Equals(b)).GetItem(0));

            return;
        }

        [TestMethod]
        public void test_median_2_OBJECT_TODO()
        {
            var a = np.arange(0, 64, 1, np.BigInt).reshape((4, 4, 4));

            var b = np.median(a, axis: new int[] { 0, 2 }, keepdims: true);
            AssertArray(b, new double[,,] { { { 25.5 }, { 29.5 }, { 33.5 }, { 37.5 } } });
            print(b);

            var c = np.median(a, new int[] { 0, 1 }, keepdims: true);
            AssertArray(c, new double[,,] { { { 30, 31, 32, 33 } } });
            print(c);

            var d = np.median(a, new int[] { 1, 2 }, keepdims: true);
            AssertArray(d, new double[,,] { { { 7.5 } }, { { 23.5 } }, { { 39.5 } }, { { 55.5 } } });
            print(d);

            return;
        }

        [TestMethod]
        public void test_average_3_OBJECT_TODO()
        {

            var a = np.array(new BigInteger[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            var w = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            var x1 = np.average(a, axis: null, weights: null, returned: true);
            Assert.AreEqual(5.5, x1.retval.GetItem(0));
            Assert.AreEqual((double)10.0, x1.sum_of_weights.GetItem(0));
            print(x1);
            print("********");

            x1 = np.average(a, axis: null, weights: w, returned: true);
            Assert.AreEqual(4.0, x1.retval.GetItem(0));
            Assert.AreEqual((double)55.0, x1.sum_of_weights.GetItem(0));
            print(x1);
            print("********");

            x1 = np.average(a.reshape((2, -1)), axis: null, weights: np.array(w).reshape((2, -1)), returned: true);
            Assert.AreEqual(4.0, x1.retval.GetItem(0));
            Assert.AreEqual((double)55.0, x1.sum_of_weights.GetItem(0));
            print(x1);
            print("********");

            x1 = np.average(a.reshape((2, -1)), axis: 0, weights: np.array(w).reshape((2, -1)), returned: true);
            AssertArray(x1.retval, new double[] { 2.66666666666667, 3.53846153846154, 4.36363636363636, 5.11111111111111, 5.71428571428571 });
            AssertArray(x1.sum_of_weights, new double[] { 15.0, 13.0, 11.0, 9.0, 7.0 });
            print(x1);
            print("********");

            x1 = np.average(a.reshape((2, -1)), axis: 1, weights: np.array(w).reshape((2, -1)), returned: true);
            AssertArray(x1.retval, new double[] { 2.75, 7.33333333333333 });
            AssertArray(x1.sum_of_weights, new double[] { 40.0, 15.0 });
            print(x1);
            print("********");

            x1 = np.average(a.reshape((1, 2, -1, 1)), axis: 1, weights: np.array(w).reshape((1, 2, -1, 1)), returned: true);
            AssertArray(x1.retval, new double[,,] { { { 2.66666666666667 }, { 3.53846153846154 }, { 4.36363636363636 }, { 5.11111111111111 }, { 5.71428571428571 } } });
            AssertArray(x1.sum_of_weights, new double[,,] { { { 15.0 }, { 13.0 }, { 11.0 }, { 9.0 }, { 7.0 } } });
            print(x1);
            print("********");

            x1 = np.average(a.reshape((1, -1, 2, 1)), axis: 1, weights: np.array(w).reshape((1, -1, 2, 1)), returned: true);
            AssertArray(x1.retval, new double[,,] { { { 3.66666666666667 }, { 4.4 } } });
            AssertArray(x1.sum_of_weights, new double[,,] { { { 30.0 }, { 25.0 } } });
            print(x1);
            print("********");

            x1 = np.average(a.reshape((2, -1, 1, 1)), axis: 1, weights: np.array(w).reshape((2, -1, 1, 1)), returned: false);
            AssertArray(x1.retval, new double[,,] { { { 2.75 } }, { { 7.33333333333333 } } });
            Assert.AreEqual(null, x1.sum_of_weights);
            print(x1);
        }

        [TestMethod]
        public void test_mean_1_OBJECT_TODO()
        {
            BigInteger[] TestData = new BigInteger[] { 10, 15, 25, 45, 78, 90, 10, 15, 25, 45, 78, 90 };
            var x = np.array(TestData, dtype: np.BigInt).reshape(new shape(3, 2, -1));
            x = x * 3;
            print(x);

            var y = np.mean(x);
            print(y);
            Assert.AreEqual(131.5, y.GetItem(0));

            y = np.mean(x, axis: 0);
            print(y);
            AssertArray(y, new double[,] { { 113, 150 }, { 113, 150 } });

            y = np.mean(x, axis: 1);
            print(y);
            AssertArray(y, new double[,] { { 52.5, 90 }, { 132, 157.5 }, { 154.5, 202.5 } });

            y = np.mean(x, axis: 2);
            print(y);
            AssertArray(y, new double[,] { { 37.5, 105 }, { 252, 37.5 }, { 105, 252 } });

        }

        [TestMethod]
        public void test_mean_2_OBJECT_TODO()
        {
            ndarray a = np.zeros(new shape(2, 512 * 512), dtype: np.BigInt);
            a[0, ":"] = 1.0;
            a[1, ":"] = 0.1;
            ndarray b = np.mean(a);
            print(b);
            Assert.AreEqual(0.5, (double)b.GetItem(0));

            ndarray c = np.mean(a, dtype: np.BigInt);
            print(c);
            Assert.AreEqual((BigInteger)0, c.GetItem(0));
        }

        [TestMethod]
        public void test_std_1_OBJECT_TODO()
        {
            ndarray a = np.array(new BigInteger[,] { { 1, 2 }, { 3, 4 } });
            ndarray b = np.std(a);
            print(b);
            Assert.AreEqual(1.1180339887498949, (double)b.GetItem(0));

            ndarray c = np.std(a, axis: 0);
            print(c);
            AssertArray(c, new double[] { 1.0, 1.0 });

            ndarray d = np.std(a, axis: 1);
            print(d);
            AssertArray(d, new double[] { 1.1180339887498949, 1.1180339887498949 }); // NOTES: TODO: slightly different than python. keepdims issue

            // In single precision, std() can be inaccurate:
            a = np.zeros(new shape(2, 512 * 512), dtype: np.BigInt);
            a[0, ":"] = 1;
            a[1, ":"] = 0;
            b = np.std(a);
            print(b);
            Assert.AreEqual(0.5, b.GetItem(0));
            // Computing the standard deviation in float64 is more accurate:
            c = np.std(a);
            print(c);
            Assert.AreEqual(0.5, c.GetItem(0));

        }

        [TestMethod]
        public void test_var_1_OBJECT_TODO()
        {
            ndarray a = np.array(new BigInteger[,] { { 1, 2 }, { 3, 4 } });
            ndarray b = np.var(a);
            Assert.AreEqual(1.25, b.GetItem(0));
            print(b);

            ndarray c = np.var(a, axis: 0);
            AssertArray(c, new double[] { 1.0, 1.0 });
            print(c);

            ndarray d = np.var(a, axis: 1);
            AssertArray(d, new double[] { 1.25, 1.25 });
            print(d);

            // In single precision, std() can be inaccurate:
            a = np.zeros(new shape(2, 512 * 512), dtype: np.BigInt);
            a[0, ":"] = 1;
            a[1, ":"] = 0;
            b = np.var(a);
            Assert.AreEqual(0.25, b.GetItem(0));
            print(b);

            // Computing the standard deviation in float64 is more accurate:
            c = np.var(a, dtype: np.Float64);
            Assert.AreEqual(0.25, c.GetItem(0));
            print(c);

        }

        [TestMethod]
        public void test_corrcoef_1_OBJECT_TODO()
        {
            var x1 = np.array(new BigInteger[,] { { 0, 2 }, { 1, 1 }, { 2, 0 } }).T;
            print(x1);

            // Note how  increases while  decreases. The covariance matrix shows this clearly:

            var a = np.corrcoef(x1);
            AssertArray(a, new double[,] { { 1, -1 }, { -1, 1 } });
            print(a);

            var x = new BigInteger[] { -21, -1, 43 };
            var y = new BigInteger[] { 3, 11, 12 };
            var X = np.stack(new object[] { x, y }, axis: 0);
            a = np.corrcoef(X);
            AssertArray(a, new double[,] { { 1.0, 0.804905985486053 }, { 0.804905985486053, 1.0 } });
            print(a);


            var b = np.corrcoef(x, y);
            AssertArray(b, new double[,] { { 1.0, 0.804905985486053 }, { 0.804905985486053, 1.0 } });
            print(b);

            var c = np.corrcoef(x, y, rowvar: false);
            AssertArray(c, new double[,] { { 1.0, 0.804905985486053 }, { 0.804905985486053, 1.0 } });
            print(c);


            return;
        }

        [TestMethod]
        public void test_correlate_1_OBJECT_TODO()
        {
            var a = np.correlate(new BigInteger[] { 1, 2, 3 }, new BigInteger[] { 0, 1, 5 });
            AssertArray(a, new BigInteger[] { 17 });
            print(a);

            var b = np.correlate(new BigInteger[] { 1, 2, 3 }, new BigInteger[] { 0, 1, 5 }, mode: NPY_CONVOLE_MODE.NPY_CONVOLVE_SAME);
            AssertArray(b, new BigInteger[] { 11, 17, 3 });
            print(b);

            var c = np.correlate(new BigInteger[] { 1, 2, 3 }, new BigInteger[] { 0, 1, 5 }, mode: NPY_CONVOLE_MODE.NPY_CONVOLVE_FULL);
            AssertArray(c, new BigInteger[] { 5, 11, 17, 3, 0 });
            print(c);

            return;
        }

        [TestMethod]
        public void test_correlate_1_Int64()
        {
            var a = np.correlate(new Int64[] { 1, 2, 3 }, new Int64[] { 0, 1, 5 });
            AssertArray(a, new Int64[] { 17 });
            print(a);

            var b = np.correlate(new Int64[] { 1, 2, 3 }, new Int64[] { 0, 1, 5 }, mode: NPY_CONVOLE_MODE.NPY_CONVOLVE_SAME);
            AssertArray(b, new Int64[] { 11, 17, 3 });
            print(b);

            var c = np.correlate(new Int64[] { 1, 2, 3 }, new Int64[] { 0, 1, 5 }, mode: NPY_CONVOLE_MODE.NPY_CONVOLVE_FULL);
            AssertArray(c, new Int64[] { 5, 11, 17, 3, 0 });
            print(c);

            return;
        }

        [TestMethod]
        public void test_cov_1_OBJECT_TODO()
        {
            var x1 = np.array(new BigInteger[,] { { 0, 2 }, { 1, 1 }, { 2, 0 } }).T;
            print(x1);

            // Note how  increases while  decreases. The covariance matrix shows this clearly:

            var a = np.cov(x1);
            AssertArray(a, new double[,] { { 1, -1 }, { -1, 1 } });
            print(a);

            var x = new BigInteger[] { -21, -1, 43 };
            var y = new BigInteger[] { 3, 11, 12 };
            var X = np.stack(new object[] { x, y }, axis: 0);
            a = np.cov(X);
            AssertArray(a, new double[,] { { 1072.0, 130.0 }, { 130.0, 24.3333333333333 } });
            print(a);


            var b = np.cov(x, y);
            AssertArray(b, new double[,] { { 1072.0, 130.0 }, { 130.0, 24.3333333333333 } });
            print(b);

            var c = np.cov(x);
            Assert.AreEqual(1072.0, c.GetItem(0));
            print(c);

            var d = np.cov(X, rowvar: false);
            AssertArray(d, new double[,] { { 288.0, 144.0, -372.0 }, { 144.0, 72.0, -186.0 }, { -372.0, -186.0, 480.5 } });
            print(d);

            var e = np.cov(X, rowvar: false, bias: true);
            AssertArray(e, new double[,] { { 144.0, 72.0, -186.0 }, { 72.0, 36.0, -93.0 }, { -186.0, -93.0, 240.25 } });
            print(e);

            var f = np.cov(X, rowvar: false, bias: true, fweights: new int[] { 1, 2 });
            AssertArray(f, new double[,] { { 128.0, 64.0, -165.333333333333 },
                                            { 64.0, 32.0, -82.6666666666667 },
                                            { -165.333333333333, -82.6666666666667, 213.555555555556 }});
            print(f);

            var g = np.cov(X, rowvar: false, bias: true, fweights: new int[] { 1, 2 }, aweights: new int[] { 1, 2 });
            AssertArray(g, new double[,] { { 92.16, 46.08, -119.04 }, { 46.08, 23.04, -59.52 }, { -119.04, -59.52, 153.76 } });
            print(g);

            return;
        }

        #endregion

        #region from TwoDimBaseTests

        [TestMethod]
        public void test_diag_1_OBJECT_TODO()
        {
            ndarray m = np.arange(9, dtype: np.BigInt);
            var n = np.diag(m);

            print(m);
            print(n);

            var ExpectedDataN = new BigInteger[,]
                {{0, 0, 0, 0, 0, 0, 0, 0, 0},
                 {0, 1, 0, 0, 0, 0, 0, 0, 0},
                 {0, 0, 2, 0, 0, 0, 0, 0, 0},
                 {0, 0, 0, 3, 0, 0, 0, 0, 0},
                 {0, 0, 0, 0, 4, 0, 0, 0, 0},
                 {0, 0, 0, 0, 0, 5, 0, 0, 0},
                 {0, 0, 0, 0, 0, 0, 6, 0, 0},
                 {0, 0, 0, 0, 0, 0, 0, 7, 0},
                 {0, 0, 0, 0, 0, 0, 0, 0, 8}};

            AssertArray(n, ExpectedDataN);

            m = np.arange(9, dtype: np.BigInt).reshape(new shape(3, 3));
            n = np.diag(m);

            print(m);
            print(n);
            AssertArray(n, new BigInteger[] { 0, 4, 8 });
        }

        [TestMethod]
        public void test_diagflat_1_OBJECT_TODO()
        {
            ndarray m = np.arange(1, 5, dtype: np.BigInt).reshape(new shape(2, 2));
            var n = np.diagflat(m);

            print(m);
            print(n);

            var ExpectedDataN = new BigInteger[,]
            {
             {1, 0, 0, 0},
             {0, 2, 0, 0},
             {0, 0, 3, 0},
             {0, 0, 0, 4}
            };
            AssertArray(n, ExpectedDataN);

            m = np.arange(1, 3, dtype: np.BigInt);
            n = np.diagflat(m, 1);

            print(m);
            print(n);

            ExpectedDataN = new BigInteger[,]
            {
             {0, 1, 0},
             {0, 0, 2},
             {0, 0, 0},
            };

            AssertArray(n, ExpectedDataN);

            m = np.arange(1, 3, dtype: np.BigInt);
            n = np.diagflat(m, -1);

            print(m);
            print(n);

            ExpectedDataN = new BigInteger[,]
            {
             {0, 0, 0},
             {1, 0, 0},
             {0, 2, 0},
            };

            AssertArray(n, ExpectedDataN);

        }

        [TestMethod]
        public void test_fliplr_1_OBJECT_TODO()
        {
            ndarray m = np.arange(8, dtype: np.BigInt).reshape(new shape(2, 2, 2));
            var n = np.fliplr(m);

            print(m);
            print(n);

            AssertArray(n, new BigInteger[,,] { { { 2, 3 }, { 0, 1 } }, { { 6, 7 }, { 4, 5 } } });
        }

        [TestMethod]
        public void test_flipud_1_OBJECT_TODO()
        {
            ndarray m = np.arange(8, dtype: np.BigInt).reshape(new shape(2, 2, 2));
            var n = np.flipud(m);

            print(m);
            print(n);

            AssertArray(n, new BigInteger[,,] { { { 4, 5 }, { 6, 7 } }, { { 0, 1 }, { 2, 3 } } });
        }

        [TestMethod]
        public void test_tri_1_OBJECT_TODO()
        {
            ndarray a = np.tri(3, 5, 2, dtype: np.BigInt);
            print(a);

            var ExpectedDataA = new BigInteger[,]
            {
             {1, 1, 1, 0, 0},
             {1, 1, 1, 1, 0},
             {1, 1, 1, 1, 1}
            };
            AssertArray(a, ExpectedDataA);

            print("***********");
            ndarray b = np.tri(3, 5, -1, dtype: np.BigInt);
            print(b);

            var ExpectedDataB = new BigInteger[,]
            {
             {0, 0, 0, 0, 0},
             {1, 0, 0, 0, 0},
             {1, 1, 0, 0, 0}
            };
            AssertArray(b, ExpectedDataB);
        }

        [TestMethod]
        public void test_tril_1_OBJECT_TODO()
        {
            ndarray a = np.array(new BigInteger[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 10, 11, 12 } });
            ndarray b = np.tril(a, -1);
            print(a);
            print("***********");
            print(b);

            var ExpectedDataB = new BigInteger[,]
            {
             {0, 0, 0},
             {4, 0, 0},
             {7, 8, 0},
             {10, 11, 12},
            };
            AssertArray(b, ExpectedDataB);

        }

        [TestMethod]
        public void test_triu_1_OBJECT_TODO()
        {
            ndarray a = np.array(new BigInteger[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 10, 11, 12 } });
            ndarray b = np.triu(a, -1);
            print(a);
            print("***********");
            print(b);

            var ExpectedDataB = new BigInteger[,]
            {
             {1, 2, 3},
             {4, 5, 6},
             {0, 8, 9},
             {0, 0, 12},
            };
            AssertArray(b, ExpectedDataB);

        }

        [TestMethod]
        public void test_vander_1_OBJECT_TODO()
        {
            var x = np.array(new BigInteger[] { 1, 2, 3, 5 });
            int N = 3;
            var y = np.vander(x, N);
            AssertArray(y, new BigInteger[,] { { 1, 1, 1 }, { 4, 2, 1 }, { 9, 3, 1 }, { 25, 5, 1 } });
            print(y);

            y = np.vander(x);
            AssertArray(y, new BigInteger[,] { { 1, 1, 1, 1 }, { 8, 4, 2, 1 }, { 27, 9, 3, 1 }, { 125, 25, 5, 1 } });
            print(y);

            y = np.vander(x, increasing: true);
            AssertArray(y, new BigInteger[,] { { 1, 1, 1, 1 }, { 1, 2, 4, 8 }, { 1, 3, 9, 27 }, { 1, 5, 25, 125 } });
            print(y);

            return;
        }

        [TestMethod]
        public void test_mask_indices_OBJECT_TODO()
        {
            var iu = np.mask_indices(3, np.triu);
            AssertArray(iu[0], new Int64[] { 0, 0, 0, 1, 1, 2 });
            AssertArray(iu[1], new Int64[] { 0, 1, 2, 1, 2, 2 });
            print(iu);

            var a = np.arange(9, dtype: np.BigInt).reshape((3, 3));
            var b = a[iu] as ndarray;
            AssertArray(b, new BigInteger[] { 0, 1, 2, 4, 5, 8 });
            print(b);

            var iu1 = np.mask_indices(3, np.triu, 1);

            var c = a[iu1] as ndarray;
            AssertArray(c, new BigInteger[] { 1, 2, 5 });
            print(c);

            return;
        }

        [TestMethod]
        public void test_tril_indices_OBJECT_TODO()
        {
            var il1 = np.tril_indices(4);
            var il2 = np.tril_indices(4, 2);

            var a = np.arange(16, dtype: np.BigInt).reshape((4, 4));
            var b = a[il1] as ndarray;
            AssertArray(b, new BigInteger[] { 0, 4, 5, 8, 9, 10, 12, 13, 14, 15 });
            print(b);

            a[il1] = -1;

            var ExpectedDataA1 = new BigInteger[,]
                {{-1,  1, 2,  3}, {-1, -1,  6,  7},
                 {-1, -1,-1, 11}, {-1, -1, -1, -1}};
            AssertArray(a, ExpectedDataA1);
            print(a);

            a[il2] = -10;

            var ExpectedDataA2 = new BigInteger[,]
                {{-10, -10, -10,  3}, {-10, -10, -10, -10},
                 {-10, -10,-10, -10}, {-10, -10, -10, -10}};
            AssertArray(a, ExpectedDataA2);
            print(a);

            return;
        }

        [TestMethod]
        public void test_tril_indices_from_OBJECT_TODO()
        {
            var a = np.arange(16, dtype: np.BigInt).reshape((4, 4));
            var il1 = np.tril_indices_from(a, 0);

            AssertArray(il1[0], new Int64[] { 0, 1, 1, 2, 2, 2, 3, 3, 3, 3 });
            AssertArray(il1[1], new Int64[] { 0, 0, 1, 0, 1, 2, 0, 1, 2, 3 });
            print(il1);

            var il2 = np.tril_indices_from(a, 2);
            AssertArray(il2[0], new Int64[] { 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 });
            AssertArray(il2[1], new Int64[] { 0, 1, 2, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3 });

            print(il2);

            return;
        }

        [TestMethod]
        public void test_triu_indices_OBJECT_TODO()
        {
            var il1 = np.triu_indices(4);
            var il2 = np.triu_indices(4, 2);

            var a = np.arange(16, dtype: np.BigInt).reshape((4, 4));
            var b = a[il1] as ndarray;
            AssertArray(b, new BigInteger[] { 0, 1, 2, 3, 5, 6, 7, 10, 11, 15 });
            print(b);

            a[il1] = -1;

            var ExpectedDataA1 = new BigInteger[,]
                {{-1, -1, -1, -1}, { 4, -1, -1, -1},
                 { 8,  9, -1, -1}, {12, 13, 14, -1}};
            AssertArray(a, ExpectedDataA1);
            print(a);

            a[il2] = -10;

            var ExpectedDataA2 = new BigInteger[,]
                {{-1, -1, -10, -10}, {4,  -1, -1, -10},
                 { 8,  9, -1,  -1},  {12, 13, 14, -1}};
            AssertArray(a, ExpectedDataA2);
            print(a);

            return;
        }

        [TestMethod]
        public void test_triu_indices_from_OBJECT_TODO()
        {
            var a = np.arange(16, dtype: np.BigInt).reshape((4, 4));
            var il1 = np.triu_indices_from(a, 0);

            AssertArray(il1[0], new Int64[] { 0, 0, 0, 0, 1, 1, 1, 2, 2, 3 });
            AssertArray(il1[1], new Int64[] { 0, 1, 2, 3, 1, 2, 3, 2, 3, 3 });
            print(il1);

            var il2 = np.triu_indices_from(a, 2);
            AssertArray(il2[0], new Int64[] { 0, 0, 1 });
            AssertArray(il2[1], new Int64[] { 2, 3, 3 });

            print(il2);

            return;
        }

        #endregion

        #region from ShapeBaseTests

        [TestMethod]
        public void test_atleast_1d_OBJECT_TODO()
        {
            var a = np.atleast_1d((BigInteger)1.0);
            print(a);
            AssertArray(a.ElementAt(0), new BigInteger[] { 1 });

            print("**************");
            var x = np.arange(9.0, dtype: np.BigInt).reshape(new shape(3, 3));
            var b = np.atleast_1d(x);
            print(b);

            var ExpectedB = new BigInteger[,]
                {{0, 1, 2},
                 {3, 4, 5},
                 {6, 7, 8}};
            AssertArray(b.ElementAt(0), ExpectedB);

            print("**************");

            var c = np.atleast_1d(new object[] { (BigInteger)1, new BigInteger[] { 3, 4 } });

            AssertArray(c.ElementAt(0), new BigInteger[] { 1 });
            AssertArray(c.ElementAt(1), new BigInteger[] { 3, 4 });
            print(c);

        }

        [TestMethod]
        public void test_atleast_2d_OBJECT_TODO()
        {
            var a = np.atleast_2d((BigInteger)1);
            print(a);
            AssertArray(a.ElementAt(0), new BigInteger[,] { { 1 } });

            print("**************");
            var x = np.arange(9.0, dtype: np.BigInt).reshape(new shape(3, 3));
            var b = np.atleast_2d(x);
            print(b);

            var ExpectedB = new BigInteger[,]
                {{0, 1, 2},
                 {3, 4, 5},
                 {6, 7, 8}};
            AssertArray(b.ElementAt(0), ExpectedB);

            print("**************");

            var c = np.atleast_2d(new object[] { (BigInteger)1, new BigInteger[] { 3, 4 }, new BigInteger[] { 5, 6 } });

            AssertArray(c.ElementAt(0), new BigInteger[,] { { 1 } });
            AssertArray(c.ElementAt(1), new BigInteger[,] { { 3, 4 } });
            AssertArray(c.ElementAt(2), new BigInteger[,] { { 5, 6 } });
            print(c);

        }

        [TestMethod]
        public void test_atleast_3d_OBJECT_TODO()
        {
            var a = np.atleast_3d((BigInteger)1);
            print(a);
            AssertArray(a.ElementAt(0), new BigInteger[,,] { { { 1 } } });

            print("**************");
            var x = np.arange(9.0, dtype: np.BigInt).reshape(new shape(3, 3));
            var b = np.atleast_3d(x);
            print(b);

            var ExpectedB = new BigInteger[,,]
             {{{0},
               {1},
               {2}},
              {{3},
               {4},
               {5}},
              {{6},
               {7},
               {8}}};

            AssertArray(b.ElementAt(0), ExpectedB);

            print("**************");

            var c = np.atleast_3d(new object[] { new BigInteger[] { 1, 2 }, new BigInteger[] { 3, 4 }, new BigInteger[] { 5, 6 } });

            AssertArray(c.ElementAt(0), new BigInteger[,,] { { { 1 }, { 2 } } });
            AssertArray(c.ElementAt(1), new BigInteger[,,] { { { 3 }, { 4 } } });
            AssertArray(c.ElementAt(2), new BigInteger[,,] { { { 5 }, { 6 } } });
            print(c);


        }

        [TestMethod]
        public void test_vstack_2_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[,] { { 1 }, { 2 }, { 3 } });
            var b = np.array(new BigInteger[,] { { 2 }, { 3 }, { 4 } });
            var c = np.vstack(new object[] { a, b });

            AssertArray(c, new BigInteger[,] { { 1 }, { 2 }, { 3 }, { 2 }, { 3 }, { 4 } });

            print(c);
        }

        [TestMethod]
        public void test_hstack_2_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[,] { { 1 }, { 2 }, { 3 } });
            var b = np.array(new BigInteger[,] { { 2 }, { 3 }, { 4 } });
            var c = np.hstack(new object[] { a, b });

            AssertArray(c, new BigInteger[,] { { 1, 2 }, { 2, 3 }, { 3, 4 } });

            print(c);
        }

        [TestMethod]
        public void test_stack_1_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[,] { { 1 }, { 2 }, { 3 } });
            var b = np.array(new BigInteger[,] { { 2 }, { 3 }, { 4 } });

            var c = np.stack(new object[] { a, b }, axis: 0);
            AssertArray(c, new BigInteger[,,] { { { 1 }, { 2 }, { 3 } }, { { 2 }, { 3 }, { 4 } } });
            print(c);
            print("**************");

            var d = np.stack(new object[] { a, b }, axis: 1);
            AssertArray(d, new BigInteger[,,] { { { 1 }, { 2 } }, { { 2 }, { 3 } }, { { 3 }, { 4 } } });
            print(d);
            print("**************");

            var e = np.stack(new object[] { a, b }, axis: 2);
            AssertArray(e, new BigInteger[,,] { { { 1, 2 } }, { { 2, 3 } }, { { 3, 4 } } });
            print(e);

        }

        [TestMethod]
        public void test_block_2_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[] { 1, 2, 3 });
            var b = np.array(new BigInteger[] { 2, 3, 4 });
            var c = np.block(new object[] { a, b, 10 });    // hstack([a, b, 10])

            AssertArray(c, new BigInteger[] { 1, 2, 3, 2, 3, 4, 10 });
            print(c);
            print("**************");

            a = np.array(new BigInteger[] { 1, 2, 3 });
            b = np.array(new BigInteger[] { 2, 3, 4 });
            c = np.block(new object[] { new object[] { a }, new object[] { b } });    // vstack([a, b])

            AssertArray(c, new BigInteger[,] { { 1, 2, 3 }, { 2, 3, 4 } });
            print(c);

        }

        [TestMethod]
        public void test_expand_dims_1_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }).reshape(new shape(2, -1, 2));
            var b = np.expand_dims(a, axis: 0);

            var ExpectedDataB = new BigInteger[,,,]
            {{{{1,  2}, {3,  4}, {5,  6}},
              {{7,  8}, {9, 10}, {11, 12}}}};

            AssertArray(b, ExpectedDataB);
            print(b);
            print("**************");

            var c = np.expand_dims(a, axis: 1);
            var ExpectedDataC = new BigInteger[,,,]
                {{{{1,  2}, {3,  4}, {5,  6}}},
                {{{ 7,  8},{ 9, 10}, {11, 12}}}};
            AssertArray(c, ExpectedDataC);
            print(c);
            print("**************");

            var d = np.expand_dims(a, axis: 2);
            var ExpectedDataD = new BigInteger[,,,]
            {{{{1,  2}},{{3,  4}},{{5,  6}}},
             {{{7,  8}},{{9, 10}},{{11, 12}}}};

            AssertArray(d, ExpectedDataD);
            print(d);

        }

        [TestMethod]
        public void test_column_stack_1_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[] { 1, 2, 3 });
            var b = np.array(new BigInteger[] { 2, 3, 4 });
            var c = np.column_stack(new object[] { a, b });

            AssertArray(c, new BigInteger[,] { { 1, 2 }, { 2, 3 }, { 3, 4 } });
            print(c);
        }

        [TestMethod]
        public void test_row_stack_1_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[] { 1, 2, 3 });
            var b = np.array(new BigInteger[] { 2, 3, 4 });
            var c = np.row_stack(new object[] { a, b });

            AssertArray(c, new BigInteger[,] { { 1, 2, 3 }, { 2, 3, 4 } });

            print(c);
        }

        [TestMethod]
        public void test_dstack_1_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[] { 1, 2, 3 });
            var b = np.array(new BigInteger[] { 2, 3, 4 });
            var c = np.dstack(new object[] { a, b });

            AssertArray(c, new BigInteger[,,] { { { 1, 2 }, { 2, 3 }, { 3, 4 } } });
            print(c);

            a = np.array(new BigInteger[,] { { 1 }, { 2 }, { 3 } });
            b = np.array(new BigInteger[,] { { 2 }, { 3 }, { 4 } });
            c = np.dstack(new object[] { a, b });

            AssertArray(c, new BigInteger[,,] { { { 1, 2 } }, { { 2, 3 } }, { { 3, 4 } } });

            print(c);
        }

        [TestMethod]
        public void test_array_split_2_OBJECT_TODO()
        {
            var x = np.arange(16.0, dtype: np.BigInt).reshape(new shape(2, 8, 1));
            var y = np.array_split(x, 3, axis: 0);


            AssertArray(y.ElementAt(0), new BigInteger[,,] { { { 0 }, { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 } } });
            AssertArray(y.ElementAt(1), new BigInteger[,,] { { { 8 }, { 9 }, { 10 }, { 11 }, { 12 }, { 13 }, { 14 }, { 15 } } });
            AssertShape(y.ElementAt(2), 0, 8, 1);

            print(y);

            print("**************");

            x = np.arange(16.0, dtype: np.BigInt).reshape(new shape(2, 8, 1));
            y = np.array_split(x, 3, axis: 1);

            AssertArray(y.ElementAt(0), new BigInteger[,,] { { { 0 }, { 1 }, { 2 } }, { { 8 }, { 9 }, { 10 } } });
            AssertArray(y.ElementAt(1), new BigInteger[,,] { { { 3 }, { 4 }, { 5 } }, { { 11 }, { 12 }, { 13 } } });
            AssertArray(y.ElementAt(2), new BigInteger[,,] { { { 6 }, { 7 } }, { { 14 }, { 15 } } });


            print(y);

            print("**************");

            x = np.arange(16.0, dtype: np.BigInt).reshape(new shape(2, 8, 1));
            y = np.array_split(x, 3, axis: 2);

            AssertArray(y.ElementAt(0), new BigInteger[,,] { { { 0 }, { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 } }, { { 8 }, { 9 }, { 10 }, { 11 }, { 12 }, { 13 }, { 14 }, { 15 } } });
            AssertShape(y.ElementAt(1), 2, 8, 0);
            AssertShape(y.ElementAt(2), 2, 8, 0);
            print(y);
        }

        [TestMethod]
        public void test_split_2_OBJECT_TODO()
        {
            var x = np.arange(16.0, dtype: np.BigInt).reshape(new shape(8, 2, 1));
            var y = np.split(x, new Int32[] { 2, 3 }, axis: 0);

            Assert.AreEqual(3, y.Count);
            AssertArray(y.ElementAt(0), new BigInteger[,,] { { { 0 }, { 1 } }, { { 2 }, { 3 } } });
            AssertArray(y.ElementAt(1), new BigInteger[,,] { { { 4 }, { 5 } } });
            AssertArray(y.ElementAt(2), new BigInteger[,,] { { { 6 }, { 7 } }, { { 8 }, { 9 } }, { { 10 }, { 11 } }, { { 12 }, { 13 } }, { { 14 }, { 15 } } });


            print(y);

            print("**************");

            x = np.arange(16.0, dtype: np.BigInt).reshape(new shape(8, 2, 1));
            y = np.split(x, new int[] { 2, 3 }, axis: 1);

            Assert.AreEqual(3, y.Count);
            AssertArray(y.ElementAt(0), new BigInteger[,,] {{{0},{1}},{{2}, {3}}, {{4}, {5}}, {{6}, { 7}},
                                                        {{8},{9}},{{10},{11}}, {{12}, {13}}, {{14}, {15}}});
            AssertShape(y.ElementAt(1), 8, 0, 1);
            AssertShape(y.ElementAt(2), 8, 0, 1);

            print(y);

            print("**************");

            x = np.arange(16.0, dtype: np.BigInt).reshape(new shape(8, 2, 1));
            y = np.split(x, new int[] { 2, 3 }, axis: 2);

            Assert.AreEqual(3, y.Count);
            AssertArray(y.ElementAt(0), new BigInteger[,,] {{{ 0},{ 1}},{{ 2}, { 3}}, {{ 4}, { 5}}, {{ 6}, { 7}},
                                                        {{ 8},{ 9}},{{10}, {11}}, {{12}, {13}}, {{14}, {15}}});
            AssertShape(y.ElementAt(1), 8, 2, 0);
            AssertShape(y.ElementAt(2), 8, 2, 0);

            print(y);
        }

        [TestMethod]
        public void test_hsplit_2_OBJECT_TODO()
        {
            var x = np.arange(8, dtype: np.BigInt).reshape(new shape(2, 2, 2));
            var y = np.hsplit(x, 2);

            Assert.AreEqual(2, y.Count);
            AssertArray(y.ElementAt(0), new BigInteger[,,] { { { 0, 1 } }, { { 4, 5 } } });
            AssertArray(y.ElementAt(1), new BigInteger[,,] { { { 2, 3 } }, { { 6, 7 } } });
            print(y);

            print("**************");

            x = np.arange(8, dtype: np.BigInt).reshape(new shape(2, 2, 2));
            y = np.hsplit(x, new Int32[] { 3, 6 });

            Assert.AreEqual(3, y.Count);
            AssertArray(y.ElementAt(0), new BigInteger[,,] { { { 0, 1 }, { 2, 3 } }, { { 4, 5 }, { 6, 7 } } });
            AssertShape(y.ElementAt(1), 2, 0, 2);
            AssertShape(y.ElementAt(2), 2, 0, 2);

            print(y);
        }

        [TestMethod]
        public void test_vsplit_2_OBJECT_TODO()
        {
            var x = np.arange(8, dtype: np.BigInt).reshape(new shape(2, 2, 2));
            var y = np.vsplit(x, 2);

            Assert.AreEqual(2, y.Count);
            AssertArray(y.ElementAt(0), new BigInteger[,,] { { { 0, 1 }, { 2, 3 } } });
            AssertArray(y.ElementAt(1), new BigInteger[,,] { { { 4, 5 }, { 6, 7 } } });
            print(y);

            print("**************");

            x = np.arange(8, dtype: np.BigInt).reshape(new shape(2, 2, 2));
            y = np.vsplit(x, new int[] { 3, 6 });

            Assert.AreEqual(3, y.Count);
            AssertArray(y.ElementAt(0), new BigInteger[,,] { { { 0, 1 }, { 2, 3 } }, { { 4, 5 }, { 6, 7 } } });
            AssertShape(y.ElementAt(1), 0, 2, 2);
            AssertShape(y.ElementAt(2), 0, 2, 2);

            print(y);
        }

        [TestMethod]
        public void test_dsplit_1_OBJECT_TODO()
        {
            var x = np.arange(16, dtype: np.BigInt).reshape(new shape(2, 2, 4));
            var y = np.dsplit(x, 2);

            Assert.AreEqual(2, y.Count);
            AssertArray(y.ElementAt(0), new BigInteger[,,] { { { 0, 1 }, { 4, 5 } }, { { 8, 9 }, { 12, 13 } } });
            AssertArray(y.ElementAt(1), new BigInteger[,,] { { { 2, 3 }, { 6, 7 } }, { { 10, 11 }, { 14, 15 } } });
            print(y);


            print("**************");

            x = np.arange(16, dtype: np.BigInt).reshape(new shape(2, 2, 4));
            y = np.dsplit(x, new int[] { 3, 6 });

            Assert.AreEqual(3, y.Count);
            AssertArray(y.ElementAt(0), new BigInteger[,,] { { { 0, 1, 2 }, { 4, 5, 6 } }, { { 8, 9, 10 }, { 12, 13, 14 } } });
            AssertArray(y.ElementAt(1), new BigInteger[,,] { { { 3 }, { 7 } }, { { 11 }, { 15 } } });
            AssertShape(y.ElementAt(2), 2, 2, 0);

            print(y);
        }

        [TestMethod]
        public void test_kron_1_OBJECT_TODO()
        {

            var a = np.kron(new BigInteger[] { 1, 10, 100 }, new BigInteger[] { 5, 6, 7 });
            AssertArray(a, new BigInteger[] { 5, 6, 7, 50, 60, 70, 500, 600, 700 });
            print(a);

            var b = np.kron(new BigInteger[] { 5, 6, 7 }, new BigInteger[] { 1, 10, 100 });
            AssertArray(b, new BigInteger[] { 5, 50, 500, 6, 60, 600, 7, 70, 700 });
            print(b);

            var x = np.array(new BigInteger[,] { { 2, 3 }, { 4, 5 } });
            var y = np.array(new BigInteger[,] { { 5, 6 }, { 7, 8 } });

            var c = np.kron(x, y);
            AssertArray(c, new BigInteger[,] { { 10, 12, 15, 18 }, { 14, 16, 21, 24 }, { 20, 24, 25, 30 }, { 28, 32, 35, 40 } });
            print(c);
            print(c.shape);

            c = np.kron(np.eye(2, dtype: np.BigInt), np.ones(new shape(2, 2), dtype: np.BigInt));
            AssertArray(c, new BigInteger[,] { { 1, 1, 0, 0 }, { 1, 1, 0, 0 }, { 0, 0, 1, 1 }, { 0, 0, 1, 1 } });


            x = np.array(new BigInteger[,,] { { { 2, 3, 3 }, { 4, 5, 3 } } });
            y = np.array(new BigInteger[,,] { { { 5, 6, 6, 6 }, { 7, 8, 6, 6 } } });

            c = np.kron(x, y);
            AssertArray(c, new BigInteger[,,] { { { 10, 12, 12, 12, 15, 18, 18, 18, 15, 18, 18, 18 },
                                           { 14, 16, 12, 12, 21, 24, 18, 18, 21, 24, 18, 18 },
                                           { 20, 24, 24, 24, 25, 30, 30, 30, 15, 18, 18, 18 },
                                           { 28, 32, 24, 24, 35, 40, 30, 30, 21, 24, 18, 18 } } });
            print(c);
            print(c.shape);


            var d = np.kron(np.ones((5, 7, 9, 11), dtype: np.Int32), np.ones((3, 4, 6, 8), dtype: np.Int32));
            AssertShape(d, 15, 28, 54, 88);
            print(d.shape);

        }

        [TestMethod]
        public void test_tile_2_OBJECT_TODO()
        {
            var a = np.array(new BigInteger[,] { { 1, 2 }, { 3, 4 } });
            var b = np.tile(a, 2);
            AssertArray(b, new BigInteger[,] { { 1, 2, 1, 2 }, { 3, 4, 3, 4 } });
            print(b);
            print("**************");

            var c = np.tile(a, new Int32[] { 2, 1 });
            AssertArray(c, new BigInteger[,] { { 1, 2 }, { 3, 4 }, { 1, 2 }, { 3, 4 } });
            print(c);
            print("**************");

            var d = np.array(new BigInteger[] { 1, 2, 3, 4 });
            var e = np.tile(d, new Int32[] { 4, 1 });

            AssertArray(e, new BigInteger[,] { { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4 }, { 1, 2, 3, 4 } });
            print(e);
        }

        #endregion

        #region from UFUNCTests

        [TestMethod]
        public void test_UFUNC_AddReduce_1_OBJECT_TODO()
        {
            var x = np.arange(8, dtype: np.BigInt);

            var a = np.ufunc.reduce(NpyArray_Ops.npy_op_add, x);
            Assert.AreEqual((BigInteger)28, a.GetItem(0));
            print(a);

            x = np.arange(8, dtype: np.BigInt).reshape((2, 2, 2));
            var b = np.ufunc.reduce(NpyArray_Ops.npy_op_add, x);
            AssertArray(b, new BigInteger[,] { { 4, 6 }, { 8, 10 } });
            print(b);

            var c = np.ufunc.reduce(NpyArray_Ops.npy_op_add, x, 0);
            AssertArray(c, new BigInteger[,] { { 4, 6 }, { 8, 10 } });
            print(c);

            var d = np.ufunc.reduce(NpyArray_Ops.npy_op_add, x, 1);
            AssertArray(d, new BigInteger[,] { { 2, 4 }, { 10, 12 } });
            print(d);

            var e = np.ufunc.reduce(NpyArray_Ops.npy_op_add, x, 2);
            AssertArray(e, new BigInteger[,] { { 1, 5 }, { 9, 13 } });
            print(e);

        }

        [TestMethod]
        public void test_UFUNC_AddAccumulate_1_OBJECT_TODO()
        {
            var x = np.arange(8, dtype: np.BigInt);

            var a = np.ufunc.accumulate(NpyArray_Ops.npy_op_add, x);
            AssertArray(a, new BigInteger[] { 0, 1, 3, 6, 10, 15, 21, 28 });
            print(a);

            x = np.arange(8, dtype: np.BigInt).reshape((2, 2, 2));
            var b = np.ufunc.accumulate(NpyArray_Ops.npy_op_add, x);
            AssertArray(b, new BigInteger[,,] { { { 0, 1 }, { 2, 3 } }, { { 4, 6 }, { 8, 10 } } });
            print(b);

            var c = np.ufunc.accumulate(NpyArray_Ops.npy_op_add, x, 0);
            AssertArray(c, new BigInteger[,,] { { { 0, 1 }, { 2, 3 } }, { { 4, 6 }, { 8, 10 } } });
            print(c);

            var d = np.ufunc.accumulate(NpyArray_Ops.npy_op_add, x, 1);
            AssertArray(d, new BigInteger[,,] { { { 0, 1 }, { 2, 4 } }, { { 4, 5 }, { 10, 12 } } });
            print(d);

            var e = np.ufunc.accumulate(NpyArray_Ops.npy_op_add, x, 2);
            AssertArray(e, new BigInteger[,,] { { { 0, 1 }, { 2, 5 } }, { { 4, 9 }, { 6, 13 } } });
            print(e);

        }

        [TestMethod]
        public void test_UFUNC_AddReduceAt_1_OBJECT_TODO()
        {
            var a = np.ufunc.reduceat(NpyArray_Ops.npy_op_add, np.arange(8, dtype: np.BigInt), new long[] { 0, 4, 1, 5, 2, 6, 3, 7 })["::2"] as ndarray;
            AssertArray(a, new BigInteger[] { 6, 10, 14, 18 });
            print(a);

            double retstep = 0;
            var x = np.linspace(0, 15, ref retstep, 16, dtype: np.BigInt).reshape((4, 4));
            var b = np.ufunc.reduceat(NpyArray_Ops.npy_op_add, x, new long[] { 0, 3, 1, 2, 0 });
            AssertArray(b, new BigInteger[,] {{12, 15, 18, 21},{12, 13, 14, 15}, {4, 5, 6, 7},
                                          {8, 9, 10, 11}, {24, 28, 32, 36}});
            print(b);

            var c = np.ufunc.reduceat(NpyArray_Ops.npy_op_multiply, x, new long[] { 0, 3 }, axis: 1);
            AssertArray(c, new BigInteger[,] { { 0, 3 }, { 120, 7 }, { 720, 11 }, { 2184, 15 } });
            print(c);
        }

        [TestMethod]
        public void test_UFUNC_AddOuter_1_OBJECT_TODO()
        {
            var x = np.arange(4, dtype: np.BigInt);

            var a = np.ufunc.outer(NpyArray_Ops.npy_op_add, null, x, x);
            AssertShape(a, 4, 4);
            print(a.shape);
            AssertArray(a, new BigInteger[,] { { 0, 1, 2, 3 }, { 1, 2, 3, 4 }, { 2, 3, 4, 5 }, { 3, 4, 5, 6 } });
            print(a);

            x = np.arange(6, dtype: np.BigInt).reshape((3, 2));
            var y = np.arange(6, dtype: np.BigInt).reshape((2, 3));
            var b = np.ufunc.outer(NpyArray_Ops.npy_op_add, null, x, y);
            AssertShape(b, 3, 2, 2, 3);
            print(b.shape);

            var ExpectedDataB = new BigInteger[,,,]

                {{{{0,  1,  2}, {3,  4,  5}}, {{1,  2,  3}, { 4,  5,  6}}},
                 {{{2,  3,  4}, {5,  6,  7}}, {{3,  4,  5}, { 6,  7,  8}}},
                 {{{4,  5,  6}, {7,  8,  9}}, {{5,  6,  7}, { 8,  9, 10}}}};

            AssertArray(b, ExpectedDataB);

            print(b);
        }

        #endregion

        #region from IndexTricksTests

        [TestMethod]
        public void test_mgrid_1_OBJECT_TODO()
        {
            var a = (ndarray)np.mgrid(new Slice[] { new Slice((BigInteger)0, (BigInteger)5) });
            print(a);
            AssertArray(a, new BigInteger[] { 0, 1, 2, 3, 4 });
            print("************");

            var b = (ndarray)np.mgrid(new Slice[] { new Slice((BigInteger)0, (BigInteger)6) });
            print(b);
            AssertArray(b, new BigInteger[] { 0, 1, 2, 3, 4, 5 });
            print("************");

            var c = (ndarray)np.mgrid(new Slice[] { new Slice((BigInteger)0, (BigInteger)5), new Slice((BigInteger)0, (BigInteger)5) });
            print(c);

            var ExpectedCArray = new BigInteger[,,]
                {{{0, 0, 0, 0, 0},  {1, 1, 1, 1, 1},  {2, 2, 2, 2, 2},  {3, 3, 3, 3, 3},  {4, 4, 4, 4, 4}},
                 {{0, 1, 2, 3, 4},  {0, 1, 2, 3, 4},  {0, 1, 2, 3, 4},  {0, 1, 2, 3, 4},  {0, 1, 2, 3, 4}}};
            AssertArray(c, ExpectedCArray);


            print("************");

            var d = (ndarray)np.mgrid(new Slice[] { new Slice((BigInteger)0, (BigInteger)6), new Slice((BigInteger)0, (BigInteger)6) });
            print(d);
            var ExpectedDArray = new BigInteger[,,]
                {{{0, 0, 0, 0, 0, 0},  {1, 1, 1, 1, 1, 1},  {2, 2, 2, 2, 2, 2},  {3, 3, 3, 3, 3, 3},  {4, 4, 4, 4, 4, 4}, {5, 5, 5, 5, 5, 5}},
                 {{0, 1, 2, 3, 4, 5},  {0, 1, 2, 3, 4, 5},  {0, 1, 2, 3, 4, 5},  {0, 1, 2, 3, 4, 5},  {0, 1, 2, 3, 4, 5}, {0, 1, 2, 3, 4, 5}}};
            AssertArray(d, ExpectedDArray);

            print("************");

            var e = (ndarray)np.mgrid(new Slice[] { new Slice((BigInteger)3, (BigInteger)5), new Slice((BigInteger)4, (BigInteger)6), new Slice((BigInteger)2, (BigInteger)5) });
            print(e);
            var ExpectedEArray = new BigInteger[,,,]
                {
                    {{{3, 3, 3}, {3, 3, 3}}, {{4, 4, 4}, {4, 4, 4}}},
                    {{{4, 4, 4}, {5, 5, 5}}, {{4, 4, 4}, {5, 5, 5}}},
                    {{{2, 3, 4}, {2, 3, 4}}, {{2, 3, 4}, {2, 3, 4}}},
                };
            AssertArray(e, ExpectedEArray);

        }

        [TestMethod]
        public void test_ogrid_1_OBJECT_TODO()
        {
            var a = (ndarray)np.ogrid(new Slice[] { new Slice((BigInteger)0, (BigInteger)5) });
            print(a);
            AssertArray(a, new BigInteger[] { 0, 1, 2, 3, 4 });
            print("************");

            var b = (ndarray)np.ogrid(new Slice[] { new Slice((BigInteger)0, (BigInteger)6) });
            print(b);
            AssertArray(b, new BigInteger[] { 0, 1, 2, 3, 4, 5 });
            print("************");

            var c = (ndarray[])np.ogrid(new Slice[] { new Slice((BigInteger)0, (BigInteger)5), new Slice((BigInteger)0, (BigInteger)5) });
            print(c);
            AssertArray(c[0], new BigInteger[,] { { 0 }, { 1 }, { 2 }, { 3 }, { 4 } });
            AssertArray(c[1], new BigInteger[,] { { 0, 1, 2, 3, 4 } });


            print("************");

            var d = (ndarray[])np.ogrid(new Slice[] { new Slice((BigInteger)0, (BigInteger)6), new Slice((BigInteger)0, (BigInteger)6) });
            print(d);
            AssertArray(d[0], new BigInteger[,] { { 0 }, { 1 }, { 2 }, { 3 }, { 4 }, { 5 } });
            AssertArray(d[1], new BigInteger[,] { { 0, 1, 2, 3, 4, 5 } });

            print("************");

            var e = (ndarray[])np.ogrid(new Slice[] { new Slice((BigInteger)3, (BigInteger)5), new Slice((BigInteger)4, (BigInteger)6), new Slice((BigInteger)2, (BigInteger)5) });
            print(e);
            AssertArray(e[0], new BigInteger[,,] { { { 3 } }, { { 4 } } });
            AssertArray(e[1], new BigInteger[,,] { { { 4 }, { 5 } } });
            AssertArray(e[2], new BigInteger[,,] { { { 2, 3, 4 } } });

        }

        [TestMethod]
        public void test_fill_diagonal_1_OBJECT_TODO()
        {
            var a = np.zeros((3, 3), np.BigInt);
            np.fill_diagonal(a, 5);
            AssertArray(a, new BigInteger[,] { { 5, 0, 0 }, { 0, 5, 0 }, { 0, 0, 5 } });
            print(a);

            a = np.zeros((3, 3, 3, 3), np.BigInt);
            np.fill_diagonal(a, 4);
            AssertArray(a[0, 0] as ndarray, new BigInteger[,] { { 4, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } });
            print(a[0, 0]);
            AssertArray(a[1, 1] as ndarray, new BigInteger[,] { { 0, 0, 0 }, { 0, 4, 0 }, { 0, 0, 0 } });
            print(a[1, 1]);
            AssertArray(a[2, 2] as ndarray, new BigInteger[,] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 4 } });
            print(a[2, 2]);

            // tall matrices no wrap
            a = np.zeros((5, 3), np.BigInt);
            np.fill_diagonal(a, 4);
            AssertArray(a, new BigInteger[,] { { 4, 0, 0 }, { 0, 4, 0 }, { 0, 0, 4 }, { 0, 0, 0 }, { 0, 0, 0 } });
            print(a);

            // tall matrices wrap
            a = np.zeros((5, 3), np.BigInt);
            np.fill_diagonal(a, 4, wrap: true);
            AssertArray(a, new BigInteger[,] { { 4, 0, 0 }, { 0, 4, 0 }, { 0, 0, 4 }, { 0, 0, 0 }, { 4, 0, 0 } });
            print(a);

            // wide matrices wrap
            a = np.zeros((3, 5), np.BigInt);
            np.fill_diagonal(a, 4, wrap: true);
            AssertArray(a, new BigInteger[,] { { 4, 0, 0, 0, 0 }, { 0, 4, 0, 0, 0 }, { 0, 0, 4, 0, 0 } });
            print(a);


        }

        [TestMethod]
        public void test_diag_indices_1_OBJECT_TODO()
        {
            var di = np.diag_indices(4);
            AssertArray(di[0], new Int32[] { 0, 1, 2, 3 });
            AssertArray(di[1], new Int32[] { 0, 1, 2, 3 });
            print(di);

            var a = np.arange(16, dtype: np.BigInt).reshape((4, 4));
            a[di] = 100;

            AssertArray(a, new BigInteger[,] { { 100, 1, 2, 3 }, { 4, 100, 6, 7 }, { 8, 9, 100, 11 }, { 12, 13, 14, 100 } });
            print(a);

            return;

        }

        [TestMethod]
        public void test_diag_indices_from_1_OBJECT_TODO()
        {
            var a = np.arange(16, dtype: np.BigInt).reshape((4, 4));
            var di = np.diag_indices_from(a);
            AssertArray(di[0], new Int32[] { 0, 1, 2, 3 });
            AssertArray(di[1], new Int32[] { 0, 1, 2, 3 });
            print(di);
        }

        #endregion

        #region from StrideTricksTests

        [TestMethod]
        public void test_broadcast_1_OBJECT_TODO()
        {
            var x = np.array(new BigInteger[,] { { 11 }, { 2 }, { 3 } });
            var y = np.array(new BigInteger[] { 4, 5, 6 });
            var b = np.broadcast(x, y);
            Assert.AreEqual(b.shape.iDims.Length, 2);
            Assert.AreEqual(b.shape.iDims[0], 3);
            Assert.AreEqual(b.shape.iDims[1], 3);
            print(b.shape);

            Assert.AreEqual(b.index, 0);
            print(b.index);

            foreach (var uv in b)
            {
                print(uv);
            }
            Assert.AreEqual(b.index, 9);
            print(b.index);

        }

        [TestMethod]
        public void test_broadcast_to_1_OBJECT_TODO()
        {
            var a = np.broadcast_to((BigInteger)5, (4, 4));
            AssertArray(a, new BigInteger[,] { { 5, 5, 5, 5 }, { 5, 5, 5, 5 }, { 5, 5, 5, 5 }, { 5, 5, 5, 5 } });
            AssertStrides(a, 0, 0);
            print(a);
            print(a.shape);
            print(a.strides);
            print("*************");


            var b = np.broadcast_to(new BigInteger[] { 1, 2, 3 }, (3, 3));
            AssertArray(b, new BigInteger[,] { { 1, 2, 3 }, { 1, 2, 3 }, { 1, 2, 3 } });
            AssertStrides(b, 0, SizeOfObject);
            print(b);
            print(b.shape);
            print(b.strides);
            print("*************");


        }

        [TestMethod]
        public void test_broadcast_arrays_1_OBJECT_TODO()
        {
            var x = np.array(new BigInteger[,] { { 1, 2, 3 } });
            var y = np.array(new BigInteger[,] { { 4 }, { 5 } });
            var z = np.broadcast_arrays(false, new ndarray[] { x, y });

            print(z);

        }

        [TestMethod]
        public void test_as_strided_1_OBJECT_TODO()
        {
            var y = np.zeros((10, 10), np.BigInt);
            AssertStrides(y, SizeOfObject * 10, SizeOfObject * 1);
            print(y.strides);

            var n = 1000;
            var a = np.arange(n, dtype: np.BigInt);

            var b = np.as_strided(a, (n, n), (0, 8));

            //print(b);

            Assert.AreEqual(1000000, b.size);
            print(b.size);
            AssertShape(b, 1000, 1000);
            print(b.shape);
            AssertStrides(b, 0, 8);
            print(b.strides);
            Assert.AreEqual(32000000, b.nbytes);
            print(b.nbytes);

        }

        #endregion

        #region from IteratorTests

        [TestMethod]
        public void test_nditer_1_OBJECT_TODO()
        {
            var a = np.arange(0.1, 6.1, dtype: np.BigInt).reshape((2, 3));
            var b = np.array(new BigInteger[] { 7, 8, 9 });

            foreach (var aa in new nditer(a))
            {
                print(aa);
            }

            foreach (var aa in new nditer((a, b)))
            {
                print(aa);
            }

            foreach (var aa in new nditer((a, b, a, b)))
            {
                print(aa);
            }

        }

        [TestMethod]
        public void test_ndindex_1_OBJECT_TODO()
        {
            var a = np.arange(0.1, 6.1, dtype: np.BigInt).reshape((2, 3));  // force numpy to be initialized

            foreach (var aa in new ndindex((2, 3)))
            {
                print(aa);
            }


            foreach (var aa in new ndindex((2, 3, 2)))
            {
                print(aa);
            }

            foreach (var aa in new ndindex((3)))
            {
                print(aa);
            }

        }

        [TestMethod]
        public void test_ndenumerate_1_OBJECT_TODO()
        {
            var a = np.arange(0.1, 6.1, dtype: np.BigInt).reshape((2, 3));

            foreach (ValueTuple<long[], object> aa in new ndenumerate(a))
            {
                print(aa.Item1);
                print(aa.Item2);
            }
        }

        #endregion

        #region very cool object unique tests

        // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading

        // build a class that supports these overloads and see how the default object class dynamic keyword
        // handles them.  It may not be necessary to have user defined types.


        [TestMethod]
        public void test_add_operations_OBJECT()
        {
            var a = np.arange(0, 20, 1, dtype: np.Int32);
            a = a.astype(np.Object);

            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            print(a);

            var b = a + 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB = new object[,]
            {{8,  9, 10, 11},
             {12, 13, 14, 15},
             {16, 17, 18, 19},
             {20, 21, 22, 23},
             {24, 25, 26, 27}
            };
            AssertArray(b, ExpectedDataB);

            a = np.arange(0, 20, 1, dtype: np.Int32).astype(np.Object);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a + 2400;
            print(b);
            print(b.shape);
            print(b.strides);

            ExpectedDataB = new object[,]
            {{2400, 2401, 2402, 2403},
             {2404, 2405, 2406, 2407},
             {2408, 2409, 2410, 2411},
             {2412, 2413, 2414, 2415},
             {2416, 2417, 2418, 2419}
            };
            AssertArray(b, ExpectedDataB);

        }

        [TestMethod]
        public void test_add_operations_OBJECT_TODO_2()
        {
            var a = np.arange(0, 20, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);

            var ExpectedDataA = new BigInteger[,]
                {{0,  1,  2,  3},
                 {4,  5,  6,  7},
                 {8,  9, 10, 11},
                 {12, 13, 14, 15},
                 {16, 17, 18, 19}};
            AssertArray(a, ExpectedDataA);

            var b = np.array(new BigInteger[] { 2 });
            var c = a + b;
            print(c);

            var ExpectedDataC = new BigInteger[,]
                {{2,  3,  4,  5},
                 {6,  7,  8,  9},
                 {10, 11, 12, 13},
                 {14, 15, 16, 17},
                 {18, 19, 20, 21}};
            AssertArray(c, ExpectedDataC);


            b = np.array(new BigInteger[] { 10, 20, 30, 40 });
            var d = a + b;
            print(d);

            var ExpectedDataD = new BigInteger[,]
                {{10, 21, 32, 43},
                 {14, 25, 36, 47},
                 {18, 29, 40, 51},
                 {22, 33, 44, 55},
                 {26, 37, 48, 59}};
            AssertArray(d, ExpectedDataD);
        }

        [TestMethod]
        public void test_subtract_operations_OBJECT_TODO()
        {
            var a = np.arange(0, 20, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            var b = a - 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB = new BigInteger[,]
            {{-8, -7, -6, -5},
             {-4, -3, -2, -1},
             {0,  1,  2,  3},
             {4,  5,  6,  7},
             {8,  9, 10, 11}
            };
            AssertArray(b, ExpectedDataB);

            a = np.arange(0, 20, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a - 2400;
            print(b);
            print(b.shape);
            print(b.strides);

            ExpectedDataB = new BigInteger[,]
            {{-2400, -2399, -2398, -2397},
             {-2396, -2395, -2394, -2393},
             {-2392, -2391, -2390, -2389},
             {-2388, -2387, -2386, -2385},
             {-2384, -2383, -2382, -2381}
            };

            AssertArray(b, ExpectedDataB);

        }

        [TestMethod]
        public void test_subtract_operations_OBJECT_TODO_2()
        {
            var a = np.arange(100, 102, 1, dtype: np.BigInt);
            var b = np.array(new BigInteger[] { 1, 63 });
            var c = a - b;
            print(a);
            print("****");
            print(b);
            print("****");
            print(c);
            print("****");
            AssertArray(c, new BigInteger[] { 99, 38 });


            a = np.arange(0, 4, 1, dtype: np.BigInt).reshape(new shape(2, 2));
            b = np.array(new BigInteger[] { 65, 78 }).reshape(new shape(1, 2));
            c = a - b;
            print(a);
            print("****");
            print(b);
            print("****");
            print(c);
            print("****");
            AssertArray(c, new BigInteger[,] { { -65, -77 }, { -63, -75 } });

        }

        [TestMethod]
        public void test_multiply_operations_OBJECT_TODO()
        {
            var a = np.arange(0, 20, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            BigInteger multiplierB1 = 9023;
            var b = a * multiplierB1;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB1 = new BigInteger[,]
            {
                {0*multiplierB1,  1*multiplierB1,  2*multiplierB1,  3*multiplierB1},
                {4*multiplierB1,  5*multiplierB1,  6*multiplierB1,  7*multiplierB1},
                {8*multiplierB1,  9*multiplierB1,  10*multiplierB1, 11*multiplierB1},
                {12*multiplierB1, 13*multiplierB1, 14*multiplierB1, 15*multiplierB1},
                {16*multiplierB1, 17*multiplierB1, 18*multiplierB1, 19*multiplierB1}
            };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(0, 20, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            BigInteger multiplierB2 = 990425023;
            b = a * multiplierB2;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB2 = new BigInteger[,]
            {
                {0*multiplierB2,  1*multiplierB2,  2*multiplierB2,  3*multiplierB2},
                {4*multiplierB2,  5*multiplierB2,  6*multiplierB2,  7*multiplierB2},
                {8*multiplierB2,  9*multiplierB2,  10*multiplierB2, 11*multiplierB2},
                {12*multiplierB2, 13*multiplierB2, 14*multiplierB2, 15*multiplierB2},
                {16*multiplierB2, 17*multiplierB2, 18*multiplierB2, 19*multiplierB2}
            };
            AssertArray(b, ExpectedDataB2);
        }

        [TestMethod]
        public void test_division_operations_OBJECT_TODO()
        {
            var a = np.arange(20000, 20020, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            BigInteger divisorB1 = 611;
            var b = a / divisorB1;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB1 = new BigInteger[,]
            {
                {20000/divisorB1, 20001/divisorB1, 20002/divisorB1, 20003/divisorB1},
                {20004/divisorB1, 20005/divisorB1, 20006/divisorB1, 20007/divisorB1},
                {20008/divisorB1, 20009/divisorB1, 20010/divisorB1, 20011/divisorB1},
                {20012/divisorB1, 20013/divisorB1, 20014/divisorB1, 20015/divisorB1},
                {20016/divisorB1, 20017/divisorB1, 20018/divisorB1, 20019/divisorB1}
            };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2000000, 2000020, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            BigInteger divisorB2 = 2411;
            b = a / divisorB2;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB2 = new BigInteger[,]
            {
                {2000000/divisorB2, 2000001/divisorB2, 2000002/divisorB2, 2000003/divisorB2},
                {2000004/divisorB2, 2000005/divisorB2, 2000006/divisorB2, 2000007/divisorB2},
                {2000008/divisorB2, 2000009/divisorB2, 2000010/divisorB2, 2000011/divisorB2},
                {2000012/divisorB2, 2000013/divisorB2, 2000014/divisorB2, 2000015/divisorB2},
                {2000016/divisorB2, 2000017/divisorB2, 2000018/divisorB2, 2000019/divisorB2},
            };
            AssertArray(b, ExpectedDataB2);
        }

        [TestMethod]
        public void test_leftshift_operations_OBJECT_TODO()
        {
            var a = np.arange(0, 20, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            var b = a << 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB1 = new BigInteger[,]
            {
                {0,  256,  512,  768},
                {1024, 1280, 1536, 1792},
                {2048, 2304, 2560, 2816},
                {3072, 3328, 3584, 3840},
                {4096, 4352, 4608, 4864}
            };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(0, 20, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a << 24;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB2 = new BigInteger[,]
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
        public void test_rightshift_operations_OBJECT_TODO()
        {
            var a = np.arange(20000, 20020, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            var b = a >> 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB1 = new BigInteger[,]
            {
                {78, 78, 78, 78},
                {78, 78, 78, 78},
                {78, 78, 78, 78},
                {78, 78, 78, 78},
                {78, 78, 78, 78}
            };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2123450, 2123470, 1, dtype: np.BigInt);
            a = a.reshape(new shape(5, -1));
            print(a);
            print(a.shape);
            print(a.strides);

            b = a >> 8;
            print(b);
            print(b.shape);
            print(b.strides);

            var ExpectedDataB2 = new BigInteger[,]
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
        public void test_bitwiseand_operations_OBJECT_TODO()
        {
            var a = np.arange(0.499, 32.499, 1, dtype: np.BigInt);
            print(a);

            var b = a & 0x0f;
            print(b);

            var ExpectedDataB1 = new BigInteger[]
            { 0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15,
              0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15 };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2048, 2048 + 32, 1, dtype: np.BigInt);
            print(a);

            b = a & 0xFF;
            print(b);

            var ExpectedDataB2 = new BigInteger[]
            { 0,  1,  2,  3,  4,  5,  6,  7,  8,  9, 10, 11, 12, 13, 14, 15,
              16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_bitwiseor_operations_OBJECT_TODO()
        {
            var a = np.arange(0.499, 32.499, 1, dtype: np.BigInt);
            print(a);

            var b = a | 0x100;
            print(b);

            var ExpectedDataB1 = new BigInteger[]
            { 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271,
              272, 273, 274, 275, 276, 277, 278, 279, 280, 281, 282, 283, 284, 285, 286, 287 };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2048, 2048 + 32, 1, dtype: np.BigInt);
            print(a);

            b = a | 0x1000;
            print(b);

            var ExpectedDataB2 = new BigInteger[]
            { 6144, 6145, 6146, 6147, 6148, 6149, 6150, 6151, 6152, 6153, 6154, 6155, 6156, 6157,
              6158, 6159, 6160, 6161, 6162, 6163, 6164, 6165, 6166, 6167, 6168, 6169, 6170, 6171,
              6172, 6173, 6174, 6175 };
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_bitwisexor_operations_OBJECT_TODO()
        {
            var a = np.arange(0.499, 32.499, 1, dtype: np.BigInt);
            print(a);

            var b = a ^ 0xAAA;
            print(b);

            var ExpectedDataB1 = new BigInteger[]
            { 2730, 2731, 2728, 2729, 2734, 2735, 2732, 2733, 2722, 2723, 2720, 2721, 2726, 2727, 2724,
              2725, 2746, 2747, 2744, 2745, 2750, 2751, 2748, 2749, 2738, 2739, 2736, 2737, 2742, 2743, 2740, 2741 };
            AssertArray(b, ExpectedDataB1);

            a = np.arange(2048, 2048 + 32, 1, dtype: np.BigInt);
            print(a);

            b = a ^ 0xAAAA;
            print(b);

            var ExpectedDataB2 = new BigInteger[]
            { 41642, 41643, 41640, 41641, 41646, 41647, 41644, 41645, 41634, 41635, 41632, 41633,
              41638, 41639, 41636, 41637, 41658, 41659, 41656, 41657, 41662, 41663, 41660, 41661,
              41650, 41651, 41648, 41649, 41654, 41655, 41652, 41653};
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_remainder_operations_OBJECT_TODO()
        {
            var a = np.arange(0, 32, 1, dtype: np.BigInt);
            print(a);

            var b = a % 6;
            print(b);

            AssertArray(b, new BigInteger[] { 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3,
                                         4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1 });

            a = np.arange(2048, 2048 + 32, 1, dtype: np.BigInt);
            print(a);

            b = a % 6;
            print(b);

            AssertArray(b, new BigInteger[] { 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5,
                                         0, 1, 2, 3, 4, 5, 0, 1, 2, 3, 4, 5, 0, 1, 2, 3 });

        }

        [TestMethod]
        public void test_negative_operations_OBJECT_TODO()
        {
            var a = np.arange(0, 12, 1, dtype: np.BigInt);
            print(a);

            var b = -a;
            print(b);

            var ExpectedDataB2 = new BigInteger[]
            {
                 0, -1, -2, -3, -4, -5, -6, -7, -8, -9, -10, -11
            };
            AssertArray(b, ExpectedDataB2);

        }

        [TestMethod]
        public void test_invert_operations_OBJECT_TODO()
        {
            var a = np.arange(-32, 32, 1, dtype: np.BigInt);
            print(a);

            var b = ~a;
            print(b);

            // this should not be changed at all.  BigInts can't be inverted.
            AssertArray(b, a.AsBigIntArray());

        }

        [TestMethod]
        public void test_LESS_operations_OBJECT_TODO()
        {
            var a = np.arange(-5, 5, 1, dtype: np.BigInt);
            print(a);

            var b = a < -2;
            print(b);

            AssertArray(b, new Boolean[] { true, true, true, false, false, false, false, false, false, false });
        }

        [TestMethod]
        public void test_LESSEQUAL_operations_OBJECT_TODO()
        {
            var a = np.arange(-5, 5, 1, dtype: np.BigInt);
            print(a);

            var b = a <= -2;
            print(b);

            AssertArray(b, new Boolean[] { true, true, true, true, false, false, false, false, false, false });
        }

        [TestMethod]
        public void test_EQUAL_operations_OBJECT_TODO()
        {
            var a = np.arange(-5, 5, 1, dtype: np.BigInt);
            print(a);

            var b = a == -2;
            print(b);

            AssertArray(b, new Boolean[] { false, false, false, true, false, false, false, false, false, false });
        }

        [TestMethod]
        public void test_NOTEQUAL_operations_OBJECT_TODO()
        {
            var a = np.arange(-5, 5, 1, dtype: np.BigInt);
            print(a);

            var b = a != -2;
            print(b);

            AssertArray(b, new Boolean[] { true, true, true, false, true, true, true, true, true, true });
        }

        [TestMethod]
        public void test_GREATER_operations_OBJECT_TODO()
        {
            var a = np.arange(-5, 5, 1, dtype: np.BigInt);
            print(a);

            var b = a > -2;
            print(b);

            AssertArray(b, new Boolean[] { false, false, false, false, true, true, true, true, true, true });

        }

        [TestMethod]
        public void test_GREATEREQUAL_operations_OBJECT_TODO()
        {
            var a = np.arange(-5, 5, 1, dtype: np.BigInt);
            print(a);

            var b = a >= -2;
            print(b);

            AssertArray(b, new Boolean[] { false, false, false, true, true, true, true, true, true, true });

        }

        [TestMethod]
        public void test_arrayarray_or_OBJECT_TODO()
        {
            var a = np.arange(0, 32, 1, dtype: np.BigInt);
            var b = np.arange(33, 33 + 32, 1, dtype: np.BigInt);
            var c = a | b;
            print(a);
            print(b);
            print(c);

            AssertArray(c, new BigInteger[] {33, 35, 35, 39, 37, 39, 39, 47, 41, 43, 43, 47,
                                        45, 47, 47, 63, 49, 51, 51, 55, 53, 55, 55, 63,
                                        57, 59, 59, 63, 61, 63, 63, 95 });
        }

        [TestMethod]
        public void test_bitwise_and_OBJECT_TODO()
        {
            var x = np.arange(1023, 1039, 1, dtype: np.BigInt).reshape(new shape(2, -1));
            var y = np.bitwise_and(x, 0x3FF);
            var z = x & 0x3FF;

            print(x);
            print(y);
            print(z);

            var ExpectedData = new BigInteger[,]
            {
                { 1023, 0, 1,  2,  3,  4,  5,  6 },
                {  7, 8, 9, 10, 11, 12, 13, 14 }
            };

            AssertArray(y, ExpectedData);
            AssertArray(z, ExpectedData);
        }

        [TestMethod]
        public void test_bitwise_or_OBJECT_TODO()
        {
            var x = np.arange(1023, 1039, 1, dtype: np.BigInt).reshape(new shape(2, -1));
            var y = np.bitwise_or(x, 0x10);
            var z = x | 0x10;

            print(x);
            print(y);
            print(z);

            var ExpectedData = new BigInteger[,]
            {
                { 1023, 1040, 1041, 1042, 1043, 1044, 1045, 1046 },
                { 1047, 1048, 1049, 1050, 1051, 1052, 1053, 1054 }
            };

            AssertArray(y, ExpectedData);
            AssertArray(z, ExpectedData);
        }

        [TestMethod]
        public void test_bitwise_xor_OBJECT_TODO()
        {
            var a = np.bitwise_xor(13, 17);
            Assert.AreEqual(28, a.GetItem(0));
            print(a);

            var b = np.bitwise_xor(31, 5);
            Assert.AreEqual(26, b.GetItem(0));
            print(b);

            var c = np.bitwise_xor(new BigInteger[] { 31, 3 }, 5);
            AssertArray(c, new BigInteger[] { 26, 6 });
            print(c);

            var d = np.bitwise_xor(new BigInteger[] { 31, 3 }, new BigInteger[] { 5, 6 });
            AssertArray(d, new BigInteger[] { 26, 5 });
            print(d);

            var e = np.bitwise_xor(new bool[] { true, true }, new bool[] { false, true });
            AssertArray(e, new bool[] { true, false });
            print(e);

            return;
        }

        [TestMethod]
        public void test_bitwise_not_OBJECT_TODO()
        {
            var a = np.bitwise_not(13);
            Assert.AreEqual(-14, a.GetItem(0));
            print(a);

            var b = np.bitwise_not(31);
            Assert.AreEqual(-32, b.GetItem(0));
            print(b);

            // can't inverse a BigInteger
            var c = np.bitwise_not(new BigInteger[] { 31, 3 });
            AssertArray(c, new BigInteger[] { 31, 3 });
            print(c);

            // can't inverse a BigInteger
            var d = np.bitwise_not(new BigInteger[] { 31, 3 });
            AssertArray(d, new BigInteger[] { 31, 3 });
            print(d);

            var e = np.bitwise_not(new bool[] { true, false });
            AssertArray(e, new bool[] { false, true });
            print(e);

            return;
        }

        [TestMethod]
        public void test_invert_OBJECT_TODO()
        {
            var a = np.invert(13);
            Assert.AreEqual(-14, a.GetItem(0));
            print(a);

            var b = np.invert(31);
            Assert.AreEqual(-32, b.GetItem(0));
            print(b);

            // can't inverse a BigInteger
            var c = np.invert(new BigInteger[] { 31, 3 });
            AssertArray(c, new BigInteger[] { 31, 3 });
            print(c);

            // can't inverse a BigInteger
            var d = np.invert(new BigInteger[] { 31, 3 });
            AssertArray(d, new BigInteger[] { 31, 3 });
            print(d);

            var e = np.invert(new bool[] { true, false });
            AssertArray(e, new bool[] { false, true });
            print(e);

            return;
        }

        [TestMethod]
        public void test_right_shift_OBJECT_TODO()
        {
            var x = np.arange(1023, 1039, 1, dtype: np.BigInt).reshape(new shape(2, -1));
            var y = np.right_shift(x, 2);
            var z = x >> 2;

            print(x);
            print(y);
            print(z);

            var ExpectedData = new BigInteger[,]
            {
                { 255, 256, 256, 256, 256, 257, 257, 257 },
                { 257, 258, 258, 258, 258, 259, 259, 259 }
            };

            AssertArray(y, ExpectedData);
            AssertArray(z, ExpectedData);
        }

        [TestMethod]
        public void test_left_shift_OBJECT_TODO()
        {
            var x = np.arange(1023, 1039, 1, dtype: np.BigInt).reshape(new shape(2, -1));
            var y = np.left_shift(x, 2);
            var z = x << 2;

            print(x);
            print(y);
            print(z);

            var ExpectedData = new BigInteger[,]
            {
                { 4092, 4096, 4100, 4104, 4108, 4112, 4116, 4120 },
                { 4124, 4128, 4132, 4136, 4140, 4144, 4148, 4152 }
            };

            AssertArray(y, ExpectedData);
            AssertArray(z, ExpectedData);
        }


        [TestMethod]
        public void test_min_OBJECT_TODO()
        {
            // would have to rewrite object implementation of Minimum to use <= >= etc.

            BigInteger[] TestData = new BigInteger[] { 25, -17, -15, -02, 02, 15, 17, 20 };
            var x = np.array(TestData);
            BigInteger y = (BigInteger)np.min(x);

            print(x);
            print(y);

            Assert.AreEqual((BigInteger)(-17), y);
        }

        [TestMethod]
        public void test_max_OBJECT_TODO()
        {
            // would have to rewrite object implementation of Minimum to use <= >= etc.

            BigInteger[] TestData = new BigInteger[] { 25, -17, -15, -02, 02, 15, 17, 20 };
            var x = np.array(TestData);
            BigInteger y = (BigInteger)np.max(x);

            print(x);
            print(y);

            Assert.AreEqual((BigInteger)25, y);
        }


        [TestMethod]
        public void test_setdiff1d_OBJECT_TODO()
        {
            // setdiff1d seems to have support for objects.  Let's see if we can make it work
            BigInteger[] TestDataA = new BigInteger[] { 1, 2, 3, 2, 4, };
            BigInteger[] TestDataB = new BigInteger[] { 3, 4, 5, 6 };

            var a = np.array(TestDataA);
            var b = np.array(TestDataB);
            ndarray c = np.setdiff1d(a, b);

            print(a);
            print(b);
            print(c);

            AssertArray(c, new BigInteger[] { 1, 2 });

        }

        [TestMethod]
        public void test_setdiff1d_2_OBJECT_TODO()
        {
            // setdiff1d seems to have support for objects.  Let's see if we can make it work

            BigInteger[] TestDataB = new BigInteger[] { 3, 4, 5, 6 };

            var a = np.arange(1, 39, dtype: np.BigInt).reshape(new shape(2, -1));
            var b = np.array(TestDataB);
            ndarray c = np.setdiff1d(a, b);

            print(a);
            print(b);
            print(c);

            AssertArray(c, new BigInteger[] {1,  2,  7,  8,  9,  10, 11, 12, 13, 14, 15, 16,
                                         17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28,
                                         29, 30, 31, 32, 33, 34, 35, 36, 37, 38 });

        }

        [TestMethod]
        public void test_logical_and_1_OBJECT_TODO()
        {

            var x = np.arange(5, dtype: np.BigInt);
            var c = np.logical_and(x > 1, x < 4);
            AssertArray(c, new bool[] { false, false, true, true, false });
            print(c);

            var y = np.arange(6, dtype: np.BigInt).reshape((2, 3));
            var d = np.logical_and(y > 1, y < 4);
            AssertArray(d, new bool[,] { { false, false, true }, { true, false, false } });
            print(d);
        }

        [TestMethod]
        public void test_logical_or_1_OBJECT_TODO()
        {

            var x = np.arange(5, dtype: np.BigInt);
            var c = np.logical_or(x < 1, x > 3);
            AssertArray(c, new bool[] { true, false, false, false, true });
            print(c);

            var y = np.arange(6, dtype: np.BigInt).reshape((2, 3));
            var d = np.logical_or(y < 1, y > 3);
            AssertArray(d, new bool[,] { { true, false, false }, { false, true, true } });
            print(d);
        }

        [TestMethod]
        public void test_logical_xor_1_OBJECT_TODO()
        {

            var x = np.arange(5, dtype: np.BigInt);
            var c = np.logical_xor(x < 1, x > 3);
            AssertArray(c, new bool[] { true, false, false, false, true });
            print(c);

            var y = np.arange(6, dtype: np.BigInt).reshape((2, 3));
            var d = np.logical_xor(y < 1, y > 3);
            AssertArray(d, new bool[,] { { true, false, false }, { false, true, true } });
            print(d);

            var e = np.logical_xor((BigInteger)0, np.eye(2, dtype: np.BigInt));
            AssertArray(e, new bool[,] { { true, false }, { false, true } });
        }

        [TestMethod]
        public void test_logical_not_1_OBJECT_TODO()
        {
            var x = np.arange(5, dtype: np.BigInt);
            var c = np.logical_not(x < 3);
            AssertArray(c, new bool[] { false, false, false, true, true });
            print(c);
        }

        [TestMethod]
        public void test_greater_1_OBJECT_TODO()
        {
            var a = np.greater(new BigInteger[] { 4, 2, 1 }, new BigInteger[] { 2, 2, 2 });
            AssertArray(a, new bool[] { true, false, false });
            print(a);

            var b = np.greater(new BigInteger[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { true, true, false });
            print(b);

            var c = np.greater((BigInteger)2, new BigInteger[] { 4, 2, 1 });
            AssertArray(c, new bool[] { false, false, true });
            print(c);

        }

        [TestMethod]
        public void test_greater_equal_1_OBJECT_TODO()
        {
            var a = np.greater_equal(new BigInteger[] { 4, 2, 1 }, new BigInteger[] { 2, 2, 2 });
            AssertArray(a, new bool[] { true, true, false });
            print(a);

            var b = np.greater_equal(new BigInteger[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { true, true, true });
            print(b);

            var c = np.greater_equal((BigInteger)2, new BigInteger[] { 4, 2, 1 });
            AssertArray(c, new bool[] { false, true, true });
            print(c);
        }

        [TestMethod]
        public void test_less_1_OBJECT_TODO()
        {
            var a = np.less(new BigInteger[] { 4, 2, 1 }, new BigInteger[] { 2, 2, 2 });
            AssertArray(a, new bool[] { false, false, true });
            print(a);

            var b = np.less(new BigInteger[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { false, false, false });
            print(b);

            var c = np.less((BigInteger)2, new BigInteger[] { 4, 2, 1 });
            AssertArray(c, new bool[] { true, false, false });
            print(c);
        }

        [TestMethod]
        public void test_less_equal_1_OBJECT_TODO()
        {
            var a = np.less_equal(new BigInteger[] { 4, 2, 1 }, new BigInteger[] { 2, 2, 2 });
            AssertArray(a, new bool[] { false, true, true });
            print(a);

            var b = np.less_equal(new BigInteger[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { false, false, true });
            print(b);

            var c = np.less_equal((BigInteger)2, new BigInteger[] { 4, 2, 1 });
            AssertArray(c, new bool[] { true, true, false });
            print(c);
        }

        [TestMethod]
        public void test_equal_1_OBJECT_TODO()
        {
            var a = np.equal(new BigInteger[] { 4, 2, 1 }, new BigInteger[] { 2, 2, 2 });
            AssertArray(a, new bool[] { false, true, false });
            print(a);

            var b = np.equal(new BigInteger[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { false, false, true });
            print(b);

            var c = np.equal((BigInteger)2, new BigInteger[] { 4, 2, 1 });
            AssertArray(c, new bool[] { false, true, false });
            print(c);
        }

        [TestMethod]
        public void test_not_equal_1_OBJECT_TODO()
        {
            var a = np.not_equal(new BigInteger[] { 4, 2, 1 }, new BigInteger[] { 2, 2, 2 });
            AssertArray(a, new bool[] { true, false, true });
            print(a);

            var b = np.not_equal(new BigInteger[] { 4, 2, 1 }, 1);
            AssertArray(b, new bool[] { true, true, false });
            print(b);

            var c = np.not_equal((BigInteger)2, new BigInteger[] { 4, 2, 1 });
            AssertArray(c, new bool[] { true, false, true });
            print(c);
        }

        #endregion

    }
}
