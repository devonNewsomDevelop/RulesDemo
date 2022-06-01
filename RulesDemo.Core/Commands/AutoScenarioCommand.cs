using RulesDemo.Core.Data;

namespace RulesDemo.Core.Commands
{
    public class AutoScenarioCommand
    {
        public ElrTaskList Task { get; set; }
        public AutoScenarioRule Rule { get; set; }
        public short Retries { get; set; } = 0;
    }
}
