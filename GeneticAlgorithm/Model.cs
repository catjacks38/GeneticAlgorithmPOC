using System;
using System.Collections.Generic;

namespace Model
{
    class LinearModel
    {
        // Variables of a linear function
        public float m = 0.0f;
        public float b = 0.0f;

        // Just a simple linear function
        // f(x) = mx + b
        // Simple algebra stuff

        public float forward(float x)
        {
            return (m * x) + b;
        }
    }
}
