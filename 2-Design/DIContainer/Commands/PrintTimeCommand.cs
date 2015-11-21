using System;

namespace DIContainer.Commands
{
    public class PrintTimeCommand : BaseCommand
    {
        public override void Execute()
        {
            Console.WriteLine(DateTime.Now);
        }
    }
}