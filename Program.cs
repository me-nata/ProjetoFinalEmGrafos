const int qtdPessoasNaRede = 100;
const int qtdInteracoesPorPessoa = 27;
const int qtdAtributos = 10;
const int intervaloAtributos = 2;
const int pertinenciaComunidade = 5;
const int pertinenciaCentroide = 25;


void q01() {
    Console.WriteLine("\n===Questao 01===");
    GrafoDirecionado<Pessoa> usuarios = new GrafoDirecionado<Pessoa>(ponderado: true);

    // Crir usuarios
    for(int i = 0; i < qtdPessoasNaRede; i++) {
        usuarios.Add(new Pessoa(qtdAtributos: qtdAtributos, intervaloAtributos: intervaloAtributos));
    }

    // Relacionar usuarios
    var rand = new Random();
    foreach(var vertice in usuarios.vertices) {
        int relacionamento = qtdInteracoesPorPessoa;
        while(relacionamento-- > 0) {
            var seguidor = usuarios.vertices[rand.Next(qtdPessoasNaRede)];
            if(seguidor.isNotPainted()) {
                usuarios.Connect(seguidor, vertice);
                seguidor.paint();
                vertice.grauEntrada++;
            }
        }

        usuarios.clearPaint();
    }

    // Listar centroides e calcular proximidade de todos os vertices para todos os vertices adjacentes
    var centroides = new List<Vertice<Pessoa>>();
    foreach(var vertice in usuarios.vertices) {
        if(vertice.grauEntrada > pertinenciaCentroide) {
            centroides.Add(vertice);
        }
        foreach(ArestaPonderada<Pessoa> aresta in vertice.adjacentes) {
            int proximidade = 0;
            for(int pos = 0; pos < qtdAtributos; pos++) {
                proximidade += Math.Abs(vertice.content.preferencias[pos]-aresta.vertice.content.preferencias[pos]);
            }

            aresta.weight = proximidade;
        }
    }

    // Formar comunidades
    var comunidades = new List<FaClube>();
    foreach(var vertice in usuarios.vertices) {
        foreach(ArestaPonderada<Pessoa> aresta in vertice.adjacentes) {
            if(centroides.Contains(aresta.vertice)) {
                if(aresta.weight <= pertinenciaComunidade) {
                    bool comunidadeExiste = false;
                    for(int i = 0; i < comunidades.Count && !comunidadeExiste; i++) {
                        if(comunidades[i].centro.Equals(aresta.vertice)) {
                            comunidades[i].comunidade.Add(vertice);
                            comunidadeExiste = true;
                        }
                    }

                    if(!comunidadeExiste) {
                        var novoFaClube = new FaClube(aresta.vertice);
                        novoFaClube.comunidade.Add(vertice);
                        comunidades.Add(novoFaClube);
                    }
                }
            }
        }
    }

    // Mostrar comunidades
    Console.WriteLine("\n\nCOMUNIDADES\nSeguidores\tSeguidores Fieis");
    foreach(var faclube in comunidades) {
        Console.Write(faclube.centro.grauEntrada);
        Console.WriteLine($"\t\t{faclube.comunidade.Count}");
    }
}

