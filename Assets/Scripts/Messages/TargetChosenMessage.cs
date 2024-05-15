namespace Messages
{
    public struct TargetChosenMessage
    {
        public readonly string Target;

        public TargetChosenMessage(string target)
        {
            Target = target;
        }
    }
}