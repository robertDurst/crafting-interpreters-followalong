using Granola.Lexar;

namespace crafting_interpreters_followalong
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = "Examples/ScannerExamples.granola";
            Lexar lexar = new Lexar(filepath);
            
            Token token;
            while(lexar.TryGetNextToken(out token))
            {
                token.DisplaySelf();
            }
        }
    }
}
