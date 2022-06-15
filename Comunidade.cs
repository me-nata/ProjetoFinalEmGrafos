
public struct FaClube {
    public Vertice<Pessoa> centro;
    public List<Vertice<Pessoa>> comunidade;

    public FaClube(Vertice<Pessoa> centro) {
        comunidade = new List<Vertice<Pessoa>>();
        this.centro = centro;
    }
}
