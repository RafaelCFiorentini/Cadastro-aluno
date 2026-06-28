using System;

// ============================================================
//  Classe que representa um Aluno
// ============================================================
class Aluno
{
    public int    Codigo { get; set; }
    public string Nome   { get; set; }
    public double Nota   { get; set; }

    public Aluno(int codigo, string nome, double nota)
    {
        Codigo = codigo;
        Nome   = nome;
        Nota   = nota;
    }

    // Exibe os dados do aluno em formato padronizado
    public override string ToString()
        => $"Código: {Codigo} | Nome: {Nome} | Nota: {Nota:F1}";
}
