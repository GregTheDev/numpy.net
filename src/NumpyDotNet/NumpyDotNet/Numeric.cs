﻿/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2018-2019
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

using NumpyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
#if NPY_INTP_64
using npy_intp = System.Int64;
using npy_ucs4 = System.Int64;
#else
using npy_intp = System.Int32;
using npy_ucs4 = System.Int32;
#endif

namespace NumpyDotNet
{
    public static partial class np
    {
        #region zeros
        /// <summary>
        /// Return a new array of given shape and type, filled with zeros
        /// </summary>
        /// <param name="shape">int or sequence of ints, Shape of the new array</param>
        /// <param name="dtype">(optional) Desired output data-type</param>
        /// <param name="order">(optional) {‘C’, ‘F’}, Whether to store multi-dimensional data in row-major (C-style) or column-major (Fortran-style) order in memory.</param>
        /// <returns>Array of zeros with the given shape, dtype, and order.</returns>
        public static ndarray zeros(object shape, dtype dtype = null, order order = order.DEFAULT)
        {
            if (shape == null)
            {
                throw new Exception("shape can't be null");
            }

            double FillValue = 0;

            return CommonFill(dtype, shape, FillValue, CheckOnlyCorF(order), false, 0);
        }
        #endregion

        #region zeros_like
        /// <summary>
        /// Return an array of zeros with the same shape and type as a given array.
        /// </summary>
        /// <param name="src">The shape and data-type of a define these same attributes of the returned array.</param>
        /// <param name="dtype">(optional) Overrides the data type of the result</param>
        /// <param name="order">(optional) {‘C’, ‘F’, ‘A’, or ‘K’}, Overrides the memory layout of the result. ‘C’ means C-order, ‘F’ means F-order, ‘A’ means ‘F’ if a is Fortran contiguous, ‘C’ otherwise. ‘K’ means match the layout of a as closely as possible.</param>
        /// <param name="subok">(optional) If True, then the newly created array will use the sub-class type of ‘a’, otherwise it will be a base-class array. Defaults to True.</param>
        /// <returns>Array of zeros with the same shape and type as a.</returns>
        public static ndarray zeros_like(object osrc, dtype dtype = null, order order = order.DEFAULT, bool subok = true)
        {
            if (osrc == null)
            {
                throw new Exception("array can't be null");
            }

            var src = asanyarray(osrc);
            shape shape = new shape(src.Array.dimensions, src.Array.nd);
            double FillValue = 0;

            return CommonFill(dtype, shape, FillValue, ConvertOrder(src, order), subok, 0);
        }
        #endregion

        #region ones
        /// <summary>
        /// Return a new array of given shape and type, filled with ones
        /// </summary>
        /// <param name="shape">int or sequence of ints, Shape of the new array</param>
        /// <param name="dtype">(optional) Desired output data-type</param>
        /// <param name="order">(optional) {‘C’, ‘F’}, Whether to store multi-dimensional data in row-major (C-style) or column-major (Fortran-style) order in memory.</param>
        /// <returns>Array of ones with the given shape, dtype, and order.</returns>
        public static ndarray ones(object shape, dtype dtype = null, order order = order.DEFAULT)
        {
            if (shape == null)
            {
                throw new Exception("shape can't be null");
            }

            double FillValue = 1;

            return CommonFill(dtype, shape, FillValue, CheckOnlyCorF(order), false, 0);
        }
        #endregion

        #region ones_like
        /// <summary>
        /// Return an array of ones with the same shape and type as a given array.
        /// </summary>
        /// <param name="src">The shape and data-type of a define these same attributes of the returned array.</param>
        /// <param name="dtype">(optional) Overrides the data type of the result</param>
        /// <param name="order">(optional) {‘C’, ‘F’, ‘A’, or ‘K’}, Overrides the memory layout of the result. ‘C’ means C-order, ‘F’ means F-order, ‘A’ means ‘F’ if src is Fortran contiguous, ‘C’ otherwise. ‘K’ means match the layout of a as closely as possible.</param>
        /// <param name="subok">(optional) If True, then the newly created array will use the sub-class type of ‘a’, otherwise it will be a base-class array. Defaults to True.</param>
        /// <returns>Array of ones with the same shape and type as a.</returns>
        public static ndarray ones_like(object osrc, dtype dtype = null, order order = order.DEFAULT, bool subok = true)
        {
            if (osrc == null)
            {
                throw new Exception("array can't be null");
            }

            var src = asanyarray(osrc);

            shape shape = new shape(src.Array.dimensions, src.Array.nd);
            double FillValue = 1;

            return CommonFill(dtype, shape, FillValue, ConvertOrder(src, order), subok, 0);
        }
        #endregion

        #region empty
        /// <summary>
        /// Return a new array of given shape and type, without initializing entries
        /// </summary>
        /// <param name="shape">int or tuple of int, Shape of the empty array</param>
        /// <param name="dtype">(optional) Desired output data-type</param>
        /// <param name="order">(optional) {‘C’, ‘F’}, Whether to store multi-dimensional data in row-major (C-style) or column-major (Fortran-style) order in memory.</param>
        /// <returns>Array of uninitialized (arbitrary) data of the given shape, dtype, and order. Object arrays will be initialized to None.</returns>
        public static ndarray empty(object shape, dtype dtype = null, order order = order.DEFAULT)
        {
            return zeros(shape, dtype, order);
        }
        #endregion

        #region empty_like
        /// <summary>
        /// Return a new array with the same shape and type as a given array.
        /// </summary>
        /// <param name="src">The shape and data-type of a define these same attributes of the returned array.</param>
        /// <param name="dtype">(optional) Overrides the data type of the result</param>
        /// <param name="order">(optional) {‘C’, ‘F’, ‘A’, or ‘K’}, Overrides the memory layout of the result. ‘C’ means C-order, ‘F’ means F-order, ‘A’ means ‘F’ if a is Fortran contiguous, ‘C’ otherwise. ‘K’ means match the layout of a as closely as possible.</param>
        /// <param name="subok">(optional) If True, then the newly created array will use the sub-class type of ‘a’, otherwise it will be a base-class array. Defaults to True.</param>
        /// <returns>Array of uninitialized (arbitrary) data with the same shape and type as a.</returns>
        public static ndarray empty_like(object src, dtype dtype = null, order order = order.DEFAULT, bool subok = true)
        {
            return zeros_like(src, dtype, order, subok);
        }
        #endregion

