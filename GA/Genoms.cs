using GeneticSharp.Domain.Chromosomes;

namespace GeneticAlgorithmWords.GA
{
    public sealed class Genoms : ChromosomeBase
    {
        public  string genoms = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ 1234567890, .-;:_!\"#%&/()=?@${[]}";
        public string _target;
        public Genoms(string target) : base(100)
        {
            _target = target;
            CreateGenes();
        }

        public override Gene GenerateGene(int geneIndex)
        {
            var characteresTarget = _target.ToCharArray();
            var characteresGenom = genoms.ToCharArray();
            var gnome = string.Empty;
            for (var i = 0; i < characteresTarget.Length; i++)
            {
                gnome += characteresGenom[i];
            }
            return new Gene(gnome);
        }

        public override IChromosome CreateNew()
        {
            return new Genoms(_target);
        }
    }
}
