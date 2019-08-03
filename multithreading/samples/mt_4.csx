// Ao tentar usar uma variável compartilhada,
// além de perdermos performance, o valor
// final não é o esperado.

long total = 0;
int ciclos = 1000000000;

void Contar() {
    int i = 0;
    for ( i = 0; i < ciclos; i++) {
        // A linha abaixo causa uma condição de corrida.
        total += 1;
    };
    Console.WriteLine($"Contei e deu {i}");
}

await Task.WhenAll(new Task[] {
    Task.Run(Contar),
    Task.Run(Contar),
    Task.Run(Contar),
    Task.Run(Contar)
});

Console.WriteLine($"O total foi {total}");