        #region full
        /// <summary>
        /// Return a new array of given shape and type, filled with fill_value
        /// </summary>
        /// <param name="shape">int or sequence of ints, Shape of the new array</param>
        /// <param name="fill_value">Fill value.  Must be scalar type</param>
        /// <param name="dtype">(optional) Desired output data-type</param>
        /// <param name="order">(optional) {‘C’, ‘F’}, Whether to store multi-dimensional data in row-major (C-style) or column-major (Fortran-style) order in memory.</param>
        /// <returns>Array of fill_value with the given shape, dtype, and order.</returns>
        public static ndarray full(object shape, object fill_value, dtype dtype = null, order order = order.DEFAULT)
        {
            if (shape == null)
            {
                throw new Exception("shape can't be null");
            }

            return CommonFill(dtype, shape, fill_value, CheckOnlyCorF(order), false, 0);
        }
        #endregion

        #region full_like
        /// <summary>
        /// Return an array of zeros with the same shape and type as a given array.
        /// </summary>
        /// <param name="src">The shape and data-type of a define these same attributes of the returned array.</param>
        /// <param name="fill_value">Fill value.  Must be scalar type</param>
        /// <param name="dtype">(optional) Overrides the data type of the result</param>
        /// <param name="order">(optional) {‘C’, ‘F’, ‘A’, or ‘K’}, Overrides the memory layout of the result. ‘C’ means C-order, ‘F’ means F-order, ‘A’ means ‘F’ if a is Fortran contiguous, ‘C’ otherwise. ‘K’ means match the layout of a as closely as possible.</param>
        /// <param name="subok">(optional) If True, then the newly created array will use the sub-class type of ‘a’, otherwise it will be a base-class array. Defaults to True.</param>
        /// <returns>Array of fill_value with the same shape and type as a.</returns>
        public static ndarray full_like(object osrc, object fill_value, dtype dtype = null, order order = order.DEFAULT, bool subok = true)
        {
            if (osrc == null)
            {
                throw new Exception("array can't be null");
            }

            var src = asanyarray(osrc);

            shape shape = new shape(src.Array.dimensions, src.Array.nd);

            return CommonFill(dtype, shape, fill_value, ConvertOrder(src, order), subok, 0);
        }
        #endregion

        #region CommonFill

        private static ndarray CommonFill(dtype dtype, object oshape, object FillValue, NPY_ORDER order, bool subok, int ndmin)
        {
            if (dtype == null)
            {
                dtype = np.Float64;
            }

            shape shape = null;
            if (oshape is shape)
            {
                shape = oshape as shape;
            }
            else if ((shape = NumpyExtensions.ConvertTupleToShape(oshape)) == null)
            {
                throw new Exception("Unable to convert shape object");
            }

            long ArrayLen = CalculateNewShapeSize(shape);


            // allocate a new array based on the calculated type and length
            var a = numpyAPI.Alloc_NewArray(dtype.TypeNum, (UInt64)ArrayLen);

            // populate the array
            for (int i = 0; i < ArrayLen; i++)
            {
                numpyAPI.SetIndex(a, i, FillValue);
            }

            // load this into a ndarray and return it to the caller
            var ndArray = array(a, dtype, false, order, subok, ndmin).reshape(shape);
            return ndArray;
        }




        #endregion

        #region count_nonzero

        public static ndarray count_nonzero(object a, int? axis = null)
        {
            //  Counts the number of non - zero values in the array ``a``.

            //  The word "non-zero" is in reference to the Python 2.x
            //  built -in method ``__nonzero__()`` (renamed ``__bool__()``
            //  in Python 3.x) of Python objects that tests an object's
            //  "truthfulness".For example, any number is considered
            // truthful if it is nonzero, whereas any string is considered
            // truthful if it is not the empty string.Thus, this function
            //(recursively) counts how many elements in ``a`` (and in
            //  sub - arrays thereof) have their ``__nonzero__()`` or ``__bool__()``
            //  method evaluated to ``True``.

            //  Parameters
            //  ----------
            //  a: array_like
            //     The array for which to count non - zeros.
            // axis : int or tuple, optional

            //     Axis or tuple of axes along which to count non - zeros.
            //     Default is None, meaning that non - zeros will be counted

            //     along a flattened version of ``a``.

            //      .. versionadded:: 1.12.0


            // Returns
            // ------ -
            // count : int or array of int

            //     Number of non - zero values in the array along a given axis.
            //     Otherwise, the total number of non - zero values in the array
            //     is returned.

            // See Also

            // --------
            // nonzero : Return the coordinates of all the non - zero values.

            // Examples
            // --------
            // >>> np.count_nonzero(np.eye(4))

            // 4
            // >>> np.count_nonzero([[0, 1, 7, 0, 0],[3, 0, 0, 2, 19]])
            //              5
            //              >>> np.count_nonzero([[0, 1, 7, 0, 0],[3,0,0,2,19]], axis=0)
            //  array([1, 1, 1, 1, 1])
            //  >>> np.count_nonzero([[0, 1, 7, 0, 0],[3, 0, 0, 2, 19]], axis=1)
            //  array([2, 3])

 
            a = asanyarray(a);

            var a_bool = asanyarray(a).astype(np.Bool, copy: false);

            return a_bool.Sum(axis: axis, dtype: np.intp);
        }
        #endregion

        #region asarray

