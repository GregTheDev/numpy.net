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
    [TestClass]
    public class IteratorTests : TestBaseClass
    {
        [TestMethod]
        public void test_nditer_1()
        {
            var a = np.arange(0, 6).reshape((2, 3));
            var b = np.array(new int[] { 7, 8, 9 });

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
        public void test_ndindex_1()
        {
            var a = np.arange(0, 6).reshape((2, 3));  // force numpy to be initialized

            foreach (var aa in new ndindex((2,3)))
            {
                print(aa);
            }


            foreach (var aa in new ndindex((2, 3, 2)))
            {
                print(aa);
            }

        }

    }
}
