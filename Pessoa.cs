public class Pessoa {
    public List<int> preferencias = new List<int>();

    public Pessoa(int qtdAtributos, int intervaloAtributos) {
        var random = new Random();
        for(int i = 0; i < qtdAtributos; i++) {
            preferencias.Add(random.Next(0, intervaloAtributos));
        }
    }
}