        public static ndarray asarray(object a, dtype dtype = null, NPY_ORDER order = NPY_ORDER.NPY_ANYORDER)
        {
            //    Convert the input to an ndarray, but pass ndarray subclasses through.

            //    Parameters
            //    ----------
            //    a: array_like
            //       Input data, in any form that can be converted to an array.This
            //       includes scalars, lists, lists of tuples, tuples, tuples of tuples,
            //        tuples of lists, and ndarrays.
            //    dtype: data - type, optional
            //         By default, the data-type is inferred from the input data.
            //     order : { 'C', 'F'}, optional
            //          Whether to use row - major(C - style) or column-major
            //          (Fortran - style) memory representation.  Defaults to 'C'.

            //      Returns
            //      ------ -
            //      out : ndarray or an ndarray subclass
            //        Array interpretation of `a`.  If `a` is an ndarray or a subclass
            //        of ndarray, it is returned as-is and no copy is performed.

            //    See Also
            //    --------
            //    asarray : Similar function which always returns ndarrays.
            //    ascontiguousarray: Convert input to a contiguous array.
            //    asfarray: Convert input to a floating point ndarray.
            //   asfortranarray : Convert input to an ndarray with column - major
            //                     memory order.
            //    asarray_chkfinite: Similar function which checks input for NaNs and

            //                       Infs.
            //   fromiter : Create an array from an iterator.
            //   fromfunction : Construct an array by executing a function on grid

            //                  positions.

            //   Examples
            //   --------

            //   Convert a list into an array:


            //   >>> a = [1, 2]
            //   >>> np.asanyarray(a)

            //   array([1, 2])

            //   Instances of `ndarray` subclasses are passed through as-is:

            //    >>> a = np.matrix([1, 2])
            //    >>> np.asanyarray(a) is a
            //    True

            return array(a, dtype, copy: false, order: order, subok: true);
        }
        #endregion

        #region asanyarray

        public static ndarray asanyarray(object a, dtype dtype = null, NPY_ORDER order = NPY_ORDER.NPY_ANYORDER)
        {
            //  Convert the input to a masked array, conserving subclasses.

            //  If `a` is a subclass of `MaskedArray`, its class is conserved.
            //  No copy is performed if the input is already an `ndarray`.

            //  Parameters
            //  ----------
            //  a : array_like
            //      Input data, in any form that can be converted to an array.
            //  dtype : dtype, optional
            //      By default, the data-type is inferred from the input data.
            //  order : {'C', 'F'}, optional
            //      Whether to use row-major('C') or column-major('FORTRAN') memory
            //    representation.Default is 'C'.
            //
            //  Returns
            //  -------
            //
            // out : MaskedArray
            //    MaskedArray interpretation of `a`.
            //
            //
            //See Also
            //  --------
            //
            //asarray : Similar to `asanyarray`, but does not conserve subclass.
            //
            //Examples
            //  --------
            //  >>> x = np.arange(10.).reshape(2, 5)
            //  >>> x
            //
            //array([[0., 1., 2., 3., 4.],
            //
            //       [5., 6., 7., 8., 9.]])
            //  >>> np.ma.asanyarray(x)
            //  masked_array(data =
            //   [[0.  1.  2.  3.  4.]
            //   [5.  6.  7.  8.  9.]],
            //               mask =
            //   False,
            //         fill_value = 1e+20)
            //  >>> type(np.ma.asanyarray(x))
            //  <class 'numpy.ma.core.MaskedArray'>

            //if (a is MaskedArray && (dtype == null || dtype == a.Dtype))
            //{
            //    return a;
            //}
            //return masked_array(a, dtype: dtype, copy: false, keep_mask: true, sub_ok: true);

            if (dtype != null)
            {
                return np.array(a, dtype, copy: false, order: order, subok: true);
            }

            if (a is ndarray)
            {
                return a as ndarray;
            }
            if (a.GetType().IsArray)
            {
                System.Array ssrc = a as System.Array;
                NPY_TYPES type_num;
                switch (ssrc.Rank)
                {
                    case 1:
                        type_num = Get_NPYType(ssrc.GetValue(0));
                        break;
                    case 2:
                        type_num = Get_NPYType(ssrc.GetValue(0, 0));
                        break;
                    case 3:
                        type_num = Get_NPYType(ssrc.GetValue(0, 0, 0));
                        break;
                    case 4:
                        type_num = Get_NPYType(ssrc.GetValue(0, 0, 0, 0));
                        break;
                    case 5:
                        type_num = Get_NPYType(ssrc.GetValue(0, 0, 0, 0, 0));
                        break;
                    case 6:
                        type_num = Get_NPYType(ssrc.GetValue(0, 0, 0, 0, 0, 0));
                        break;
                    case 7:
                        type_num = Get_NPYType(ssrc.GetValue(0, 0, 0, 0, 0, 0, 0));
                        break;
                    case 8:
                        type_num = Get_NPYType(ssrc.GetValue(0, 0, 0, 0, 0, 0, 0));
                        break;
                    default:
                        throw new Exception("Number of dimensions is not supported");
                }

                return ndArrayFromMD(ssrc, type_num, ssrc.Rank);
            }

            if (IsNumericType(a))
            {
                return np.array(GetSingleElementArray(a), null);
            }

            throw new Exception("Unable to convert object to ndarray");
        }
        #endregion

        #region ascontiguousarray

        public static ndarray ascontiguousarray(object a, dtype dtype = null)
        {
            // Return a contiguous array in memory(C order).

            // Parameters
            // ----------
            // a: array_like
            //    Input array.
            //dtype : str or dtype object, optional
            //     Data - type of returned array.

            //   Returns
            //   ------ -
            // out : ndarray
            //     Contiguous array of same shape and content as `a`, with type `dtype`
            //     if specified.

            // See Also
            // --------
            // asfortranarray : Convert input to an ndarray with column - major
            //                  memory order.
            // require: Return an ndarray that satisfies requirements.
            // ndarray.flags : Information about the memory layout of the array.

            // Examples
            // --------
            // >>> x = np.arange(6).reshape(2, 3)
            // >>> np.ascontiguousarray(x, dtype = np.float32)
            // array([[0., 1., 2.],
            //        [ 3.,  4.,  5.]], dtype=float32)
            // >>> x.flags['C_CONTIGUOUS']
            // True

            return array(a, dtype, copy: false, order: NPY_ORDER.NPY_CORDER, ndmin: 1);

        }

