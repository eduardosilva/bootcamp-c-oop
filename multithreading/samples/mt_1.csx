// Abaixo, exemplo de função que pode ser paralelizada

int ciclos = 1000000000;

void Contar() {
    int i = 0;
    for ( i = 0; i < ciclos; i++);
    Console.WriteLine($"Contei e deu {i}");
}

Contar();
Contar();
Contar();
Contar();