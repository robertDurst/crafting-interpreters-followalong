using Granola.Commandline;

namespace crafting_interpreters_followalong
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cl = new Commandline();
            cl.Invoke(args);
        }
    }
}
