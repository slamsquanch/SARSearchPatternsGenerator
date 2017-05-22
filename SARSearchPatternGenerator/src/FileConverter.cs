using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SARSearchPatternGenerator
{
    public class FileConverter
    {

        /*
         * Creates a new FileConverter if one does not exist yet.
         */
        public FileConverter()
        {
        }


        /*
         *  Extracts just the name of the file without the extension or directory.
         */
        protected String extractName(String filePath)
        {
            //Get just the name without the file path or file extension.
            Char delimiter = '\\';
            String[] substring = filePath.Split(delimiter);
            int index = substring.Length - 1;
            String name = substring[index];
            delimiter = '.';
            substring = name.Split(delimiter);
            return substring[0];
        }
    }
}