        #endregion

        #region asfortranarray

        public static ndarray asfortranarray(ndarray a, dtype dtype = null)
        {
            // Return an array laid out in Fortran order in memory.

            // Parameters
            // ----------
            // a: array_like
            //    Input array.
            //dtype : str or dtype object, optional
            //     By default, the data-type is inferred from the input data.

            // Returns
            // ------ -
            // out : ndarray
            //     The input `a` in Fortran, or column-major, order.

            // See Also
            // --------
            // ascontiguousarray : Convert input to a contiguous(C order) array.
            //asanyarray : Convert input to an ndarray with either row or
            //     column - major memory order.
            // require: Return an ndarray that satisfies requirements.
            // ndarray.flags : Information about the memory layout of the array.

            // Examples
            // --------
            // >>> x = np.arange(6).reshape(2, 3)
            // >>> y = np.asfortranarray(x)
            // >>> x.flags['F_CONTIGUOUS']
            // False
            // >>> y.flags['F_CONTIGUOUS']
            // True

            return array(a, dtype, copy: false, order: NPY_ORDER.NPY_FORTRANORDER, ndmin: 1);
        }
        #endregion

        #region asfarray

        public static ndarray asfarray(object a, dtype dtype = null)
        {

            var a1 = asanyarray(a);

            if (dtype == null)
            {
                dtype = np.Float64;
            }

            var arr = a1.Array;
            if (NpyCoreApi.ScalarKind(dtype.TypeNum, ref arr) != NPY_SCALARKIND.NPY_FLOAT_SCALAR)
            {
                dtype = np.Float64;
            }

            return asarray(a1, dtype: dtype);
        }

        #endregion

        #region require

        public static ndarray require(ndarray a, dtype dtype = null, char[] requirements = null)
        {
            // Return an ndarray of the provided type that satisfies requirements.

            // This function is useful to be sure that an array with the correct flags
            // is returned for passing to compiled code(perhaps through ctypes).

            // Parameters
            // ----------
            // a : array_like
            //    The object to be converted to a type - and - requirement - satisfying array.
            // dtype : data - type
            //    The required data - type.If None preserve the current dtype.If your
            //    application requires the data to be in native byteorder, include
            //    a byteorder specification as a part of the dtype specification.
            // requirements : str or list of str
            //    The requirements list can be any of the following

            //    * 'F_CONTIGUOUS'('F') - ensure a Fortran - contiguous array
            //    * 'C_CONTIGUOUS'('C') - ensure a C - contiguous array
            //    * 'ALIGNED'('A') - ensure a data - type aligned array
            //    * 'WRITEABLE'('W') - ensure a writable array
            //    * 'OWNDATA'('O') - ensure an array that owns its own data
            //    * 'ENSUREARRAY', ('E') - ensure a base array, instead of a subclass

            // See Also
            // --------
            // asarray : Convert input to an ndarray.
            // asanyarray : Convert to an ndarray, but pass through ndarray subclasses.
            // ascontiguousarray : Convert input to a contiguous array.
            // asfortranarray : Convert input to an ndarray with column - major
            //                  memory order.
            // ndarray.flags : Information about the memory layout of the array.

            // Notes
            // ---- -
            // The returned array will be guaranteed to have the listed requirements
            // by making a copy if needed.

            // Examples
            // --------
            // >>> x = np.arange(6).reshape(2, 3)
            // >>> x.flags
            //   C_CONTIGUOUS: True
            //  F_CONTIGUOUS : False
            //  OWNDATA : False
            //  WRITEABLE : True
            //  ALIGNED : True
            //  WRITEBACKIFCOPY : False
            //  UPDATEIFCOPY : False

            //>>> y = np.require(x, dtype = np.float32, requirements =['A', 'O', 'W', 'F'])
            //>>> y.flags
            //   C_CONTIGUOUS: False
            //  F_CONTIGUOUS : True
            //  OWNDATA : True
            //  WRITEABLE : True
            //  ALIGNED : True
            //  WRITEBACKIFCOPY : False
            //  UPDATEIFCOPY : False

            return asanyarray(a, dtype);

        }

        #endregion

        #region isfortran

        public static bool isfortran(ndarray a)
        {
           // Returns True if the array is Fortran contiguous but* not*C contiguous.

           // This function is obsolete and, because of changes due to relaxed stride
           // checking, its return value for the same array may differ for versions
           // of NumPy >= 1.10.0 and previous versions.If you only want to check if an
           // array is Fortran contiguous use ``a.flags.f_contiguous`` instead.

           // Parameters
           // ----------
           // a: ndarray
           //    Input array.


           //Examples
           //--------

           // np.array allows to specify whether the array is written in C - contiguous
           // order(last index varies the fastest), or FORTRAN-contiguous order in
           // memory(first index varies the fastest).

           // >>> a = np.array([[1, 2, 3], [4, 5, 6]], order='C')
           // >>> a
           // array([[1, 2, 3],
           //        [4, 5, 6]])
           // >>> np.isfortran(a)
           // False

           // >>> b = np.array([[1, 2, 3], [4, 5, 6]], order='FORTRAN')
           // >>> b
           // array([[1, 2, 3],
           //        [4, 5, 6]])
           // >>> np.isfortran(b)
           // True


           // The transpose of a C-ordered array is a FORTRAN-ordered array.

           // >>> a = np.array([[1, 2, 3], [4, 5, 6]], order='C')
           // >>> a
           // array([[1, 2, 3],
           //        [4, 5, 6]])
           // >>> np.isfortran(a)
           // False
           // >>> b = a.T
           // >>> b
           // array([[1, 4],
           //        [2, 5],
           //        [3, 6]])
           // >>> np.isfortran(b)
           // True

           // C-ordered arrays evaluate as False even if they are also FORTRAN-ordered.

           // >>> np.isfortran(np.array([1, 2], order='FORTRAN'))
           // False

            return a.IsFortran;
        }

