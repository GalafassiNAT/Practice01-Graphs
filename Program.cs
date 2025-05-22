using Practice01_Graphs.models;

class Program
{
    static void Main(String[] args)
    {
        File entrada = new File(args[0]);
        Graph G = new Graph(entrada);
        Console.WriteLine(G);
    }
}
