using NUnit.Framework;
using System.Collections.Generic;

namespace Lab3.Tests
{
    [TestFixture]
    public class SoldierFormationTests
    {
        private bool CheckFormationPossible(int n, int[][] pairs)
        {
            // Initialize the adjacency list and visited array
            List<int>[] adj = new List<int>[n + 1];
            int[] visited = new int[n + 1];

            for (int i = 0; i <= n; i++)
            {
                adj[i] = new List<int>();
            }

            // Build the graph from pairs
            foreach (var pair in pairs)
            {
                adj[pair[0]].Add(pair[1]);
            }

            // Check for cycles using DFS
            for (int i = 1; i <= n; i++)
            {
                if (visited[i] == 0)
                {
                    if (DFS(i, adj, visited))
                        return false; // Cycle found
                }
            }
            return true; // No cycles found
        }

        private bool DFS(int v, List<int>[] adj, int[] visited)
        {
            if (visited[v] == 1) return true;  // Cycle detected
            if (visited[v] == 2) return false; // Already fully explored

            visited[v] = 1; // Mark as being explored

            foreach (int u in adj[v])
            {
                if (DFS(u, adj, visited))
                    return true;
            }

            visited[v] = 2; // Mark as fully explored
            return false;
        }

        [Test]
        public void Test1_SingleSoldier_ShouldBePossible()
        {
            // Minimum case - single soldier
            int n = 1;
            int[][] pairs = new int[][] { };

            bool result = CheckFormationPossible(n, pairs);

            Assert.That(result, Is.True, "Single soldier should always be possible");
        }

        [Test]
        public void Test2_SimplePair_ShouldBePossible()
        {
            // Basic case - two soldiers with one relation
            int n = 2;
            int[][] pairs = new int[][] 
            {
                new int[] {1, 2}  // 1 is taller than 2
            };

            bool result = CheckFormationPossible(n, pairs);

            Assert.That(result, Is.True, "Simple pair arrangement should be possible");
        }

        [Test]
        public void Test3_SimpleChain_ShouldBePossible()
        {
            // Three soldiers in a chain
            int n = 3;
            int[][] pairs = new int[][]
            {
                new int[] {1, 2},
                new int[] {2, 3}  // 1 > 2 > 3
            };

            bool result = CheckFormationPossible(n, pairs);

            Assert.That(result, Is.True, "Chain formation should be possible");
        }

        [Test]
        public void Test4_SimpleCycle_ShouldBeImpossible()
        {
            // Three soldiers forming a cycle
            int n = 3;
            int[][] pairs = new int[][]
            {
                new int[] {1, 2},
                new int[] {2, 3},
                new int[] {3, 1}  // 1 > 2 > 3 > 1 (impossible)
            };

            bool result = CheckFormationPossible(n, pairs);

            Assert.That(result, Is.False, "Cyclic arrangement should be impossible");
        }

        [Test]
        public void Test5_NoRelations_ShouldBePossible()
        {
            // Multiple soldiers with no relations
            int n = 5;
            int[][] pairs = new int[][] { };

            bool result = CheckFormationPossible(n, pairs);

            Assert.That(result, Is.True, "Formation with no relations should be possible");
        }

        [Test]
        public void Test6_DisconnectedPairs_ShouldBePossible()
        {
            // Two separate pairs of relations
            int n = 4;
            int[][] pairs = new int[][]
            {
                new int[] {1, 2},
                new int[] {3, 4}  // Two independent pairs
            };

            bool result = CheckFormationPossible(n, pairs);

            Assert.That(result, Is.True, "Independent pairs should be possible");
        }

        [Test]
        public void Test7_ComplexValid_ShouldBePossible()
        {
            // Complex but valid arrangement
            int n = 5;
            int[][] pairs = new int[][]
            {
                new int[] {1, 2},
                new int[] {2, 3},
                new int[] {1, 3},
                new int[] {4, 5}
            };

            bool result = CheckFormationPossible(n, pairs);

            Assert.That(result, Is.True, "Complex valid arrangement should be possible");
        }

        [Test]
        public void Test8_ComplexCycle_ShouldBeImpossible()
        {
            // Complex arrangement with a cycle
            int n = 5;
            int[][] pairs = new int[][]
            {
                new int[] {1, 2},
                new int[] {2, 3},
                new int[] {3, 4},
                new int[] {4, 2}  // Creates a cycle
            };

            bool result = CheckFormationPossible(n, pairs);

            Assert.That(result, Is.False, "Complex cyclic arrangement should be impossible");
        }

        [Test]
        public void Test9_MaxSizeValid_ShouldBePossible()
        {
            // Maximum allowed size (100) with valid chain
            int n = 100;
            var pairs = new List<int[]>();
            for (int i = 1; i < 100; i++)
            {
                pairs.Add(new int[] { i, i + 1 });
            }

            bool result = CheckFormationPossible(n, pairs.ToArray());

            Assert.That(result, Is.True, "Maximum size chain should be possible");
        }

        [Test]
        public void Test10_SelfLoop_ShouldBeImpossible()
        {
            // Invalid case - soldier compared to themselves
            int n = 3;
            int[][] pairs = new int[][]
            {
                new int[] {1, 1}  // Self comparison
            };

            bool result = CheckFormationPossible(n, pairs);

            Assert.That(result, Is.False, "Self comparison should be impossible");
        }
    }
}