using RulesDemo.Core.Commands;
using RulesDemo.Core.Data;
using RulesEngine.Models;

namespace RulesDemo.Core.Services
{
    /// <summary>
    /// Services for auto import scenarios
    /// </summary>
    public class AutoScenarioService : IAutoScenarioService
    {
        private List<string> workFlowRules;

        private readonly short maxRetries = 3;

        public AutoScenarioService()
        {
            // One example of how we could retrieve the json for the rules
            // Could also store in database or simply define it here as a string
            workFlowRules = new List<string>();
            using (StreamReader reader = new StreamReader("../../../Resources/AutoScenarioRules.json"))
            {
                workFlowRules.Add(reader.ReadToEnd());
            }
        }

        /// <summary>
        /// Uses rule engine to process a rule along a list of parameters from the command and a single ElrTaskList item.
        /// </summary>
        /// <param name="command">The command containing the rule, the task, and the auto scenario for passing the rule</param>
        /// <returns>
        /// The event resulting from executing the auto scenario rules.
        /// This could either be the auto scenario, failure (failed to pass the rules),
        /// or error due to problems parsing / executing the json rules or other runtime issues
        /// </returns>
        public async Task<AutoScenarioResult> GetAutoScenario(AutoScenarioCommand command)
        {
            try
            {
                // Builds engine from rules json and creates rule parameters from command
                var rulesEngine = new RulesEngine.RulesEngine(workFlowRules.ToArray(), null);

                List<RuleParameter> ruleParameters = new List<RuleParameter> { new RuleParameter("task", command.Task) };

                foreach (var parameter in command.Rule.Parameters)
                {
                    ruleParameters.Add(new RuleParameter(parameter.ColumnName, parameter));
                }

                // Executes the specific rule defined in the command with our parameters
                var result = await rulesEngine.ExecuteActionWorkflowAsync("AutoScenarioRules", command.Rule.Name, ruleParameters.ToArray());

                // If the rule succeeded, return / emit the auto scenario
                if (result != null && !result.Results.Any(r => !r.IsSuccess))
                {
                    return new AutoScenarioResult { Command = command, Result = command.Rule.AutoScenario };
                }

                // If there is an error within the parser that caused the failure, throw it to catch handler
                if (result.Exception != null)
                {
                    throw result.Exception;
                }

                // Errors can also be nested
                if (result.Results.Any(r => !string.IsNullOrEmpty(r.ExceptionMessage))) {
                    var exceptionResult = result.Results.First(r => !string.IsNullOrEmpty(r.ExceptionMessage));
                    var exception = new Exception(exceptionResult.ExceptionMessage);
                    throw exception;
                }

                // Otherwise, return / emit a failure to pass (distinct from an error)
                return new AutoScenarioResult { Command = command, Result = AutoScenarioType.Failure };
            }
            catch (Exception ex)
            {
                // Can do additional error handling with caught exception here, like logging

                // If the rule error'd but hasn't exceeded max retries, increment and return / emit a retry with the command
                if (command.Retries < maxRetries)
                {
                    command.Retries++;
                    return new AutoScenarioResult { Command = command, Result = AutoScenarioType.Retry };
                }

                // If the rule has failed more than our max retries return / emit an error state with the command
                return new AutoScenarioResult { Command = command, Result = AutoScenarioType.Error };
            }
        }
    }
}
