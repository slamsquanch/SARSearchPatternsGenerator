using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SARSearchPatternGenerator
{
    /// <summary>
    /// Takes a pattern and writes it to a file in KML format.
    /// </summary>
    class KML : FileConverter
    {
        private Pattern p;
        private Boolean airMode = false;  //Flag for using "absolute" with a set altitude of 10,000m.  Off by default.  
        private Boolean boundingBox = false; //Flag for drawing "bounding box" around pattern.  Off by default. 
        private double altitude = 300.0;  //default elevation for ground search mode.

        //Constructor takes in a pattern instance.
        public KML(Pattern p)
        {
            this.p = p;
        }


        //Turns on the flag for using "absolute" with a set altitude of 10,000m.
        public void airModeOn()
        {
            airMode = true;
        }


        //Turns off the flag for using "absolute" with a set altitude of 10,000m.
        public void airModeOff()
        {
            airMode = false;
        }


        //Turns on the flag for drawing "bounding box" around pattern.
        public void boundingBoxOn()
        {
            boundingBox = true;
        }


        //Turns off the flag for drawing "bounding box" around pattern.
        public void boundingBoxOff()
        {
            boundingBox = false;
        }


        /*
         *  Sets the altitude to what the user input for elevation. 
         */
        public void setAltitude(double input)
        {
            this.altitude = input;
        }



        /*
        *  Returns the altitude to whatever the user input for elevation.
        *  If the user didn't enter an altitude, the default is 300m. 
        */
        public double getAltitude()
        {
            return this.altitude;
        }




        /*
         * Saves the pattern KML route file to a file path specified by name.
         */
        public void writeFile(String filePath)
        {
            Color[] colours = p.getColours();
            List<Coordinate> points = p.getPattern();
            Char delimiter = '.';
            String[] fileName = filePath.Split(delimiter);
            filePath = fileName[0];

            //Create the "route" file
            XmlWriter xmlWriter = startKMLFile(filePath + "_rte.kml", points);

            //Create the "waypoint" file
            writeWptFile(filePath, points);

            //Create the "bounding box" file if option selected.
            if (boundingBox)  
                writeBoundingBoxFile(filePath, points);


            //Styles: colours of lines etc.
            setupStyles(xmlWriter);
           

            for (int i = 1; i < points.Count;  i++)
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
                    xmlWriter.WriteString(selectColour(colours[(i - 1) % colours.Length]));
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
                if (!airMode)
                    xmlWriter.WriteString("relativeToGround");
                else
                {
                    xmlWriter.WriteString("absolute");
                    setAltitude(10000.0);
                }
                         //close altitudeMode
                xmlWriter.WriteEndElement();
                        //open coordinates
                xmlWriter.WriteStartElement("coordinates");
                        //LOOPED COORDINATES GO HERE
                xmlWriter.WriteString(System.Convert.ToString(points[i-1].getLng()) + "," +
                    System.Convert.ToString(points[i-1].getLat()) + "," +
                    System.Convert.ToString(getAltitude()) + " \n\t\t\t\t\t\t\t" +
                    System.Convert.ToString(points[i].getLng()) + "," +
                    System.Convert.ToString(points[i].getLat()) + "," +
                    System.Convert.ToString(getAltitude()) + "\n\t\t\t\t");
                         //close coordinates
                xmlWriter.WriteEndElement();
                    //close LineString
                xmlWriter.WriteEndElement();
                //close Placemark
                xmlWriter.WriteEndElement();

            }
            //end of for loop.

            //Close off the outer tags and file.
            endKMLfile(xmlWriter);
         }



        /*
         * Saves the KML waypoint file to a file path specified by name.
         */
        private void writeWptFile(String filePath, List<Coordinate> points)
        {
            XmlWriter xmlWriter = startKMLFile(filePath + "_wpt.kml", points);
            Coordinate datum = p.getDatum();

             //START POINT BELOW 
            //Open Placemark
            xmlWriter.WriteStartElement("Placemark");
                //Open name
            xmlWriter.WriteStartElement("name");
            xmlWriter.WriteString("Start");
                //Close name
            xmlWriter.WriteEndElement();
                //Open Point
            xmlWriter.WriteStartElement("Point");
                    //Open coordinates
            xmlWriter.WriteStartElement("coordinates");
            xmlWriter.WriteString(
                System.Convert.ToString(points[0].getLng()) + "," +
                System.Convert.ToString(points[0].getLat()) + "," +
                System.Convert.ToString(getAltitude()));
                    //Close coordinates
            xmlWriter.WriteEndElement();
            //Open altitudeMode
            xmlWriter.WriteStartElement("altitudeMode");
            xmlWriter.WriteString("relativeToGround");
                    //Close altitudeMode
            xmlWriter.WriteEndElement();
                    //Open extrude
            xmlWriter.WriteStartElement("extrude");
            xmlWriter.WriteString("1");
                    //Close extrude
            xmlWriter.WriteEndElement();
                //Close Point
            xmlWriter.WriteEndElement();
            //Close Placemark
            xmlWriter.WriteEndElement();

            //DATUM POINT BELOW
            //Open Placemark
            xmlWriter.WriteStartElement("Placemark");
                //Open name
            xmlWriter.WriteStartElement("name");
            xmlWriter.WriteString("Datum");
                //Close name
            xmlWriter.WriteEndElement();
            //Open Point
            xmlWriter.WriteStartElement("Point");
                    //Open coordinates
            xmlWriter.WriteStartElement("coordinates");
            xmlWriter.WriteString(
                System.Convert.ToString(p.getDatum().getLng()) + "," +
                System.Convert.ToString(p.getDatum().getLat()) + "," +
                System.Convert.ToString(getAltitude()));
                    //Close coordinates
            xmlWriter.WriteEndElement();
                    //Open altitudeMode
            xmlWriter.WriteStartElement("altitudeMode");
            xmlWriter.WriteString("relativeToGround");
                    //Close altitudeMode
            xmlWriter.WriteEndElement();
                    //Open extrude
            xmlWriter.WriteStartElement("extrude");
            xmlWriter.WriteString("1");
                    //Close extrude
            xmlWriter.WriteEndElement();
                    //Close Point
            xmlWriter.WriteEndElement();
            //Close Placemark
            xmlWriter.WriteEndElement();

            //Close off the outer tags and file.
            endKMLfile(xmlWriter);

        }


        /*
         * Saves the KML bounding box file to a file path specified by name.
         */
        private void writeBoundingBoxFile(String filePath, List<Coordinate> points)
        {
            XmlWriter xmlWriter = startKMLFile(filePath + "_bbox.kml", points);
            Coordinate datum = p.getDatum();
            //Get only the name of the file that the user chose and not the directory path.
            String name = extractName(filePath);

            //Styles: colours of lines etc.
            setupStyles(xmlWriter);

            //Open Placemark
            xmlWriter.WriteStartElement("Placemark");
                //Open name
            xmlWriter.WriteStartElement("name");
            xmlWriter.WriteString("bounding box");
                //Close name
            xmlWriter.WriteEndElement();
                //open stylUrl
            xmlWriter.WriteStartElement("styleUrl");
            xmlWriter.WriteString("#boundingBoxStyle");
                //Close stylUrl
            xmlWriter.WriteEndElement();
                //Open Polygon
            xmlWriter.WriteStartElement("Polygon");
                    //Open altitudeMode
            xmlWriter.WriteStartElement("altitudeMode");
            xmlWriter.WriteString("relativeToGround");
                    //Close altitudeMode
            xmlWriter.WriteEndElement();
                    //Open extrude
            xmlWriter.WriteStartElement("extrude");
            xmlWriter.WriteString("1");
                    //Close extrude
            xmlWriter.WriteEndElement();
            //Open outerBoundaryIs
            xmlWriter.WriteStartElement("outerBoundaryIs");
                        //Open LinearRing
            xmlWriter.WriteStartElement("LinearRing");
                            //Open coordinates
            xmlWriter.WriteStartElement("coordinates");
            xmlWriter.WriteString( "\n\t\t\t\t" +
                System.Convert.ToString(p.minLong()) + "," + 
                System.Convert.ToString(p.maxLat()) + "," +
                System.Convert.ToString(altitude) + "\n\t\t\t\t" +
                System.Convert.ToString(p.maxLong()) + "," +
                System.Convert.ToString(p.maxLat()) + "," +
                System.Convert.ToString(altitude) + "\n\t\t\t\t" +
                System.Convert.ToString(p.maxLong()) + "," +
                System.Convert.ToString(p.minLat()) + "," +
                System.Convert.ToString(altitude) + "\n\t\t\t\t" +
                System.Convert.ToString(p.minLong()) + "," +
                System.Convert.ToString(p.minLat()) + "," +
                System.Convert.ToString(altitude) + "\n\t\t\t\t" +
                System.Convert.ToString(p.minLong()) + "," +
                System.Convert.ToString(p.maxLat()) + "," +
                System.Convert.ToString(altitude) + "\n\t\t\t\t"
                );
                            //Close coordinates
            xmlWriter.WriteEndElement();
                        //Close LinearRing
            xmlWriter.WriteEndElement();
                    //Close outerBoundaryIs
            xmlWriter.WriteEndElement();
                //Close Polygon
            xmlWriter.WriteEndElement();
            //Close Placemark
            xmlWriter.WriteEndElement();

            //Close off the outer tags and file.
            endKMLfile(xmlWriter);

        }




        /*
         * Creates the KML file. sets up indented formatting, names and description.
         */
        private XmlWriter startKMLFile(String filePath, List<Coordinate> points)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";

            //Create the "route" file
            XmlWriter xmlWriter = XmlWriter.Create(filePath, settings);  //Makes file and formats indentation
            xmlWriter.WriteStartDocument(); //Starts the document


            //Get only the name of the file that the user chose and not the directory path.
            String name = extractName(filePath);


            //open KML tag
            xmlWriter.WriteStartElement("kml", "http://www.opengis.net/kml/2.2");
                //open Document tag
            xmlWriter.WriteStartElement("Document");
                    //open name tag
            xmlWriter.WriteStartElement("name");
            xmlWriter.WriteString(name);
                    //close name tag
            xmlWriter.WriteEndElement();
                    //open description tag
            xmlWriter.WriteStartElement("description");
            xmlWriter.WriteString("Search Pattern created by SAR Technology - 'Pattern Commander'");
                    //close description tag
            xmlWriter.WriteEndElement();

            return xmlWriter;
        }



        /*
        *  Saftely closes off the KML file that we are writing to.
        */
        private void endKMLfile(XmlWriter xmlWriter)
        {
                //close Document tag
            xmlWriter.WriteEndElement();
            //close KML tag
            xmlWriter.WriteEndElement();

            //End the file.
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }



        /*
         * Sets up the different line colours and type that will be used in the file export.
         */
        private void setupStyles(XmlWriter xmlWriter)
        {
            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "boundingBoxStyle");
                //open LineStyle tag
            xmlWriter.WriteStartElement("LineStyle");
                    //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("5a140096");
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
            xmlWriter.WriteString("3c787878");
                    //close color
            xmlWriter.WriteEndElement();
                //close PolyStyle
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();


            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "Red");
                //open LineStyle tag
            xmlWriter.WriteStartElement("LineStyle");
                    //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("641400E6");
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
            xmlWriter.WriteString("4b1400E6");
                    //close color
            xmlWriter.WriteEndElement();
                //close PolyStyle
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();


            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "Blue");
            //open LineStyle tag
            xmlWriter.WriteStartElement("LineStyle");
            //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("64F00014");
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
            xmlWriter.WriteString("4bF00014");
            //close color
            xmlWriter.WriteEndElement();
            //close PolyStyle
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();


            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "Yellow");
            //open LineStyle tag
            xmlWriter.WriteStartElement("LineStyle");
            //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("6414E7FF");
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
            xmlWriter.WriteString("4b14E7FF");
            //close color
            xmlWriter.WriteEndElement();
            //close PolyStyle
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();


            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "Magenta");
            //open LineStyle tag
            xmlWriter.WriteStartElement("LineStyle");
            //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("64FF78B4");
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
            xmlWriter.WriteString("4bFF78B4");
            //close color
            xmlWriter.WriteEndElement();
            //close PolyStyle
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();

            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "Green");
            //open LineStyle tag
            xmlWriter.WriteStartElement("LineStyle");
            //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("64009614");
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
            xmlWriter.WriteString("4b009614");
            //close color
            xmlWriter.WriteEndElement();
            //close PolyStyle
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();


            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "Cyan");
            //open LineStyle tag
            xmlWriter.WriteStartElement("LineStyle");
            //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("64F0DC14");
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
            xmlWriter.WriteString("4bF0DC14");
            //close color
            xmlWriter.WriteEndElement();
            //close PolyStyle
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();

        }

    }
}
