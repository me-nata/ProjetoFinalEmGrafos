using System.Collections.Generic;
using System.Linq;

public abstract class Grafo<T> {
    public List<Vertice<T>> vertices;
    protected bool ponderado;
    
    public abstract void Connect(int v1, int v2, float weight=1);

    public Grafo(bool ponderado) {
        this.vertices = new List<Vertice<T>>();
        this.ponderado = ponderado;
    }

    public void Add(string label, T value) {
        vertices.Add(new Vertice<T>(label, value));
    }
    public void Add(T value) {
        vertices.Add(new Vertice<T>("No Label", value));
    }

    public void Remove(int pos) {
        vertices.RemoveAt(pos);
    }

    public void clearPaint() {
        vertices.ForEach(vertice => vertice.alternPaint());
    }

    public void Show() {
        int i = 0;
        foreach(var v in vertices){
            Console.Write($"({v.label}) -> [");
            if(this.ponderado) ShowAdjacentesWithWeight(v); else ShowAdjacentes(v);
            Console.WriteLine("]");
            i++;
        }
    }

    private void ShowAdjacentesWithWeight(Vertice<T> v) {
        foreach(ArestaPonderada<T> adj in v.adjacentes) {
            int pos = vertices.IndexOf(adj.vertice);
            Console.Write($" ({adj.vertice.label}, w={adj.weight}) ");
        }
    }

    private void ShowAdjacentes(Vertice<T> v) {
        foreach(ArestaPonderada<T> adj in v.adjacentes) {
            int pos = vertices.IndexOf(adj.vertice);
            Console.Write($" ({pos}) ");
        }
    }
    
    //LARGURA
    public List<T> BuscaEmLargura(int start) {
        var output = new List<T>();
        var queue = new Queue<Vertice<T>>();

        queue.push(vertices[start]);
        vertices[start].paint();

        while(queue.isNotEmpty()) {
            var current_vertice = queue.first();
            output.Add(current_vertice.content);

            foreach (var aresta in current_vertice.adjacentes) {
                var next_vertice = aresta.vertice;

                if(next_vertice.isNotPainted()) {
                    next_vertice.paint();
                    queue.push(next_vertice);
                }
            }
            
            queue.pop();
        }

        return output;
    }

    //PROFUNDIDADE
    public List<T> BuscaEmProfundidade(int start) {
        var output = new List<T>();
        var stack = new Stack<Vertice<T>>();
        
        stack.push(vertices[start]);
        
        while(stack.isNotEmpty()) {
            var current_vertice = stack.get();

            if(current_vertice.isNotPainted()) {
                current_vertice.paint();
                output.Add(current_vertice.content);
            }

            var next_vertice = current_vertice.getAdjacentNotPainted();
            if(next_vertice == null) stack.pop(); else stack.push(next_vertice);
        }

        return output;
    }

    public bool hasVerticeNotPainted() {
        bool output = false;    
        for(int i = 0; i<vertices.Count && !output; i++) {
            output = output || vertices[i].isNotPainted();
        }

        return output;
    }
}

public class GrafoDirecionado<T> : Grafo<T> {
    public override void Connect(int v1, int v2, float weight=1){
        vertices[v1].Connect(vertices[v2], this.ponderado, weight);  
    }
    public void Connect(Vertice<T> v1, Vertice<T> v2, float weight=1){
        v1.Connect(v2, this.ponderado, weight);
    }

    // public void Connect(T value1, T value2, float? weight=null) {
    //     Vertice<T>? v1 = null, v2 = null;
    //     foreach(var v in vertices) {
    //         if(v.content!.Equals(value1)) {
    //             v1 = v; 
    //         } else if(v.content!.Equals(value2)) {
    //             v2 = v;
    //         }
    //     }

    //     this.Connect(v1!, v2!, weight);
    // }
    
    public bool Contain(T value) {
        bool output = false;
        for(int i = 0; i < vertices.Count && !output; i++) {
            if(vertices[i].Equals(value)) {
                output=true;
            }
        }

        return output;
    }

    public GrafoDirecionado(bool ponderado) : base(ponderado) {}
}

public class GrafoNaoDirecionado<T> : Grafo<T> {
    public override void Connect(int v1, int v2, float weight=1){
        vertices[v1].Connect(vertices[v2], this.ponderado, weight);
        vertices[v2].Connect(vertices[v1], this.ponderado, weight);    
    }

    public GrafoNaoDirecionado(bool ponderado) : base(ponderado) {}
}