        #endregion

        #region argwhere

        public static ndarray argwhere(ndarray a)
        {
            //    Find the indices of array elements that are non - zero, grouped by element.

            //      Parameters
            //      ----------
            //    a: array_like
            //       Input data.

            //   Returns
            //   ------ -
            //   index_array : ndarray
            //       Indices of elements that are non - zero.Indices are grouped by element.

            //    See Also
            //    --------
            //    where, nonzero

            //    Notes
            //    -----
            //    ``np.argwhere(a)`` is the same as ``np.transpose(np.nonzero(a))``.

            //    The output of ``argwhere`` is not suitable for indexing arrays.
            //    For this purpose use ``nonzero(a)`` instead.

            //    Examples
            //    --------
            //    >>> x = np.arange(6).reshape(2, 3)
            //    >>> x
            //    array([[0, 1, 2],
            //           [3, 4, 5]])
            //    >>> np.argwhere(x > 1)
            //    array([[0, 2],
            //           [1, 0],
            //           [1, 1],
            //           [1, 2]])

            return transpose(nonzero(a));
        }

        #endregion

        #region flatnonzero

        public static ndarray flatnonzero(object a)
        {
            // Return indices that are non - zero in the flattened version of a.

            //   This is equivalent to np.nonzero(np.ravel(a))[0].

            //   Parameters
            //   ----------
            // a: array_like
            //    Input data.

            //Returns
            //------ -
            //res : ndarray
            //    Output array, containing the indices of the elements of `a.ravel()`
            //     that are non - zero.

            // See Also
            // --------
            // nonzero : Return the indices of the non-zero elements of the input array.
            // ravel : Return a 1 - D array containing the elements of the input array.

            // Examples
            // --------
            // >>> x = np.arange(-2, 3)
            // >>> x
            // array([-2, -1, 0, 1, 2])
            // >>> np.flatnonzero(x)
            // array([0, 1, 3, 4])

            // Use the indices of the non-zero elements as an index array to extract
            // these elements:

            // >>> x.ravel()[np.flatnonzero(x)]
            // array([-2, -1, 1, 2])

            return np.nonzero(np.ravel(asanyarray(a)))[0];

        }

        #endregion

        #region correlate

        public static ndarray correlate(ndarray a, ndarray v, string mode)
        {
            //Cross - correlation of two 1 - dimensional sequences.

            //This function computes the correlation as generally defined in signal
            //processing texts::

            //    c_{ av}[k] = sum_n a[n + k] * conj(v[n])

            //with a and v sequences being zero-padded where necessary and conj being
            //the conjugate.

            //Parameters
            //----------
            //a, v : array_like
            //    Input sequences.
            //mode : { 'valid', 'same', 'full'}, optional
            //Refer to the `convolve` docstring.Note that the default
            //    is 'valid', unlike `convolve`, which uses 'full'.
            //old_behavior : bool
            //    `old_behavior` was removed in NumPy 1.10. If you need the old
            //    behavior, use `multiarray.correlate`.

            //Returns
            //-------
            //out : ndarray
            //    Discrete cross-correlation of `a` and `v`.

            //See Also
            //--------
            //convolve : Discrete, linear convolution of two one-dimensional sequences.
            //multiarray.correlate : Old, no conjugate, version of correlate.

            //Notes
            //-----
            //The definition of correlation above is not unique and sometimes correlation
            //may be defined differently.Another common definition is::

            //    c'_{av}[k] = sum_n a[n] conj(v[n+k])

            //which is related to ``c_{av} [k]`` by ``c'_{av}[k] = c_{av}[-k]``.

            //Examples
            //--------
            //>>> np.correlate([1, 2, 3], [0, 1, 0.5])
            //array([3.5])
            //>>> np.correlate([1, 2, 3], [0, 1, 0.5], "same")
            //array([ 2. ,  3.5,  3. ])
            //>>> np.correlate([1, 2, 3], [0, 1, 0.5], "full")
            //array([ 0.5,  2. ,  3.5,  3. ,  0. ])

            //Using complex sequences:

            //>>> np.correlate([1 + 1j, 2, 3 - 1j], [0, 1, 0.5j], 'full')
            //array([ 0.5-0.5j,  1.0+0.j,  1.5-1.5j,  3.0-1.j,  0.0+0.j])

            //Note that you get the time reversed, complex conjugated result
            //when the two input sequences change places, i.e.,
            //``c_{va} [k] = c^{*}_{av}[-k]``:

            //>>> np.correlate([0, 1, 0.5j], [1 + 1j, 2, 3 - 1j], 'full')
            //array([ 0.0+0.j,  3.0+1.j,  1.5+1.5j,  1.0+0.j,  0.5+0.5j])

            throw new NotImplementedException();
        }

        #endregion

        #region convolve

