using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    /*
     *  This class is a Singleton design pattern.
     */
    public abstract class FileConverter<T> where T : class, new()
    {
        private static T instance;

        public static T GetInstance()
        {
            if (instance == null)
                instance = new T();
            return instance;

        }
    }
}
