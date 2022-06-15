public struct Entiqueta<T>{
    public Vertice<T> atual;
    public Vertice<T>? anterior = null;

    public Entiqueta(Vertice<T> atual) {
        this.atual = atual;
    }
}