        public static ndarray convolve(ndarray a, ndarray v, string mode)
        {
          //  Returns the discrete, linear convolution of two one - dimensional sequences.

          //  The convolution operator is often seen in signal processing, where it
          //  models the effect of a linear time-invariant system on a signal[1]_.In
          // probability theory, the sum of two independent random variables is
          // distributed according to the convolution of their individual
          //  distributions.

          //  If `v` is longer than `a`, the arrays are swapped before computation.

          //  Parameters
          //  ----------
          //  a: (N,) array_like
          //     First one - dimensional input array.
          //  v: (M,) array_like
          //     Second one - dimensional input array.
          //  mode: { 'full', 'valid', 'same'}, optional
          //      'full':
          //        By default, mode is 'full'.This returns the convolution
          //      at each point of overlap, with an output shape of(N+M - 1,). At
          //       the end - points of the convolution, the signals do not overlap
 
          //         completely, and boundary effects may be seen.

          //      'same':
          //        Mode 'same' returns output of length ``max(M, N)``.  Boundary
          //        effects are still visible.

          //      'valid':
          //        Mode 'valid' returns output of length
          //        ``max(M, N) - min(M, N) + 1``.  The convolution product is only given
          //        for points where the signals overlap completely.Values outside
          //        the signal boundary have no effect.

          //  Returns
          //  ------ -
          //  out : ndarray
          //      Discrete, linear convolution of `a` and `v`.

          //  See Also
          //  --------
          //  scipy.signal.fftconvolve : Convolve two arrays using the Fast Fourier
          //                             Transform.
          //  scipy.linalg.toeplitz : Used to construct the convolution operator.
          //  polymul: Polynomial multiplication. Same output as convolve, but also
          //            accepts poly1d objects as input.

          //  Notes
          //  -----
          //  The discrete convolution operation is defined as

          //  .. math:: (a * v)[n] = \\sum_{ m = -\\infty}^{\\infty}
          //          a[m] v[n - m]

          //  It can be shown that a convolution: math:`x(t) * y(t)` in time / space
          //  is equivalent to the multiplication :math:`X(f) Y(f)` in the Fourier
          //  domain, after appropriate padding(padding is necessary to prevent
          //  circular convolution).Since multiplication is more efficient (faster)
          //  than convolution, the function `scipy.signal.fftconvolve` exploits the
          //  FFT to calculate the convolution of large data-sets.

          //  References
          //  ----------
          //  .. [1] Wikipedia, "Convolution", http://en.wikipedia.org/wiki/Convolution.

          //          Examples
          //          --------
          //  Note how the convolution operator flips the second array
          //  before "sliding" the two across one another:

          //  >>> np.convolve([1, 2, 3], [0, 1, 0.5])
          //  array([ 0. ,  1. ,  2.5,  4. ,  1.5])

          //  Only return the middle values of the convolution.
          //  Contains boundary effects, where zeros are taken
          //  into account:

          //  >>> np.convolve([1, 2, 3],[0, 1, 0.5], 'same')
          //  array([ 1. ,  2.5,  4. ])

          //  The two arrays are of the same length, so there
          //  is only one position where they completely overlap:

          //  >>> np.convolve([1, 2, 3],[0, 1, 0.5], 'valid')
          //  array([ 2.5])

            throw new NotImplementedException();
        }

        #endregion

        #region outer

        public static ndarray UFunc_Outer(object a, object b, numericOp op)
        {
           // Compute the outer product of two vectors.

           // Given two vectors, ``a = [a0, a1, ..., aM]`` and
           // ``b = [b0, b1, ..., bN]``,
           // the outer product[1]_ is::


           //  [[a0 * b0  a0 * b1...a0 * bN]
           //   [a1 * b0.

           //   [ ...          .

           //   [aM * b0            aM * bN]]

           // Parameters
           // ----------
           // a: (M,) array_like
           //    First input vector.  Input is flattened if

           //    not already 1 - dimensional.
           //b : (N,) array_like
           //    Second input vector.  Input is flattened if

           //    not already 1 - dimensional.
           // out : (M, N) ndarray, optional
           //     A location where the result is stored

           //     ..versionadded:: 1.9.0

           // Returns
           // ------ -
           // out : (M, N) ndarray
           //     ``out[i, j] = a[i] * b[j]``

           // See also
           // --------
           // inner
           // einsum : ``einsum('i,j->ij', a.ravel(), b.ravel())`` is the equivalent.
           // ufunc.outer : A generalization to N dimensions and other operations.
           //               ``np.multiply.outer(a.ravel(), b.ravel())`` is the equivalent.


            var a1 = asanyarray(a).ravel();
            var b1 = asanyarray(b).ravel();

            int alen = len(a1);
            int blen = len(b1);

            ndarray r = empty(new shape(alen, blen), dtype: np.Float64);
            for (int i = 0; i < alen; i++)
            {
                for (int j = 0; j < blen; j++)
                {
                    r[i, j] = Convert.ToDouble(op(a1[i], b1[j]));     // op = ufunc in question
                }

            }

            return r;
        }

        public static ndarray outer(object a, object b)
        {
            // Compute the outer product of two vectors.

            // Given two vectors, ``a = [a0, a1, ..., aM]`` and
            // ``b = [b0, b1, ..., bN]``,
            // the outer product[1]_ is::


            //  [[a0 * b0  a0 * b1...a0 * bN]
            //   [a1 * b0.

            //   [ ...          .

            //   [aM * b0            aM * bN]]

            // Parameters
            // ----------
            // a: (M,) array_like
            //    First input vector.  Input is flattened if

            //    not already 1 - dimensional.
            //b : (N,) array_like
            //    Second input vector.  Input is flattened if

            //    not already 1 - dimensional.
            // out : (M, N) ndarray, optional
            //     A location where the result is stored

            //     ..versionadded:: 1.9.0

            // Returns
            // ------ -
            // out : (M, N) ndarray
            //     ``out[i, j] = a[i] * b[j]``

            // See also
            // --------
            // inner
            // einsum : ``einsum('i,j->ij', a.ravel(), b.ravel())`` is the equivalent.
            // ufunc.outer : A generalization to N dimensions and other operations.
            //               ``np.multiply.outer(a.ravel(), b.ravel())`` is the equivalent.


            var a1 = asarray(a);
            var b1 = asarray(b);

            //return multiply(a1.ravel()[":", null] as ndarray, b1.ravel()[null, ":"] as ndarray);
            return multiply(a1.ravel(), b1.ravel());
        }

        #endregion

        #region tensordot

        public static ndarray tensordot(ndarray a, ndarray b, int axis = 2)
        {

            throw new NotImplementedException();
        }

        #endregion

        #region roll

