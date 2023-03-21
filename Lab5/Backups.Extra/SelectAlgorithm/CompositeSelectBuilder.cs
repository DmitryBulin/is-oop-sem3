using Backups.Backup;

namespace Backups.Extra.SelectAlgorithm;

public class CompositeSelectBuilder
{
    public static CompositeBuilder OrFilter => new CompositeBuilder(Combine);
    public static CompositeBuilder AndFilter => new CompositeBuilder(SatysfyAll);

    private static IReadOnlyList<RestorePoint> Combine(IReadOnlyList<ISelectAlgorithm> algorithms, IReadOnlyList<RestorePoint> restorePoints)
    {
        return algorithms.SelectMany(algorithm => algorithm.SelectBlacklist(restorePoints)).Distinct().ToList();
    }

    private static IReadOnlyList<RestorePoint> SatysfyAll(IReadOnlyList<ISelectAlgorithm> algorithms, IReadOnlyList<RestorePoint> restorePoints)
    {
        IReadOnlyList<RestorePoint> result = restorePoints;

        foreach (ISelectAlgorithm algorithm in algorithms)
        {
            result = algorithm.SelectBlacklist(result);
            if (result.Count == 0)
            {
                break;
            }
        }

        return result;
    }

    public class CompositeBuilder
    {
        private readonly List<ISelectAlgorithm> _algorithms = new List<ISelectAlgorithm>();
        private readonly Func<
            IReadOnlyList<ISelectAlgorithm>,
            IReadOnlyList<RestorePoint>,
            IReadOnlyList<RestorePoint>> _filter;

        public CompositeBuilder(Func<
            IReadOnlyList<ISelectAlgorithm>,
            IReadOnlyList<RestorePoint>,
            IReadOnlyList<RestorePoint>> filter)
        {
            _filter = filter;
        }

        public ISelectAlgorithm Build()
        {
            if (_algorithms.Count == 0)
            {
                throw new Exception();
            }

            return new Filter(_algorithms, _filter);
        }

        public CompositeBuilder With(ISelectAlgorithm algorithm)
        {
            _algorithms.Add(algorithm);
            return this;
        }
    }

    private class Filter : ISelectAlgorithm
    {
        private readonly List<ISelectAlgorithm> _algorithms;
        private readonly Func<
            IReadOnlyList<ISelectAlgorithm>,
            IReadOnlyList<RestorePoint>,
            IReadOnlyList<RestorePoint>> _filter;

        public Filter(
            List<ISelectAlgorithm> algorithms,
            Func<
                IReadOnlyList<ISelectAlgorithm>,
                IReadOnlyList<RestorePoint>,
                IReadOnlyList<RestorePoint>> filter)
        {
            _algorithms = algorithms;
            _filter = filter;
        }

        public IReadOnlyList<RestorePoint> SelectBlacklist(IReadOnlyList<RestorePoint> restorePoints) => _filter.Invoke(_algorithms, restorePoints);
    }
}
