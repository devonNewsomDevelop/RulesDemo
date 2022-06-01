namespace RulesDemo.Core.Data
{
    /// <summary>
    /// Parameter for auto scenario rules
    /// </summary>
    public class AutoScenarioRuleParameter
    {
        public AutoScenarioRuleParameter()
        {
            ValueCollection = new List<string>();
        }
        /// <summary>
        /// Collection of values to do column comparisons against
        /// Such as checking if a task list Icd9Code is contained by the ValueCollection
        /// See AutoScenarioRules.json rule ContainsIcd9 for an example
        /// </summary>
        public List<string> ValueCollection { get; set; }
        /// <summary>
        /// A comparison date
        /// </summary>
        public DateTime? CompareDate { get; set; }
        /// <summary>
        /// Name of the column that is being evaluated
        /// Used as parameter variable name
        /// See usage in AutoScenarioRules.json in rule ContainsIcd9AndDateReportedEarlier
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// Low value param for range comparisons
        /// </summary>
        public short? RangeLow { get; set; }
        /// <summary>
        /// High value param for range comparisons
        /// </summary>
        public short? RangeHigh { get; set; }
    }
}
