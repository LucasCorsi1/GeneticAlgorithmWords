using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

namespace GeneticAlgorithmWords.GA
{
    public class MyFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            var gene = chromosome.GetGene(0);
            var characteresTarget = Genoms._target.ToCharArray();
            var characteresGenom = gene.Value.ToString()?.ToCharArray();
            double fitness = 0;
            for (var i = 0; i < characteresTarget.Length; i++)
            {
                if (characteresGenom != null && characteresGenom[i] != characteresTarget[i])
                    fitness++;
            }
            return fitness;
        }
    }
}
