using System;
using System.Drawing;
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


        /*
         * Returns a colour string for the corresponding pattern legs to use.
         * @param Colour. 
         */
        protected String selectColour(Color c)
        {
            switch (c.Name)
            {
                case "Red":
                    return c.Name;

                case "Blue":
                    return c.Name;

                case "Yellow":
                    return c.Name;

                case "Purple":
                    return "Magenta";

                case "Green":
                    return c.Name;

                case "Cyan":
                    return c.Name;

                default:
                    return "";
            }
        }
    }
}
