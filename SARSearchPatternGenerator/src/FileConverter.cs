using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    public abstract class FileConverter<T> where T : class, new()
    {
        private static T instance;

        /*
         * Creates a new FileConverter if one does not exist yet.
         */
        public static T GetInstance()
        {
            if (instance == null)
                instance = new T();
            return instance;

        }
    }
}
