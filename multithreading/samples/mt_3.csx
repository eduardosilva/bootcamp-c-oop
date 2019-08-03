// Ao aguardar o retorno das tasks, conseguimos
// rodar em 1/4 do tempo e exibir o resultado.

int ciclos = 1000000000;

void Contar() {
    int i = 0;
    for ( i = 0; i < ciclos; i++);
    Console.WriteLine($"Contei e deu {i}");
}

await Task.WhenAll(new Task[] {
    Task.Run(Contar),
    Task.Run(Contar),
    Task.Run(Contar),
    Task.Run(Contar)
});
