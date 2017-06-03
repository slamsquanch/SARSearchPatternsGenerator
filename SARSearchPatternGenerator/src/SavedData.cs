using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SARSearchPatternsGenerator.src
{
    [DataContract]
    public class SavedData
    {
        [DataMember]
        public string PointToPointComment, ParallelTrackComment, ExpandingSquareComment, SectorSearchComment;
        [DataMember]
        public int unitSystem, coordinateSystem, patternType;

        public SavedData()
        {
            PointToPointComment = (string)DefaultComments.ResourceManager.GetObject("PointToPointComment");
            ParallelTrackComment = (string)DefaultComments.ResourceManager.GetObject("ParallelTrackComment");
            ExpandingSquareComment = (string)DefaultComments.ResourceManager.GetObject("ExpandingSquareComment");
            SectorSearchComment = (string)DefaultComments.ResourceManager.GetObject("SectorSearchComment");
        }
    }
}
