using System;
using System.Collections.Generic;
using Model;
using static Utils.Utils;
using static Loss.Loss;
using Optimizer;

namespace GeneticAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            int generations = 900;
            List<(float, float)> trainSet = new List<(float, float)> { (0.0f, -600.0f), (1.0f, -300.0f), (2.0f, 0.0f), (3.0f, 300.0f), (4.0f, 600.0f) };

            GeneticOptimizer optim = new GeneticOptimizer(0.5f, GenInitialPopulation(250, (-255, 255)));


            for (int generation = 0; generation < generations; generation++)
            {
                float averageLoss = 0.0f;
                (LinearModel, LinearModel) bestModels = optim.FindBothFittestModels(trainSet);

                foreach (LinearModel model in optim.Models)
                {
                    averageLoss += MSELoss(trainSet, model);
                }

                averageLoss /= optim.Models.Length;

                Console.WriteLine("---------------------------------");
                Console.WriteLine("Generation: " + generation);
                Console.WriteLine("Average Loss: " + averageLoss);
                Console.WriteLine("\nTop 2 Models: ");
                Console.WriteLine("1. M: " + bestModels.Item1.m + "  B: " + bestModels.Item1.b + "  L: " + MSELoss(trainSet, bestModels.Item1));
                Console.WriteLine("2. M: " + bestModels.Item2.m + "  B: " + bestModels.Item2.b + "  L: " + MSELoss(trainSet, bestModels.Item2));

                optim.Evolve(trainSet);
            }

            Console.WriteLine("---------------------------------");

            (LinearModel, LinearModel) fittestModels = optim.FindBothFittestModels(trainSet);

            Console.WriteLine("Final Model: \nM: " + (fittestModels.Item1.m + fittestModels.Item2.m) / 2 + "  B: " + (fittestModels.Item1.b + fittestModels.Item2.b) / 2);
        }
    }
}