void q02(GrafoDirecionado<float> g, int origem, int fim) {
    Console.WriteLine("\n===Questao 02===");
    Console.WriteLine("Para o grafo:");
    g.Show();

    var queue = new Queue<Vertice<float>>();

    queue.push(g.vertices[origem]);
    g.vertices[origem].paint();
    g.vertices[origem].content = 1;

    float peso;
    while(queue.isNotEmpty()) {
        var current_vertice = queue.first();
        peso = current_vertice.content;

        foreach (ArestaPonderada<float> aresta in current_vertice.adjacentes) {
            var next_vertice = aresta.vertice;      

            if(next_vertice.isNotPainted()) {
                next_vertice.paint();
                next_vertice.content = peso+1;
                queue.push(next_vertice);
            }

            aresta.weight = peso;
        }
        
        queue.pop();
    }

    var caminhos = new List<List<String>>();
    bool acessoFim;
    do {
        acessoFim = false;
        var borda = new List<Vertice<float>>();

        g.vertices[origem].dist = 0;

        queue = new Queue<Vertice<float>>();
        queue.push(g.vertices[origem]);
        while(queue.isNotEmpty() && !queue.last().Equals(g.vertices[fim])) {
            foreach(ArestaPonderada<float> aresta in queue.first().adjacentes) {
                if(aresta.isNotPainted() && (aresta.vertice.dist == float.MaxValue || aresta.vertice.dist >= queue.first().dist+aresta.weight)) {
                    aresta.vertice.dist = queue.first().dist+aresta.weight;
                    aresta.vertice.returns = queue.first();
                    borda.Add(aresta.vertice);
                }
            }

            Vertice<float>? min = null;
            foreach(Vertice<float> v in borda) {
                if(min == null || v.dist < min.dist) {
                    min = v;
                }
            }
            borda.Remove(min!);
            
            if(min != null) {
                queue.push(min);
            }

            if(queue.last() == g.vertices[fim]) {
                acessoFim = true;
            } 

            queue.pop();
        }

        if(acessoFim) {
            var solucao = new List<String>();
            Stack<Vertice<float>> stack = new Stack<Vertice<float>>();
            
            var aux = g.vertices[fim];
            while(aux.returns != null) {
                foreach(var aresta in aux.returns.adjacentes) {
                    if(aresta.vertice == aux) {
                        aresta.paint();
                        stack.push(aux);
                        break;
                    }
                }

                aux = aux.returns;              
            }
            stack.push(g.vertices[origem]);

            while(stack.isNotEmpty()) {
                stack.get().returns = null;
                stack.get().dist = float.MaxValue;
                solucao.Add(stack.get().label);
                stack.pop();
            }

            caminhos.Add(solucao);
        }
    } while(acessoFim);

    Console.WriteLine($"Foi/Foram encontrado(s) {caminhos.Count} caminho(s)");
    foreach(var lista in caminhos) {
        Console.Write("(");
        foreach(var label in lista) {
            Console.Write($" {label} ");
        }
        Console.Write(")");
        Console.WriteLine();
    }
}

GrafoDirecionado<float> g1 = new GrafoDirecionado<float>(ponderado: true);
GrafoDirecionado<float> g2 = new GrafoDirecionado<float>(ponderado: true);
GrafoDirecionado<float> g3 = new GrafoDirecionado<float>(ponderado: true);
g1.Add("A", 0f);
g1.Add("B", 0f);
g1.Add("C", 0f);
g1.Add("D", 0f);
g1.Add("E", 0f);
g1.Add("F", 0f);

g1.Connect(0, 1);
g1.Connect(0, 2);
g1.Connect(1, 2);
g1.Connect(1, 3);
g1.Connect(2, 4);
g1.Connect(3, 5);
g1.Connect(4, 3);
g1.Connect(4, 5);

g2.Add("A", 0f);
g2.Add("B", 0f);
g2.Add("C", 0f);
g2.Add("D", 0f);
g2.Add("E", 0f);
g2.Add("F", 0f);

g2.Connect(0, 1);
g2.Connect(0, 2);
g2.Connect(0, 3);
g2.Connect(1, 2);
g2.Connect(2, 3);
g2.Connect(2, 4);
g2.Connect(3, 4);
g2.Connect(4, 5);
g2.Connect(3, 5);

g3.Add("A", 0f);
g3.Add("B", 0f);
g3.Add("C", 0f);
g3.Add("D", 0f);
g3.Add("E", 0f);
g3.Add("F", 0f);

g3.Connect(0, 1);
g3.Connect(0, 3);
g3.Connect(0, 4);
g3.Connect(1, 3);
g3.Connect(1, 2);
g3.Connect(1, 4);
g3.Connect(2, 5);
g3.Connect(3, 2);
g3.Connect(3, 5);
g3.Connect(4, 2);
g3.Connect(4, 5);

q01();
q02(g3, 0, 5);