using System;
using System.Diagnostics;
using System.Threading;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Reinsertions;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;

namespace GeneticAlgorithmWords.GA
{
    public class GeneticAlgorithmWords
    {
        private GeneticAlgorithm _ga;

        public event Action StateHasChanged;

        private Timer _timer;

        private Stopwatch _stopwatch;
        private bool IsRunning => _timer != null;

        public double Fitness => _ga != null && _ga.BestChromosome?.Fitness != null ? _ga.BestChromosome.Fitness.Value : 0;
        public int GenerationsNumber => _ga != null ? _ga.GenerationsNumber : 0;
        public Gene[] Genes => _ga?.BestChromosome?.GetGenes();
        public string Counter => _stopwatch != null ? _stopwatch.Elapsed.ToString(@"\.hh\:mm\:ss") : "00:00:00";

        public void StartGeneticAlgo(string target)
        {
            var selection = new EliteSelection();
            var crossover = new ThreeParentCrossover();
            var mutation = new ReverseSequenceMutation();
            var fitness = new MyFitness { Target = target };
            var chromosome = new Chromosome(target);
            var population = new Population(500, 1000, chromosome);
            _stopwatch = Stopwatch.StartNew();

            _ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation)
            {
                Termination = new GenerationNumberTermination(300),
                MutationProbability = float.MaxValue
            };
            _timer = new Timer(new TimerCallback(_ =>
            {
                _ga.Termination = new GenerationNumberTermination(_ga.GenerationsNumber + 1);
                if (_ga.GenerationsNumber > 0)
                    _ga.Resume();
                else
                    _ga.Start();

                var teste = string.Empty;
                for (var i = 0; i < _ga.BestChromosome.GetGenes().Length; i++)
                {
                    teste += _ga.BestChromosome.GetGenes()[i].Value;
                    if (teste == target)
                    {
                        Stop();
                        _stopwatch.Stop();
                    }
                }
                StateHasChanged?.Invoke();
            }), null, 0, 1);
        }

        private void Stop()
        {
            if (!IsRunning) return;
            _timer.Dispose();
            _timer = null;
            _ga.Stop();
        }

    }
}
