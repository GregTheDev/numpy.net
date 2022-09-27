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

using NumpyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NPY_INTP_64
using npy_intp = System.Int64;
#else
using npy_intp = System.Int32;
#endif

namespace NumpyDotNet
{
    static class DumpData
    {

        public static void DumpArray(List<string> sb, NpyArray arr, bool repr)
        {
            // Equivalent to array_repr_builtin (arrayobject.c)
            if (repr)
            {
                sb.Add("array(");
            }
            else
            {
                string ItemType = arr.ItemType.ToString().Substring("NPY_".Length);
                if (string.IsNullOrEmpty(arr.Name))
                {
                    sb.Add(string.Format("{0} \n", ItemType));
                }
                else
                {
                    sb.Add(string.Format("{0}:Name({1}) \n", ItemType, arr.Name));
                }
            }

            npy_intp totalElements = numpyAPI.NpyArray_Size(arr);
            DumpArray(arr, sb, arr.dimensions, arr.strides, 0, 0, totalElements, 0, !repr);

            if (repr)
            {
                if (NpyDefs.IsExtended(arr.descr.type_num))
                {
                    sb.Add(string.Format(", '{0}{1}')", arr.ItemType, arr.ItemSize));
                }
                else
                {
                    sb.Add(String.Format(", '{0}')", arr.ItemType));
                }
            }

   
        }

        /// <summary>
        /// Recursively walks the array and appends a representation of each element
        /// to the passed string builder.  Square brackets delimit each array dimension.
        /// </summary>
        /// <param name="sb">StringBuilder instance to append to</param>
        /// <param name="dimensions">Array of size of each dimension</param>
        /// <param name="strides">Offset in bytes to reach next element in each dimension</param>
        /// <param name="dimIdx">Index of the current dimension (starts at 0, recursively counts up)</param>
        /// <param name="offset">Byte offset into data array, starts at 0</param>
        private static void DumpArray(NpyArray arr, List<string> sb, npy_intp[] dimensions, npy_intp[] strides, int dimIdx, npy_intp offset, npy_intp totalElements, int IndentCount, bool UseParensForMarkers)
        {

            if (dimIdx == arr.nd)
            {
                Object value = arr.descr.f.getitem(offset, arr);
                if (value == null)
                {
                    sb.Add("None");
                }
                else
                {
                    string strValue = "";
                    switch (arr.ItemType)
                    {
                        case NPY_TYPES.NPY_FLOAT:
                            float fValue = (float)value;
                            strValue = fValue.ToString();
                            if (!strValue.Contains(".") && !float.IsInfinity(fValue))
                            {
                                strValue += ".0";
                            }
                            strValue += "f";
                            break;

                        case NPY_TYPES.NPY_DOUBLE:
                            double dValue = (double)value;
                            strValue = dValue.ToString();
                            if (!strValue.Contains(".") && !double.IsInfinity(dValue))
                            {
                                strValue += ".0";
                            }
                            break;
                        case NPY_TYPES.NPY_DECIMAL:
                            decimal ddValue = (decimal)value;
                            strValue = ddValue.ToString();
                            if (!strValue.Contains("."))
                            {
                                strValue += ".0";
                            }
                            strValue += "m";
                            break;
                        default:
                            strValue = value.ToString();
                            break;
                    }

    
                    sb.Add(strValue);
                }
            }
            else
            {
                string RowStartMarker;
                string RowEndMarker;

                if (UseParensForMarkers)
                {
                    RowStartMarker = "{ ";
                    RowEndMarker = " }\n";
                }
                else
                {
                    RowStartMarker = "[ ";
                    RowEndMarker = " ]\n";
                }

                string StartRow = InsertRowStartOffsets(sb,IndentCount);
     
                StartRow += RowStartMarker;
                sb.Add(StartRow);
                IndentCount++;

                for (int i = 0; i < dimensions[dimIdx]; i++)
                {
                    DumpArray(arr, sb, dimensions, strides, dimIdx + 1,  offset + (strides[dimIdx] * i), totalElements, IndentCount, UseParensForMarkers);
                    if (i < dimensions[dimIdx] - 1)
                    {
                        string LastRow = sb[sb.Count - 1];
                        if (LastRow.EndsWith(RowEndMarker))
                        {
                            sb[sb.Count - 1] = LastRow.TrimEnd() + ",\n";
                        }
                        else
                        {
                            sb.Add(", ");
                        }
                    }
                }

                string LastRow2 = sb[sb.Count - 1];
                if (LastRow2.EndsWith(RowEndMarker))
                {
                    sb[sb.Count - 1] = LastRow2.TrimEnd() + RowEndMarker;
                }
                else
                {
                    sb.Add(RowEndMarker);
                    IndentCount--;
                }
            }
        }

        private static string InsertRowStartOffsets(List<string> sb, int IndentCount)
        {
            string LastString = sb[sb.Count - 1];
            if (LastString.EndsWith("},\n"))
            {
                string indentString = "";
                for (int i = 0; i < IndentCount; i++)
                    indentString += "  ";
                return indentString;
            }

            return "";
        }
    }
}
