using System.Collections.Generic;

public class Vertice<T> : Element {
    public T content;
    public List<Aresta<T>> adjacentes;
    public float dist = float.MaxValue;
    public Vertice<T>? returns = null;
    public int grauEntrada = 0;
    private int index_get_adj = 0;
    private int iterator_count = 0;

    public void Connect(Vertice<T> v, bool ponderado, float weight=1) {
        if(ponderado) {
            adjacentes.Add(new ArestaPonderada<T>(this, v, weight));
        } else {
            adjacentes.Add(new Aresta<T>(this, v));
        }
    }

    public Vertice(string label, T content) : base(label, false) {
        this.content = content;
        adjacentes = new List<Aresta<T>>();
    }

    public bool hasAdjacents() {
        return adjacentes.Count != 0;
    }

    public Vertice<T>? getAdjacentNotPainted() {
        Vertice<T>? adjacent = null;

        foreach(var aresta in adjacentes) {
            if(aresta.vertice.isNotPainted()) {
                adjacent = aresta.vertice;
                break;
            }
        }

        return adjacent;
    }

    public Vertice<T>? getAdjacent() {
        Vertice<T>? output = null;
        
        do {
            output = adjacentes[index_get_adj++].vertice;
            if(output == null) adjacentes.RemoveAt(index_get_adj-1);
            if(index_get_adj == adjacentes.Count) index_get_adj = 0;
        } while(adjacentes.Count != 0 && output == null);

        return output;
    }

    public bool iteratorAdjacent(ref Vertice<T>? v) {
        bool output = false;

        if(adjacentes.Count > 0) {
            v = getAdjacent();
            if(v != null) {
                iterator_count++;
                output = true;
            }
        }

        if(iterator_count >= adjacentes.Count){
            iterator_count = 0;
            output = false;
        }

        return output;
    }
}