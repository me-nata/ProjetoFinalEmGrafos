public class Aresta<T> : Element {
    public Vertice<T>? saida;
    public Vertice<T> vertice;

    public Aresta(Vertice<T> saida, Vertice<T> adjacente) : base("No label", false) {
        this.vertice = adjacente;
        this.saida = saida;
    }
}

public class ArestaPonderada<T> : Aresta<T> {
    public float weight;

    public ArestaPonderada(Vertice<T> saida, Vertice<T> adjacente, float w=1) : base(saida, adjacente) {
        this.weight = w; 
    }
}
