using System;
using System.Text;

namespace Practice01_Graphs.models;

internal class Bag<T>
{
}

class Graph {

    readonly int _V;
    int _E;
    Bag<int>[] _adj;

    Graph(int V) {
        if (V < 0) throw new ArithmeticException("Number of vertices must be non-negative");
        _V = V;
        _E = 0;
        _adj = (Bag<int>[])new Bag[V];
        for (int v = 0; v < V; v++) {
            _adj[v] = new Bag<int>();
        }
    }

    Graph(Graph G) {
        _V = G.V();
        _E = G.E();
        if (_V < 0) throw new ArithmeticException("Number of vertices must be non-negative");

        // update adjacency lists
        _adj = (Bag<int>[])new Bag[_V];
        for (int v = 0; v < _V; v++) {
            _adj[v] = new Bag<int>();
        }

        for (int v = 0; v < G.V(); v++) {
            // reverse so that adjacency list is in same order as original
            Stack<int> reverse = new Stack<int>();
            for (int w : G._adj[v]) {
                reverse.push(w);
            }
            for (int w : reverse) {
                _adj[v].add(w);
            }
        }
    }

    int V() {
        return V;
    }

    int E() {
        return E;
    }

    private void validateVertex(int v) {
        if (v < 0 || v >= _V)
            throw new ArithmeticException("vertex " + v + " is not between 0 and " + (V - 1));
    }

    void addEdge(int v, int w) {
        validateVertex(v);
        validateVertex(w);
        _E++;
        _adj[v].add(w);
        _adj[w].add(v);
    }

    Iterable<int> _adj(int v) {
        validateVertex(v);
        return _adj[v];
    }

    int degree(int v) {
        validateVertex(v);
        return _adj[v].size();
    }

    String toString() {
        StringBuilder s = new StringBuilder();
        s.Append(V + " vertices, " + E + " edges " + NEWLINE);
        for (int v = 0; v < V; v++) {
            s.Append(v + ": ");
            for (int w : _adj[v]) {
                s.Append(w + " ");
            }
            s.Append(NEWLINE);
        }
        return s.toString();
    }
    String toDot() {
        StringBuilder s = new StringBuilder();
        s.Append("graph {" + NEWLINE);
        s.Append("node[shape=circle, style=filled, fixedsize=true, width=0.3, fontsize=\"10pt\"]" + NEWLINE);
        int selfLoops = 0;
        for (int v = 0; v < V; v++) {
            for (int w : _adj[v]) {
                if (v < w) {
                    s.Append(v + " -- " + w + NEWLINE);
                }
                else if (v == w) {
                    // include only one copy of each self loop (self loops will be consecutive)
                    if (selfLoops % 2 == 0) {
                        s.Append(v + " -- " + w + NEWLINE);
                    }
                    selfLoops++;
                }
            }
        }
        s.Append("}" + NEWLINE);
        return s.toString();
    }

    static void main(String[] args) {
        File entrada = new File(args[0]);
        Graph G = new Graph(entrada);
        Console.WriteLine(G);
    }

}