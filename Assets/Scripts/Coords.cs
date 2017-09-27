﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coords
{
    public struct PolarCoord
    {
        public float R;
        public float Phi;

        public PolarCoord(float r, float phi)
        {
            R = r;
            Phi = phi;
        }

        public CartesianCoord ToCartesian()
        {
            return new CartesianCoord(R * Mathf.Cos(Phi), R * Mathf.Sin(Phi));
        }
    }

    public struct CartesianCoord
    {
        public float X;
        public float Y;

        public CartesianCoord(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public PolarCoord ToPolar()
        {
            // x = r*cos(phi)
            // x/r = cos(phi)
            // acos x/r = acos (cos(phi))
            // acos x/r = phi

            // x^2 + y^2 = r^2
            // sqrt(x^2 + y^2) = sqrt(r^2)
            // sqrt(x^2 + y^2) = r 

            float r = Mathf.Sqrt(X * X + Y * Y);
            float phi = Mathf.Acos(X / r);

            return new PolarCoord(r, phi);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, 0);
        }
    }
}