        public static ndarray roll(ndarray input, int shift, int? axis = null)
        {
            // Roll array elements along a given axis.

            // Elements that roll beyond the last position are re - introduced at
            //   the first.

            //   Parameters
            //   ----------
            // a: array_like
            //    Input array.
            //shift : int or tuple of ints
            //    The number of places by which elements are shifted.  If a tuple,
            //     then `axis` must be a tuple of the same size, and each of the
            //     given axes is shifted by the corresponding number.If an int
            //     while `axis` is a tuple of ints, then the same value is used for
            //     all given axes.
            // axis : int or tuple of ints, optional
            //     Axis or axes along which elements are shifted.By default, the
            //     array is flattened before shifting, after which the original
            //     shape is restored.

            // Returns
            // -------
            // res : ndarray
            //     Output array, with the same shape as `a`.

            // See Also
            // --------
            // rollaxis : Roll the specified axis backwards, until it lies in a
            //            given position.

            // Notes
            // ---- -
            // ..versionadded:: 1.12.0

            // Supports rolling over multiple dimensions simultaneously.

            ndarray AdjustedArray = input;

            if (axis.HasValue)
            {
                if (axis.Value == 0)
                {
                    AdjustedArray = input.A(":");
                }
                else
                {
                    throw new Exception("axis != 0 not implemented yet");
                }

                throw new Exception("axis != 0 not implemented yet");

            }
            else
            {
                var copy = input.Copy();
                var rawdatavp = copy.rawdata(0);
                dynamic RawData = (dynamic)rawdatavp.datap;
                dynamic LastElement = 0;

                if (shift < 0)
                {
                    for (long shiftCnt = 0; shiftCnt < Math.Abs(shift); shiftCnt++)
                    {
                        LastElement = RawData[0];

                        for (long index = 1; index < RawData.Length; index++)
                        {
                            RawData[index - 1] = RawData[index];
                        }
                        RawData[RawData.Length - 1] = LastElement;
                    }
                }
                else
                {
                    for (long shiftCnt = 0; shiftCnt < Math.Abs(shift); shiftCnt++)
                    {
                        LastElement = RawData[RawData.Length - 1];

                        for (long index = RawData.Length - 2; index >= 0; index--)
                        {
                            RawData[index + 1] = RawData[index];
                        }
                        RawData[0] = LastElement;
                    }
                }


                return array(RawData, dtype: input.Dtype);
            }
        }

        #endregion

        #region rollaxis
        public static ndarray rollaxis(ndarray a, int axis, int start = 0)
        {
            //  Roll the specified axis backwards, until it lies in a given position.

            //  This function continues to be supported for backward compatibility, but you
            //  should prefer `moveaxis`. The `moveaxis` function was added in NumPy
            //  1.11.

            //  Parameters
            //  ----------
            //  a : ndarray
            //      Input array.
            //  axis : int
            //      The axis to roll backwards.The positions of the other axes do not
            //    change relative to one another.
            //start : int, optional
            //      The axis is rolled until it lies before this position.The default,
            //      0, results in a "complete" roll.

            //  Returns
            //  ------ -
            //  res : ndarray
            //      For NumPy >= 1.10.0 a view of `a` is always returned. For earlier
            //      NumPy versions a view of `a` is returned only if the order of the
            //      axes is changed, otherwise the input array is returned.

            //  See Also
            //  --------
            //  moveaxis : Move array axes to new positions.
            //  roll : Roll the elements of an array by a number of positions along a
            //      given axis.

            //  Examples
            //  --------
            //  >>> a = np.ones((3, 4, 5, 6))
            //  >>> np.rollaxis(a, 3, 1).shape
            //  (3, 6, 4, 5)
            //  >>> np.rollaxis(a, 2).shape
            //  (5, 3, 4, 6)
            //  >>> np.rollaxis(a, 1, 4).shape
            //  (3, 5, 6, 4)

            throw new NotImplementedException();
        }
        #endregion

        #region normalize_axis_tuple
        private static dynamic normalize_axis_tuple(dynamic axis, int ndim, string argname = null, bool allow_duplicates = false)
        {
            //Normalizes an axis argument into a tuple of non - negative integer axes.

            //This handles shorthands such as ``1`` and converts them to ``(1,)``,
            //as well as performing the handling of negative indices covered by
            //`normalize_axis_index`.

            //By default, this forbids axes from being specified multiple times.

            //Used internally by multi-axis - checking logic.


            //  ..versionadded:: 1.13.0

            //Parameters
            //----------
            //axis: int, iterable of int
            //   The un - normalized index or indices of the axis.
            //ndim: int
            //   The number of dimensions of the array that `axis` should be normalized
            //    against.
            //argname : str, optional
            //    A prefix to put before the error message, typically the name of the
            //    argument.
            //allow_duplicate : bool, optional
            //    If False, the default, disallow an axis from being specified twice.

            //Returns
            //------ -
            //normalized_axes : tuple of int
            //    The normalized axis index, such that `0 <= normalized_axis < ndim`

            //Raises
            //------
            //AxisError
            //    If any axis provided is out of range
            //ValueError
            //    If an axis is repeated

            //See also
            //--------
            //normalize_axis_index: normalizing a single scalar axis

            return 0;
            throw new NotImplementedException();


            //    try:
            //    axis = [operator.index(axis)]
            //except TypeError:
            //    axis = tuple(axis)
            //axis = tuple(normalize_axis_index(ax, ndim, argname) for ax in axis)
            //        if not allow_duplicate and len(set(axis)) != len(axis):
            //    if argname:
            //        raise ValueError('repeated axis in `{}` argument'.format(argname))
            //    else:
            //        raise ValueError('repeated axis')
            //return axis

        }
        #endregion

