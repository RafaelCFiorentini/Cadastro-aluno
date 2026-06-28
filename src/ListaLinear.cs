using System;

// ============================================================
//  Lista Linear Manual — array de tamanho fixo
// ============================================================
class ListaLinear
{
    private Aluno[] elementos;   // Array interno que armazena os alunos
    private int     quantidade;  // Quantidade atual de elementos na lista
    private int     capacidade;  // Tamanho máximo da lista

    // Construtor: define a capacidade máxima da lista
    public ListaLinear(int capacidadeMaxima = 100)
    {
        capacidade  = capacidadeMaxima;
        elementos   = new Aluno[capacidade];
        quantidade  = 0;
    }

    // Retorna a quantidade de elementos atualmente na lista
    public int Quantidade => quantidade;

    // Verifica se a lista está vazia
    public bool EstaVazia() => quantidade == 0;

    // Verifica se a lista está cheia
    public bool EstaCheia() => quantidade == capacidade;

    // Acessa um elemento pelo índice (leitura e escrita)
    public Aluno this[int indice]
    {
        get => elementos[indice];
        set => elementos[indice] = value;
    }

    // Insere um aluno no final da lista
    // Complexidade: O(1)
    public bool Inserir(Aluno aluno)
    {
        if (EstaCheia())
            return false; // Lista cheia, não é possível inserir

        elementos[quantidade] = aluno;
        quantidade++;
        return true;
    }

    // Verifica se já existe um aluno com o código informado
    // Complexidade: O(n)
    public bool CodigoExiste(int codigo)
    {
        for (int i = 0; i < quantidade; i++)
            if (elementos[i].Codigo == codigo)
                return true;
        return false;
    }
}
