using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Takes a pattern and writes it to a file in KML format.
    /// </summary>
    class KML
    {
        Pattern p;
        //Constructor takes in a pattern instance.
        public KML(Pattern p)
        {
            this.p = p;
        }

        public void writeFile(String name)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            List<Coordinate> points = p.getPattern();
            //CREATE THE FILE
            XmlWriter xmlWriter = XmlWriter.Create(name, settings);  //Makes file and formats indentation
            xmlWriter.WriteStartDocument();

            //open KML tag
            xmlWriter.WriteStartElement("kml", "http://www.opengis.net/kml/2.2");
            //xmlWriter.WriteAttributeString("xmlns", "", null, "http://earth.google.com/kml/2.1");
            //open Document tag
            xmlWriter.WriteStartElement("Document");
            //open name tag
            xmlWriter.WriteStartElement("name");
            xmlWriter.WriteString(name);
            //close name tag
            xmlWriter.WriteEndElement();
            //open description tag
            xmlWriter.WriteStartElement("description");
            xmlWriter.WriteString("This search pattern was exported by SAR Technology: Pattern Commander");
            //close description tag
            xmlWriter.WriteEndElement();
            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "styleyy");
            //open LineStyle tag
            xmlWriter.WriteStartElement("LineStyle");
            //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("7f00ffff");
            //close color
            xmlWriter.WriteEndElement();
            //open width
            xmlWriter.WriteStartElement("width");
            xmlWriter.WriteString("7");
            //close width
            xmlWriter.WriteEndElement();
            //close LineStyle tag
            xmlWriter.WriteEndElement();
            //open PolyStyle
            xmlWriter.WriteStartElement("PolyStyle");
            //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("501400D2");
            //close color
            xmlWriter.WriteEndElement();
            //close PolyStyle
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();


            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "style");
            //open LineStyle tag
            xmlWriter.WriteStartElement("LineStyle");
            //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("501400D2");
            //close color
            xmlWriter.WriteEndElement();
            //open width
            xmlWriter.WriteStartElement("width");
            xmlWriter.WriteString("7");
            //close width
            xmlWriter.WriteEndElement();
            //close LineStyle tag
            xmlWriter.WriteEndElement();
            //open PolyStyle
            xmlWriter.WriteStartElement("PolyStyle");
            //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("501400D2");
            //close color
            xmlWriter.WriteEndElement();
            //close PolyStyle
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();

            for (int i = 1; i < points.Count; i++)
            {

                //open Placemark
                xmlWriter.WriteStartElement("Placemark");
                //open name
                xmlWriter.WriteStartElement("name");
                xmlWriter.WriteString("Relative");
                //close name
                xmlWriter.WriteEndElement();
                //open stylUrl
                xmlWriter.WriteStartElement("styleUrl");
                if (i % 2 == 0)
                    xmlWriter.WriteString("#styleyy");
                else
                    xmlWriter.WriteString("#style");
                //close stylUrl
                xmlWriter.WriteEndElement();
                //open LineString
                xmlWriter.WriteStartElement("LineString");
                //open extrude
                xmlWriter.WriteStartElement("extrude");
                xmlWriter.WriteString("1");
                //close extrude
                xmlWriter.WriteEndElement();
                //open tessellate
                xmlWriter.WriteStartElement("tesselate");
                xmlWriter.WriteString("1");
                //close tessellate
                xmlWriter.WriteEndElement();
                //open altitudeMode
                xmlWriter.WriteStartElement("altitudeMode");
                xmlWriter.WriteString("clampToGround");
                //close altitudeMode
                xmlWriter.WriteEndElement();
                //open coordinates
                xmlWriter.WriteStartElement("coordinates");
                //LOOPED COORDINATES GO HERE
                xmlWriter.WriteString(System.Convert.ToString(points[i-1].getLng()) + "," +
                    System.Convert.ToString(points[i-1].getLat()) + "," + " \n\t\t\t\t\t\t\t" +
                    System.Convert.ToString(points[i].getLng()) + "," +
                    System.Convert.ToString(points[i].getLat()) + "," + "\n\t\t\t\t");
                //close coordinates
                xmlWriter.WriteEndElement();
                //close LineString
                xmlWriter.WriteEndElement();
                //close Placemark
                xmlWriter.WriteEndElement();
            } 
            //end of for loop.

             //close Document tag
             xmlWriter.WriteEndElement();
             //close KML tag
             xmlWriter.WriteEndElement();

             //End the file.
             xmlWriter.WriteEndDocument();
             xmlWriter.Close();
         } 

        }
}
