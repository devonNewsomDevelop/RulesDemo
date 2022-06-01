using RulesDemo.Core.Commands;
using RulesDemo.Core.Data;

namespace RulesDemo.Core.Services
{
    public interface IAutoScenarioService
    {
        public Task<AutoScenarioResult> GetAutoScenario(AutoScenarioCommand command);
    }
}
