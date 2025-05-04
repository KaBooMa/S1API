namespace S1API.PhoneCalls.Constants
{
    /// <summary>
    /// Defines when associated actions (like variable or quest setters) should be triggered based on condition evaluation result.
    /// </summary>
    public enum EvaluationType
    {
        /// <summary>
        /// Trigger actions when the condition evaluates to true.
        /// </summary>
        PassOnTrue,
        /// <summary>
        /// Trigger actions when the condition evaluates to false.
        /// </summary>
        PassOnFalse
    }
}
