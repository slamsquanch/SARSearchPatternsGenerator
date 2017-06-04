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
        private Boolean airMode = true;  //Flag for using "absolute" with a set altitude of 10,000m.  Off by default.  
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
                xmlWriter.WriteString(selectColour(colours[(i - 1) % colours.Length]));
                    //close stylUrl
                xmlWriter.WriteEndElement();
                    //open LineString
                xmlWriter.WriteStartElement("LineString");
                        //open altitudeMode
                xmlWriter.WriteStartElement("altitudeMode");
                if (!airMode)
                {
                    xmlWriter.WriteString("clampToGround");
                    setAltitude(0);
                }
                else
                {
                    xmlWriter.WriteString("absolute");
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

            //Setup the styles for marker icons.
            setupWptStyles(xmlWriter);

             //START POINT BELOW 
             //Open Placemark
            xmlWriter.WriteStartElement("Placemark");
                //Open name
            xmlWriter.WriteStartElement("name");
            xmlWriter.WriteString("Start");
                //Close name
            xmlWriter.WriteEndElement();
                //Open styleUrl
            xmlWriter.WriteStartElement("styleUrl");
            xmlWriter.WriteString("#msn_blu-blank");
                //Close styleUrl
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
            if (!airMode)
            {
                xmlWriter.WriteString("clampToGround");
                setAltitude(0);
            }
            else
            {
                xmlWriter.WriteString("absolute");
            }
                    //Close altitudeMode
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
                //Open styleUrl
            xmlWriter.WriteStartElement("styleUrl");
            xmlWriter.WriteString("#msn_D");
                //Close styleUrl
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
            xmlWriter.WriteString("absolute");
                    //Close altitudeMode
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
            //Bounding Box style below.

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

            //LINE COLOURS BELOW.

            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "Red");
                //open LineStyle tag
            xmlWriter.WriteStartElement("LineStyle");
                    //open color
            xmlWriter.WriteStartElement("color");
            xmlWriter.WriteString("FF1400E6");
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
            xmlWriter.WriteString("00FFFFF");
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
            xmlWriter.WriteString("FFF00014");
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
            xmlWriter.WriteString("00FFFFF");
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
            xmlWriter.WriteString("FF14E7FF");
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
            xmlWriter.WriteString("00FFFFF");
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
            xmlWriter.WriteString("FFFF78B4");
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
            xmlWriter.WriteString("00FFFFF");
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
            xmlWriter.WriteString("FF009614");
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
            xmlWriter.WriteString("00FFFFF");
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
            xmlWriter.WriteString("FFF0DC14");
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
            xmlWriter.WriteString("00FFFFF");
            //close color
            xmlWriter.WriteEndElement();
            //close PolyStyle
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();
        }




        /*
        * Sets up the Start and Datum marker icons that will be used in the wpt file export.
        */
        private void setupWptStyles(XmlWriter xmlWriter)
        {
            //DATUM icon - "D" (red) -Scale 1.3
            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "sn_D");
                //open IconStyle tag
            xmlWriter.WriteStartElement("IconStyle");
                    //open scale tag
            xmlWriter.WriteStartElement("scale");
            xmlWriter.WriteString("1.3");
                    //close scale tag
            xmlWriter.WriteEndElement();
                    //open Icon tag
            xmlWriter.WriteStartElement("Icon");
                        //open href tag
            xmlWriter.WriteStartElement("href");
            xmlWriter.WriteString("http://maps.google.com/mapfiles/kml/paddle/D.png");
                        //close href tag
            xmlWriter.WriteEndElement();
                    //close Icon tag
            xmlWriter.WriteEndElement();
                    //open hotSpot tag
            xmlWriter.WriteStartElement("hotSpot");
            xmlWriter.WriteAttributeString("x", "32");
            xmlWriter.WriteAttributeString("y", "1");
            xmlWriter.WriteAttributeString("xunits", "pixels");
            xmlWriter.WriteAttributeString("yunits", "pixels");
                    //close hotSpot tag
            xmlWriter.WriteEndElement();
                //close IconStyle tag
            xmlWriter.WriteEndElement();
                //open ListStyle tag
            xmlWriter.WriteStartElement("ListStyle");
                    //open ItemIcon tag
            xmlWriter.WriteStartElement("ItemIcon");
                        //open href tag
            xmlWriter.WriteStartElement("href");
            xmlWriter.WriteString("http://maps.google.com/mapfiles/kml/paddle/D-lv.png");
                        //close href tag
            xmlWriter.WriteEndElement();
                    //close ItemIcon tag
            xmlWriter.WriteEndElement();
                //close ListStyle tag
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();


            //DATUM icon - "D" (red) -Scale 1.1
            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "sn_D");
            //open IconStyle tag
            xmlWriter.WriteStartElement("IconStyle");
            //open scale tag
            xmlWriter.WriteStartElement("scale");
            xmlWriter.WriteString("1.1");
            //close scale tag
            xmlWriter.WriteEndElement();
            //open Icon tag
            xmlWriter.WriteStartElement("Icon");
            //open href tag
            xmlWriter.WriteStartElement("href");
            xmlWriter.WriteString("http://maps.google.com/mapfiles/kml/paddle/D.png");
            //close href tag
            xmlWriter.WriteEndElement();
            //close Icon tag
            xmlWriter.WriteEndElement();
            //open hotSpot tag
            xmlWriter.WriteStartElement("hotSpot");
            xmlWriter.WriteAttributeString("x", "32");
            xmlWriter.WriteAttributeString("y", "1");
            xmlWriter.WriteAttributeString("xunits", "pixels");
            xmlWriter.WriteAttributeString("yunits", "pixels");
            //close hotSpot tag
            xmlWriter.WriteEndElement();
            //close IconStyle tag
            xmlWriter.WriteEndElement();
            //open ListStyle tag
            xmlWriter.WriteStartElement("ListStyle");
            //open ItemIcon tag
            xmlWriter.WriteStartElement("ItemIcon");
            //open href tag
            xmlWriter.WriteStartElement("href");
            xmlWriter.WriteString("http://maps.google.com/mapfiles/kml/paddle/D-lv.png");
            //close href tag
            xmlWriter.WriteEndElement();
            //close ItemIcon tag
            xmlWriter.WriteEndElement();
            //close ListStyle tag
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();




            //START icon (blue) -Scale 1.1
            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "sn_blu-blank");
            //open IconStyle tag
            xmlWriter.WriteStartElement("IconStyle");
            //open scale tag
            xmlWriter.WriteStartElement("scale");
            xmlWriter.WriteString("1.1");
            //close scale tag
            xmlWriter.WriteEndElement();
            //open Icon tag
            xmlWriter.WriteStartElement("Icon");
            //open href tag
            xmlWriter.WriteStartElement("href");
            xmlWriter.WriteString("http://maps.google.com/mapfiles/kml/paddle/blu-blank.png");
            //close href tag
            xmlWriter.WriteEndElement();
            //close Icon tag
            xmlWriter.WriteEndElement();
            //open hotSpot tag
            xmlWriter.WriteStartElement("hotSpot");
            xmlWriter.WriteAttributeString("x", "32");
            xmlWriter.WriteAttributeString("y", "1");
            xmlWriter.WriteAttributeString("xunits", "pixels");
            xmlWriter.WriteAttributeString("yunits", "pixels");
            //close hotSpot tag
            xmlWriter.WriteEndElement();
            //close IconStyle tag
            xmlWriter.WriteEndElement();
            //open ListStyle tag
            xmlWriter.WriteStartElement("ListStyle");
            //open ItemIcon tag
            xmlWriter.WriteStartElement("ItemIcon");
            //open href tag
            xmlWriter.WriteStartElement("href");
            xmlWriter.WriteString("http://maps.google.com/mapfiles/kml/paddle/blu-blank-lv.png");
            //close href tag
            xmlWriter.WriteEndElement();
            //close ItemIcon tag
            xmlWriter.WriteEndElement();
            //close ListStyle tag
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();


            //START icon (blue) -Scale 1.3
            //open Style tag
            xmlWriter.WriteStartElement("Style");
            xmlWriter.WriteAttributeString("id", "sn_blu-blank");
            //open IconStyle tag
            xmlWriter.WriteStartElement("IconStyle");
            //open scale tag
            xmlWriter.WriteStartElement("scale");
            xmlWriter.WriteString("1.3");
            //close scale tag
            xmlWriter.WriteEndElement();
            //open Icon tag
            xmlWriter.WriteStartElement("Icon");
            //open href tag
            xmlWriter.WriteStartElement("href");
            xmlWriter.WriteString("http://maps.google.com/mapfiles/kml/paddle/blu-blank.png");
            //close href tag
            xmlWriter.WriteEndElement();
            //close Icon tag
            xmlWriter.WriteEndElement();
            //open hotSpot tag
            xmlWriter.WriteStartElement("hotSpot");
            xmlWriter.WriteAttributeString("x", "32");
            xmlWriter.WriteAttributeString("y", "1");
            xmlWriter.WriteAttributeString("xunits", "pixels");
            xmlWriter.WriteAttributeString("yunits", "pixels");
            //close hotSpot tag
            xmlWriter.WriteEndElement();
            //close IconStyle tag
            xmlWriter.WriteEndElement();
            //open ListStyle tag
            xmlWriter.WriteStartElement("ListStyle");
            //open ItemIcon tag
            xmlWriter.WriteStartElement("ItemIcon");
            //open href tag
            xmlWriter.WriteStartElement("href");
            xmlWriter.WriteString("http://maps.google.com/mapfiles/kml/paddle/blu-blank-lv.png");
            //close href tag
            xmlWriter.WriteEndElement();
            //close ItemIcon tag
            xmlWriter.WriteEndElement();
            //close ListStyle tag
            xmlWriter.WriteEndElement();
            //close style tag
            xmlWriter.WriteEndElement();


            //STYLE MAP for blue start
            //open StyleMap tag
            xmlWriter.WriteStartElement("StyleMap");
            xmlWriter.WriteAttributeString("id", "msn_blu-blank");
                //open Pair tag
            xmlWriter.WriteStartElement("Pair");
                    //open key tag
            xmlWriter.WriteStartElement("key");
            xmlWriter.WriteString("normal");
                    //close key tag
            xmlWriter.WriteEndElement();
                    //open styleUrl tag
            xmlWriter.WriteStartElement("styleUrl");
            xmlWriter.WriteString("#sn_blu-blank");
                    //close styleUrl tag
            xmlWriter.WriteEndElement();
                //close Pair tag
            xmlWriter.WriteEndElement();
                //open Pair tag
            xmlWriter.WriteStartElement("Pair");
                    //open key tag
            xmlWriter.WriteStartElement("key");
            xmlWriter.WriteString("highlight");
                    //close key tag
            xmlWriter.WriteEndElement();
                    //open styleUrl tag
            xmlWriter.WriteStartElement("styleUrl");
            xmlWriter.WriteString("#sh_blu-blank");
                    //close styleUrl tag
            xmlWriter.WriteEndElement();
                //close Pair tag
            xmlWriter.WriteEndElement();
            //close StyleMap tag
            xmlWriter.WriteEndElement();


            //STYLE MAP for red Datum "D"
            //open StyleMap tag
            xmlWriter.WriteStartElement("StyleMap");
            xmlWriter.WriteAttributeString("id", "msn_D");
            //open Pair tag
            xmlWriter.WriteStartElement("Pair");
            //open key tag
            xmlWriter.WriteStartElement("key");
            xmlWriter.WriteString("normal");
            //close key tag
            xmlWriter.WriteEndElement();
            //open styleUrl tag
            xmlWriter.WriteStartElement("styleUrl");
            xmlWriter.WriteString("#sn_D");
            //close styleUrl tag
            xmlWriter.WriteEndElement();
            //close Pair tag
            xmlWriter.WriteEndElement();
            //open Pair tag
            xmlWriter.WriteStartElement("Pair");
            //open key tag
            xmlWriter.WriteStartElement("key");
            xmlWriter.WriteString("highlight");
            //close key tag
            xmlWriter.WriteEndElement();
            //open styleUrl tag
            xmlWriter.WriteStartElement("styleUrl");
            xmlWriter.WriteString("#sh_D");
            //close styleUrl tag
            xmlWriter.WriteEndElement();
            //close Pair tag
            xmlWriter.WriteEndElement();
            //close StyleMap tag
            xmlWriter.WriteEndElement();

        }

    }
}
