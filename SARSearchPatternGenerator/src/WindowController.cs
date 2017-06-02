using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SARSearchPatternGenerator
{
    /*
     *  This class has the sole purpose of being a Controller of the main Window. 
     */
    public class WindowController
    {
        Window mainWindow;

        /*
* Constructor for this WindowController class.  It starts running the main Window. 
*/
        public WindowController() {
            //This is what STARTS the main window.
            mainWindow = new Window();
            mainWindow.setController(this);
            writeSystemText("Program loaded");

            this.onProgramStart();
            Application.Run(mainWindow);

        }
        
        private void writeSystemText(string txt)
        {
            mainWindow.setSystemLabel(txt);
        }

        public void onFileNew()
        {
            PatternController pc = new PatternController();
            mainWindow.setDisplay(pc);
            pc.updateSettings();
            mainWindow.unitChange();
            mainWindow.coordSystemChange();
            writeSystemText("New pattern created");
        }
        public void createFromPattern(Pattern p)
        {
            PatternController pc = new PatternController();
            mainWindow.setDisplay(pc);
            pc.updateSettings();
            mainWindow.unitChange();
            mainWindow.coordSystemChange();
            pc.createFromPattern(p);
            writeSystemText("Pattern Loaded");
        }
        public void onClose()
        {
            Pattern p = mainWindow.getCurrentPattern();
            if (p != null) {
                FileStream fStream = null;
                try
                {
                    DataContractSerializer dcs = new DataContractSerializer(p.GetType());
                    fStream = new FileStream(".\\pattern.xml", FileMode.Create);
                    dcs.WriteObject(fStream, p);
                }
                finally
                {
                    if (fStream != null)
                    {
                        fStream.Close();
                    }
                }
            }
            Properties.Settings.Default.comment = this.mainWindow.getDisplay().getComment();
            Console.WriteLine("Program closed");
        }
        public void onProgramStart()
        {
            FileStream fStream = null;
            try
            {
                DataContractSerializer dcs = new DataContractSerializer(typeof(Pattern));
                fStream = new FileStream(".\\pattern.xml", FileMode.Open);
                Pattern p = (Pattern)dcs.ReadObject(fStream);
                createFromPattern(p);
            }
            catch (FileNotFoundException)
            {
                writeSystemText("No previous pattern found");
            }
            catch (SerializationException)
            {
                writeSystemText("Problem reading previous pattern data");
            }
            finally
            {
                if (fStream != null)
                {
                    fStream.Close();
                }
            }
        }
    }
}
