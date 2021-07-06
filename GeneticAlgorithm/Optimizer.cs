using System;
using System.Collections.Generic;
using Model;
using static Loss.Loss;

namespace Optimizer
{
    class GeneticOptimizer
    {
        public float MaxStep = 0.0f;
        public LinearModel[] Models;

        public GeneticOptimizer(float maxStep, LinearModel[] models)
        {
            MaxStep = maxStep;
            Models = models;
        }

        public (LinearModel, LinearModel) FindBothFittestModels(List<(float, float)> trainSet)
        {
            (LinearModel, LinearModel) fittestModels = (Models[0], Models[0]);
            int fittestModelIDX = 0;

            for (int i = 0; i < Models.Length; i++)
            {
                if (MSELoss(trainSet, Models[i]) < MSELoss(trainSet, fittestModels.Item1))
                {
                    fittestModels.Item1 = Models[i];
                    fittestModelIDX = i;
                }
            }

            for (int i = 0; i < Models.Length; i++)
            {
                if (i == fittestModelIDX) continue;

                if (MSELoss(trainSet, Models[i]) < MSELoss(trainSet, fittestModels.Item2))
                {
                    fittestModels.Item2 = Models[i];
                }
            }

            return fittestModels;
        }

        public void Evolve(List<(float, float)> trainSet)
        {
            Random rng = new Random();

            (LinearModel, LinearModel) bestFitModels = FindBothFittestModels(trainSet);

            float meanM = (bestFitModels.Item1.m + bestFitModels.Item2.m) / 2;
            float meanB = (bestFitModels.Item1.b + bestFitModels.Item2.b) / 2;

            for (int i = 0; i < Models.Length; i++)
            {
                Models[i].m = meanM + (float) (rng.NextDouble() * (MaxStep * 2)) - MaxStep;
                Models[i].b = meanB + (float) (rng.NextDouble() * (MaxStep * 2)) - MaxStep;
            }
        }
    }
}
