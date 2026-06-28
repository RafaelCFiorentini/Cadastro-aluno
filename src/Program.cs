using System;

// ============================================================
//  Classe principal com todas as operações
// ============================================================
class SistemaAlunos
{
    // Lista linear manual com capacidade para 100 alunos
    private static ListaLinear lista = new ListaLinear(100);

    // --------------------------------------------------------
    //  Ponto de entrada do programa
    // --------------------------------------------------------
    static void Main(string[] args)
    {
        int opcao;

        do
        {
            ExibirMenu();
            opcao = LerInteiro("Escolha uma opção: ");

            switch (opcao)
            {
                case 1: CadastrarAluno();   break;
                case 2: ListarAlunos();     break;
                case 3: BuscarSequencial(); break;
                case 4: OrdenarLista();     break;
                case 5: BuscarBinaria();    break;
                case 6: BuscarRecursiva();  break;
                case 0: Console.WriteLine("\nSaindo... Até logo!"); break;
                default: Console.WriteLine("\nOpção inválida. Tente novamente."); break;
            }

        } while (opcao != 0);
    }

    // --------------------------------------------------------
    //  Exibe o menu principal
    // --------------------------------------------------------
    static void ExibirMenu()
    {
        Console.WriteLine("\n===== SISTEMA DE ALUNOS =====");
        Console.WriteLine("1 - Cadastrar aluno");
        Console.WriteLine("2 - Listar alunos");
        Console.WriteLine("3 - Buscar por código (Pesquisa Sequencial)");
        Console.WriteLine("4 - Ordenar lista por código");
        Console.WriteLine("5 - Buscar por código (Pesquisa Binária)");
        Console.WriteLine("6 - Busca recursiva (Pesquisa Binária Recursiva)");
        Console.WriteLine("0 - Sair");
    }

    // --------------------------------------------------------
    //  1. Cadastrar aluno
    //  Insere no final do array manualmente.
    //  Complexidade: O(n) — verifica duplicata percorrendo a lista
    // --------------------------------------------------------
    static void CadastrarAluno()
    {
        Console.WriteLine("\n--- Cadastro de Aluno ---");

        if (lista.EstaCheia())
        {
            Console.WriteLine("Erro: lista cheia. Capacidade máxima atingida.");
            return;
        }

        int codigo = LerInteiro("Código: ");

        // Percorre o array para verificar se o código já existe
        if (lista.CodigoExiste(codigo))
        {
            Console.WriteLine("Erro: já existe um aluno com esse código.");
            return;
        }

        Console.Write("Nome: ");
        string nome = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(nome))
        {
            Console.WriteLine("Erro: nome não pode ser vazio.");
            return;
        }

        double nota = LerDouble("Nota (0,0 a 10,0): ");

        if (nota < 0 || nota > 10)
        {
            Console.WriteLine("Erro: nota deve estar entre 0,0 e 10,0.");
            return;
        }

