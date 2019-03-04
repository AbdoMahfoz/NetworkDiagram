using System;
using NetworkDiagramCore;

namespace NetworkDiagramConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkDiagram networkDiagram = new NetworkDiagram();
            do
            {
                NetworkDiagramEntity Entity = new NetworkDiagramEntity();
                Console.Write("Entity Title = ");
                Entity.Title = Console.ReadLine();
                Console.Write("Entity Duration = ");
                Entity.Duration = int.Parse(Console.ReadLine());
                networkDiagram.AddEntity(Entity);
                Console.WriteLine("Do you want to input another entity? (Y/N)");
            } while (char.ToLower(Console.ReadKey(true).KeyChar) == 'y');
            Console.Clear();
            NetworkDiagramEntity[] Entities = networkDiagram.GetAllEntites();
            for (int i = 0; i < Entities.Length; i++)
            {
                Console.WriteLine($"{i} - {Entities[i].Title}");
            }
            Console.WriteLine("Please define relations between entites by\n" +
                              "typing the number of the parent entity followed by" +
                              "the number of the child entity in a sinlge line" +
                              "type \"-1\" (without the quotes) to finish");
            while(true)
            {
                string line = Console.ReadLine();
                if(line.Trim() == "-1")
                {
                    break;
                }
                int[] arr = Array.ConvertAll(line.Split(' '), int.Parse);
                networkDiagram.SetRelation(arr[0], arr[1]);
            }
            networkDiagram.Solve();
            Console.Clear();
            foreach(var Entity in networkDiagram.GetAllEntites())
            {
                Console.WriteLine($"{Entity.Title}:\n\tDuration = {Entity.Duration}\n\tEarliest Start = {Entity.EarliestStart}\n\t" +
                                  $"Earliest Finish = {Entity.EarliestFinish}\n\tLatest Start = {Entity.LatestStart}\n\t" +
                                  $"Latest Finish = {Entity.LatestFinish}\n\tFloat = {Entity.TotalFloat}\n");
            }
        }
    }
}
