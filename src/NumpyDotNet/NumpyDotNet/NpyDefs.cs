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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NumpyLib;
#if NPY_INTP_64
using npy_intp = System.Int64;
using npy_ucs4 = System.Int64;
using NpyArray_UCS4 = System.UInt64;
#else
using npy_intp = System.Int32;
using npy_ucs4 = System.Int32;
using NpyArray_UCS4 = System.UInt32;
#endif

namespace NumpyDotNet {
    internal class NpyDefs {
        #region ConstantDefs

        public const int NPY_VALID_MAGIC = 1234567;

 
        public enum NPY_COMPARE_OP {
            NPY_LT = 0,
            NPY_LE = 1,
            NPY_EQ = 2,
            NPY_NE = 3,
            NPY_GT = 4,
            NPY_GE = 5,
        };

        internal const int NPY_MAXDIMS = 32;
        internal const int NPY_MAXARGS = 32;

        #endregion
  

        #region Type functions

        internal static bool IsBool(NPY_TYPES type)
        {
            return type == NPY_TYPES.NPY_BOOL;
        }

        internal static bool IsUnsigned(NPY_TYPES type)
        {
            switch (type)
            {
                case NPY_TYPES.NPY_UBYTE:
                case NPY_TYPES.NPY_UINT16:
                case NPY_TYPES.NPY_UINT32:
                case NPY_TYPES.NPY_UINT64:
                    return true;
                default:
                    return false;
            }
        }

        internal static bool IsSigned(NPY_TYPES type)
        {
            switch (type)
            {
                case NPY_TYPES.NPY_BYTE:
                case NPY_TYPES.NPY_INT16:
                case NPY_TYPES.NPY_INT32:
                case NPY_TYPES.NPY_INT64:
                case NPY_TYPES.NPY_BIGINT:
                    return true;
                default:
                    return false;
            }

        }

        internal static bool IsInteger(NPY_TYPES type)
        {
            switch (type)
            {
                case NPY_TYPES.NPY_BYTE:
                case NPY_TYPES.NPY_UBYTE:
                case NPY_TYPES.NPY_INT16:
                case NPY_TYPES.NPY_UINT16:
                case NPY_TYPES.NPY_INT32:
                case NPY_TYPES.NPY_UINT32:
                case NPY_TYPES.NPY_INT64:
                case NPY_TYPES.NPY_UINT64:
                case NPY_TYPES.NPY_BIGINT:
                    return true;
                default:
                    return false;
            }

        }

        internal static bool IsFloat(NPY_TYPES type)
        {
            switch (type)
            {
                case NPY_TYPES.NPY_FLOAT:
                case NPY_TYPES.NPY_DOUBLE:
                case NPY_TYPES.NPY_DECIMAL:
                case NPY_TYPES.NPY_COMPLEX:
                    return true;
                default:
                    return false;
            }
        }

        internal static bool IsNumber(NPY_TYPES type)
        {
            switch (type)
            {
                case NPY_TYPES.NPY_BYTE:
                case NPY_TYPES.NPY_UBYTE:
                case NPY_TYPES.NPY_INT16:
                case NPY_TYPES.NPY_UINT16:
                case NPY_TYPES.NPY_INT32:
                case NPY_TYPES.NPY_UINT32:
                case NPY_TYPES.NPY_INT64:
                case NPY_TYPES.NPY_UINT64:
                case NPY_TYPES.NPY_FLOAT:
                case NPY_TYPES.NPY_DOUBLE:
                case NPY_TYPES.NPY_DECIMAL:
                case NPY_TYPES.NPY_COMPLEX:
                case NPY_TYPES.NPY_BIGINT:
                    return true;
                default:
                    return false;
            }
        }

        internal static bool IsString(NPY_TYPES type)
        {
            switch (type)
            {
                case NPY_TYPES.NPY_STRING:
                    return true;
                default:
                    return false;
            }
        }

        internal static bool IsComplex(NPY_TYPES type)
        {
            switch (type)
            {
                case NPY_TYPES.NPY_COMPLEX:
                    return true;
                default:
                    return false;
            }
        }

        internal static bool IsBigInt(NPY_TYPES type)
        {
            switch (type)
            {
                case NPY_TYPES.NPY_BIGINT:
                    return true;
                default:
                    return false;
            }
        }


        internal static bool IsDecimal(NPY_TYPES type)
        {
            switch (type)
            {
                case NPY_TYPES.NPY_DECIMAL:
                    return true;
                default:
                    return false;
            }
        }


        internal static bool IsFlexible(NPY_TYPES type)
        {
            switch (type)
            {
                case NPY_TYPES.NPY_STRING:
                    return true;
                default:
                    return false;
            }

        }

        internal static bool IsUserDefined(NPY_TYPES type)
        {
            return NPY_TYPES.NPY_USERDEF <= type &&
                (int)type <= (int)NPY_TYPES.NPY_USERDEF + 0; // TODO: Need GetNumUserTypes
        }

        internal static bool IsExtended(NPY_TYPES type)
        {
            return IsFlexible(type) || IsUserDefined(type);
        }

        internal static bool IsNativeByteOrder(char endian)
        {
            return BitConverter.IsLittleEndian;
        }

        #endregion

    }
}
