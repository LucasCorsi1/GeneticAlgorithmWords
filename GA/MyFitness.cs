using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;

namespace GeneticAlgorithmWords.GA
{
    public class MyFitness : IFitness
    {
        public string Target { get; set; }

        public double Evaluate(IChromosome chromosome)
        {
            var characteresTarget = Target.ToCharArray();
            var genes = chromosome.GetGenes();
            var count = 0;
            double fitness = 0;
            foreach (var gene in genes)
            {
                if (Convert.ToChar(gene.Value) == characteresTarget[count++])
                    fitness++;
            }

            return fitness;
        }
    }
}
