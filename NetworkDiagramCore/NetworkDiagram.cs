using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDiagramCore
{
    public class NetworkDiagram
    {
        private readonly List<NetworkDiagramEntity> Entites = new List<NetworkDiagramEntity>();
        private readonly List<List<int>> ForwardAdjList = new List<List<int>>();
        private readonly List<List<int>> BackwardAdjList = new List<List<int>>();
        private bool IsSolved = false;
        public void Solve()
        {
            for(int i = 0; i < Entites.Count; i++)
            {
                Entites[i] = new NetworkDiagramEntity()
                {
                    Title = Entites[i].Title,
                    Duration = Entites[i].Duration
                };
            }
            int[] dp = new int[Entites.Count + 5];
            void ForwardPropagation(int i)
            {
                dp[i] = 2;
                int MaxFinish = 1, MaxLevel = 0;
                foreach(int j in BackwardAdjList[i])
                {
                    if(dp[j] != 2)
                    {
                        ForwardPropagation(j);
                    }
                    MaxFinish = Math.Max(MaxFinish, Entites[j].EarliestFinish + 1);
                    MaxLevel = Math.Max(MaxLevel, Entites[j].Level + 1);
                }
                Entites[i].Level = MaxLevel;
                Entites[i].EarliestStart = MaxFinish;
                Entites[i].EarliestFinish = Entites[i].EarliestStart + Entites[i].Duration - 1;
            }
            for(int i = 0; i < Entites.Count; i++)
            {
                if(ForwardAdjList[i].Count == 0)
                {
                    ForwardPropagation(i);
                }
            }
            int MinStart = Entites.Max(e => e.EarliestFinish);
            void BackwardPropagation(int i)
            {
                dp[i] = 3;
                int minStart = MinStart;
                foreach (int j in ForwardAdjList[i])
                {
                    if(dp[j] != 3)
                    {
                        BackwardPropagation(j);
                    }
                    minStart = Math.Min(minStart, Entites[j].LatestStart - 1);
                }
                Entites[i].LatestFinish = minStart;
                Entites[i].LatestStart = Entites[i].LatestFinish - Entites[i].Duration + 1;
            }
            for(int i = 0; i < Entites.Count; i++)
            {
                if(BackwardAdjList[i].Count == 0)
                {
                    BackwardPropagation(i);
                }
            }
            IsSolved = true;
        }
        public int AddEntity(NetworkDiagramEntity Entity)
        {
            Entites.Add(Entity);
            ForwardAdjList.Add(new List<int>());
            BackwardAdjList.Add(new List<int>());
            return Entites.Count - 1;
        }
        public void SetRelation(NetworkDiagramEntity Parent, NetworkDiagramEntity Child)
        {
            SetRelation(Entites.IndexOf(Parent), Entites.IndexOf(Child));
        }
        public void SetRelation(int ParentIndex, int ChildIndex)
        {
            ForwardAdjList[ParentIndex].Add(ChildIndex);
            BackwardAdjList[ChildIndex].Add(ParentIndex);
        }
        public NetworkDiagramEntity[] GetAllEntites()
        {
            return Entites.ToArray();
        }
    }
}
