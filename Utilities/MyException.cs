using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CARS.Utilities
{
    public class MyException:Exception,ISerializable
    {
        public MyException (string msg) : base(msg)
        {

        }
    }
}