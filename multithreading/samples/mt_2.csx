// Ao usar task sem await, o programa termina sem exibir nada.

int ciclos = 1000000000;

void Contar() {
    int i = 0;
    for ( i = 0; i < ciclos; i++);
    Console.WriteLine($"Contei e deu {i}");
}

// NÃO usar o objeto Thread a menos que você tenha
// plena certeza que sabe o que está fazendo
Task.Run(Contar);
Task.Run(Contar);
Task.Run(Contar);
Task.Run(Contar);
