using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataContract.Geographical
{
    public class CalculateDistanceDto
    {
        public double SourceX { get; set; }
        public double SourceY { get; set; }
        public double DestinationX { get; set; }
        public double DestinationY { get; set; }
       
    }
}
