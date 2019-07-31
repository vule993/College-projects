///////////////////////////////////////////////////////////
//  Position.cs
//  Implementation of the Class Position
//  Generated by Enterprise Architect
//  Created on:      05-May-2019 22:35:11
//  Original author: vule9
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace QueueApp.Common
{
    [Serializable]
    public class Position
    {

        private double x;
        private double y;
        private double z;

        public Position(double x = 0, double y = 0, double z = 0)
        {
            if (x > Double.MaxValue || x < Double.MinValue || y > Double.MaxValue || y < Double.MinValue || z > Double.MaxValue || z < Double.MinValue)
            {
                throw new ArgumentOutOfRangeException($"Parametri moraju biti >= {Double.MinValue} i <= {Double.MaxValue}");
            }

            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Position()
        {

        }

        ~Position()
        {

        }


        //id
        public int PositionId { get; set; }

        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }

        public override string ToString()
        {
            return $@"[{X}   {Y}   {Z}]";
        }

    }//end Position
}