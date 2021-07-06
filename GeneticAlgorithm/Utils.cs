using System;
using System.Collections.Generic;
using Model;

namespace Utils
{
    static class Utils
    {
        public static LinearModel[] GenInitialPopulation(int size, (int, int) rngRange)
        {
            LinearModel[] models = new LinearModel[size];
            Random rng = new Random();

            for (int i = 0; i < models.Length; i++)
            {
                models[i] = new LinearModel();

                models[i].m = (float) rng.NextDouble() * (rngRange.Item1 - rngRange.Item2) + rngRange.Item2;
                models[i].b = (float) rng.NextDouble() * (rngRange.Item1 - rngRange.Item2) + rngRange.Item2;
            }

            return models;
        }
    }

}
