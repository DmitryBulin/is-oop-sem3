using Banks.Console.State;

namespace Banks.Console.Comand;

public class ComandParser : IComandHandler
{
    private readonly List<IComandHandler> _subHandlers = new List<IComandHandler>();

    private ComandParser(string prefix, List<IComandHandler> subHandlers)
    {
        Prefix = prefix;
        _subHandlers = subHandlers;
    }

    public string Prefix { get; }

    public static ComandBuilder Builder(string prefix) => new ComandBuilder(prefix);

    public string Handle(string input, IStateMachine stateMachine)
    {
        if (Prefix.Equals("help") && input.Equals("help"))
        {
            return HelpInfo(string.Empty);
        }

        IComandHandler? handler = _subHandlers.FirstOrDefault(handler => input.StartsWith(handler.Prefix));
        if (handler is null)
        {
            return "Failed to find comand";
        }

        input = input.Replace(handler.Prefix, string.Empty);
        input = input.Trim();

        return handler.Handle(input, stateMachine);
    }

    public string HelpInfo(string seed)
    {
        string response = $"{seed}{Prefix}\n";
        seed = $"{seed}* ";
        _subHandlers.ForEach(handler => response += $"{handler.HelpInfo(seed)}");
        return response;
    }

    public class ComandBuilder
    {
        private readonly List<IComandHandler> _subHandlers = new List<IComandHandler>();
        private readonly string _prefix;

        public ComandBuilder(string prefix)
        {
            _prefix = prefix;
        }

        public ComandBuilder WithSubHandler(IComandHandler handler)
        {
            _subHandlers.Add(handler);
            return this;
        }

        public ComandParser Build()
        {
            return new ComandParser(_prefix, _subHandlers);
        }
    }
}