        lista.Inserir(new Aluno(codigo, nome, nota));
        Console.WriteLine("Aluno cadastrado com sucesso!");
    }

    // --------------------------------------------------------
    //  2. Listar todos os alunos
    //  Percorre o array do índice 0 até quantidade-1.
    //  Complexidade: O(n)
    // --------------------------------------------------------
    static void ListarAlunos()
    {
        Console.WriteLine("\n--- Lista de Alunos ---");

        if (lista.EstaVazia())
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }

        // Percorre manualmente o array até a posição ocupada
        for (int i = 0; i < lista.Quantidade; i++)
            Console.WriteLine(lista[i]);
    }

    // --------------------------------------------------------
    //  3. Pesquisa Sequencial (iterativa)
    //  Percorre o array do índice 0 ao fim comparando o código.
    //
    //  Melhor caso : O(1)  — elemento na 1ª posição
    //  Pior caso   : O(n)  — elemento na última posição ou ausente
    //  Caso médio  : O(n/2) → O(n)
    //  Função custo: T(n) = c1 + c2*n  →  O(n)
    // --------------------------------------------------------
    static void BuscarSequencial()
    {
        Console.WriteLine("\n--- Pesquisa Sequencial ---");

        if (lista.EstaVazia())
        {
            Console.WriteLine("Lista vazia. Cadastre alunos primeiro.");
            return;
        }

        int   codigo = LerInteiro("Digite o código a buscar: ");
        Aluno achado = null;

        // Percorre o array posição por posição
        for (int i = 0; i < lista.Quantidade; i++)
        {
            if (lista[i].Codigo == codigo)
            {
                achado = lista[i];
                break; // Encerra assim que encontrar
            }
        }

        if (achado != null)
        {
            Console.WriteLine("Aluno encontrado:");
            Console.WriteLine(achado);
        }
        else
        {
            Console.WriteLine($"Aluno com código {codigo} não encontrado.");
        }
    }

    // --------------------------------------------------------
    //  4. Ordenação por Bubble Sort
    //  Compara pares adjacentes no array e troca se necessário.
    //
    //  Melhor caso : O(n)   — lista já ordenada (com flag)
    //  Pior caso   : O(n²)  — lista em ordem inversa
    //  Caso médio  : O(n²)
    //  Função custo: T(n) = c1*n² + c2*n  →  O(n²)
    // --------------------------------------------------------
    static void OrdenarLista()
    {
        Console.WriteLine("\n--- Ordenação por Bubble Sort ---");

        if (lista.EstaVazia())
        {
            Console.WriteLine("Lista vazia.");
            return;
        }

        int  n         = lista.Quantidade;
        bool houveTroca;

        for (int i = 0; i < n - 1; i++)
        {
            houveTroca = false;

            // Compara pares adjacentes no array
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (lista[j].Codigo > lista[j + 1].Codigo)
                {
                    // Troca manualmente os elementos no array
                    Aluno temp   = lista[j];
                    lista[j]     = lista[j + 1];
                    lista[j + 1] = temp;
                    houveTroca   = true;
                }
            }

            // Se não houve nenhuma troca, a lista já está ordenada
            if (!houveTroca) break;
        }

        Console.WriteLine("Lista ordenada por código com sucesso!");
        ListarAlunos();
    }

    // --------------------------------------------------------
    //  5. Pesquisa Binária (iterativa)
    //  Divide o intervalo do array ao meio a cada iteração.
    //  REQUISITO: o array deve estar ordenado pelo código.
    //
    //  Melhor caso : O(1)      — elemento no meio
    //  Pior caso   : O(log n)  — elemento ausente ou nas extremidades
    //  Caso médio  : O(log n)
    //  Função custo: T(n) = c1 + c2*log₂(n)  →  O(log n)
    // --------------------------------------------------------
    static void BuscarBinaria()
    {
        Console.WriteLine("\n--- Pesquisa Binária ---");

        if (lista.EstaVazia())
        {
            Console.WriteLine("Lista vazia. Cadastre alunos primeiro.");
            return;
        }

        Console.WriteLine("Atenção: certifique-se de que a lista está ordenada (opção 4).");
        int codigo   = LerInteiro("Digite o código a buscar: ");

        int esquerda = 0;
        int direita  = lista.Quantidade - 1;
        int indice   = -1;

        while (esquerda <= direita)
        {
            int meio = (esquerda + direita) / 2;

            if (lista[meio].Codigo == codigo)
            {
                indice = meio;
                break; // Encontrado
            }
            else if (lista[meio].Codigo < codigo)
                esquerda = meio + 1; // Busca na metade direita do array
            else
                direita = meio - 1;  // Busca na metade esquerda do array
        }

        if (indice >= 0)
        {
            Console.WriteLine("Aluno encontrado:");
            Console.WriteLine(lista[indice]);
        }
        else
        {
            Console.WriteLine($"Aluno com código {codigo} não encontrado.");
        }
    }

    // --------------------------------------------------------
    //  6. Pesquisa Binária RECURSIVA (opção B da atividade)
    //
    //  Caso base 1  : esquerda > direita → intervalo vazio, retorna -1
    //  Caso base 2  : lista[meio].Codigo == codigo → encontrado, retorna meio
    //  Passo recursivo: chama a si mesma com metade do intervalo
    //
    //  Máximo de chamadas: ⌊log₂(n)⌋ + 1
    //  Complexidade : O(log n)
    // --------------------------------------------------------
    static void BuscarRecursiva()
    {
        Console.WriteLine("\n--- Pesquisa Binária Recursiva ---");

        if (lista.EstaVazia())
        {
            Console.WriteLine("Lista vazia. Cadastre alunos primeiro.");
            return;
        }

        Console.WriteLine("Atenção: certifique-se de que a lista está ordenada (opção 4).");
        int codigo = LerInteiro("Digite o código a buscar: ");

        int indice = PesquisaBinariaRecursiva(codigo, 0, lista.Quantidade - 1);

        if (indice >= 0)
        {
            Console.WriteLine("Aluno encontrado (busca recursiva):");
            Console.WriteLine(lista[indice]);
        }
        else
        {
            Console.WriteLine($"Aluno com código {codigo} não encontrado.");
        }
    }

    /// <summary>
    /// Pesquisa binária recursiva sobre o array da lista linear.
    /// </summary>
    /// <param name="codigo">Código a ser buscado</param>
    /// <param name="esquerda">Limite esquerdo do intervalo atual</param>
    /// <param name="direita">Limite direito do intervalo atual</param>
    /// <returns>Índice do elemento ou -1 se não encontrado</returns>
    static int PesquisaBinariaRecursiva(int codigo, int esquerda, int direita)
    {
        // ---- CASO BASE 1: intervalo vazio → não existe ----
        if (esquerda > direita)
            return -1;

        int meio = (esquerda + direita) / 2;

        // ---- CASO BASE 2: elemento encontrado ----
        if (lista[meio].Codigo == codigo)
            return meio;

        // ---- PASSO RECURSIVO ----
        if (lista[meio].Codigo < codigo)
            // Código está na metade DIREITA do array
            return PesquisaBinariaRecursiva(codigo, meio + 1, direita);
        else
            // Código está na metade ESQUERDA do array
            return PesquisaBinariaRecursiva(codigo, esquerda, meio - 1);
    }

    // --------------------------------------------------------
    //  Auxiliares de leitura com tratamento de entrada inválida
    // --------------------------------------------------------
    static int LerInteiro(string mensagem)
    {
        int valor;
        while (true)
        {
            Console.Write(mensagem);
            if (int.TryParse(Console.ReadLine(), out valor))
                return valor;
            Console.WriteLine("Entrada inválida. Digite um número inteiro.");
        }
    }

    static double LerDouble(string mensagem)
    {
        double valor;
        while (true)
        {
            Console.Write(mensagem);
            string entrada = Console.ReadLine()?.Replace('.', ',');
            if (double.TryParse(entrada,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out valor))
                return valor;
            Console.WriteLine("Entrada inválida. Digite um número decimal (ex: 8,5).");
        }
    }
}
