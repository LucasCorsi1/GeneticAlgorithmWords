using GeneticSharp.Domain.Chromosomes;

using System;

namespace GeneticAlgorithmWords.GA
{
    public sealed class Chromosome : ChromosomeBase
    {
        public string genoms = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890, .-;:_!\"#%&/()=?@${[]}";
        public string _target { get; set; }

        public Chromosome(string target) : base(target.Length)
        {
            _target = target;
            CreateGenes();
        }

        public override Gene GenerateGene(int geneIndex)
        {
            var characteresGenom = genoms.ToCharArray();
            var rand = new Random();
            return new Gene(characteresGenom[rand.Next(characteresGenom.Length - 1)]);
        }

        public override IChromosome CreateNew()
        {
            return new Chromosome(_target);
        }
    }
}