        #region moveaxis
        public static ndarray moveaxis(ndarray a, object source, object destination)
        {
            // Move axes of an array to new positions.

            // Other axes remain in their original order.

            // ..versionadded:: 1.11.0

            // Parameters
            // ----------
            // a: np.ndarray
            //    The array whose axes should be reordered.
            // source: int or sequence of int
            //    Original positions of the axes to move. These must be unique.
            // destination: int or sequence of int
            //    Destination positions for each of the original axes.These must also be

            //    unique.

            //Returns
            //------ -
            //result : np.ndarray

            //    Array with moved axes.This array is a view of the input array.


            //See Also
            //--------

            //transpose: Permute the dimensions of an array.
            //swapaxes: Interchange two axes of an array.

            //Examples
            //--------

            //>>> x = np.zeros((3, 4, 5))
            //>>> np.moveaxis(x, 0, -1).shape
            //(4, 5, 3)
            //>>> np.moveaxis(x, -1, 0).shape
            //(5, 3, 4)


            //These all achieve the same result:


            //>>> np.transpose(x).shape
            //(5, 4, 3)
            //>>> np.swapaxes(x, 0, -1).shape
            //(5, 4, 3)
            //>>> np.moveaxis(x, [0, 1], [-1, -2]).shape
            //(5, 4, 3)
            //>>> np.moveaxis(x, [0, 1, 2], [-1, -2, -3]).shape
            //(5, 4, 3)

            try
            {
                // allow duck-array types if they define transpose
                var transpose = a.Transpose();
            }
            catch (Exception ex)
            {
                throw new Exception("moveaxis:Failure on transpose");
            }

            source = normalize_axis_tuple(source, a.ndim, "source");
            destination = normalize_axis_tuple(destination, a.ndim, "destination");
            //if (len(source) != len(destination))
            //{
            //    throw new Exception("`source` and `destination` arguments must have the same number of elements");
            //}

            return null;
        }
        #endregion

        #region cross

        public static ndarray cross(ndarray a, object source, object destination)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region indices

        public static ndarray indices(ndarray a, object source, object destination)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region fromfunction

        public static ndarray fromfunction(ndarray a, object source, object destination)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region isscalar

        public static ndarray isscalar(ndarray a, object source, object destination)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region binary_repr

        public static ndarray binary_repr()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region base_repr

        public static ndarray base_repr()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region identity
        /// <summary>
        /// Return the identity array.
        /// </summary>
        /// <param name="n">Number of rows (and columns) in n x n output</param>
        /// <param name="dtype">(optional) Data-type of the output. Defaults to float</param>
        /// <returns> x n array with its main diagonal set to one, and all other elements 0.</returns>
        public static ndarray identity(int n, dtype dtype = null)
        {
            /*
               Return the identity array.

                The identity array is a square array with ones on
                the main diagonal.

                Parameters
                ----------
                n : int
                    Number of rows (and columns) in `n` x `n` output.
                dtype : data-type, optional
                    Data-type of the output.  Defaults to ``float``.

                Returns
                -------
                out : ndarray
                    `n` x `n` array with its main diagonal set to one,
                    and all other elements 0.

                Examples
                --------
                >>> np.identity(3)
                array([[ 1.,  0.,  0.],
                       [ 0.,  1.,  0.],
                       [ 0.,  0.,  1.]])
            */

            return eye(n, dtype: dtype);
        }
        #endregion

        #region allclose

        public static ndarray allclose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region isclose

        public static ndarray isclose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region array_equal

        public static bool array_equal(object a1, object a2)
        {
            // True if two arrays have the same shape and elements, False otherwise.

            // Parameters
            // ----------
            // a1, a2: array_like
            //    Input arrays.

            //Returns
            //------ -
            //b : bool
            //    Returns True if the arrays are equal.

            // See Also
            // --------
            // allclose: Returns True if two arrays are element - wise equal within a
            //           tolerance.
            // array_equiv: Returns True if input arrays are shape consistent and all
            //              elements equal.

            // Examples
            // --------
            // >>> np.array_equal([1, 2], [1, 2])
            // True
            // >>> np.array_equal(np.array([1, 2]), np.array([1, 2]))
            // True
            // >>> np.array_equal([1, 2], [1, 2, 3])
            // False
            // >>> np.array_equal([1, 2], [1, 4])
            // False

            ndarray arr1 = null;
            ndarray arr2 = null;

            try
            {
                arr1 = asanyarray(a1);
                arr2 = asanyarray(a2);
            }
            catch (Exception ex)
            {
                return false;
            }

            if (arr1.shape != arr2.shape)
            {
                return false;
            }

            return (bool)(arr1.Equals(arr2).All().GetItem(0));
        }
        #endregion

        #region array_equiv

        public static bool array_equiv(object a1, object a2)
        {
            // Returns True if input arrays are shape consistent and all elements equal.

            // Shape consistent means they are either the same shape, or one input array
            // can be broadcasted to create the same shape as the other one.

            // Parameters
            // ----------
            // a1, a2: array_like
            //    Input arrays.

            //Returns
            //------ -
            // out : bool
            //     True if equivalent, False otherwise.

            // Examples
            // --------
            // >>> np.array_equiv([1, 2], [1, 2])
            // True
            // >>> np.array_equiv([1, 2], [1, 3])
            // False

            // Showing the shape equivalence:

            // >>> np.array_equiv([1, 2], [[1, 2], [1, 2]])
            // True
            // >>> np.array_equiv([1, 2], [[1, 2, 1, 2], [1, 2, 1, 2]])
            // False

            // >>> np.array_equiv([1, 2], [[1, 2], [1, 3]])
            // False


            ndarray arr1 = null;
            ndarray arr2 = null;

            try
            {
                arr1 = asanyarray(a1);
                arr2 = asanyarray(a2);
            }
            catch (Exception ex)
            {
                return false;
            }

            try
            {
                //multiarray.broadcast(arr1, arr2);
                broadcast(arr1, arr2);
            }
            catch
            {
                return false;
            }
            //if (!broadcastable(arr1,arr2.Dims, arr2.ndim))
            //{
            //    return false;
            //}

            return (bool)(arr1.Equals(arr2).All().GetItem(0));
        }
        #endregion



    }
}