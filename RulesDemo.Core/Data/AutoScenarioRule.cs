namespace RulesDemo.Core.Data
{
    public class AutoScenarioRule
    {
        public AutoScenarioRule()
        {
            Parameters = new List<AutoScenarioRuleParameter>();
        }
        public List<AutoScenarioRuleParameter> Parameters { get; set; }
        public string Name { get; set; }
        public string AutoScenario { get; set; }
    }
}
