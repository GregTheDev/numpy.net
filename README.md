
# NumpyDotNet

This is a 100% pure .NET implementation of the Numpy API.  This library is ported from the real Numpy source code. The C and the Python code of Numpy have been ported to C#.  This approach allows us to capture all of the nuances that are in the original Numpy libraries.

We have near 100% of the API ported and unit tested.  The one notable missing function is np.einsum (volunteers welcome).

The result is a .NET library that is 100% compatible with the Numpy API.  It can be run anywhere that .NET can run. There are no dependencies on having Python installed.  There are no performance issues related to interoping with Python. 

Since all of the underlying data are pure .NET System.Array types, they can be used like any other .NET array.

Our ndarray class is iterable, which means that it can be data bound to windows UI elements.

## Nuget packaging

The built release mode libraries are available from here:  

https://www.nuget.org/packages/NumpyDotNet/  

The unit tests that demonstrate how to use use all of the APIs and features are available here:  

https://www.nuget.org/packages/NumpyDotNet.UnitTests/  


## Pure .NET data types
The underlying technology uses 100% .NET data types.   If you are working with doubles, then an array of doubles are allocated.  There is no worry about mismatching Python allocated C pointers to the .NET data type.  There is no worry about interop 'marshalling' of data and the complexities and problems that can cause.

##### Our API has full support of the following .NET data types:

* System.Boolean
* System.Sbyte
* System.Byte
* System.UInt16
* System.Int16
* System.UInt32
* System.Int32
* System.UInt64
* System.Int64
* System.Single (float)
* System.Double.

##### Currently we have partial support of the following .NET data types:

* System.Decimal

##### Future plans include support for:

* System.Numerics.Complex
* System.Numerics.BigInteger

## Accessing the underlying array

We have extended the Numpy API to allow you to access the underlying System.Array type data.

    ndarray A = np.array(new int[] {1,2,3});

    int[] A1 = A.AsInt32Array();    // gets a reference to internally created array  
    Int16[] A2 = A.AsInt16Array();  // gets a reference to a copy of the original data, now int Int16[] form.  

## Accessing a scalar return value

Many Numpy operations can return an ndarray full of data, or a single scalar object if the resultant is a single item.  Python, not being a strongly typed language, can get away with that.  .NET languages can't. .NET functions need to specify the exact return type.  In most cases, our API will always return an ndarray, even if the resultant is a single data item. To help with this issue, we have extended the ndarray class to have the following APIs.  


    ndarray A = np.array(new int[] {1,2,3});  
    
    int I1 = (int)A.GetItem(1);  // I1 = 2;  
    A.SetItem(99, 1);  
    int I2 = (int)A.GetItem(1);  // I2 = 99;  


## Array Slicing

Numpy allows you to create different views of an array using a technique called ([array slicing](https://docs.scipy.org/doc/numpy/reference/arrays.indexing.html#arrays-indexing)).  As an interpreted language, python can use syntax that C# can't.  This necessitates a small difference in NumpyDotNet.

In the example of python slicing array like this:  

    A1 = A[1:4:2, 10:0:-2]    

NumpyDotNet supports the slicing syntax  like this:  

    var A1 = A["1:4:2", "10:0:-2"];  

or like this:  

    var A1 = A[new Slice(1,4,2), new Slice(10,2,-2)];  

We also support Ellipsis slicing:  

    var A1 = A["..."];  



## Documentation

We have worked hard to make NumpyDotNET as similar to the python NumPy as possible.  We rely on the official [NumPy manual](https://docs.scipy.org/doc/numpy/). 


