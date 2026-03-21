using System;
namespace AulaPOO.Models
{
public abstract class ItemBiblioteca
{
public string Codigo { get; set; }
public string Localizacao { get; set; }
public abstract string Tipo { get; }
public ItemBiblioteca(string codigo, string localizacao)
{
Codigo = codigo;
Localizacao = localizacao;
}
// Método abstrato - deve ser implementado pelas classes filhas
public abstract decimal CalcularMulta(int diasAtraso);
// Método virtual - pode ser sobrescrito
public virtual void ExibirLocalizacao()
{
Console.WriteLine($"Código: {Codigo} - Local: {Localizacao}");
}
}
}